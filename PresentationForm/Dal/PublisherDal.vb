Public Class PublisherDal
    Public Function GetPublishers(code As Int64, name As String, status As Boolean?) As List(Of PublisherDto)
        Using conn As New ContextSqlServer()
            If status.Equals(Nothing) Then
                Return conn.Publisher.Where(Function(i) i.Id.Equals(If(code > 0, code, i.Id)) And i.Name.Contains(name)).ToList()
            Else
                Return conn.Publisher.Where(Function(i) i.Id.Equals(If(code > 0, code, i.Id)) And i.Name.Contains(name) And i.Status = status).ToList()
            End If
        End Using
    End Function

    Public Function GetPublisher(id As Int64) As PublisherDto
        Using conn As New ContextSqlServer()
            Return conn.Publisher.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreatePublisher(obj As PublisherDto) As PublisherDto
        Dim insert As New PublisherDto

        insert.Name = obj.Name
        insert.Status = True

        Using conn As New ContextSqlServer()
            conn.Publisher.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdatePublisher(obj As PublisherDto) As PublisherDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Publisher.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Name = obj.Name
                update.Status = obj.Status
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function EnableDisablePublisher(obj As PublisherDto) As PublisherDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Publisher.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Status = If(update.Status, False, True)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function ExistsPublisher(obj As PublisherDto) As Boolean
        Using conn As New ContextSqlServer()
            Dim exists = conn.Publisher.Count(Function(i) i.Id <> obj.Id And i.Name = obj.Name)
            Return If(exists > 0, True, False)
        End Using
    End Function
End Class
