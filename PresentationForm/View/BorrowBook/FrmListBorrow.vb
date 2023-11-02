Public Class FrmListBorrow
    Private _utils As New Utils()
    Private _bll As New BorrowBll()
    Private _bllCategory As New CategoryBll()
    Private _bllAuthor As New AuthorBll()
    Private _bllPublisher As New PublisherBll()

    Private Sub txtCode_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = _utils.OnlyNumers(e.KeyChar)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub FrmListUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub
    Private Function LoadGrid()
        Dim data = _bll.GetBorrows(txtBook.Text, cboCategory.SelectedValue, cboAuthor.SelectedValue, cboPublisher.SelectedValue)
        Dim bs As New BindingSource()
        bs.DataSource = data
        dgv.DataSource = bs.DataSource
        bn.BindingSource = bs
        LoadAllCombobox()
        FormatGrid()

    End Function

    Private Function LoadAllCombobox()
        Dim insetCategory As New CategoryDto()
        insetCategory.Id = 0
        insetCategory.Name = "All"
        Dim category = _bllCategory.GetCategories("0", "", "Active")
        category.Add(insetCategory)
        cboCategory.DataSource = category.OrderBy(Function(x) x.Id).ToList()
        cboCategory.DisplayMember = "Name"
        cboCategory.ValueMember = "Id"

        Dim insertAuthor As New AuthorDto()
        insertAuthor.Id = 0
        insertAuthor.Name = "All"
        Dim author = _bllAuthor.GetAuthors("0", "", "Active")
        author.Add(insertAuthor)
        cboAuthor.DataSource = author.OrderBy(Function(x) x.Id).ToList()
        cboAuthor.DisplayMember = "Name"
        cboAuthor.ValueMember = "Id"

        Dim insertPublisher As New PublisherDto()
        insertPublisher.Id = 0
        insertPublisher.Name = "All"
        Dim publisher = _bllPublisher.GetPublishers("0", "", "Active")
        publisher.Add(insertPublisher)
        cboPublisher.DataSource = publisher.OrderBy(Function(x) x.Id).ToList()
        cboPublisher.DisplayMember = "Name"
        cboPublisher.ValueMember = "Id"

    End Function

    Private Function FormatGrid()

        dgv.Columns("PublisherId").Visible = False
        dgv.Columns("Publisher").Visible = False
        dgv.Columns("AuthorId").Visible = False
        dgv.Columns("Author").Visible = False
        dgv.Columns("CategoryId").Visible = False
        dgv.Columns("Category").Visible = False
        dgv.Columns("Description").Visible = False
        dgv.Columns("Status").Visible = False
        dgv.Columns("StatusDescription").Visible = False



        dgv.Columns("Id").HeaderText = "Code"
        dgv.Columns("Id").Width = 100
        dgv.Columns("Id").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgv.Columns("Name").HeaderText = "Name"
        dgv.Columns("Name").Width = 100
        dgv.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgv.Columns("CategoryName").HeaderText = "Category Name"
        dgv.Columns("CategoryName").Width = 100
        dgv.Columns("CategoryName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("CategoryName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgv.Columns("AuthorName").HeaderText = "Author Name"
        dgv.Columns("AuthorName").Width = 100
        dgv.Columns("AuthorName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("AuthorName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


        dgv.Columns("PublisherName").HeaderText = "Publisher Name"
        dgv.Columns("PublisherName").Width = 100
        dgv.Columns("PublisherName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("PublisherName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgv.Columns("Borrow").HeaderText = "Status Borrow"
        dgv.Columns("Borrow").Width = 100
        dgv.Columns("Borrow").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


    End Function

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadGrid()
    End Sub


    Private Sub dgv_DoubleClick(sender As Object, e As EventArgs) Handles dgv.DoubleClick
        If dgv.Rows.Count > 0 Then
            Dim row = dgv.CurrentRow
            row.Cells("Id").Value.ToString()
            Dim choise As New FrmChoseCreateReturn(row.Cells("Borrow").Value.ToString(), row.Cells("Id").Value.ToString())
            choise.ShowDialog()
            LoadGrid()
        End If
    End Sub

End Class