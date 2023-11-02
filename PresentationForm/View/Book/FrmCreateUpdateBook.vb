Public Class FrmCreateUpdateBook
    Private _code As String
    Private _name As String
    Private _category As String
    Private _Book As String
    Private _publisher As String
    Private _description As String
    Private _status As String

    Private _utils As New Utils()
    Private _bll As New BookBll()
    Private _bllAutor As New AuthorBll()
    Private _bllCategory As New CategoryBll()
    Private _bllPublisher As New PublisherBll()


    Public Sub New(code As String, name As String, category As String, Book As String, publisher As String, description As String, status As String)
        InitializeComponent()
        _code = code
        _name = name
        _category = category
        _Book = Book
        _publisher = publisher
        _description = description
        _status = status
    End Sub
    Private Function LoadAllComboBox()
        Dim allCategories = _bllCategory.GetCategories("0", "", "Active")

        If allCategories.Count > 0 Then
            cboCategory.DataSource = allCategories
            cboCategory.DisplayMember = "Name"
            cboCategory.ValueMember = "Id"
        End If

        Dim allAuthors = _bllAutor.GetAuthors("0", "", "Active")
        If allAuthors.Count > 0 Then
            cboAuthor.DataSource = allAuthors
            cboAuthor.DisplayMember = "Name"
            cboAuthor.ValueMember = "Id"
        End If

        Dim allPublisher = _bllPublisher.GetPublishers("0", "", "Active")
        If allPublisher.Count > 0 Then
            cboPublisher.DataSource = allPublisher
            cboPublisher.DisplayMember = "Name"
            cboPublisher.ValueMember = "Id"
        End If
    End Function
    Private Sub FrmCreateUpdateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _utils.LoadComboBoxStatus(False, cboStatus)
        LoadAllComboBox()
        txtCode.Text = _code
        txtName.Text = _name
        cboCategory.Text = _category
        cboAuthor.Text = _Book
        cboPublisher.Text = _publisher
        rtbDescription.Text = _description
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
        Dim obj As New BookDto()

        obj = _bll.CreateUpdateBook(txtCode.Text, txtName.Text, cboCategory.SelectedValue, cboAuthor.SelectedValue, cboPublisher.SelectedValue, cboStatus.Text, msg, action)
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