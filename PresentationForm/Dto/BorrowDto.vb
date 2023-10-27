Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

<Table("Borrow")>
Public Class BorrowDto
    Inherits CreateDatabaseIfNotExists(Of ContextSqlServer)

    Private _id As Int64

    Private _bookId As Int64
    Private _book As BookDto
    Private _clientId As Int64
    Private _client As ClientDto
    Private _dateBorrow As DateTime
    Private _dateReturn As DateTime
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
    <ForeignKey("Book")>
    Public Property BookId As Long
        Get
            Return _bookId
        End Get
        Set(value As Long)
            _bookId = value
        End Set
    End Property

    Public Property Book As BookDto
        Get
            Return _book
        End Get
        Set(value As BookDto)
            _book = value
        End Set
    End Property
    <ForeignKey("Client")>
    Public Property ClientId As Long
        Get
            Return _clientId
        End Get
        Set(value As Long)
            _clientId = value
        End Set
    End Property

    Public Property Client As ClientDto
        Get
            Return _client
        End Get
        Set(value As ClientDto)
            _client = value
        End Set
    End Property

    Public Property DateBorrow As Date
        Get
            Return _dateBorrow
        End Get
        Set(value As Date)
            _dateBorrow = value
        End Set
    End Property

    Public Property DateReturn As Date
        Get
            Return _dateReturn
        End Get
        Set(value As Date)
            _dateReturn = value
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
