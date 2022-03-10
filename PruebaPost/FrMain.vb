Imports System
Imports System.Text
Imports System.Xml
Imports System.Security.Cryptography
'Imports Microsoft.VisualBasic



Public Class FrMain
    Dim bActivate As Boolean
    Dim sParamOP As String

    Dim idClienteMaster As Integer

    Private Sub FrMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            bdMySQL.Close()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub FrMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        bActivate = False
        Me.CenterToScreen()
        DiscoBase = "c:"

        cbTipoEnvio.Items.Add(New ELista("Envio Http normal de Geca", 1, ""))
        cbTipoEnvio.Items.Add(New ELista("Envio a Valde (Ciclismo)", 2, ""))
        cbTipoEnvio.Items.Add(New ELista("Envio simulando Valde (F1, F3,F4,F9, F13, F15, F24, F26, F30, F40)", 3, ""))
        cbTipoEnvio.Items.Add(New ELista("Envio a PrisaCom", 4, ""))
        cbTipoEnvio.Items.Add(New ELista("Envio Ciclismo como Valde", 5, ""))

    End Sub

    Private Sub FrMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If bActivate = False Then
            bActivate = True

            idClienteMaster = -1
            iModoTrabajo = MODO_TRABAJO_EN_SERVIDOR
            ' iModoTrabajo = MODO_TRABAJO_EN_LOCAL
            Nivel_Usuario = Get_Nivel_Usuario()

            If Nivel_Usuario = 99 Then
                'Nivel_Usuario = 99
                FrmModo.ShowDialog()
            Else
                Nivel_Usuario = 1
            End If
           
            Call Abrir_BBDD_MySQL()
            Select Case iModoTrabajo
                Case MODO_TRABAJO_EN_LOCAL
                    Unidad_Servidor = "C:"

                Case MODO_TRABAJO_EN_SERVIDOR
                    Unidad_Servidor = Sacar_Unidad_Servidor()
                    If Trim(Unidad_Servidor) = "" Then
                        End
                    End If
            End Select

            CbCliente.Enabled = False
            Call CargarClientesMaster(Me.cbCliM)

            'Me.TxtURL.Text = "http://172.20.140.97/gecasport/post/t2.php"
            'Me.OpenFileDialog1.Title = "Selecciona la foto"
            'Me.OpenFileDialog1.InitialDirectory = ""
            'Me.OpenFileDialog1.FileName = ""
            'Me.OpenFileDialog1.Filter = "(*.*" + ")|"
            'OpenFileDialog1.ShowDialog()
            'If Me.OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            'End If
        End If
        'Me.OpenFile1.Title = "Selecciona la foto"
        'Me.OpenFile1.InitialDirectory = ""
        'Me.OpenFile1.FileName = ""
        'Me.OpenFile1.Filter = "(*.*" + ")|"
        'If Me.OpenFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    szTmp = Me.OpenFile1.FileName
        'End If
    End Sub

    Private Sub BtnSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSel.Click
        Dim szTmp As String

        Me.OpenFile1.Title = "Selecciona la foto"
        Me.OpenFile1.InitialDirectory = ""
        Me.OpenFile1.FileName = ""
        Me.OpenFile1.Filter = "(*.*" + ")|*.*"
        If Me.OpenFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            szTmp = Me.OpenFile1.FileName
            Me.TxtFichero.Text = szTmp
        Else
            Me.TxtFichero.Text = ""
        End If


    End Sub

    Private Sub BtnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        Dim HttpPost As New HttpPostRequest
        Dim iWebProxy As System.Net.IWebProxy
        Dim szFileString As String
        Dim iTime, iTime2 As Integer
        Dim szTmp As String
        Dim mi_header, mi_header2 As String
        Dim idCompeticion, idUnico As Integer
        Dim idSeason, idDeporte As Short
        Dim TipoPro, sFN, sFG As String
        Dim XmlFile As XmlDocument
        Dim XmlNodes As XmlNodeList
        Dim szT As String
        Dim i As Short
        Dim szFileName As String
        Dim sPartesNombre() As String

        'idSeason = 2012
        'idUnico = 199
        'idCompeticion = 687
        'idDeporte = 1
        'TipoPro = "CNC"
        'sFN = "CNC_00687.Xml"
        'sFG = "CNC_00687.Xml"

        If Trim(Me.TxtFichero.Text) = "" Then
            MsgBox("Hay que seleccionar un archivo", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If
        If Trim(Me.TxtURL.Text) = "" Then
            MsgBox("Hay que definiar una URL", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If
        If Trim(Me.cbTipoEnvio.SelectedIndex) < 0 Then
            MsgBox("Hay que elegir un tipo de envio", MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End If
        Me.TxtRes.Text = ""
        mi_header = ""
        iWebProxy = Nothing
        iTime = My.Computer.Clock.TickCount
        szFileString = GetFileInString(Me.TxtFichero.Text)
        'Call WriteFichero("PruebaCal.Xml", szFileString)
        Me.TxtRes.Text = ""
        Me.TxtRes.Refresh()
        Cursor = System.Windows.Forms.Cursors.WaitCursor
        Select Case cbTipoEnvio.SelectedItem.indice
            Case 1 ' envio http Geca Tradicional
                ' Leo  lo necesario del archivo Xml
                Try
                    XmlFile = New XmlDocument
                    XmlFile.Load(Me.TxtFichero.Text)
                Catch ex As Exception
                    MsgBox("Error, no se ha podido cargar el archivo: " + Me.TxtFichero.Text + " Err:" + ex.ToString, MsgBoxStyle.Critical, "Atención")
                    Exit Sub
                End Try
                XmlNodes = XmlFile.SelectNodes("*/DG")
                idCompeticion = CInt(XmlNodes.Item(0).Attributes("ID").Value)
                idDeporte = Trim(XmlNodes.Item(0).Attributes("DEP").Value)
                sFN = Trim(XmlNodes.Item(0).Attributes("FN").Value)
                sFG = Trim(XmlNodes.Item(0).Attributes("FG").Value)
                szT = Mid(sFN, 1, 3)
                If InStr(Me.TxtFichero.Text, szT + "_") <> 0 Then
                    TipoPro = szT
                Else
                    TipoPro = Mid(sFG, 1, 3)
                End If
                Try
                    idSeason = CInt(XmlNodes.Item(0).Attributes("TEMP").Value)
                Catch ex As Exception
                    idSeason = -1
                End Try
                Try
                    idUnico = CInt(XmlNodes.Item(0).Attributes("IDUNICO").Value)
                Catch ex As Exception
                    idUnico = -1
                End Try
                XmlFile = Nothing

                mi_header = "x-meta-feed-parameters: {"
                'mi_header = mi_header + """" + "callback_module" + """" + ":" + """" + "OptaSpain.Transmitter.MRG1" + """"
                'mi_header = mi_header + "{" + """" + "competition_id" + """" + ":" + """" + Trim(idCompeticion) + """"
                'mi_header = mi_header + "{" + """" + "season_id" + """" + ":" + """" + Trim(idSeason) + """"
                mi_header = mi_header + """" + "v_feed_type" + """" + ":" + """" + Trim(TipoPro) + """"
                mi_header = mi_header + "," + """" + "v_year_comp_id" + """" + ":" + """" + Trim(idCompeticion) + """"
                mi_header = mi_header + "," + """" + "v_sport_id" + """" + ":" + """" + Trim(idDeporte) + """"
                mi_header = mi_header + "," + """" + "v_comp_id" + """" + ":" + """" + Trim(idUnico) + """"
                mi_header = mi_header + "," + """" + "v_season_id" + """" + ":" + """" + Trim(idSeason) + """"
                mi_header = mi_header + "," + """" + "v_fg" + """" + ":" + """" + Trim(sFG) + """"
                mi_header = mi_header + "," + """" + "v_fn" + """" + ":" + """" + Trim(sFN) + """"
                mi_header = mi_header + "}"

            Case 2 ' envios a Valde Ciclismo
                ' Leo  lo necesario del archivo Xml
                Try
                    XmlFile = New XmlDocument
                    XmlFile.Load(Me.TxtFichero.Text)
                Catch ex As Exception
                    MsgBox("Error, no se ha podido cargar el archivo: " + Me.TxtFichero.Text + " Err:" + ex.ToString, MsgBoxStyle.Critical, "Atención")
                    Exit Sub
                End Try
                XmlNodes = XmlFile.SelectNodes("*/DG")
                idCompeticion = CInt(XmlNodes.Item(0).Attributes("ID").Value)
                idDeporte = Trim(XmlNodes.Item(0).Attributes("DEP").Value)
                sFN = Trim(XmlNodes.Item(0).Attributes("FN").Value)
                sFG = Trim(XmlNodes.Item(0).Attributes("FG").Value)
                szT = Mid(sFN, 1, 3)
                If InStr(Me.TxtFichero.Text, szT + "_") <> 0 Then
                    TipoPro = szT
                Else
                    TipoPro = Mid(sFG, 1, 3)
                End If
                Try
                    idSeason = CInt(XmlNodes.Item(0).Attributes("TEMP").Value)
                Catch ex As Exception
                    idSeason = -1
                End Try
                Try
                    idUnico = CInt(XmlNodes.Item(0).Attributes("IDUNICO").Value)
                Catch ex As Exception
                    idUnico = -1
                End Try
                XmlFile = Nothing

                Call EnviarAValde(Me.TxtFichero.Text)
                szFileString = sParamOP

            Case 3 ' envia como Valde
                i = InStrRev(Trim(Me.TxtFichero.Text), "\")
                If i <> 0 Then
                    szFileName = Mid(Trim(Me.TxtFichero.Text), i + 1)
                Else
                    szFileName = Trim(Me.TxtFichero.Text)
                End If
                mi_header = "x-meta-default-filename:" + szFileName
                mi_header = mi_header + "#" + "x-meta-sport-id:1"

                sPartesNombre = Split(szFileName, "-")
                If InStr(szFileName, "-results") <> 0 Then ' srml-8-2010-results.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:f1"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                End If
                If InStr(szFileName, "standings") <> 0 Then ' srml-21-2011-standings.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F3"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                End If
                If InStr(szFileName, "matchreport") <> 0 Then ' opta-456550-matchreport.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F4"
                    mi_header = mi_header + "#" + "x-meta-game-id:" + sPartesNombre(1)
                End If
                If InStr(szFileName, "matchresults") <> 0 Then ' srml-5-2010-f350713-matchresults.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F9"
                    'mi_header = mi_header + "#" + "x-meta-feed-type:f7"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                    mi_header = mi_header + "#" + "x-meta-game-id:" + Replace(sPartesNombre(3), "f", "")
                End If
                If InStr(szFileName, "commentary") <> 0 Then ' commentary-8-2009-301586-en.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F13"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                    mi_header = mi_header + "#" + "x-meta-game-id:" + sPartesNombre(3)
                End If
                If InStr(szFileName, "rankings") <> 0 Then ' srml-23-2012-rankings.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F15"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                End If
                If InStr(szFileName, "eventdetails") <> 0 Then 'f24-5-2009-313590-eventdetails.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F24"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                    mi_header = mi_header + "#" + "x-meta-game-id:" + sPartesNombre(3)
                End If
                If InStr(szFileName, "_results") <> 0 Then 'football_results.4.20100702.235959.xml
                    sPartesNombre = Split(szFileName, ".")
                    mi_header = mi_header + "#" + "x-meta-feed-type:F26"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                End If
                If InStr(szFileName, "seasonstats") <> 0 Then 'seasonstats-4-2009-357.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F30"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                    mi_header = mi_header + "#" + "x-meta-team-id:" + sPartesNombre(3)
                End If
                If InStr(szFileName, "squads") <> 0 Then 'srml-23-2012-squads.xml
                    mi_header = mi_header + "#" + "x-meta-feed-type:F40"
                    mi_header = mi_header + "#" + "x-meta-competition-id:" + sPartesNombre(1)
                    mi_header = mi_header + "#" + "x-meta-season-id:" + sPartesNombre(2)
                End If
            Case 4 ' envios a Prisa
                ' Leo  lo necesario del archivo Xml
                Try
                    XmlFile = New XmlDocument
                    XmlFile.Load(Me.TxtFichero.Text)
                Catch ex As Exception
                    MsgBox("Error, no se ha podido cargar el archivo: " + Me.TxtFichero.Text + " Err:" + ex.ToString, MsgBoxStyle.Critical, "Atención")
                    Exit Sub
                End Try
                XmlNodes = XmlFile.SelectNodes("*/DG")
                idCompeticion = CInt(XmlNodes.Item(0).Attributes("ID").Value)
                idDeporte = Trim(XmlNodes.Item(0).Attributes("DEP").Value)
                sFN = Trim(XmlNodes.Item(0).Attributes("FN").Value)
                sFG = Trim(XmlNodes.Item(0).Attributes("FG").Value)
                szT = Mid(sFN, 1, 3)
                If InStr(Me.TxtFichero.Text, szT + "_") <> 0 Then
                    TipoPro = szT
                Else
                    TipoPro = Mid(sFG, 1, 3)
                End If
                Try
                    idSeason = CInt(XmlNodes.Item(0).Attributes("TEMP").Value)
                Catch ex As Exception
                    idSeason = -1
                End Try
                Try
                    idUnico = CInt(XmlNodes.Item(0).Attributes("IDUNICO").Value)
                Catch ex As Exception
                    idUnico = -1
                End Try
                XmlFile = Nothing

                Call EnviarAPrisa(Me.TxtFichero.Text)
                szFileString = sParamOP


            Case 5 ' envia un fichero de ciclismo como Valde
                Call SacarDatosFicheroXml(Me.TxtFichero.Text)
                '                mi_header = mi_header + "#" + "x-meta-sport-id:18"
                mi_header = mi_header + "x-meta-sport-id:18"
                mi_header = mi_header + "#" + "x-meta-feed-type:" + Trim(StrDatosValde.siglasOpta)
                mi_header = mi_header + "#" + "x-meta-competition-id:" + Trim(StrDatosValde.idUnico)
                mi_header = mi_header + "#" + "x-meta-season-id:" + Trim(StrDatosValde.idSeason)
                mi_header = mi_header + "#" + "x-meta-matchday:" + Trim(StrDatosValde.Jornada)
                mi_header = mi_header + "#" + "x-meta-game-id:" + Trim(StrDatosValde.GameId)
                mi_header = mi_header + "#" + "x-meta-default-filename:" + StrDatosValde.defaultFilename
                mi_header = mi_header + "#" + "x-meta-feed-parameters:{"
                mi_header = mi_header + """" + "v_feed_type" + """" + ":" + """" + Trim(StrDatosValde.v_feed_type) + """"
                mi_header = mi_header + "," + """" + "v_year_comp_id" + """" + ":" + """" + Trim(StrDatosValde.idCompeticion) + """"
                mi_header = mi_header + "," + """" + "v_sport_id" + """" + ":" + """" + Trim(StrDatosValde.idDeporte) + """"
                mi_header = mi_header + "," + """" + "v_comp_id" + """" + ":" + """" + Trim(StrDatosValde.idUnico) + """"
                mi_header = mi_header + "," + """" + "v_season_id" + """" + ":" + """" + Trim(StrDatosValde.idSeason) + """"
                mi_header = mi_header + "," + """" + "v_fg" + """" + ":" + """" + Trim(StrDatosValde.v_fg) + """"
                mi_header = mi_header + "," + """" + "v_fn" + """" + ":" + """" + Trim(StrDatosValde.v_fn) + """"
                mi_header = mi_header + "}"

                'mi_header2 = mi_header + "," + """" + "v_year_comp_id" + """" + ":" + """" + Trim(StrDatosValde.idCompeticion) + """"
                'mi_header2 = mi_header + "," + """" + "v_sport_id" + """" + ":" + """" + Trim(StrDatosValde.idDeporte) + """"
                'mi_header2 = mi_header + "," + """" + "v_comp_id" + """" + ":" + """" + Trim(StrDatosValde.idUnico) + """"
                'mi_header2 = mi_header + "," + """" + "v_season_id" + """" + ":" + """" + Trim(StrDatosValde.idSeason) + """"
                'mi_header2 = mi_header + "," + """" + "v_fg" + """" + ":" + """" + Trim(StrDatosValde.v_fg) + """"
                'mi_header2 = mi_header + "," + """" + "v_fn" + """" + ":" + """" + Trim(StrDatosValde.v_fn) + """"
                'mi_header2 = mi_header + "}"
                '************************************************************************************************
                ' Para trazar lo que se va a enviar
                '**************************************************************************************************
                Call TrazarEnvio(mi_header + "###" + szFileString)



        End Select


        Me.TxtRes.Text = HttpPost.HttpMethodPost(Me.TxtURL.Text, szFileString, iWebProxy, Trim(Me.TxtUser.Text), Trim(Me.TxtPass.Text), mi_header, cbTipoEnvio.SelectedItem.indice)
        iTime2 = My.Computer.Clock.TickCount
        iTime = iTime2 - iTime
        Cursor = System.Windows.Forms.Cursors.Default
        szTmp = "Ha tardado: " + Trim(iTime \ 1000) + " sgs y " + Trim(iTime Mod 1000) + " milesimas"
        MsgBox("Post enviado. " + szTmp, MsgBoxStyle.Information, "Atención")

    End Sub

    Function GetFileInString(ByVal szfile As String) As String
        'Dim utf8 As Encoding = Encoding.UTF8
        Dim szTmp As String
        'Dim i As Integer
        'Dim j As Integer
        Dim encoding As System.Text.Encoding = System.Text.Encoding.Default

        'Dim szTmp2 As String

        'szTmp = My.Computer.FileSystem.ReadAllText(szfile, utf8)
        'szTmp = My.Computer.FileSystem.ReadAllText(szfile)
        ' Call WriteFichero("DespuesLeerFichero.Xml", szTmp)
        Dim value As Byte() = My.Computer.FileSystem.ReadAllBytes(szfile)
        'szTmp = ""
        'j = UBound(value)
        'szTmp = value(0).ToString
        'szTmp = value(1).ToString
        'szTmp = value(2).ToString
        'szTmp = value(3).ToString
        'szTmp = value(4).ToString
        'szTmp = ""
        'szTmp = value.
        szTmp = encoding.GetString(value)

        ''For i = 0 To j - 1
        ''    szTmp = szTmp + Chr(value(i))
        ''    'szTmp = CStr(My.Computer.FileSystem.ReadAllBytes(szfile))

        ''Next


        GetFileInString = szTmp


    End Function


    ' ''Public Sub WriteFichero(ByVal fichero As String, ByVal Texto As String)
    ' ''    Dim FicheroLog As String

    ' ''    FicheroLog = "c:\infofutbol\" + fichero
    ' ''    FileOpen(1, FicheroLog, OpenMode.Append)
    ' ''    PrintLine(1, Texto)
    ' ''    FileClose(1)

    ' '' End Sub


    ''Function ByteArrayToString(ByVal byteArray() As Byte, ByVal start As Integer, ByVal length As Integer)
    ''    Dim temp As New String(Chr(0), length + 1)

    ''    CopyMemory(temp, byteArray(start), length)

    ''    Return temp.ToString()
    ''End Function



    Private Sub BtnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLimpiar.Click

        Me.TxtRes.Text = ""

    End Sub

    Private Sub CbCliente_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbCliente.SelectedIndexChanged
        Dim sURL, sUsr, sPass As String

        sURL = ""
        sPass = ""
        sUsr = ""
        If Me.CbCliente.SelectedIndex > -1 Then
            sURL = GetDatosCliente(Me.CbCliente.SelectedItem.indice, sUsr, sPass)
            Me.TxtPass.Text = sPass
            Me.TxtURL.Text = sURL
            Me.TxtUser.Text = sUsr
        End If

    End Sub

    
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim iret As Integer
        Dim sMensaje As String

        Dim ftprequest = New ClaseFTP
        sMensaje = ""
        iret = ftprequest.subirFichero("y:\Infofutbol\Graficos\AsCS4\Futbol\32_CLASIFICACION_EQUIPOS_PRIMERA.txt", "ftp.grupoprisa.net", "Agencia24", "4g3nc1424", "32_CLASIFICACION_EQUIPOS_PRIMERA.txt", True, sMensaje)
        MsgBox(sMensaje, MsgBoxStyle.Information, "Atención")


    End Sub

    Sub EnviarAValde(ByVal szXmlFile As String)
        ' Dim defaultFilename As String
        Call SacarDatosFicheroXml(szXmlFile)

        'StrDatosValde.TimeStamp = String.Format("{0:0000}", My.Computer.Clock.GmtTime.Year) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Month) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Day)
        'StrDatosValde.TimeStamp += "T" + String.Format("{0:00}", My.Computer.Clock.GmtTime.Hour) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Minute) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Second)
        'StrDatosValde.TimeStamp += ",7200Z" '+ String.Format("{0:000}", My.Computer.) + "Z"
        StrDatosValde.TimeStamp = String.Format("{0:0000}", My.Computer.Clock.LocalTime.Year) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Month) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Day)
        StrDatosValde.TimeStamp += "T" + String.Format("{0:00}", My.Computer.Clock.LocalTime.Hour) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Minute) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Second)
        StrDatosValde.TimeStamp += "," + Trim(GetOffsetTime()) + "Z"  '",7200Z" '+ String.Format("{0:000}", My.Computer.) + "Z"

        sParamOP = "defaultFilename="
        sParamOP += StrDatosValde.defaultFilename
        'Call MD5EncryptPass(StrDatosValde.defaultFilename)
        sParamOP += "&messageDigest=" + MD5EncryptPass(StrDatosValde.defaultFilename)
        sParamOP += "&productionServer=" + "gsserver2"
        sParamOP += "&productionServerModule=" + "OptaSpain.Transmitter." + StrDatosValde.siglasOpta
        sParamOP += "&feedParameters="
        sParamOP += "{"
        sParamOP += """" + "callback_module" + """" + ":" + """" + "OptaSpain.Transmitter." + StrDatosValde.siglasOpta + """"
        sParamOP += "," + """" + "v_feed_type" + """" + ":" + """" + Trim(StrDatosValde.v_feed_type) + """"
        sParamOP += "," + """" + "v_year_comp_id" + """" + ":" + """" + Trim(StrDatosValde.idCompeticion) + """"
        sParamOP += "," + """" + "v_sport_id" + """" + ":" + """" + Trim(StrDatosValde.idDeporte) + """"  'Trim(StrDatosValde.opta_sport_id)
        sParamOP += "," + """" + "v_comp_id" + """" + ":" + """" + Trim(StrDatosValde.idUnico) + """"
        sParamOP += "," + """" + "v_season_id" + ":" + """" + Trim(StrDatosValde.idSeason) + """"
        sParamOP += "," + """" + "v_fg" + """" + ":" + """" + StrDatosValde.v_fg + """"
        sParamOP += "," + """" + "v_fn" + """" + ":" + """" + StrDatosValde.v_fn + """"
        sParamOP += "}"
        sParamOP += "&deliveryType=" + "latest"
        sParamOP += "&mimeType=" + "text/xml"
        sParamOP += "&encoding=" + "UTF8"
        sParamOP += "&feedType=" + StrDatosValde.siglasOpta 'varfeedType
        sParamOP += "&seasonId=" + Trim(StrDatosValde.idSeason) ' v_season_id
        sParamOP += "&competitionId=" + Trim(StrDatosValde.idUnico)
        sParamOP += "&sportId=" + Trim(StrDatosValde.opta_sport_id)
        sParamOP += "&productionServerTimeStamp=" + StrDatosValde.TimeStamp ' date("Ymd\THis\,Z\Z") "20190521T130634,7200Z" 
        sParamOP += StrDatosValde.sParamGS
        sParamOP += "&content=" + GetFileInStringUrlEncoded(szXmlFile)

        '************************************************************************************************
        ' Para trazar lo que se va a enviar
        '**************************************************************************************************
        Call TrazarEnvio(sParamOP)

    End Sub

    Private Function MD5EncryptPass(ByVal StrPass As String) As String
        Dim PasConMd5 As String
        Dim md5 As New MD5CryptoServiceProvider
        Dim bytValue() As Byte
        Dim bytHash() As Byte
        Dim i As Integer


        PasConMd5 = ""

        bytValue = System.Text.Encoding.UTF8.GetBytes(StrPass)

        bytHash = md5.ComputeHash(bytValue)
        md5.Clear()

        For i = 0 To bytHash.Length - 1
            PasConMd5 &= bytHash(i).ToString("x").PadLeft(2, "0")
        Next

        ' MsgBox(PasConMd5)
        MD5EncryptPass = PasConMd5

    End Function
    Sub SacarDatosFicheroXml(ByVal sFichero As String)
        Dim XmlFile As XmlDocument
        Dim XmlNodes As XmlNodeList
        Dim szT As String


        Try
            XmlFile = New XmlDocument
            XmlFile.Load(sFichero)
        Catch ex As Exception
            MsgBox("Error, no se ha podido cargar el archivo: " + sFichero + " Err:" + ex.ToString, MsgBoxStyle.Critical, "Atención")
            Exit Sub
        End Try
        StrDatosValde.Jornada = ""
        StrDatosValde.GameId = ""
        XmlNodes = XmlFile.SelectNodes("*/DG")
        StrDatosValde.idCompeticion = CInt(XmlNodes.Item(0).Attributes("ID").Value)
        StrDatosValde.idDeporte = Trim(XmlNodes.Item(0).Attributes("DEP").Value)
        StrDatosValde.v_fn = Trim(XmlNodes.Item(0).Attributes("FN").Value)
        StrDatosValde.v_fg = Trim(XmlNodes.Item(0).Attributes("FG").Value)
        szT = Mid(StrDatosValde.v_fn, 1, 3)
        If InStr(Me.TxtFichero.Text, szT + "_") <> 0 Then
            StrDatosValde.v_feed_type = szT
        Else
            StrDatosValde.v_feed_type = Mid(StrDatosValde.v_fg, 1, 3)
        End If
        Try
            StrDatosValde.idSeason = CInt(XmlNodes.Item(0).Attributes("TEMP").Value)
        Catch ex As Exception
            StrDatosValde.idSeason = -1
        End Try
        Try
            StrDatosValde.idUnico = CInt(XmlNodes.Item(0).Attributes("IDUNICO").Value)
        Catch ex As Exception
            StrDatosValde.idUnico = -1
        End Try
        If StrDatosValde.v_feed_type = "POB" Then
            StrDatosValde.Jornada = Trim(CInt(XmlNodes.Item(0).Attributes("JN").Value))
            StrDatosValde.GameId = Trim(StrDatosValde.idCompeticion) + Trim(StrDatosValde.Jornada)
        End If
        If StrDatosValde.v_feed_type = "CSF" Then
            StrDatosValde.Jornada = Trim(CInt(XmlNodes.Item(0).Attributes("JOR").Value))
            StrDatosValde.GameId = Trim(StrDatosValde.idCompeticion) + Trim(StrDatosValde.Jornada)
        End If
        'If StrDatosValde.v_feed_type = "DCO" Then
        '    StrDatosValde.Jornada = "300" 'Trim(CInt(XmlNodes.Item(0).Attributes("JOR").Value))
        '    StrDatosValde.GameId = Trim(StrDatosValde.idCompeticion) + Trim(StrDatosValde.Jornada)
        'End If

        XmlFile = Nothing
        StrDatosValde.defaultFilename = ""

        Select Case StrDatosValde.idDeporte
            Case 9, 109
                StrDatosValde.defaultFilename = StrDatosValde.v_fn
                Select Case StrDatosValde.v_feed_type
                    Case "CNC"
                        StrDatosValde.siglasOpta = "CY1"
                    Case "CSF"
                        StrDatosValde.siglasOpta = "CY3"
                        StrDatosValde.sParamGS = "&matchday=" + StrDatosValde.Jornada
                        StrDatosValde.sParamGS += "&gameId=" + StrDatosValde.GameId
                    Case "POB"
                        StrDatosValde.siglasOpta = "CY9"
                        StrDatosValde.sParamGS = "&matchday=" + StrDatosValde.Jornada
                        StrDatosValde.sParamGS += "&gameId=" + StrDatosValde.GameId
                    Case "CMP"
                        StrDatosValde.siglasOpta = "CY40"
                    Case "CGF"
                        StrDatosValde.siglasOpta = "CY4"
                        StrDatosValde.defaultFilename = StrDatosValde.v_fg
                    Case "DCO"
                        StrDatosValde.siglasOpta = "CY15"
                        StrDatosValde.defaultFilename = StrDatosValde.v_fg
                        StrDatosValde.sParamGS = "&matchday=" + StrDatosValde.Jornada
                        StrDatosValde.sParamGS += "&gameId=" + StrDatosValde.GameId
                    Case "CCO"
                        StrDatosValde.siglasOpta = "CY10"
                        StrDatosValde.defaultFilename = StrDatosValde.v_fg
                        StrDatosValde.sParamGS = "&matchday=" + StrDatosValde.Jornada
                        StrDatosValde.sParamGS += "&gameId=" + StrDatosValde.GameId
                    Case Else
                        StrDatosValde.siglasOpta = "ERROR"
                End Select
                StrDatosValde.opta_sport_id = 18
            Case Else
                StrDatosValde.siglasOpta = "F1"
                StrDatosValde.opta_sport_id = 0
        End Select



    End Sub
    Function GetFileInStringUrlEncoded(ByVal szfile As String) As String
        'Dim utf8 As Encoding = Encoding.UTF8
        Dim szTmp As String
        'Dim i As Integer
        'Dim j As Integer
        Dim encoding As System.Text.Encoding = System.Text.Encoding.Default

        'Dim szTmp2 As String

        'szTmp = My.Computer.FileSystem.ReadAllText(szfile, utf8)
        'szTmp = My.Computer.FileSystem.ReadAllText(szfile)
        ' Call WriteFichero("DespuesLeerFichero.Xml", szTmp)


        'Dim value As Byte() = My.Computer.FileSystem.ReadAllBytes(szfile)


        'szTmp = ""
        'j = UBound(value)
        'szTmp = ""
        'szTmp = value.
        'szTmp = encoding.GetString(value)

        szTmp = System.Web.HttpUtility.UrlEncode(My.Computer.FileSystem.ReadAllBytes(szfile))

        ''For i = 0 To j - 1
        ''    szTmp = szTmp + Chr(value(i))
        ''    'szTmp = CStr(My.Computer.FileSystem.ReadAllBytes(szfile))

        ''Next


        GetFileInStringUrlEncoded = szTmp


    End Function

    Function GetOffsetTime() As Integer
        Dim localZone As TimeZone = TimeZone.CurrentTimeZone
        Dim currentDate As DateTime = DateTime.Now
        Dim currentYear As Integer = currentDate.Year

        Dim currentOffset As TimeSpan = localZone.GetUtcOffset(currentDate)

        GetOffsetTime = currentOffset.Hours * 3600 + currentOffset.Minutes * 60 + currentOffset.Seconds
        'Console.WriteLine(currentOffset)


    End Function

    Sub EnviarAPrisa(ByVal szXmlFile As String)
        Dim tipo, userfile As String
        Dim Documentoxml As String
        ' Dim defaultFilename As String
        Call SacarDatosFicheroXml(szXmlFile)

        'StrDatosValde.TimeStamp = String.Format("{0:0000}", My.Computer.Clock.GmtTime.Year) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Month) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Day)
        'StrDatosValde.TimeStamp += "T" + String.Format("{0:00}", My.Computer.Clock.GmtTime.Hour) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Minute) + String.Format("{0:00}", My.Computer.Clock.GmtTime.Second)
        'StrDatosValde.TimeStamp += ",7200Z" '+ String.Format("{0:000}", My.Computer.) + "Z"
        StrDatosValde.TimeStamp = String.Format("{0:0000}", My.Computer.Clock.LocalTime.Year) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Month) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Day)
        StrDatosValde.TimeStamp += "T" + String.Format("{0:00}", My.Computer.Clock.LocalTime.Hour) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Minute) + String.Format("{0:00}", My.Computer.Clock.LocalTime.Second)
        StrDatosValde.TimeStamp += "," + Trim(GetOffsetTime()) + "Z"  '",7200Z" '+ String.Format("{0:000}", My.Computer.) + "Z"


        sParamOP = ""


        sParamOP += "--" + boundary + vbCr + vbLf
        sParamOP += "Content-Disposition: form-data; name=" + """" + "ID_COMP" + """" + vbCr + vbLf
        sParamOP += vbCr + vbLf + Trim(StrDatosValde.idUnico) + vbCr + vbLf
        sParamOP += "--" + boundary + vbCr + vbLf

        sParamOP += "--" + boundary + vbCr + vbLf
        sParamOP += "Content-Disposition: form-data; name=" + """" + "ID_TEMP" + """" + vbCr + vbLf
        sParamOP += vbCr + vbLf + Trim(StrDatosValde.idSeason) + vbCr + vbLf
        sParamOP += "--" + boundary + vbCr + vbLf

        sParamOP += "--" + boundary + vbCr + vbLf
        sParamOP += "Content-Disposition: form-data; name=" + """" + "ID_DEP" + """" + vbCr + vbLf
        sParamOP += vbCr + vbLf + Trim(StrDatosValde.idDeporte) + vbCr + vbLf
        sParamOP += "--" + boundary + vbCr + vbLf

        sParamOP += "--" + boundary + vbCr + vbLf
        sParamOP += "Content-Disposition: form-data; name=" + """" + "TIPO_FICH" + """" + vbCr + vbLf
        sParamOP += vbCr + vbLf + StrDatosValde.v_feed_type + vbCr + vbLf
        sParamOP += "--" + boundary + vbCr + vbLf

        sParamOP += "--" + boundary + vbCr + vbLf
        sParamOP += "Content-Disposition: form-data; name=" + """" + "ID_COMP_TEMP" + """" + vbCr + vbLf
        sParamOP += vbCr + vbLf + Trim(StrDatosValde.idCompeticion) + vbCr + vbLf
        sParamOP += "--" + boundary + vbCr + vbLf

        sParamOP += "--" + boundary + vbCr + vbLf

        tipo = "text/html; charset=\" + """" + "UTF-8\" + """"
        userfile = "fn"

        If StrDatosValde.v_feed_type = "CGF" Then ' chapuza para que los  de PrisaCom salven un CGF
            sParamOP += "Content-Disposition: form-data; name=" + """" + "files" + """" + "; filename=" + """" + StrDatosValde.v_fg + """" + vbCr + vbLf
        Else
            sParamOP += "Content-Disposition: form-data; name=" + """" + "files" + """" + "; filename=" + """" + StrDatosValde.v_fn + """" + vbCr + vbLf
            'sParamOP.="Content-Disposition: form-data; name=\"files\"; filename=\"".$v_fn."\"\r\n"; 
        End If
        '//sParamOP.="Content-Disposition: form-data; name=\"files\"; filename=\"".$v_fn."\"\r\n"; 
        sParamOP += "Content-Type: " + tipo + vbCr + vbLf + vbCr + vbLf

        Documentoxml = GetFileInString(szXmlFile)
        sParamOP += "" + documentoxml + vbCr + vbLf
        sParamOP += vbCr + vbLf + "--" + boundary + "--" + vbCr + vbLf


        '************************************************************************************************
        ' Para trazar lo que se va a enviar
        '**************************************************************************************************
        Call TrazarEnvio(sParamOP)


    End Sub



    Private Sub cbCliM_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCliM.SelectedIndexChanged

        If cbCliM.SelectedIndex > -1 Then
            CbCliente.Enabled = True
            idClienteMaster = cbCliM.SelectedItem.indice

            Call CargarClientes(CbCliente, cbCliM.SelectedItem.indice)

        End If


    End Sub
End Class

