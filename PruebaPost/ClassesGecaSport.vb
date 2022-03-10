Public Class ELista
    Private myName As String
    Private myClave As Integer
    Private myClaveAlfa As String
    Public Sub New(ByVal strName As String, ByVal lMyClave As Integer, ByVal sMyClaveAlfa As String)
        Me.myName = strName
        Me.myClave = lMyClave
        Me.myClaveAlfa = sMyClaveAlfa
    End Sub 'New

    Public ReadOnly Property Nombre() As String
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property Indice() As Integer
        Get
            Return myClave
        End Get
    End Property
    Public ReadOnly Property IndiceAlfa() As String
        Get
            Return myClaveAlfa
        End Get
    End Property
    Public Overrides Function ToString() As String
        Return Me.myName
    End Function 'ToString


End Class

Public Class EListaChar
    Private myName As String
    Private myClave As String

    Public Sub New(ByVal strName As String, ByVal lMyClave As String)
        Me.myName = strName
        Me.myClave = lMyClave
    End Sub 'New

    Public ReadOnly Property Nombre() As String
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property Indice() As String
        Get
            Return myClave
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.myName
    End Function 'ToString


End Class
'Public Class ADOconnection
'    Private bdTmp As ADODB.Connection


'    Public Function OpenDatabase(ByVal strConexion As String) As ADODB.Connection

'        Try
'            bdTmp = New ADODB.Connection
'            bdTmp.Open(strConexion)
'            OpenDatabase = bdTmp
'        Catch ex As Exception
'            MsgBox("Error no se ha podido abrir la base de datos. String conexión: " + strConexion, MsgBoxStyle.Critical, "Atención")
'            OpenDatabase = Nothing
'        End Try
'    End Function
'End Class

