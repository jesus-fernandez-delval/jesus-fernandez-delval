Module Mll_Funciones

    Structure STR_DATOS_VALDE

        Dim idDeporte As Integer
        Dim opta_sport_id As Integer
        Dim idCompeticion As Integer
        Dim idSeason As Integer
        Dim idUnico As Integer
        Dim v_fn As String
        Dim v_fg As String
        Dim v_feed_type As String
        Dim defaultFilename As String
        Dim siglasOpta As String
        Dim TimeStamp As String
        Dim Jornada As String
        Dim GameId As String
        Dim sParamGS As String
        ' itipo:  1 = almacenado en access , 2 = almacenado en mysql
    End Structure
    Public StrDatosValde As STR_DATOS_VALDE

    Public Function Get_Nivel_Usuario() As Integer

        Get_Nivel_Usuario = -1
        If My.Computer.FileSystem.FileExists("c:\infofutbol\config\desarrollo.txt") = True Then
            Get_Nivel_Usuario = 99
        End If


    End Function
    Function Sacar_Unidad_Servidor() As String
        'Dim rsTmp As ADODB.Recordset

        Sacar_Unidad_Servidor = "c:"

        'sql = "SELECT nbMaquina, DiscoDat FROM Maquinas"
        'sql = sql + " WHERE Servidor = 1"

        'rsTmp = New ADODB.Recordset
        'rsTmp.Open(sql, bdMySQL)

        'If Not rsTmp.EOF Then
        '    Sacar_Unidad_Servidor = rsTmp.Fields("DiscoDat").Value
        '    ' Maquina_Servidor = rsTmp.Fields("nbMaquina").Value
        'End If

        'rsTmp.Close()
        'rsTmp = Nothing

    End Function
    Sub Abrir_BBDD_MySQL()

        ' MsgBox("aa")

        '   If bMySQLAbierta = False Then
        '    MsgBox("1")
        bdADO = New ADOconnection
        '   MsgBox("2")
        bdMySQL = bdADO.OpenDatabase("", "", "optaesp_allsports", BBDD_MYSQL)
        '  MsgBox("3")
        bdADO = Nothing

        '  End If
        'MsgBox("bb")

        '     bMySQLAbierta = True

    End Sub

    Sub WriteLog(ByVal Texto As String)
        Dim Hora As String
        Dim FicheroLogGeneral As String

        Hora = My.Computer.Clock.LocalTime.ToString
        FicheroLogGeneral = "c:\infofutbol\log\PruebasPost.log"
        FileOpen(1, FicheroLogGeneral, OpenMode.Append)
        PrintLine(1, Trim(Hora) + ": " + Texto)
        FileClose(1)
    End Sub


    'Function OpenDataBaseAccess(ByVal szBase As String) As ADODB.Connection
    '    Dim sConexion As String
    '    Dim bdTmp As ADODB.Connection

    '    AdoBDAccess = New ADOconnection
    '    bdTmp = Nothing
    '    Try
    '        sConexion = ConstruirStringConexioAceesADO(szBase)
    '        bdTmp = AdoBDAccess.OpenDatabase(sConexion)
    '    Catch ex As Exception
    '        MsgBox("ERROR OpenBaseAccess, abriendo la base: " + szBase + " Err: " + ex.ToString, MsgBoxStyle.Critical, "Atención")
    '    End Try
    '    OpenDataBaseAccess = bdTmp

    'End Function
    'Sub CloseDataBaseAccess(ByVal bdTmp As ADODB.Connection)

    '    Try
    '        bdTmp.Close()
    '        AdoBDAccess = Nothing
    '    Catch ex As Exception
    '    End Try

    'End Sub
    Function ConstruirStringConexioAceesADO(ByVal szDatabase As String)
        Dim szTmp As String
        Dim PassW As String

        PassW = Sacar_Password(szDatabase)
        szTmp = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
             "Data Source=" + szDatabase + ";" & _
             "Jet OLEDB:Database Password=" + PassW 'MyPwd"

        ConstruirStringConexioAceesADO = szTmp
    End Function
    Public Function Sacar_Password(ByVal szBase As String) As String
        Dim szTmp As String
        Dim iTipo As String
        Dim szNb As String
        Dim i As Integer
        Dim spss As String
        Dim iNodos As Integer
        Dim sztmp2 As String
        Dim j As Integer
        Dim snum As String
        Dim sDisco As String
        Dim XmlCnf As XmlDocument 'MSXML2.DOMDocument60
        Dim XmlNCnf As XmlNodeList ' MSXML2.IXMLDOMNodeList

        i = InStr(szBase, "\")
        If i <> 0 Then
            sDisco = Mid(szBase, 1, i - 1)
        Else
            sDisco = "c:\"
        End If
        If sDisco = "" Then
            sDisco = "c:\"
        End If
        Sacar_Password = ""
        iTipo = -1

        i = InStrRev(szBase, "\", -1)
        If i <> 0 Then
            szNb = Mid(szBase, i + 1)
        Else
            szNb = szBase
        End If
        i = InStr(UCase(szNb), ".MDB")
        If i <> 0 Then
            szNb = Mid(szNb, 1, i - 1)
        Else
            szNb = szBase
        End If
        szTmp = sDisco + DIRECTORIO_CONFIGURACIONES_XML + FICHERO_XML_CLAVES
        If Dir(szTmp) <> "" Then
            XmlCnf = New XmlDocument 'MSXML2.DOMDocument60
            XmlCnf.Load(szTmp)
            XmlNCnf = XmlCnf.selectNodes("GS/ITEMS/ITEM")
            iNodos = XmlNCnf.Count 'XmlNCnf.length
            For i = 0 To iNodos - 1
                'If UCase(szNb) = UCase(Trim(XmlNCnf.item(i).selectSingleNode("@NB").text)) Then
                If UCase(szNb) = UCase(Trim(XmlNCnf.Item(i).SelectSingleNode("@NB").Value)) Then
                    iTipo = CInt(Trim(XmlNCnf.Item(i).SelectSingleNode("@CLS").Value))
                    Exit For
                End If
            Next
            XmlNCnf = Nothing
            If iTipo = -1 Then
                sztmp2 = Mid(szNb, 1, 2)
                For i = 3 To Len(szNb)
                    sztmp2 = sztmp2 + "?"
                Next
                szNb = sztmp2
                XmlNCnf = XmlCnf.selectNodes("GS/ITEMS/ITEM")
                iNodos = XmlNCnf.Count 'XmlNCnf.length
                For i = 0 To iNodos - 1
                    If UCase(szNb) = UCase(Trim(XmlNCnf.Item(i).SelectSingleNode("@NB").Value)) Then
                        iTipo = CInt(Trim(XmlNCnf.Item(i).SelectSingleNode("@CLS").Value))
                        Exit For
                    End If
                Next
                XmlNCnf = Nothing
            End If

            XmlNCnf = XmlCnf.selectNodes("GS/TIPOS/TIPO")
            iNodos = XmlNCnf.Count 'XmlNCnf.length
            For i = 0 To iNodos - 1
                If iTipo = CInt(Trim(XmlNCnf.Item(i).SelectSingleNode("@ID").Value)) Then
                    spss = Trim(XmlNCnf.Item(i).SelectSingleNode("@PAR1").Value)
                    spss = Mid(spss, 1, Len(spss) - 2)
                    spss = Mid(spss, 3)
                    sztmp2 = ""
                    For j = 1 To Len(spss) Step 6
                        snum = Mid(spss, j, 1) + Mid(spss, j + 2, 1) + Mid(spss, j + 4, 1)
                        sztmp2 = sztmp2 + Chr(CInt(snum))
                    Next
                    Sacar_Password = sztmp2
                End If
            Next
            XmlCnf = Nothing
            XmlNCnf = Nothing
        Else
            Sacar_Password = ""
        End If

    End Function
    Sub CargarClientesMaster(ByVal cbC As ComboBox)
        Dim sqlTmp As String
        Dim rsTmp As ADODB.Recordset

        sqlTmp = ""
        Try
            rsTmp = New ADODB.Recordset
            sqlTmp = "SELECT  idClienteMaster, nbClienteMaster FROM ClientesMaster  ORDER BY nbClienteMaster"
            rsTmp = New ADODB.Recordset
            rsTmp.Open(sqlTmp, bdMySQL) ', ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cbC.Items.Clear()
            cbC.Items.Add(New ELista("Todos", 0, ""))
            Do While Not rsTmp.EOF
                cbC.Items.Add(New ELista(Trim(rsTmp.Fields("nbClienteMaster").Value), rsTmp.Fields("idClienteMaster").Value, ""))
                rsTmp.MoveNext()
            Loop
            rsTmp.Close()
            rsTmp = Nothing
        Catch ex As Exception
            MsgBox("Error accediendo a " + sqlTmp + ". Err: " + ex.ToString, MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End Try

    End Sub
    Sub CargarClientes(ByVal cbC As ComboBox, ByVal idClienteMaster As Integer)
        Dim sqlTmp As String
        Dim rsTmp As ADODB.Recordset

        sqlTmp = ""
        Try
            rsTmp = New ADODB.Recordset
            If idClienteMaster = 0 Then
                sqlTmp = "SELECT  idCliente, nbCliente, Envio_http_url FROM Clientes ORDER BY nbCliente"

            Else
                sqlTmp = "SELECT  idCliente, nbCliente, Envio_http_url FROM Clientes WHERE idClienteMaster = " + Trim(idClienteMaster) + " ORDER BY nbCliente"
            End If
            '            sqlTmp = "SELECT  idCliente, nbCliente, Envio_http_url, Envio_http_url, Envio_http_url FROM Clientes WHERE idClienteMaster = " + Trim(idClienteMaster) + " ORDER BY nbCliente"
            rsTmp = New ADODB.Recordset
            rsTmp.Open(sqlTmp, bdMySQL) ', ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cbC.Items.Clear()
            Do While Not rsTmp.EOF
                If Not IsDBNull(rsTmp.Fields("Envio_http_url").Value) Then
                    If Trim(rsTmp.Fields("Envio_http_url").Value) <> "" Then
                        cbC.Items.Add(New ELista(Trim(rsTmp.Fields("nbCliente").Value), rsTmp.Fields("idCliente").Value, ""))
                    End If
                End If
                rsTmp.MoveNext()
            Loop
            rsTmp.Close()
            rsTmp = Nothing
        Catch ex As Exception
            MsgBox("Error accediendo a " + sqlTmp + ". Err: " + ex.ToString, MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End Try

    End Sub
    Function GetDatosCliente(ByVal idCliente As Integer, ByRef sUser As String, ByRef sPass As String) As String
        Dim sqlTmp As String
        Dim rsTmp As ADODB.Recordset

        GetDatosCliente = ""
        sUser = ""
        sPass = ""
        rsTmp = New ADODB.Recordset
        sqlTmp = "SELECT Envio_http_url, Envio_http_usuario, Envio_http_password FROM Clientes WHERE idCliente = " + Trim(idCliente)
        rsTmp = New ADODB.Recordset
        rsTmp.Open(sqlTmp, bdMySQL) ', ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        If Not rsTmp.EOF Then
            GetDatosCliente = Trim(rsTmp.Fields("Envio_http_url").Value)
            If Not IsDBNull(rsTmp.Fields("Envio_http_usuario").Value) Then
                sUser = Trim(rsTmp.Fields("Envio_http_usuario").Value)
            End If
            If Not IsDBNull(rsTmp.Fields("Envio_http_password").Value) Then
                sPass = Trim(rsTmp.Fields("Envio_http_password").Value)
            End If
        End If
        rsTmp.Close()
        rsTmp = Nothing

    End Function
    Sub TrazarEnvio(ByVal sContenido As String)

        '************************************************************************************************
        ' Para trazar lo que se va a enviar
        '**************************************************************************************************
        ' Variables de stream
        Dim streamWriter As StreamWriter

        streamWriter = New StreamWriter("c:\Infofutbol\log\" + Replace(StrDatosValde.defaultFilename, ".", "_") + Mid(StrDatosValde.TimeStamp, 1, 15) + ".txt", False, System.Text.Encoding.UTF8)
        streamWriter.WriteLine(sContenido)
        streamWriter.Flush()
        streamWriter.Close()
        streamWriter = Nothing                    ' PrintLine(6, "</Partido>")
        '************************************************************************************************
        '**************************************************************************************************
    End Sub
End Module
