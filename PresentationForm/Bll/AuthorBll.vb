﻿Public Class AuthorBll
    Private _dal As New AuthorDal()
    Public Function GetAuthors(code As String, name As String, status As String) As List(Of AuthorDto)
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Dim vStatus As Boolean?

        If status.Equals("All") Then
            vStatus = Nothing
        Else
            If status.ToUpper().Trim().Equals("ACTIVE") Then
                vStatus = True
            Else
                vStatus = False
            End If
        End If

        Return _dal.GetAuthors(id, name, vStatus)
    End Function

    Public Function GetAuthor(code As String) As AuthorDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetAuthor(id)
    End Function

    Public Function CreateUpdateAuthor(code As String, name As String, status As String, ByRef msg As String, ByRef action As Boolean) As AuthorDto

        Try
            action = True
            msg = If(String.IsNullOrEmpty(code), "Author create with successful", "Author updated with successful")

            Dim obj As New AuthorDto()
            Int64.TryParse(code, obj.Id)

            obj.Name = If(String.IsNullOrEmpty(name), "", name)

            If status.Trim().ToUpper().Equals("ACTIVE") Or obj.Id = 0 Then
                obj.Status = True
            ElseIf status.Trim().ToUpper().Equals("INACTIVE") Then
                obj.Status = False
            Else
                obj.Status = Nothing
            End If

            If _dal.ExistsAuthor(obj) Then
                action = False
                msg = "This record already exists!"
                Return Nothing
            End If

            If obj.Id = 0 Then
                Return _dal.CreateAuthor(obj)
            Else
                Return _dal.UpdateAuthor(obj)
            End If
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteAuthor(code As String, ByRef action As String, ByRef msg As String) As AuthorDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New AuthorDto()
            Int64.TryParse(code, obj.Id)

            Return _dal.EnableDisableAuthor(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function
End Class
