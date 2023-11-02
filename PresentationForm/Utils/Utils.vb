Public Class Utils
    Public Function LoadComboBoxStatus(search As Boolean, cb As ComboBox)
        If search Then
            cb.Items.Add("All")
            cb.Items.Add("Active")
            cb.Items.Add("Inactive")
        Else
            cb.Items.Add("Active")
            cb.Items.Add("Inactive")
        End If
        cb.SelectedIndex = 0
    End Function
    Public Function OnlyNumers(e As Char) As Boolean
        If Not Char.IsDigit(e) And Not Char.IsControl(e) And Convert.ToInt32(e) <> 32 Then
            Return True
        End If
        Return False
    End Function
    Public Function OnlyLettersAndNumbers(e As Char) As Boolean
        If Not Char.IsLetterOrDigit(e) And Convert.ToInt32(e) <> 32 And e <> vbBack Then
            Return True
        End If
        Return False
    End Function
    Public Function TabFunction(e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Function
End Class
