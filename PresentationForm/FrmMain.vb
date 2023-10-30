Public Class FrmMain
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = 0
        Me.Left = 0
        Me.Width = Screen.PrimaryScreen.Bounds.Width
        Me.Height = Screen.PrimaryScreen.Bounds.Height - 4
    End Sub

    Private Sub ListbooksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListbooksToolStripMenuItem.Click
        ShowForm(New FrmListBooks())
    End Sub

    Private Sub UserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserToolStripMenuItem.Click
        ShowForm(New FrmListUsers())
    End Sub

    Private Function ShowForm(form As Form)
        form.Top = 0
        form.Left = 0
        form.Width = Me.Width - 25
        form.Height = Me.Height - 65
        form.MdiParent = Me
        form.Show()
    End Function

    Private Sub ClientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClientToolStripMenuItem.Click
        ShowForm(New FrmListClients())
    End Sub

    Private Sub AuthorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AuthorToolStripMenuItem.Click
        ShowForm(New FrmListAuthors())
    End Sub

    Private Sub CategoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CategoryToolStripMenuItem.Click
        ShowForm(New FrmListCategories())
    End Sub

    Private Sub PublisherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PublisherToolStripMenuItem.Click
        ShowForm(New FrmListPublishers())
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        If MessageBox.Show("Would you like to close the system?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
            Close()
        End If
    End Sub
End Class
