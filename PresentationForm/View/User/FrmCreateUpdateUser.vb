Public Class FrmCreateUpdateUser
    Private _code As String
    Private _name As String
    Private _login As String
    Private _password As String
    Private _email As String
    Private _status As String

    Private _utils As New Utils()
    Dim _bll As New UserBll()
    Public Sub New(code As String, name As String, login As String, password As String, email As String, status As String)
        InitializeComponent()
        _code = code
        _name = name
        _login = login
        _password = password
        _email = email
        _status = status
    End Sub

    Private Sub FrmCreateUpdateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _utils.LoadComboBoxStatus(False, cboStatus)
        txtCode.Text = _code
        txtName.Text = _name
        txtLogin.Text = _login
        txtPassword.Text = _password
        txtEmail.Text = _email
        cboStatus.Text = _status
        txtName.Select()
        ValidateCreateUpdate()
    End Sub

    Private Function ValidateCreateUpdate()
        If String.IsNullOrEmpty(txtCode.Text) Then
            cboStatus.Enabled = False
        Else
            cboStatus.Enabled = True
        End If
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnClean_Click(sender As Object, e As EventArgs) Handles btnClean.Click
        txtName.Clear()
        txtLogin.Clear()
        txtPassword.Clear()
        txtEmail.Clear()
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim action As Boolean
        Dim msg As String
        Dim obj As New UserDto()

        obj = _bll.CreateUpdateUser(txtCode.Text, txtName.Text, txtLogin.Text, txtPassword.Text, txtEmail.Text, cboStatus.Text, msg, action)
        If action Then
            If String.IsNullOrEmpty(txtCode.Text) Then
                cboStatus.Text = "Active"
            End If
            txtCode.Text = obj.Id.ToString()
        End If

        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        ValidateCreateUpdate()
    End Sub

    Private Sub FrmCreateUpdateUser_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

    End Sub
End Class