Public Class FrmMain
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = 0
        Me.Left = 0
        Me.Width = Screen.PrimaryScreen.Bounds.Width
        Me.Height = Screen.PrimaryScreen.Bounds.Height - 45

        Dim bll As New CategoryBll()

        bll.GetCategories()

    End Sub

    Private Sub ListbooksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListbooksToolStripMenuItem.Click

    End Sub
End Class
