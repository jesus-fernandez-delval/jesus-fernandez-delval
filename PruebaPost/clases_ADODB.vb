Public Class ADOconnection
    Private bdTmp As ADODB.Connection


    'Public Function OpenDatabase(ByVal sUnidadDisco As String, ByVal sDirectorioBD As String, ByVal sNombreBD As String, ByVal iTipoConexion As Short, ByVal iTipoPassword As Short) As ADODB.Connection
    '    Dim sConexion As String

    '    bdTmp = New ADODB.Connection

    '    sConexion = Sacar_String_Conexion(sUnidadDisco, sDirectorioBD, sNombreBD, iTipoConexion, iTipoPassword)



    '    bdTmp.Open(sConexion)


    '    OpenDatabase = bdTmp

    'End Function
    Public Function OpenDatabase(ByVal sUnidadDisco As String, ByVal sDirectorioBD As String, ByVal sNombreBD As String, ByVal iTipoConexion As Short) As ADODB.Connection
        Dim sConexion As String

        bdTmp = New ADODB.Connection

        '  MsgBox("entro a la funcion de string de conexion")
        sConexion = Sacar_String_Conexion(sUnidadDisco, sDirectorioBD, sNombreBD, iTipoConexion)



        bdTmp.Open(sConexion)


        OpenDatabase = bdTmp

    End Function
End Class
