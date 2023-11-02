Imports System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder

Public Class FrmChoseCreateReturn
    Private _status As String
    Private _bookId As String
    Private _bll As New BorrowBll()
    Public Sub New(status As String, bookId As String)
        InitializeComponent()
        _status = status
        _bookId = bookId
    End Sub

    Private Sub FrmChoseCreateReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status.Equals("Available") Then
            btnAction.Text = "Borrow"
            lblQuestion.Text = "Would you like to borrow this book?"
        Else
            btnAction.Text = "Return"
            lblQuestion.Text = "Would you like to return this book?"
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction.Click
        If btnAction.Text.Equals("Borrow") Then
            Close()
            Dim borrow As New FrmBorrow(_bookId)
            borrow.ShowDialog()
        Else
            If MessageBox.Show("Would you like to return this record?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Dim action As Boolean
                Dim msg As String
                _bll.DeleteBorrow(_bookId, action, msg)
            End If
            Close()
        End If
    End Sub
End Class