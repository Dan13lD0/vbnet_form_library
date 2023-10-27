Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

<Table("Client")>
Public Class ClientDto
    Inherits CreateDatabaseIfNotExists(Of ContextSqlServer)

    Private _id As Int64
    Private _name As String
    Private _birthDay As DateTime
    Private _age As Int16
    Private _rg As String
    Private _cpf As String
    Private _addressId As Int64
    Private _address As AddressDto
    Private _contactId As Int64
    Private _contact As ContactDto
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

    Public Property BirthDay() As DateTime
        Get
            Return _birthDay
        End Get
        Set(ByVal value As DateTime)
            _birthDay = value
        End Set
    End Property

    Public Property Age() As Int16
        Get
            Return _age
        End Get
        Set(ByVal value As Int16)
            _age = value
        End Set
    End Property

    Public Property RG() As String
        Get
            Return _rg
        End Get
        Set(ByVal value As String)
            _rg = value
        End Set
    End Property

    Public Property CPF() As String
        Get
            Return _cpf
        End Get
        Set(ByVal value As String)
            _cpf = value
        End Set
    End Property

    <ForeignKey("Contact")>
    Public Property ContactId() As Int64
        Get
            Return _contactId
        End Get
        Set(ByVal value As Int64)
            _contactId = value
        End Set
    End Property
    Public Property Contact() As ContactDto
        Get
            Return _contact
        End Get
        Set(ByVal value As ContactDto)
            _contact = value
        End Set
    End Property

    <ForeignKey("Address")>
    Public Property AddressId() As Int64
        Get
            Return _addressId
        End Get
        Set(ByVal value As Int64)
            _addressId = value
        End Set
    End Property
    Public Property Address() As AddressDto
        Get
            Return _address
        End Get
        Set(ByVal value As AddressDto)
            _address = value
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
