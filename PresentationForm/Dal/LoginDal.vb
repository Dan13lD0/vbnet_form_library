Public Class LoginDal
    Public Function ValidateUser(user As String, password As String) As Boolean
        Using conn As New ContextSqlServer()
            Return If(conn.User.Where(Function(i) i.Login.Equals(user) And i.Password.Equals(password)).Count() > 0, True, False)
        End Using
    End Function
End Class
