Public Class CategoryBll
    Private _dal As New CategoryDal()



    Public Function GetCategories() As List(Of CategoryDto)
        Return _dal.GetCategories(0, "", "")
    End Function
End Class
