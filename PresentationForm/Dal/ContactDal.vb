Public Class ContactDal


    Public Function GetContact(id As Int64) As ContactDto
        Using conn As New ContextSqlServer()
            Return conn.Contact.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateContact(obj As ContactDto) As ContactDto
        Dim insert As New ContactDto

        insert.Type = obj.Type
        insert.Contact = obj.Contact
        insert.Person = obj.Person

        Using conn As New ContextSqlServer()
            conn.Contact.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateContact(obj As ContactDto) As ContactDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Contact.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Type = obj.Type
                update.Contact = obj.Contact
                update.Person = obj.Person
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function RemoveContact(obj As ContactDto) As ContactDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Contact.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                conn.Contact.Remove(update)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function


End Class
