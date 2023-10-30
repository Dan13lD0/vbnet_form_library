Public Class ContactBll
    Private _dal As New ContactDal()

    Public Function GetContact(code As String) As ContactDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetContact(id)
    End Function

    Public Function CreateUpdateContact(code As String, type As String, contact As String, person As String, ByRef msg As String, ByRef action As Boolean) As ContactDto

        Try
            action = True
            msg = "Contact create with successful"

            Dim obj As New ContactDto()
            Int64.TryParse(code, obj.Id)

            obj.Type = If(String.IsNullOrEmpty(type), "", type)
            obj.Contact = If(String.IsNullOrEmpty(contact), "", contact)
            obj.Person = If(String.IsNullOrEmpty(person), "", person)

            If obj.Id = 0 Then
                Return _dal.CreateContact(obj)
            Else
                Return _dal.UpdateContact(obj)
            End If
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteContact(code As String, ByRef action As String, ByRef msg As String) As ContactDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New ContactDto()
            Int64.TryParse(code, obj.Id)

            Return _dal.RemoveContact(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function

End Class
