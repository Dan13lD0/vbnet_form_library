﻿Public Class CategoryBll

    Private _dal As New CategoryDal()

    Public Function GetCategories(code As String, name As String, status As String) As List(Of CategoryDto)
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

        Return _dal.GetCategories(id, name, vStatus)
    End Function

    Public Function GetCategory(code As String) As CategoryDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetCategory(id)
    End Function

    Public Function CreateUpdateCategory(code As String, name As String, status As String, ByRef msg As String, ByRef action As Boolean) As CategoryDto

        Try
            action = True
            msg = If(String.IsNullOrEmpty(code), "Category create with successful", "Category updated with successful")

            Dim obj As New CategoryDto()
            Int64.TryParse(code, obj.Id)

            obj.Name = If(String.IsNullOrEmpty(name), "", name)

            If status.Trim().ToUpper().Equals("ACTIVE") Or obj.Id = 0 Then
                obj.Status = True
            ElseIf status.Trim().ToUpper().Equals("INACTIVE") Then
                obj.Status = False
            Else
                obj.Status = Nothing
            End If

            If _dal.ExistsCategory(obj) Then
                action = False
                msg = "This record already exists!"
                Return Nothing
            End If

            If obj.Id = 0 Then
                Return _dal.CreateCategory(obj)
            Else
                Return _dal.UpdateCategory(obj)
            End If
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteCategory(code As String, ByRef action As String, ByRef msg As String) As CategoryDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New CategoryDto()
            Int64.TryParse(code, obj.Id)

            Return _dal.EnableDisableCategory(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function
End Class
