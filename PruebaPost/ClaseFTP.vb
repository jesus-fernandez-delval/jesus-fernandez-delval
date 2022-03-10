Imports System
Imports System.Net
Imports System.IO

Public Class ClaseFTP


    Public Function subirFichero(ByVal ficheroOrigen As String, ByVal ftpAdd As String, ByVal sUser As String, ByVal sPassword As String, ByVal ficheroDestino As String, ByVal bPassiveMode As Boolean, ByRef sMensaje As String) As Integer
        ' Ftp address debe incluir directorio si lo tiene
        'retorno 0 = OK, 1 = Error
        Dim infoFichero As New FileInfo(ficheroOrigen)
        Dim uri As String
        ' Dim iProxy As IWebProxy
        ' Dim szTmp As String
        Dim ftpResponse As FtpWebResponse

        subirFichero = 0
        sMensaje = ""
        uri = "ftp://" + ftpAdd
        uri = uri + "/" + ficheroDestino

        Dim peticionFTP As FtpWebRequest = Nothing

        Try
            ' Creamos una peticion FTP con la dirección del fichero que vamos a subir
            peticionFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)

        Catch ex As Exception
            peticionFTP.Credentials = Nothing
            sMensaje = ex.Message + ". " + ex.ToString
            subirFichero = 1
            Exit Function
        End Try
        Try
            peticionFTP.Credentials = New NetworkCredential(sUser, sPassword)
        Catch ex As Exception
            sMensaje = ex.Message + ". " + ex.ToString
            subirFichero = 1
            Exit Function
        End Try


        'peticionFTP.Method = WebRequestMethods.Ftp.GetFileSize

        'ftpResponse = peticionFTP.GetResponse
        'sMensaje = ftpResponse.ContentLength.ToString



        '   ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true
        '  ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf ValidateServerCertificate)


        'peticionFTP.KeepAlive = False
        peticionFTP.KeepAlive = True

        '********************************************************
        '********************************************************
        '********** Para usar FTPS   ****************************
        ' peticionFTP.EnableSsl = True
        '********************************************************
        '********************************************************
        '********************************************************

        If bPassiveMode = True Then
            peticionFTP.UsePassive = True
        Else
            peticionFTP.UsePassive = False
        End If

        ' Seleccionamos el comando que vamos a utilizar: Subir un fichero
        peticionFTP.Method = WebRequestMethods.Ftp.UploadFile




        ' Especificamos el tipo de transferencia de datos
        peticionFTP.UseBinary = True

        ' Informamos al servidor sobre el tamaño del fichero que vamos a subir
        peticionFTP.ContentLength = infoFichero.Length
        peticionFTP.Timeout = 120000 ' 2 minutos


        'iProxy = peticionFTP.Proxy
        'szTmp = iProxy.ToString


        ' Fijamos un buffer de 2KB
        Dim longitudBuffer As Integer
        longitudBuffer = 2048
        Dim lector As Byte() = New Byte(2048) {}

        Dim num As Integer



        ' Abrimos el fichero para subirlo
        Dim fs As FileStream
        fs = infoFichero.OpenRead()



        Try
            Dim escritor As Stream
            escritor = peticionFTP.GetRequestStream()

            ' Leemos 2 KB del fichero en cada iteración
            num = fs.Read(lector, 0, longitudBuffer)

            While (num <> 0)
                ' Escribimos el contenido del flujo de lectura en el
                ' flujo de escritura del comando FTP
                escritor.Write(lector, 0, num)
                num = fs.Read(lector, 0, longitudBuffer)
            End While

            escritor.Close()
            fs.Close()
            ' Si todo ha ido bien, se devolverá String.Empty
            subirFichero = 0
            sMensaje = "OK"

            peticionFTP = Nothing
            Try
                ' Creamos una peticion FTP con la dirección del fichero que vamos a subir
                peticionFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)

            Catch ex As Exception
                peticionFTP.Credentials = Nothing
                sMensaje = ex.Message + ". " + ex.ToString
                subirFichero = 1
                Exit Function
            End Try
            Try
                peticionFTP.Credentials = New NetworkCredential(sUser, sPassword)
            Catch ex As Exception
                sMensaje = ex.Message + ". " + ex.ToString
                subirFichero = 1
                Exit Function
            End Try


            peticionFTP.Method = WebRequestMethods.Ftp.GetFileSize

            ftpResponse = peticionFTP.GetResponse
            sMensaje = ftpResponse.ContentLength.ToString

        Catch ex As Exception
            ' Si se produce algún fallo, se devolverá el mensaje del error
            sMensaje = ex.Message + ". " + ex.ToString
            subirFichero = 1

            MsgBox(ex.ToString, MsgBoxStyle.Critical, "Atención")
        End Try

    End Function



    '    public static void Main()
    '{
    ' try
    ' {

    '  FtpWebRequest ftpFileSizeRequest =
    '   (FtpWebRequest)WebRequest.Create("ftp://myuri");
    '  ftpFileSizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;
    '  FtpWebResponse ftpResponse = (FtpWebResponse)ftpFileSizeRequest.GetResponse();
    '  Console.WriteLine(ftpResponse.ContentLength.ToString());
    '  StreamReader streamReader = new StreamReader(ftpResponse.GetResponseStream());
    '  string fileSizeString = streamReader.ReadToEnd();  // This is ALWAYS an empty string
    '  Console.WriteLine(fileSizeString);
    '  streamReader.Close();

    ' }
    ' catch(Exception ex)
    ' {
    '  Console.WriteLine(ex);

    ' }


End Class
