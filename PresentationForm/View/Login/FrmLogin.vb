Public Class FrmLogin
    Private _bll As New UserBll()
    Private _utils As New Utils()
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If _bll.ValidateUser(txtUser.Text, txtPassword.Text) Then
            Dim form As New FrmMain()
            Hide()
            form.ShowDialog()
            Show()
        Else
            MessageBox.Show("User not found!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUser.Clear()
            txtPassword.Clear()
            txtUser.Select()
        End If
    End Sub

    Private Sub FrmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        _utils.TabFunction(e)
    End Sub

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _bll.InitializeUsers()
        txtUser.Select()
    End Sub

    Private Sub FrmLogin_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        txtUser.Select()
    End Sub
End Class