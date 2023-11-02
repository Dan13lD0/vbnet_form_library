Imports System.Drawing.Text
Imports System.Security.Policy

Public Class BorrowBll
    Private _dal As New BorrowDal()
    Private _category As New CategoryDal()
    Private _author As New AuthorDal()
    Private _publisher As New PublisherDal()
    Private _client As New ClientDal()
    Private _book As New BookDal()
    Public Function GetBorrows(book As String, category As String, author As String, publisher As String) As List(Of BookDto)
        Dim categoryId As Int64 = 0
        Int64.TryParse(category, categoryId)

        Dim authorId As Int64 = 0
        Int64.TryParse(author, authorId)

        Dim publisherId As Int64 = 0
        Int64.TryParse(publisher, publisherId)

        Dim data = _dal.GetBooksForBorrow(book, categoryId, authorId, publisherId)
        For Each item In data

            item.CategoryName = _category.GetCategory(item.CategoryId).Name
            item.AuthorName = _author.GetAuthor(item.AuthorId).Name
            item.PublisherName = _publisher.GetPublisher(item.PublisherId).Name

            Dim borrow = _dal.GetBorrowByBookId(item.Id)

            item.Borrow = If(_dal.GetBorrowByBookId(item.Id) IsNot Nothing, "Unavaliable", "Available")
        Next

        Return data
    End Function

    Public Function GetBorrow(code As String) As BorrowDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetBorrow(id)
    End Function

    Public Function GetBorrows(client As String, book As String) As List(Of BorrowDto)

        Dim clientId As Int64 = 0
        Int64.TryParse(client, clientId)

        Dim bookId As Int64 = 0
        Int64.TryParse(book, bookId)

        Dim data = _dal.GetBorrows(clientId, bookId)

        For Each item In data
            item.ClientName = _client.GetClient(item.ClientId).Name
            item.BookName = _book.GetBook(item.BookId).Name
        Next

        Return data
    End Function

    Public Function CreateUpdateBorrow(bookId As String, clientId As String, dateborrow As String, datereturn As String, ByRef msg As String, ByRef action As Boolean) As BorrowDto

        Try
            action = True
            msg = "Borrow create with successful"

            Dim obj As New BorrowDto()


            Int64.TryParse(bookId, obj.BookId)
            Int64.TryParse(clientId, obj.ClientId)
            DateTime.TryParse(dateborrow, obj.DateBorrow)
            DateTime.TryParse(datereturn, obj.DateReturn)

            Return _dal.CreateBorrow(obj)
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteBorrow(code As String, ByRef action As String, ByRef msg As String) As BorrowDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New BorrowDto()
            Int64.TryParse(code, obj.BookId)

            Return _dal.EnableDisableBorrow(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function

    Public Function BorrowChangeStatus(id As String)
        Dim borrwId As New BorrowDto()
        Int64.TryParse(id, borrwId.Id)
        _dal.BorrowChangeStatus(borrwId)
    End Function
End Class
