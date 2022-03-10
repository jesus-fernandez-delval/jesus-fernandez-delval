Module Modulos_Connection_String
    Function Sacar_String_Conexion(ByVal sUnidadDisco As String, ByVal sDirectorioBD As String, ByVal sNombreBD As String, ByVal iTipoConexion As Short) As String
        Dim sClaveAcceso As String = ""
        Dim sConexion As String
        Dim sServidor As String

        sConexion = ""

        Select Case iTipoConexion
            Case BBDD_ACCESS
                sConexion = "Provider = Microsoft.Jet.OLEDB.4.0; " & _
                                    "Persist Security Info= False; " & _
                                    "Data Source = " + sUnidadDisco + sDirectorioBD + sNombreBD + ".mdb" + "; " & _
                                    "Jet OLEDB:Database Password = " + sClaveAcceso + ";"
            Case BBDD_ACCESS
                sConexion = "Provider = Microsoft.Jet.OLEDB.3.5; " & _
                            "Persist Security Info= False; " & _
                            "Data Source = " + sUnidadDisco + sDirectorioBD + sNombreBD + ".mdb" + "; " & _
                            "Jet OLEDB:Database Password = " + sClaveAcceso + ";"

            Case BBDD_SQLSERVER
                sServidor = ""
                Select Case iModoTrabajo
                    Case MODO_TRABAJO_EN_LOCAL
                        sServidor = "(local)\SQLEXPRESS; "

                    Case MODO_TRABAJO_EN_SERVIDOR
                        sServidor = "GSSERVER2; "
                End Select

                sConexion = "Provider=SQLOLEDB; " & _
                            "Data Source=" + sServidor & _
                            "Initial Catalog=" + sNombreBD + "; " & _
                            "integrated security=SSPI; persist security info=True;"

            Case BBDD_MYSQL
                sConexion = Sacar_String_Conexion_XML()
        End Select

        Sacar_String_Conexion = sConexion

    End Function
    Function Sacar_String_Conexion_XML() As String
        Sacar_String_Conexion_XML = ""

        Select Case iModoTrabajo
            Case MODO_TRABAJO_EN_LOCAL
                Sacar_String_Conexion_XML = Leer_String_Conexion_XML()
                Exit Function

            Case MODO_TRABAJO_EN_SERVIDOR
                '-------------------------------------------------------------------
                ' SI EL FICHERO DE CONFIGURACIÓN EXISTE EN EL SERVIDOR LO COPIA A C:
                '-------------------------------------------------------------------
                If My.Computer.FileSystem.FileExists(Unidad_Servidor + DIR_CONFIG + FICHERO_CONFIG) = True Then
                    Try
                        FileCopy(Unidad_Servidor + DIR_CONFIG + FICHERO_CONFIG, "C:" + DIR_CONFIG + FICHERO_CONFIG)
                    Catch ex As Exception
                    End Try
                End If
                If My.Computer.FileSystem.FileExists("C:" + DIR_CONFIG + FICHERO_CONFIG) = True Then
                    Try
                        Sacar_String_Conexion_XML = Leer_String_Conexion_XML()
                        Exit Function
                    Catch ex As Exception
                        MsgBox(" ERROR!!!! " + ex.Message.ToString())
                    End Try
                End If
        End Select



        If Dir("C:" + DIR_CONFIG + "CFG_MYSQL_CONNECTION.Xml") = "" Then
            MsgBox("NO EXISTE EL FICHERO DE CONFIGURACIÓN" + vbCr + vbCr + "C:" + DIR_CONFIG + "CFG_MYSQL_CONNECTION.Xml", MsgBoxStyle.Critical, "")
            End
            Exit Function
        End If


    End Function
    Function Leer_String_Conexion_XML() As String
        Dim xmlFIC As XmlDocument
        Dim xmlBBDD As XmlNodeList

        Leer_String_Conexion_XML = ""

        xmlFIC = New XmlDocument
        Try
            xmlFIC.Load("C:" + DIR_CONFIG + FICHERO_CONFIG)
        Catch ex As Exception
            MsgBox("ERROR AL CARGAR EL FICHERO DE CONFIGURACIÓN" + vbCr + vbCr + "C:" + DIR_CONFIG + FICHERO_CONFIG, MsgBoxStyle.Critical, "")
            Exit Function
        End Try



        '------------------------------------------------------------
        ' BASE DE DATOS MYSQL
        '------------------------------------------------------------
        xmlBBDD = Nothing
        Select Case iModoTrabajo
            Case MODO_TRABAJO_EN_LOCAL
                xmlBBDD = xmlFIC.SelectNodes("Configuracion/ParametrosBBDD_Local/DataBase")

            Case MODO_TRABAJO_EN_SERVIDOR
                xmlBBDD = xmlFIC.SelectNodes("Configuracion/ParametrosBBDD_Servidor/DataBase")
        End Select




        For i As Short = 0 To xmlBBDD.Count - 1
            If CShort(xmlBBDD.Item(i).SelectSingleNode("@TIPO").Value) = BBDD_MYSQL Then

                If xmlBBDD.Item(i).SelectSingleNode("@ACTIVA").Value = "1" Then
                    Leer_String_Conexion_XML = xmlBBDD.Item(i).SelectSingleNode("@STRCONEXION ").Value
                    xmlBBDD = Nothing
                    xmlFIC = Nothing
                    Exit Function
                End If
            End If
        Next

        xmlBBDD = Nothing
        xmlFIC = Nothing

        MsgBox("NO EXISTE NINGUNA CONEXIÓN MySQL ACTIVA EN EL FICHERO DE CONFIGURACIÓN" + vbCr + vbCr + "C:" + DIR_CONFIG + FICHERO_CONFIG, MsgBoxStyle.Critical, "")

        End
    End Function

End Module
