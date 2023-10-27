Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

<Table("Address")>
Public Class AddressDto
    Inherits CreateDatabaseIfNotExists(Of ContextSqlServer)

    Private _id As String
    Private _zipCode As String
    Private _street As String
    Private _complement As String
    Private _city As String
    Private _state As String
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

    Public Property ZipCode() As String
        Get
            Return _zipCode
        End Get
        Set(ByVal value As String)
            _zipCode = value
        End Set
    End Property

    Public Property Street() As String
        Get
            Return _street
        End Get
        Set(ByVal value As String)
            _street = value
        End Set
    End Property
    Public Property Complement() As String
        Get
            Return _complement
        End Get
        Set(ByVal value As String)
            _complement = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return _state
        End Get
        Set(ByVal value As String)
            _state = value
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
            If _statusDescription Then
                Return "Active"
            Else
                Return "Inactive"
            End If
        End Get
    End Property
End Class
