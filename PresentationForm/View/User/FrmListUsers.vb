﻿Public Class FrmListUsers
    Private _utils As New Utils()
    Private _bll As New UserBll()
    Private Sub txtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCode.KeyPress
        e.Handled = _utils.OnlyNumers(e.KeyChar)
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        e.Handled = _utils.OnlyLettersAndNumbers(e.KeyChar)
    End Sub

    Private Sub txtMail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMail.KeyPress
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
        Dim data = _bll.GetUsers(txtCode.Text, txtName.Text, txtLogin.Text, txtMail.Text, cboStatus.Text)
        Dim bs As New BindingSource()
        bs.DataSource = data
        dgv.DataSource = bs.DataSource
        bn.BindingSource = bs
        FormatGrid()
        dgv_Click(Nothing, Nothing)
    End Function

    Private Function FormatGrid()

        dgv.Columns("Status").Visible = False
        dgv.Columns("Password").Visible = False

        dgv.Columns("Id").HeaderText = "Code"
        dgv.Columns("Id").Width = 100
        dgv.Columns("Id").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgv.Columns("Name").HeaderText = "Name"
        dgv.Columns("Name").Width = 150
        dgv.Columns("Name").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgv.Columns("Login").HeaderText = "Login"
        dgv.Columns("Login").Width = 150
        dgv.Columns("Login").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgv.Columns("Email").HeaderText = "E-mail"
        dgv.Columns("Email").Width = 150
        dgv.Columns("Email").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.Columns("Email").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

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
                _bll.DeleteUser(id, action, msg)
                If action Then
                    MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                    LoadGrid()
                End If
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frmNew As New FrmCreateUpdateUser("", "", "", "", "", "")
        frmNew.ShowDialog()
        LoadGrid()
    End Sub

    Private Sub dgv_DoubleClick(sender As Object, e As EventArgs) Handles dgv.DoubleClick
        If dgv.Rows.Count > 0 Then
            Dim row = dgv.CurrentRow
            Dim frmUpdate As New FrmCreateUpdateUser(
            row.Cells("Id").Value.ToString(),
            row.Cells("Name").Value.ToString(),
            row.Cells("Login").Value.ToString(),
            row.Cells("Password").Value.ToString(),
            row.Cells("Email").Value.ToString(),
            row.Cells("StatusDescription").Value.ToString()
            )
            frmUpdate.ShowDialog()
            LoadGrid()
        End If
    End Sub

    Private Sub dgv_Click(sender As Object, e As EventArgs) Handles dgv.Click
        If dgv.Rows.Count > 0 Then
            Dim row = dgv.CurrentRow
            If row.Cells("StatusDescription").Value.ToString().Equals("Active") Then
                btnChangeStatus.Text = "Inactive"
            Else
                btnChangeStatus.Text = "Active"
            End If
        End If

    End Sub
End Class