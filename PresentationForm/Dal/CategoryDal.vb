Public Class CategoryDal

    Public Function GetCategories(code As Int64, name As String, status As String) As List(Of CategoryDto)
        Using conn As New ContextSqlServer()
            If String.IsNullOrEmpty(status) Then
                Return conn.Category.ToList()
            Else
                Return conn.Category.ToList()
            End If
        End Using
    End Function
End Class
