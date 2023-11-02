Imports System.Runtime.CompilerServices.RuntimeHelpers

Public Class FrmBorrow
    Private _bookId As Int64


    Private _utils As New Utils()

    Dim _bll As New BorrowBll()
    Dim _bllBook As New BookBll()
    Dim _bllAuthor As New AuthorBll()
    Dim _bllCategory As New CategoryBll()
    Dim _bllPublisher As New PublisherBll()
    Dim _bllClient As New ClientBll()
    Dim _bllClientAddress As New AddressBll()
    Dim _bllClientContact As New ContactBll()
    Public Sub New(bookId As Long)
        InitializeComponent()
        _bookId = bookId
    End Sub

    Private Sub FrmCreateUpdateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim book = _bllBook.GetBook(_bookId)

        txtBook.Text = book.Name
        txtAuthor.Text = _bllAuthor.GetAuthor(book.AuthorId.ToString()).Name
        txtCategory.Text = _bllCategory.GetCategory(book.CategoryId.ToString()).Name
        txtPublisher.Text = _bllPublisher.GetPublisher(book.PublisherId.ToString()).Name


        cboClient.DataSource = _bllClient.GetClients("0", "", "", "", "Active")
        cboClient.DisplayMember = "Name"
        cboClient.ValueMember = "Id"


    End Sub

    Private Function SearchClient()
        If cboClient.SelectedValue IsNot Nothing Then
            Dim client = _bllClient.GetClient(cboClient.SelectedValue)
            txtClient.Text = client.Name

            txtRG.Text = client.RG
            txtCPF.Text = client.CPF

            Dim address = _bllClientAddress.GetAddress(client.AddressId.ToString())
            txtCEP.Text = address.ZipCode
            txtStreet.Text = address.Street
            txtComplement.Text = address.Complement
            txtCity.Text = address.City
            txtState.Text = address.State

            Dim contact = _bllClientContact.GetContact(client.ContactId.ToString())
            txtType.Text = contact.Type
            txtContact.Text = contact.Contact
            txtFor.Text = contact.Person
        End If

    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim action As Boolean
        Dim msg As String
        Dim obj As New BorrowDto()

        obj = _bll.CreateUpdateBorrow(_bookId, cboClient.SelectedValue, dtpBorrow.Value, dtpReturn.Value, msg, action)

        MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

    End Sub

    Private Sub cboClient_Leave(sender As Object, e As EventArgs) Handles cboClient.Leave
        Try
            SearchClient()
        Catch ex As Exception

        End Try
    End Sub
End Class