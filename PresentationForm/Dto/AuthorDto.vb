Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

<Table("Author")>
Public Class AuthorDto
    Inherits CreateDatabaseIfNotExists(Of ContextSqlServer)

    Private _id As Int64
    Private _name As String
    Private _status As Boolean
    Private _statusDescription As String

    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property Id() As Int64
        Get
            Return _id
        End Get
        Set(ByVal value As Int64)
            _id = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property Status() As Boolean
        Get
            Return _status
        End Get
        Set(ByVal value As Boolean)
            _status = value
        End Set
    End Property
    <NotMapped>
    Public ReadOnly Property StatusDescription() As String
        Get
            If _status Then
                Return "Active"
            Else
                Return "Inactive"
            End If
        End Get
    End Property
End Class
