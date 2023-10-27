Imports System.Configuration
Imports System.Data.Entity

Public Class ContextSqlServer
    Inherits DbContext

    Public Sub New()
        MyBase.New(ConfigurationManager.ConnectionStrings("ConnectionString").ToString())
    End Sub

    Private _user As DbSet(Of UserDto)
    Private _category As DbSet(Of CategoryDto)
    Private _publisher As DbSet(Of PublisherDto)
    Private _address As DbSet(Of AddressDto)
    Private _contact As DbSet(Of ContactDto)
    Private _client As DbSet(Of ClientDto)
    Private _author As DbSet(Of AuthorDto)
    Private _book As DbSet(Of BookDto)
    Private _borrow As DbSet(Of BorrowDto)

    Public Property User As DbSet(Of UserDto)
        Get
            Return _user
        End Get
        Set(value As DbSet(Of UserDto))
            _user = value
        End Set
    End Property

    Public Property Category As DbSet(Of CategoryDto)
        Get
            Return _category
        End Get
        Set(value As DbSet(Of CategoryDto))
            _category = value
        End Set
    End Property

    Public Property Publisher As DbSet(Of PublisherDto)
        Get
            Return _publisher
        End Get
        Set(value As DbSet(Of PublisherDto))
            _publisher = value
        End Set
    End Property

    Public Property Address As DbSet(Of AddressDto)
        Get
            Return _address
        End Get
        Set(value As DbSet(Of AddressDto))
            _address = value
        End Set
    End Property

    Public Property Contact As DbSet(Of ContactDto)
        Get
            Return _contact
        End Get
        Set(value As DbSet(Of ContactDto))
            _contact = value
        End Set
    End Property

    Public Property Client As DbSet(Of ClientDto)
        Get
            Return _client
        End Get
        Set(value As DbSet(Of ClientDto))
            _client = value
        End Set
    End Property

    Public Property Author As DbSet(Of AuthorDto)
        Get
            Return _author
        End Get
        Set(value As DbSet(Of AuthorDto))
            _author = value
        End Set
    End Property

    Public Property Book As DbSet(Of BookDto)
        Get
            Return _book
        End Get
        Set(value As DbSet(Of BookDto))
            _book = value
        End Set
    End Property

    Public Property Borrow As DbSet(Of BorrowDto)
        Get
            Return _borrow
        End Get
        Set(value As DbSet(Of BorrowDto))
            _borrow = value
        End Set
    End Property
End Class
