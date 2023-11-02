Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

<Table("Book")>
Public Class BookDto
    Inherits CreateDatabaseIfNotExists(Of ContextSqlServer)

    Private _id As Int64
    Private _name As String
    Private _description As String
    Private _categoryId As Int64
    Private _category As CategoryDto
    Private _authorId As Int64
    Private _author As AuthorDto
    Private _publisherId As Int64
    Private _publisher As PublisherDto
    Private _status As Boolean
    Private _authorName As String
    Private _categoryName As String
    Private _publisherName As String
    Private _borrow As String

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
    <ForeignKey("Publisher")>
    Public Property PublisherId() As Int64
        Get
            Return _publisherId
        End Get
        Set(ByVal value As Int64)
            _publisherId = value
        End Set
    End Property
    Public Property Publisher() As PublisherDto
        Get
            Return _publisher
        End Get
        Set(ByVal value As PublisherDto)
            _publisher = value
        End Set
    End Property
    <ForeignKey("Author")>
    Public Property AuthorId() As Int64
        Get
            Return _authorId
        End Get
        Set(ByVal value As Int64)
            _authorId = value
        End Set
    End Property
    Public Property Author() As AuthorDto
        Get
            Return _author
        End Get
        Set(ByVal value As AuthorDto)
            _author = value
        End Set
    End Property

    <ForeignKey("Category")>
    Public Property CategoryId() As Int64
        Get
            Return _categoryId
        End Get
        Set(ByVal value As Int64)
            _categoryId = value
        End Set
    End Property

    Public Property Category() As CategoryDto
        Get
            Return _category
        End Get
        Set(ByVal value As CategoryDto)
            _category = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
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
    <NotMapped>
    Public Property CategoryName As String
        Get
            Return _categoryName
        End Get
        Set(value As String)
            _categoryName = value
        End Set
    End Property
    <NotMapped>
    Public Property AuthorName As String
        Get
            Return _authorName
        End Get
        Set(value As String)
            _authorName = value
        End Set
    End Property

    <NotMapped>
    Public Property PublisherName As String
        Get
            Return _publisherName
        End Get
        Set(value As String)
            _publisherName = value
        End Set
    End Property
    <NotMapped>
    Public Property Borrow As String
        Get
            Return _borrow
        End Get
        Set(value As String)
            _borrow = value
        End Set
    End Property
End Class
