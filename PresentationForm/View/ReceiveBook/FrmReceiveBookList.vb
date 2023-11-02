Imports System.Runtime.CompilerServices.RuntimeHelpers

Public Class FrmReceiveBookList
    Private _bll As New BorrowBll()
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadGrid()
    End Sub
    Private Function LoadGrid()
        Dim data = _bll.GetBorrows(txtClient.Text, txtBook.Text)
        Dim bs As New BindingSource()
        bs.DataSource = data
        dgv.DataSource = bs.DataSource
        bn.BindingSource = bs
        FormatGrid()
        FormatGrid()
    End Function
    Private Function FormatGrid()
        dgv.Columns("Status").Visible = False
        dgv.Columns("BookId").Visible = False
        dgv.Columns("Book").Visible = False
        dgv.Columns("ClientId").Visible = False
        dgv.Columns("Client").Visible = False


        dgv.Columns("Id").HeaderText = "Code"
        dgv.Columns("Id").Width = 100
        dgv.Columns("Id").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgv.Columns("BookName").HeaderText = "Book Name"
        dgv.Columns("BookName").Width = 150
        dgv.Columns("BookName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("BookName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgv.Columns("ClientName").HeaderText = "Client Name"
        dgv.Columns("ClientName").Width = 150
        dgv.Columns("ClientName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("ClientName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgv.Columns("DateBorrow").HeaderText = "Borrw"
        dgv.Columns("DateBorrow").Width = 150
        dgv.Columns("DateBorrow").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        dgv.Columns("DateReturn").HeaderText = "Return"
        dgv.Columns("DateReturn").Width = 150
        dgv.Columns("DateReturn").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        dgv.Columns("StatusDescription").HeaderText = "Status"
        dgv.Columns("StatusDescription").Width = 100
        dgv.Columns("StatusDescription").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Function

    Private Sub FrmReceiveBookList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Sub dgv_DoubleClick(sender As Object, e As EventArgs) Handles dgv.DoubleClick
        If dgv.Rows.Count > 0 Then
            If MessageBox.Show("Would you like to return this book?", "Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                _bll.BorrowChangeStatus(dgv.CurrentRow.Cells("Id").Value.ToString())
                LoadGrid()
            End If
        End If
    End Sub
End Class