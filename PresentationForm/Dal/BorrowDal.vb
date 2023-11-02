Imports System.Security.Policy

Public Class BorrowDal
    Public Function GetBorrows(clientId As Int64, bookId As Int64) As List(Of BorrowDto)
        Using conn As New ContextSqlServer()
            Return conn.Borrow.Where(Function(i) i.ClientId.Equals(If(clientId > 0, clientId, i.ClientId)) And i.BookId.Equals(If(bookId > 0, bookId, i.BookId)) And i.Status = True).ToList()
        End Using
    End Function
    Public Function GetBooksForBorrow(name As String, category As Int64, author As Int64, publisher As Int64) As List(Of BookDto)
        Using conn As New ContextSqlServer()
            Return conn.Book.Where(Function(i) name.Contains(name) And i.CategoryId.Equals(If(category > 0, category, i.CategoryId)) And i.AuthorId.Equals(If(author > 0, author, i.AuthorId)) And i.PublisherId.Equals(If(publisher > 0, publisher, i.PublisherId))).ToList()
        End Using
    End Function

    Public Function GetBorrow(id As Int64) As BorrowDto
        Using conn As New ContextSqlServer()
            Return conn.Borrow.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function GetBorrowByBookId(id As Int64) As BorrowDto
        Using conn As New ContextSqlServer()
            Return conn.Borrow.FirstOrDefault(Function(i) i.BookId = id And i.Status = True)
        End Using
    End Function

    Public Function CreateBorrow(obj As BorrowDto) As BorrowDto
        Dim insert As New BorrowDto

        insert.BookId = obj.BookId
        insert.ClientId = obj.ClientId
        insert.DateBorrow = obj.DateBorrow
        insert.DateReturn = obj.DateReturn
        insert.Status = True

        Using conn As New ContextSqlServer()
            conn.Borrow.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateBorrow(obj As BorrowDto) As BorrowDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Borrow.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.BookId = obj.BookId
                update.ClientId = obj.ClientId
                update.DateBorrow = obj.DateBorrow
                update.DateReturn = obj.DateReturn
                update.Status = obj.Status
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function EnableDisableBorrow(obj As BorrowDto) As BorrowDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Borrow.FirstOrDefault(Function(i) i.BookId.Equals(obj.BookId))
            If update IsNot Nothing Then
                conn.Borrow.Remove(update)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function BorrowChangeStatus(obj As BorrowDto) As BorrowDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Borrow.FirstOrDefault(Function(i) i.Id.Equals(obj.Id))
            If update IsNot Nothing Then
                update.Status = False
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

End Class
