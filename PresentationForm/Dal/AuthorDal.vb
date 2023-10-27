Public Class AuthorDal
    Public Function GetAuthors(code As Int64?, name As String, status As Boolean) As List(Of AuthorDto)
        Using conn As New ContextSqlServer()
            If status = Nothing Then
                Return conn.Author.Where(Function(i) i.Id = If(code > 0, code, i.Id) & i.Name.Contains(name)).ToList()
            Else
                Return conn.Author.Where(Function(i) i.Id = If(code > 0, code, i.Id) & i.Name.Contains(name) & i.Status = status).ToList()
            End If
        End Using
    End Function

    Public Function GetAuthor(id As Int64) As AuthorDto
        Using conn As New ContextSqlServer()
            Return conn.Author.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateAuthor(obj As AuthorDto) As AuthorDto
        Dim insert As AuthorDto

        insert.Name = obj.Name
        insert.Status = True

        Using conn As New ContextSqlServer()
            conn.Author.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateAuthor(obj As AuthorDto) As AuthorDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Author.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Name = obj.Name
                update.Status = obj.Status
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function EnableDisableAuthor(obj As AuthorDto) As AuthorDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Author.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Status = If(obj.Status, False, True)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function ExistsAuthor(obj As AuthorDto) As Boolean
        Using conn As New ContextSqlServer()
            Dim exists = conn.Author.Count(Function(i) i.Id <> obj.Id And i.Name = obj.Name)
            Return If(exists > 0, True, False)
        End Using
    End Function
End Class
