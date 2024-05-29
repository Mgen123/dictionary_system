Imports System.Data.OleDb
Public Class Form1
    Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\practice.mdb;"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DictionaryDataSet.users' table. You can move, or remove it, as needed.
        Me.UsersTableAdapter.Fill(Me.DictionaryDataSet.users)
        Button2.Text = "&form2"

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim searchTerm As String = TextBox1.Text.Trim

        RichTextBox1.Text = ""
        If String.IsNullOrWhiteSpace(searchTerm) Then
            MessageBox.Show("please enter a search term")
            Return
        End If
        Dim query As String = "SELECT * FROM Dictionary WHERE Terms LIKE @searchTerm"
        Dim searchText As String = "%" & searchTerm & "%"
        Using connection As New OleDbConnection(connectionString)
            Using command As New OleDbCommand(query, connection)
                command.Parameters.AddWithValue("@searchTerms", searchText)
                Try
                    connection.Open()
                    Using reader As OleDbDataReader = command.ExecuteReader()
                        If reader.HasRows Then
                            While reader.Read()
                                Dim result As String = $"Terms: {reader("Terms")}, Description: {reader("Description")}"
                                RichTextBox1.AppendText(result & Environment.NewLine)
                            End While
                        Else
                            RichTextBox1.Text = "No matching records found"
                        End If
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)

                End Try


            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form3.Show()

    End Sub
End Class
