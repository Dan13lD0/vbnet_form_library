Public Class ClientDal
    Public Function GetClients(code As Int64?, name As String, rg As String, cpf As String, status As Boolean) As List(Of ClientDto)
        Using conn As New ContextSqlServer()
            If status = Nothing Then
                Return conn.Client.Where(Function(i) i.Id = If(code > 0, code, i.Id) & i.Name.Contains(name) & i.RG.Contains(rg) & i.CPF.Contains(cpf)).ToList()
            Else
                Return conn.Client.Where(Function(i) i.Id = If(code > 0, code, i.Id) & i.Name.Contains(name) & i.RG.Contains(rg) & i.CPF.Contains(cpf) & i.Status = status).ToList()
            End If
        End Using
    End Function

    Public Function GetClient(id As Int64) As ClientDto
        Using conn As New ContextSqlServer()
            Return conn.Client.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateClient(obj As ClientDto) As ClientDto
        Dim insert As ClientDto

        insert.Name = obj.Name
        insert.BirthDay = obj.BirthDay
        insert.Age = obj.Age
        insert.RG = obj.RG
        insert.CPF = obj.CPF
        insert.AddressId = obj.AddressId
        insert.ContactId = obj.ContactId
        insert.Status = True

        Using conn As New ContextSqlServer()
            conn.Client.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateClient(obj As ClientDto) As ClientDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Client.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Name = obj.Name
                update.BirthDay = obj.BirthDay
                update.Age = obj.Age
                update.RG = obj.RG
                update.CPF = obj.CPF
                update.AddressId = obj.AddressId
                update.ContactId = obj.ContactId
                update.Status = obj.Status
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function EnableDisableClient(obj As ClientDto) As ClientDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Client.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Status = If(obj.Status, False, True)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function ExistsClient(obj As ClientDto) As Boolean
        Using conn As New ContextSqlServer()
            Dim exists = conn.Client.Count(Function(i) i.Id <> obj.Id And i.Name = obj.Name)
            Return If(exists > 0, True, False)
        End Using
    End Function
End Class
