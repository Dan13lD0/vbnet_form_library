Public Class BookDal
    Public Function GetBooks(name As String, categoryId As Int64, authorId As Int64, publisherId As Int64, status As Boolean) As List(Of BookDto)
        Using conn As New ContextSqlServer()
            If status = Nothing Then
                Return conn.Book.Where(Function(i) i.Name.Contains(name) & i.CategoryId = If(categoryId > 0, categoryId, i.CategoryId) & i.AuthorId = If(authorId > 0, authorId, i.AuthorId) & i.PublisherId = If(publisherId > 0, publisherId, i.PublisherId)).ToList()
            Else
                Return conn.Book.Where(Function(i) i.Name.Contains(name) & i.CategoryId = If(categoryId > 0, categoryId, i.CategoryId) & i.AuthorId = If(authorId > 0, authorId, i.AuthorId) & i.PublisherId = If(publisherId > 0, publisherId, i.PublisherId) & i.Status = status).ToList()
            End If
        End Using
    End Function

    Public Function GetBook(id As Int64) As BookDto
        Using conn As New ContextSqlServer()
            Return conn.Book.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateBook(obj As BookDto) As BookDto
        Dim insert As BookDto

        insert.Name = obj.Name
        insert.Description = obj.Description
        insert.AuthorId = obj.AuthorId
        insert.CategoryId = obj.CategoryId
        insert.PublisherId = obj.PublisherId
        insert.Status = True

        Using conn As New ContextSqlServer()
            conn.Book.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateBook(obj As BookDto) As BookDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Book.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Name = obj.Name
                update.Description = obj.Description
                update.AuthorId = obj.AuthorId
                update.CategoryId = obj.CategoryId
                update.PublisherId = obj.PublisherId
                update.Status = obj.Status
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function EnableDisableBook(obj As BookDto) As BookDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Book.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Status = If(obj.Status, False, True)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function ExistsBook(obj As BookDto) As Boolean
        Using conn As New ContextSqlServer()
            Dim exists = conn.Book.Count(Function(i) i.Id <> obj.Id And i.Name = obj.Name)
            Return If(exists > 0, True, False)
        End Using
    End Function
End Class
