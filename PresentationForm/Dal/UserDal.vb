Public Class UserDal
    Public Function GetUsers(code As Int64, name As String, login As String, email As String, status As Boolean?) As List(Of UserDto)
        Try
            Using conn As New ContextSqlServer()
                If status.Equals(Nothing) Then
                    Return conn.User.Where(Function(i) i.Id.Equals(If(code > 0, code, i.Id)) And i.Name.Contains(name) And i.Login.Contains(login) And i.Email.Contains(email)).ToList()
                Else
                    Return conn.User.Where(Function(i) i.Id.Equals(If(code > 0, code, i.Id)) And i.Name.Contains(name) And i.Login.Contains(login) And i.Email.Contains(email) And i.Status = status).ToList()
                End If
            End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function GetUser(id As Int64) As UserDto
        Using conn As New ContextSqlServer()
            Return conn.User.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateUser(obj As UserDto) As UserDto
        Dim insert As New UserDto()

        insert.Name = obj.Name
        insert.Login = obj.Login
        insert.Password = obj.Password
        insert.Email = obj.Email
        insert.Status = True

        Using conn As New ContextSqlServer()
            conn.User.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateUser(obj As UserDto) As UserDto
        Using conn As New ContextSqlServer()
            Dim update = conn.User.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Name = obj.Name
                update.Login = obj.Login
                update.Password = obj.Password
                update.Email = obj.Email
                update.Status = obj.Status
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function EnableDisableUser(obj As UserDto) As UserDto
        Using conn As New ContextSqlServer()
            Dim update = conn.User.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Status = If(update.Status, False, True)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function ExistsUser(obj As UserDto) As Boolean
        Using conn As New ContextSqlServer()
            Dim exists = conn.User.Count(Function(i) i.Id <> obj.Id And i.Name = obj.Name And i.Login = obj.Login)
            Return If(exists > 0, True, False)
        End Using
    End Function
End Class
