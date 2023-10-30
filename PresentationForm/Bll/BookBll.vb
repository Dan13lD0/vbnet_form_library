Imports System.Security.Policy

Public Class BookBll
    Private _dal As New BookDal()
    Public Function GetBooks(code As String, name As String, categoryId As String, authorId As String, publishId As String, status As String) As List(Of BookDto)
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Dim category As Int64 = 0
        Int64.TryParse(categoryId, category)

        Dim author As Int64 = 0
        Int64.TryParse(authorId, author)

        Dim publisher As Int64 = publishId
        Int64.TryParse(publishId, publisher)

        Dim vStatus As Boolean

        If String.IsNullOrEmpty(status) Then
            vStatus = Nothing
        Else
            If status.ToUpper().Trim().Equals("ACTIVE") Then
                vStatus = True
            Else
                vStatus = False
            End If
        End If

        Return _dal.GetBooks(name, category, author, publisher, vStatus)
    End Function

    Public Function GetBook(code As String) As BookDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetBook(id)
    End Function

    Public Function CreateUpdateBook(code As String, name As String, categoryId As String, authorId As String, publisherId As String, status As String, ByRef msg As String, ByRef action As Boolean) As BookDto

        Try
            action = True
            msg = "Book create with successful"

            Dim obj As New BookDto()
            Int64.TryParse(code, obj.Id)

            obj.Name = If(String.IsNullOrEmpty(name), "", name)

            Int64.TryParse(categoryId, obj.CategoryId)

            Int64.TryParse(authorId, obj.AuthorId)

            Int64.TryParse(publisherId, obj.PublisherId)

            If status.Trim().ToUpper().Equals("ACTIVE") Or obj.Id = 0 Then
                obj.Status = True
            ElseIf status.Trim().ToUpper().Equals("INACTIVE") Then
                obj.Status = False
            Else
                obj.Status = Nothing
            End If

            If _dal.ExistsBook(obj) Then
                action = False
                msg = "This record already exists!"
                Return Nothing
            End If

            If obj.Id = 0 Then
                Return _dal.CreateBook(obj)
            Else
                Return _dal.UpdateBook(obj)
            End If
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteBook(code As String, ByRef action As String, ByRef msg As String) As BookDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New BookDto()
            Int64.TryParse(code, obj.Id)

            Return _dal.EnableDisableBook(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function

End Class
