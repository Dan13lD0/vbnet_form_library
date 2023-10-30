Public Class UserBll
    Private _dal As New UserDal()
    Public Function GetUsers(code As String, name As String, login As String, email As String, status As String) As List(Of UserDto)
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Dim vStatus As Boolean?

        If status.Equals("All") Then
            vStatus = Nothing
        Else
            If status.ToUpper().Trim().Equals("ACTIVE") Then
                vStatus = True
            Else
                vStatus = False
            End If
        End If

        Return _dal.GetUsers(id, name, login, email, vStatus)
    End Function

    Public Function GetUser(code As String) As UserDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetUser(id)
    End Function

    Public Function CreateUpdateUser(code As String, name As String, login As String, password As String, email As String, status As String, ByRef msg As String, ByRef action As Boolean) As UserDto

        Try
            action = True
            msg = If(String.IsNullOrEmpty(code), "User create with successful", "User updated with successful")

            Dim obj As New UserDto()
            Int64.TryParse(code, obj.Id)

            If String.IsNullOrEmpty(name) Then
                action = False
                msg = "The field name is requerid!"
                Return Nothing
            Else
                obj.Name = name.Trim()
            End If

            If String.IsNullOrEmpty(login) Then
                action = False
                msg = "The field login is requerid!"
                Return Nothing
            Else
                obj.Login = login.Trim()
            End If

            If String.IsNullOrEmpty(password) Then
                action = False
                msg = "The field password is requerid!"
                Return Nothing
            Else
                obj.Password = password.Trim()
            End If

            If String.IsNullOrEmpty(email) Then
                action = False
                msg = "The field email is requerid!"
                Return Nothing
            Else
                obj.Email = email.Trim()
            End If

            If status.Trim().ToUpper().Equals("ACTIVE") Or obj.Id = 0 Then
                obj.Status = True
            ElseIf status.Trim().ToUpper().Equals("INACTIVE") Then
                obj.Status = False
            Else
                obj.Status = Nothing
            End If

            If _dal.ExistsUser(obj) Then
                action = False
                msg = "This record already exists!"
                Return Nothing
            End If

            If obj.Id = 0 Then
                Return _dal.CreateUser(obj)
            Else
                Return _dal.UpdateUser(obj)
            End If
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteUser(code As String, ByRef action As Boolean, ByRef msg As String) As UserDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New UserDto()
            Int64.TryParse(code, obj.Id)

            Return _dal.EnableDisableUser(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function

End Class
