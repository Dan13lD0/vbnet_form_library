Public Class BorrowBll
    Private _dal As New BorrowDal()
    Public Function GetBorrows() As List(Of BorrowDto)
        Return _dal.GetBorrows()
    End Function

    Public Function GetBorrow(code As String) As BorrowDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetBorrow(id)
    End Function

    Public Function CreateUpdateBorrow(code As String, bookId As String, clientId As String, dateborrow As String, datereturn As String, status As String, ByRef msg As String, ByRef action As Boolean) As BorrowDto

        Try
            action = True
            msg = "Borrow create with successful"

            Dim obj As New BorrowDto()
            Int64.TryParse(code, obj.Id)

            Int64.TryParse(bookId, obj.BookId)
            Int64.TryParse(clientId, obj.ClientId)
            DateTime.TryParse(dateborrow, obj.DateBorrow)
            DateTime.TryParse(datereturn, obj.DateReturn)

            If status.Trim().ToUpper().Equals("ACTIVE") Or obj.Id = 0 Then
                obj.Status = True
            ElseIf status.Trim().ToUpper().Equals("INACTIVE") Then
                obj.Status = False
            Else
                obj.Status = Nothing
            End If


            If obj.Id = 0 Then
                Return _dal.CreateBorrow(obj)
            Else
                Return _dal.UpdateBorrow(obj)
            End If
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
            Int64.TryParse(code, obj.Id)

            Return _dal.EnableDisableBorrow(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function
End Class
