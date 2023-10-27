Public Class BorrowDal
    Public Function GetBorrows() As List(Of BorrowDto)
        Using conn As New ContextSqlServer()
            Return conn.Borrow.Where(Function(i) i.Status = True).ToList()
        End Using
    End Function

    Public Function GetBorrow(id As Int64) As BorrowDto
        Using conn As New ContextSqlServer()
            Return conn.Borrow.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateBorrow(obj As BorrowDto) As BorrowDto
        Dim insert As BorrowDto

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
            Dim update = conn.Borrow.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Status = If(obj.Status, False, True)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

End Class
