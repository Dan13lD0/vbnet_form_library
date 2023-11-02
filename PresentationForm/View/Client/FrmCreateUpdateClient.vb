Imports System.Net

Public Class FrmCreateUpdateClient
    Private _code As String
    Private _name As String
    Private _birthday As String
    Private _rg As String
    Private _cpf As String
    Private _contactId As String
    Private _addressId As String
    Private _status As String

    Private _utils As New Utils()
    Private _bll As New ClientBll()
    Private _bllAddress As New AddressBll()
    Private _bllContact As New ContactBll()

    Public Sub New(code As String, name As String, birthday As String, rg As String, cpf As String, contactId As String, addressId As String, status As String)
        InitializeComponent()
        _code = code
        _name = name
        _birthday = birthday
        _rg = rg
        _cpf = cpf
        _contactId = contactId
        _addressId = addressId
        _status = status
    End Sub

    Private Sub FrmCreateUpdateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _utils.LoadComboBoxStatus(False, cboStatus)
        txtCode.Text = _code
        txtName.Text = _name
        If Not String.IsNullOrEmpty(_birthday) Then
            dtpBirth.Value = _birthday
        End If
        mtbRg.Text = _rg
        mtbCpf.Text = _cpf

        If Not String.IsNullOrEmpty(_addressId) Then
            Dim address = _bllAddress.GetAddress(_addressId)
            If address?.Id > 0 Then
                txtZipCode.Text = address.ZipCode
                txtStreet.Text = address.Street
                txtComplement.Text = address.Complement
                txtCity.Text = address.City
                txtState.Text = address.State
            End If
        End If

        If Not String.IsNullOrEmpty(_contactId) Then
            Dim contact = _bllContact.GetContact(_contactId)
            If contact?.Id > 0 Then
                txtType.Text = contact.Type
                txtContact.Text = contact.Contact
                txtPerson.Text = contact.Person
            End If
        End If

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
        mtbRg.Clear()
        mtbCpf.Clear()
        txtZipCode.Clear()
        txtStreet.Clear()
        txtComplement.Clear()
        txtCity.Clear()
        txtState.Clear()
        txtType.Clear()
        txtContact.Clear()
        txtPerson.Clear()
        txtName.Select()
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim action As Boolean
        Dim msg As String
        Dim obj As New ClientDto()

        '_bllAddress.DeleteAddress(_addressId, action, msg)
        Dim address = _bllAddress.CreateUpdateAddress(_addressId, txtZipCode.Text, txtStreet.Text, txtComplement.Text, txtCity.Text, txtState.Text, msg, action)

        '_bllContact.DeleteContact(_contactId, action, msg)
        Dim contact = _bllContact.CreateUpdateContact(_contactId, txtType.Text, txtContact.Text, txtPerson.Text, msg, action)

        obj = _bll.CreateUpdateClient(txtCode.Text, txtName.Text, dtpBirth.Value, mtbRg.Text, mtbCpf.Text, contact.Id.ToString(), address.Id.ToString(), cboStatus.Text, msg, action)

        If action Then

            If String.IsNullOrEmpty(txtCode.Text) Then
                cboStatus.Text = "Active"
            End If
            txtCode.Text = obj.Id.ToString()
        End If

        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        ValidateCreateUpdate()
    End Sub

    Private Sub dtpBirth_ValueChanged(sender As Object, e As EventArgs) Handles dtpBirth.ValueChanged
        Try
            Dim nowdate As New DateTime()
            txtAge.Text = DateDiff(DateInterval.Year, dtpBirth.Value, New DateTime().Now)

        Catch ex As Exception

        End Try
    End Sub
End Class