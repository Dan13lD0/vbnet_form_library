Imports System.Data.Entity

Public Class CategoryDal

    Public Function GetCategories(code As Int64, name As String, status As Boolean?) As List(Of CategoryDto)
        Using conn As New ContextSqlServer()
            If status.Equals(Nothing) Then
                Return conn.Category.Where(Function(i) i.Id = If(code > 0, code, i.Id) And i.Name.Contains(name)).ToList()
            Else
                Return conn.Category.Where(Function(i) i.Id = If(code > 0, code, i.Id) And i.Name.Contains(name) And i.Status = status).ToList()
            End If
        End Using
    End Function

    Public Function GetCategory(id As Int64) As CategoryDto
        Using conn As New ContextSqlServer()
            Return conn.Category.FirstOrDefault(Function(i) i.Id = id)
        End Using
    End Function

    Public Function CreateCategory(obj As CategoryDto) As CategoryDto
        Dim insert As New CategoryDto

        insert.Name = obj.Name
        insert.Status = True

        Using conn As New ContextSqlServer()
            conn.Category.Add(insert)
            conn.SaveChanges()
        End Using
        Return insert
    End Function

    Public Function UpdateCategory(obj As CategoryDto) As CategoryDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Category.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Name = obj.Name
                update.Status = obj.Status
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function EnableDisableCategory(obj As CategoryDto) As CategoryDto
        Using conn As New ContextSqlServer()
            Dim update = conn.Category.FirstOrDefault(Function(i) i.Id = obj.Id)
            If update IsNot Nothing Then
                update.Status = If(update.Status, False, True)
                conn.SaveChanges()
                Return update
            End If
        End Using
        Return Nothing
    End Function

    Public Function ExistsCategory(obj As CategoryDto) As Boolean
        Using conn As New ContextSqlServer()
            Dim exists = conn.Category.Count(Function(i) i.Id <> obj.Id And i.Name = obj.Name)
            Return If(exists > 0, True, False)
        End Using
    End Function
End Class
