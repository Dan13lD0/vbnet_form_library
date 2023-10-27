Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

<Table("Contact")>
Public Class ContactDto
    Inherits CreateDatabaseIfNotExists(Of ContextSqlServer)

    Private _id As String
    Private _type As String
    Private _contact As String
    Private _person As String



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

    Public Property Type() As String
        Get
            Return _type
        End Get
        Set(ByVal value As String)
            _type = value
        End Set
    End Property

    Public Property Contact() As String
        Get
            Return _contact
        End Get
        Set(ByVal value As String)
            _contact = value
        End Set
    End Property
    Public Property Person() As String
        Get
            Return _person
        End Get
        Set(ByVal value As String)
            _person = value
        End Set
    End Property

End Class
