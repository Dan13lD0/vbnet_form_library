Public Class FrmListClients
    Private _utils As New Utils()
    Private _bll As New ClientBll()
    Private Sub txtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCode.KeyPress
        e.Handled = _utils.OnlyNumers(e.KeyChar)
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        e.Handled = _utils.OnlyLettersAndNumbers(e.KeyChar)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub FrmListUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _utils.LoadComboBoxStatus(True, cboStatus)
        LoadGrid()
    End Sub
    Private Function LoadGrid()
        Dim data = _bll.GetClients(txtCode.Text, txtName.Text, mtbRg.Text, mtbCpf.Text, cboStatus.Text)
        Dim bs As New BindingSource()
        bs.DataSource = data
        dgv.DataSource = bs.DataSource
        bn.BindingSource = bs
        FormatGrid()
        dgv_Click(Nothing, Nothing)
    End Function

    Private Function FormatGrid()

        dgv.Columns("BirthDay").Visible = False
        dgv.Columns("Age").Visible = False
        dgv.Columns("ContactId").Visible = False
        dgv.Columns("Contact").Visible = False
        dgv.Columns("AddressId").Visible = False
        dgv.Columns("Address").Visible = False
        dgv.Columns("Status").Visible = False


        dgv.Columns("Id").HeaderText = "Code"
        dgv.Columns("Id").Width = 100
        dgv.Columns("Id").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgv.Columns("Name").HeaderText = "Name"
        dgv.Columns("Name").Width = 150
        dgv.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgv.Columns("RG").HeaderText = "RG"
        dgv.Columns("RG").Width = 100
        dgv.Columns("RG").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        dgv.Columns("CPF").HeaderText = "CPF"
        dgv.Columns("CPF").Width = 100
        dgv.Columns("CPF").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        dgv.Columns("StatusDescription").HeaderText = "Status"
        dgv.Columns("StatusDescription").Width = 100
        dgv.Columns("StatusDescription").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Function

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadGrid()
    End Sub

    Private Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        If dgv.Rows.Count > 0 Then
            If MessageBox.Show("Would you like to change the status?", "Change Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Dim id = dgv.CurrentRow.Cells("Id").Value.ToString()
                Dim action As Boolean
                Dim msg As String
                _bll.DeleteClient(id, action, msg)
                If action Then
                    MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                    LoadGrid()
                End If
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frmNew As New FrmCreateUpdateClient("", "", "", "", "", "0", "0", "")
        frmNew.ShowDialog()
        LoadGrid()
    End Sub

    Private Sub dgv_DoubleClick(sender As Object, e As EventArgs) Handles dgv.DoubleClick
        If dgv.Rows.Count > 0 Then
            Dim row = dgv.CurrentRow
            Dim frmUpdate As New FrmCreateUpdateClient(
            row.Cells("Id").Value.ToString(),
            row.Cells("Name").Value.ToString(),
            row.Cells("Birthday").Value.ToString(),
            row.Cells("RG").Value.ToString(),
            row.Cells("CPF").Value.ToString(),
            row.Cells("ContactId").Value.ToString(),
            row.Cells("AddressId").Value.ToString(),
            row.Cells("StatusDescription").Value.ToString()
            )
            frmUpdate.ShowDialog()
            LoadGrid()
        End If
    End Sub

    Private Sub dgv_Click(sender As Object, e As EventArgs) Handles dgv.Click

    End Sub
End Class