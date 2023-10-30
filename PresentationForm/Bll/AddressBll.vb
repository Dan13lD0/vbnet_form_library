Public Class AddressBll
    Private _dal As New AddressDal()

    Public Function GetAddress(code As String) As AddressDto
        Dim id As Int64 = 0
        Int64.TryParse(code, id)

        Return _dal.GetAddress(id)
    End Function

    Public Function CreateUpdateAddress(code As String, zipcode As String, street As String, complement As String, city As String, state As String, ByRef msg As String, ByRef action As Boolean) As AddressDto

        Try
            action = True
            msg = "Address create with successful"

            Dim obj As New AddressDto()
            Int64.TryParse(code, obj.Id)

            obj.ZipCode = If(String.IsNullOrEmpty(zipcode), "", zipcode)
            obj.Street = If(String.IsNullOrEmpty(street), "", street)
            obj.Complement = If(String.IsNullOrEmpty(complement), "", complement)
            obj.City = If(String.IsNullOrEmpty(city), "", city)
            obj.State = If(String.IsNullOrEmpty(street), "", state)



            If obj.Id = 0 Then
                Return _dal.CreateAddress(obj)
            Else
                Return _dal.UpdateAddress(obj)
            End If
        Catch ex As Exception
            action = False
            msg = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function DeleteAddress(code As String, ByRef action As String, ByRef msg As String) As AddressDto
        Try
            action = True
            msg = "Status changed with successful!"
            Dim obj As New AddressDto()
            Int64.TryParse(code, obj.Id)

            Return _dal.DeleteAddress(obj)
        Catch ex As Exception
            action = True
            msg = ex.Message
        End Try
    End Function

End Class
