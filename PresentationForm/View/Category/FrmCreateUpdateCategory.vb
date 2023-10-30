Public Class FrmCreateUpdateCategory
    Private _code As String
    Private _name As String
    Private _status As String

    Private _utils As New Utils()
    Dim _bll As New CategoryBll()
    Public Sub New(code As String, name As String, status As String)
        InitializeComponent()
        _code = code
        _name = name
        _status = status
    End Sub

    Private Sub FrmCreateUpdateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _utils.LoadComboBoxStatus(False, cboStatus)
        txtCode.Text = _code
        txtName.Text = _name
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
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim action As Boolean
        Dim msg As String
        Dim obj As New CategoryDto()

        obj = _bll.CreateUpdateCategory(txtCode.Text, txtName.Text, cboStatus.Text, msg, action)
        If action Then
            If String.IsNullOrEmpty(txtCode.Text) Then
                cboStatus.Text = "Active"
            End If
            txtCode.Text = obj.Id.ToString()
        End If

        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        ValidateCreateUpdate()
    End Sub
End Class