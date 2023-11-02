Public Class ClientBll
    Private _dal As New ClientDal()
    Public Function GetClients(code As String, name As String, rg As String, cpf As String, status As String) As List(Of ClientDto)
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
        rg = rg.Replace(".", "").Replace("-", "").Replace(",", "").Trim()
        cpf = cpf.Replace(".", "").Replace("-", "").Replace(",", "").Trim()
        Return _dal.GetClients(id, name, rg, cpf, vStatus)
    End Function

    Public Function GetClient(code As String) As ClientDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetClient(id)
    End Function

    Public Function CreateUpdateClient(code As String, name As String, birthday As String, rg As String, cpf As String, contactId As String, addressId As String, status As String, ByRef msg As String, ByRef action As Boolean) As ClientDto

        Try
            action = True
            msg = If(String.IsNullOrEmpty(code), "Client create with successful", "Client updated with successful")

            Dim obj As New ClientDto()
            Int64.TryParse(code, obj.Id)

            If String.IsNullOrEmpty(name) Then
                action = False
                msg = "The field name is requerid!"
                Return Nothing
            Else
                obj.Name = name
            End If

            If DateTime.TryParse(birthday, obj.BirthDay) = False Then
                action = False
                msg = "The field birthday is requerid!"
                Return Nothing
            End If

            If String.IsNullOrEmpty(rg) Then
                action = False
                msg = "The field RG is requerid!"
                Return Nothing
            Else
                obj.RG = rg
            End If

            If String.IsNullOrEmpty(cpf) Then
                action = False
                msg = "The field cpf is requerid!"
                Return Nothing
            Else
                obj.CPF = cpf
            End If

            obj.Age = 1

            If status.Trim().ToUpper().Equals("ACTIVE") Or obj.Id = 0 Then
                obj.Status = True
            ElseIf status.Trim().ToUpper().Equals("INACTIVE") Then
                obj.Status = False
            Else
                obj.Status = Nothing
            End If

            If _dal.ExistsClient(obj) Then
                action = False
                msg = "This record already exists!"
                Return Nothing
            End If

            If String.IsNullOrEmpty(addressId) Then
                action = False
                msg = "The addressId cpf is requerid!"
                Return Nothing
            Else
                Int64.TryParse(addressId, obj.AddressId)
            End If

            If String.IsNullOrEmpty(contactId) Then
                action = False
                msg = "The contactId cpf is requerid!"
                Return Nothing
            Else
                Int64.TryParse(contactId, obj.ContactId)
            End If

            If obj.Id = 0 Then
                Return _dal.CreateClient(obj)
            Else
                Return _dal.UpdateClient(obj)
            End If
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteClient(code As String, ByRef action As String, ByRef msg As String) As ClientDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New ClientDto()
            Int64.TryParse(code, obj.Id)

            Return _dal.EnableDisableClient(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function

End Class
