Public Class AddressDal

    Public Function GetAddress(id As Int64) As AddressDto
        Using conn As New ContextSqlServer()
            Return conn.Address.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateAddress(obj As AddressDto) As AddressDto
        Dim insert As New AddressDto
        insert.ZipCode = obj.ZipCode
        insert.Street = obj.Street
        insert.Complement = obj.Complement
        insert.City = obj.City
        insert.Street = obj.Street
        insert.State = obj.State
        Using conn As New ContextSqlServer()
            conn.Address.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateAddress(obj As AddressDto) As AddressDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Address.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.ZipCode = obj.ZipCode
                update.Street = obj.Street
                update.Complement = obj.Complement
                update.City = obj.City
                update.Street = obj.Street
                update.State = obj.State
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function DeleteAddress(obj As AddressDto) As AddressDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Address.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                conn.Address.Remove(update)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

End Class
