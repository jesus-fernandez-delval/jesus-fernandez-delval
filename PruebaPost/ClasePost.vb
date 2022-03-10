Imports System
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Web


' Simple class that encapsulates the HTTP post sample.
Public Class HttpPostRequest
    ' Displays simple usage information.        
    Shared Sub usage()
        Console.WriteLine("Executable_file_name [-u URL] [-d data] [-s file] [-p proxy]")
        Console.WriteLine("Available options:")
        Console.WriteLine("     -u URL          URL to post data to")
        Console.WriteLine("     -d data         Data to post")
        Console.WriteLine("     -s file         File name to save response to")
        Console.WriteLine("     -p HTTP URL     Proxy to use for post operation")
        Console.WriteLine()
    End Sub

    ' This routine validates the data being posted to the web page. It parses
    ' the string for reserved characters '?', '=', and '&'. The individual
    ' validated parts are returned via a StringBuilder object.        
    ' <param name="postData">Data to validate</param>
    ' <returns>StringBuilder object representing the parsed elements</returns>
    Public Function ValidatePostData(ByVal postData As String) As StringBuilder
        Dim encodedPostData As StringBuilder = New StringBuilder()
        ' These characters should be more out there...
        Dim reservedChars() As Char = {"?", "=", "&", "+"}
        Dim pos As Integer
        Dim offset As Integer

        ' Validate the data to be posted
        offset = 0
        While (offset < postData.Length)
            pos = postData.IndexOfAny(reservedChars, offset)
            If (pos = -1) Then
                ' Append the remaining part of the string
                encodedPostData.Append(HttpUtility.UrlEncode( _
                    postData.Substring(offset, postData.Length - offset)))
                Exit While
            End If
            ' Found a special character so append up to the special character
            'Console.WriteLine("Found a special character so append up to the special character...")
            encodedPostData.Append(HttpUtility.UrlEncode( _
                postData.Substring(offset, pos - offset)))
            encodedPostData.Append(postData.Substring(pos, 1))
            offset = pos + 1
        End While
        ValidatePostData = encodedPostData
    End Function
    ' This routine validates the data being posted to the web page. It parses
    ' the string for reserved characters '?', '=', and '&'. The individual
    ' validated parts are returned via a StringBuilder object.        
    ' <param name="postData">Data to validate</param>
    ' <returns>StringBuilder object representing the parsed elements</returns>
    Public Function ValidatePostData_VOri(ByVal postData As String) As StringBuilder
        Dim encodedPostData As StringBuilder = New StringBuilder()
        ' These characters should be more out there...
        Dim reservedChars() As Char = {"?", "=", "&", "+"}
        Dim pos As Integer
        Dim offset As Integer

        ' Validate the data to be posted
        Console.WriteLine("Validating the data to be posted...")
        offset = 0
        While (offset < postData.Length)
            pos = postData.IndexOfAny(reservedChars, offset)
            If (pos = -1) Then
                ' Append the remaining part of the string
                Console.WriteLine("Appending the remaining part of the string...")
                encodedPostData.Append(HttpUtility.UrlEncode( _
                    postData.Substring(offset, postData.Length - offset)))
                Exit While
            End If
            ' Found a special character so append up to the special character
            Console.WriteLine("Found a special character so append up to the special character...")
            encodedPostData.Append(HttpUtility.UrlEncode( _
                postData.Substring(offset, pos - offset)))
            encodedPostData.Append(postData.Substring(pos, 1))
            offset = pos + 1
        End While
        ValidatePostData_VOri = encodedPostData
    End Function



    ' This method creates an HttpWebRequest object, sets the method to "POST",
    ' and builds the data to post. Once the HttpWebRequest object is created,
    ' the request stream is obtained and the post data is sent and the 
    ' request stream closed. The response is then retrieved.        
    ' <param name="postUrl">URL to post data to</param>
    ' <param name="postData">Data to post</param>
    ' <param name="proxyServer">Proxy server to use</param>
    ' <param name="saveFile">Filename to save response to</param>
    Public Sub HttpMethodPost_VOri( _
        ByVal postUrl As String, _
        ByVal postData As String, _
        ByVal proxyServer As IWebProxy, _
        ByVal saveFile As String)

        Dim httpRequest As HttpWebRequest = Nothing
        Dim httpResponse As HttpWebResponse = Nothing
        Dim httpPostStream As Stream = Nothing
        Dim httpResponseStream As BinaryReader = Nothing
        Dim localFile As FileStream = Nothing

        Try
            Dim encodedPostData As StringBuilder
            Dim postBytes() As Byte = Nothing

            ' Create HTTP web request
            'Console.WriteLine("Creating HTTP web request...")
            httpRequest = CType(WebRequest.Create(postUrl), HttpWebRequest)
            ''httpRequest.Credentials.
            ' Change method from the default "GET" to "POST"
            'Console.WriteLine("Changing method from the default GET to POST...")
            httpRequest.Method = "POST"
            ' Posted forms need to be encoded so change the content type   
            'Console.WriteLine("Changing the content type (encoding)...")
            httpRequest.ContentType = "application/x-www-form-urlencoded"
            ' Set the proxy
            'Console.WriteLine("Setting the proxy...")
            httpRequest.Proxy = proxyServer
            ' Validate and encode the data to POST
            'Console.WriteLine("Validating and encode the data to POST...")
            encodedPostData = ValidatePostData(postData)

            'Console.WriteLine("Encoded POST string: '{0}'", encodedPostData.ToString())

            ' Retrieve a byte array representation of the data
            'Console.WriteLine("Retrieving a byte array representation of the data...")
            postBytes = Encoding.UTF8.GetBytes(encodedPostData.ToString())


            ' Set the content length (the number of bytes in the POST request)
            'Console.WriteLine("Setting the content length - the number of bytes in the POST request...")
            httpRequest.ContentLength = postBytes.Length
            ' Retrieve the request stream so we can write the POST data
            'Console.WriteLine("Retrieving the request stream so we can write the POST data...")
            httpPostStream = httpRequest.GetRequestStream()
            ' Write the POST request
            'Console.WriteLine("Writing the POST request...")
            httpPostStream.Write(postBytes, 0, postBytes.Length)

            httpPostStream.Close()
            httpPostStream = Nothing

            ' Retrieve the response
            'Console.WriteLine("Retrieving the response...")
            httpResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
            ' Retrieve the response stream
            'Console.WriteLine("Retrieving the response stream...")
            httpResponseStream = New BinaryReader( _
                httpResponse.GetResponseStream(), _
                Encoding.UTF8 _
                )

            Dim readData() As Byte

            ' Open the file to save the response to
            'Console.WriteLine("Opening the file to save the response to...")
            localFile = File.Open( _
                saveFile, _
                System.IO.FileMode.Create, _
                System.IO.FileAccess.Write, _
                System.IO.FileShare.None _
                )
            'Console.WriteLine("Saving response to: {0}", localFile.Name)
            'Console.WriteLine("Receiving response...")

            ' Receive the response stream until the end
            'Console.WriteLine("Receiving the response stream until the end...")
            Dim count As Integer = 0
            Dim percent As Long

            While (True)
                readData = httpResponseStream.ReadBytes(4096)
                If (readData.Length = 0) Then
                    Exit While
                End If

                localFile.Write(readData, 0, readData.Length)

                ' Calculate the progress and display to the console
                count += readData.Length
                percent = (count * 100) / httpResponse.ContentLength

                ' Console.WriteLine(" ")
                Console.WriteLine("{0}% progress...", percent.ToString().PadLeft(2))
            End While
            'Console.WriteLine()
        Catch wex As WebException
            'Console.WriteLine("Exception occurred: {0}", wex.ToString())
            httpResponse = CType(wex.Response, HttpWebResponse)
        Finally
            ' Close any remaining resources
            'Console.WriteLine("Closing any remaining resources...")
            If (Not IsNothing(httpResponse)) Then
                httpResponse.Close()
            End If
            If (Not IsNothing(localFile)) Then
                localFile.Close()
            End If
        End Try
    End Sub
    ' This method creates an HttpWebRequest object, sets the method to "POST",
    ' and builds the data to post. Once the HttpWebRequest object is created,
    ' the request stream is obtained and the post data is sent and the 
    ' request stream closed. The response is then retrieved.        
    ' <param name="postUrl">URL to post data to</param>
    ' <param name="postData">Data to post</param>
    ' <param name="proxyServer">Proxy server to use</param>
    ' <param name="saveFile">Filename to save response to</param>

    Public Function HttpMethodPost( _
        ByVal postUrl As String, _
        ByVal postData As String, _
        ByVal proxyServer As IWebProxy, _
        ByVal user As String, _
        ByVal password As String, _
        ByVal my_var_header As String, _
        ByVal TipoEnvio As Short) As String

        Dim httpRequest As HttpWebRequest = Nothing
        Dim httpResponse As HttpWebResponse = Nothing
        Dim httpPostStream As Stream = Nothing
        Dim httpResponseStream As BinaryReader = Nothing
        Dim localFile As FileStream = Nothing
        Dim szResponse As String
        Dim encoding As System.Text.Encoding = System.Text.Encoding.Default
        Dim iret As Integer
        ' Dim szTmp As String
        Dim i As Short
        Dim sHeaders() As String

        'Dim sUri As System.Uri
        'Dim suriProxy As System.Uri

        'Dim iTempproxyServer As System.Net.IWebProxy

        szResponse = ""
        Try
            Dim postBytes() As Byte = Nothing

            System.Net.ServicePointManager.Expect100Continue = False 'before making your call. Para que ignore proxy
            System.Net.ServicePointManager.SecurityProtocol = 3072 ';SecurityProtocolType.Tls1.2,  'SecurityProtocolTypeExtensions.Tls12
            If IsNothing(proxyServer) Then
                System.Net.ServicePointManager.Expect100Continue = False 'before making your call. Para que ignore proxy
            End If
            ' httpRequest.Proxy = proxyServer

            'System.Net.ServicePointManager.Expect100Continue = False 'before making your call.
            'System.Net.ServicePointManager.SecurityProtocol = 3072 ';SecurityProtocolType.Tls1.2

            ' Create HTTP web request
            httpRequest = CType(WebRequest.Create(postUrl), HttpWebRequest)
            ' Change method from the default "GET" to "POST"
            httpRequest.Method = "POST"
            ' Posted forms need to be encoded so change the content type 


            httpRequest.ContentType = "text/xml"
            ' httpRequest.ContentType = ""

            '***************************************************************
            '******************************************************
            '' Set the proxy ' para que ignore el proxy
            '            System.Net.ServicePointManager.Expect100Continue = False 'before making your call.
            '            System.Net.ServicePointManager.SecurityProtocol = 3072 ';SecurityProtocolType.Tls1.2
            httpRequest.Proxy = proxyServer

            'Dim proxy As IWebProxy = httpRequest.Proxy
            'If IsNothing(proxy) = False Then
            '    Try
            'If proxy.IsBypassed(httpRequest.RequestUri) = True Then
            '    httpRequest.Proxy = Nothing
            'Else
            '    httpRequest.Proxy = proxy

            'End If
            ' proxy.GetProxy(httpRequest.RequestUri, True)
            '    Catch ex As Exception
            '        MsgBox(ex.ToString)
            '    End Try

            'Else
            '    i = 1
            'End If

            ''                        IWebProxy proxy = myWebRequest.Proxy;
            ''if (proxy != null) {
            ''    string proxyuri = proxy.GetProxy(myWebRequest.RequestUri).ToString;
            ''    myWebRequest.UseDefaultCredentials = true;
            ''    myWebRequest.Proxy = new WebProxy(proxyuri, false);
            ''    myWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            ''}

            'iTempproxyServer = New WebProxy
            ''sUri = New Uri(postUrl)
            ' '' ''sUriProxy = New Uri("")
            'suriProxy = iTempproxyServer.GetProxy(httpRequest.RequestUri)
            'iTempproxyServer = WebRequest.DefaultWebProxy
            'suriProxy = iTempproxyServer.GetProxy(httpRequest.RequestUri)
            ''iTempproxyServer.Credentials = New NetworkCredential(user, password)

            'Dim proxyObject As New WebProxy(suriProxy, True)

            'httpRequest.Proxy = proxyObject


            ' If iTempproxyServer.IsBypassed(suriProxy) = True Then
            ''    i = 1
            ''End If

            'iTempproxyServer = WebRequest.DefaultWebProxy
            ''httpRequest.Proxy = iTempproxyServer ' proxyServer

            'httpRequest.Proxy = iTempproxyServer

            '***************************************************************
            '***************************************************************
            '***************************************************************
            '***************************************************************
            'httpRequest.Headers.Add("miheader:Value")


            Dim myCred As New NetworkCredential(user, password)
            Dim myCache As New CredentialCache

            myCache.Add(New Uri(postUrl), "Basic", myCred)

            'httpRequest.Credentials = myCred
            httpRequest.Credentials = myCache
            'httpRequest.UseDefaultCredentials = False


            ' Con esto funciona  las de Http
            'httpRequest.Credentials = New NetworkCredential(user, Password)
            '***************************************************************************************
            '*********************************************************************
            '*********************************************************************
            ' Validate and encode the data to POST, No hace falta por que se supone que aqui los datos son XML correctos

            '''encodedPostData = ValidatePostData(postData)
            ''' 
            ' Added the header 


            'httpRequest.Headers.Add(my_var_header)

            ''httpRequest.Headers.Add("x-meta-default-filename:football_results.23.20130325.000004.xml")
            'httpRequest.Headers.Add("x-meta-feed-type:f24")
            'httpRequest.Headers.Add("x-meta-competition-id:999")
            'httpRequest.Headers.Add("x-meta-game-id:459778")

            '            mi_header = mi_header + ",x-meta-feed-type:f26"
            'httpRequest.Headers.Set(
            ' Retrieve a byte array representation of the data
            postBytes = encoding.Default.GetBytes(postData)
            'Dim szTmp As String
            'sztmp = ""
            'For i = 0 To 1000
            '    szTmp = szTmp + "-" + Trim(Hex(CInt(postBytes(i))))
            'Next
            'MsgBox(szTmp)



            ' Para el archivo XML ya no hay que pasarlo a UTF8
            '''postBytes = Encoding.UTF8.GetBytes(encodedPostData.ToString())
            ' Set the content length (the number of bytes in the POST request)


            '            httpRequest.ContentLength = postBytes.Length
            Select Case TipoEnvio
                Case 2
                    ' **** Para para envios a Valde ***************************
                    httpRequest.ProtocolVersion = HttpVersion.Version11
                    httpRequest.Host = "dove.nexus.opta.net" '"$host"
                    httpRequest.KeepAlive = False
                    httpRequest.ContentType = "application/x-www-form-urlencoded"
                Case 4 ' envios a PrisaCom
                    ' httpRequest.Headers.Add("Content-Length", Trim(postBytes.Length))
                    httpRequest.ProtocolVersion = HttpVersion.Version11
                    httpRequest.Host = "data.entrypoint.as.com" '"$host"
                    httpRequest.ContentLength = Trim(postBytes.Length)
                    httpRequest.KeepAlive = False
                    httpRequest.ContentType = "multipart/form-data; boundary=" + boundary
                    ' httpRequest.ContentType = "text/xml; boundary=" + boundary
                    'httpRequest.Headers.Add("pepe", Trim(postBytes.Length))
                    '                            ## compose HTTP request header ##
                    '$header = "Host: $host\r\n";
                    '      $header .= "Content-Type: multipart/form-data; boundary=$boundary\r\n"; 		
                    '$header .= "Content-Length: ".strlen($sParamOP)."\r\n";
                    '$header .= "Connection: close\r\n\r\n";

                Case 5
                    sHeaders = Split(my_var_header, "#")
                    For i = 0 To UBound(sHeaders)
                        httpRequest.Headers.Add(sHeaders(i))
                    Next

                    '                    httpRequest.Host = "https://www.la-croix.com/resultats-sportifs/tour-de-france/opta/push"
                    'httpRequest.Host = "www.la-croix.com"
                    'httpRequest.UserAgent = "[statsperform]"

                    'req.UserAgent = "[any words that is more than 5 characters]";
                Case Else
                    sHeaders = Split(my_var_header, "#")
                    For i = 0 To UBound(sHeaders)
                        httpRequest.Headers.Add(sHeaders(i))
                    Next
            End Select

            ' Retrieve the request stream so we can write the POST data
            httpPostStream = httpRequest.GetRequestStream()

            ' Write the POST request
            httpPostStream.Write(postBytes, 0, postBytes.Length)

            httpPostStream.Close()
            httpPostStream = Nothing

            ' Retrieve the response
            '''Console.WriteLine("Retrieving the response...")

            httpResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
            ' Retrieve the response stream
            '''Console.WriteLine("Retrieving the response stream...")
            httpResponseStream = New BinaryReader( _
                httpResponse.GetResponseStream(), _
                encoding.UTF8 _
                )

            Dim readData() As Byte

            ' Open the file to save the response to
            '''Console.WriteLine("Opening the file to save the response to...")
            ' '' ''localFile = File.Open( _
            ' '' ''    saveFile, _
            ' '' ''    System.IO.FileMode.Create, _
            ' '' ''    System.IO.FileAccess.Write, _
            ' '' ''    System.IO.FileShare.None _
            ' '' ''    )
            '''Console.WriteLine("Saving response to: {0}", localFile.Name)
            '''Console.WriteLine("Receiving response...")

            ' Receive the response stream until the end
            '''Console.WriteLine("Receiving the response stream until the end...")
            Dim count As Integer = 0
            'Dim percent As Long
            szResponse = ""
            While (True)
                readData = httpResponseStream.ReadBytes(4096)
                If (readData.Length = 0) Then
                    Exit While
                End If
                szResponse = szResponse + encoding.GetString(readData)
                '''''localFile.Write(readData, 0, readData.Length)

                ' Calculate the progress and display to the console
                count += readData.Length
                'percent = (count * 100) / httpResponse.ContentLength
                'Console.WriteLine("{0}% progress...", percent.ToString().PadLeft(2))
            End While
            '''Console.WriteLine()
        Catch wex As WebException
            MsgBox(wex.ToString, MsgBoxStyle.Critical, "Atención")
            Call WriteLog(wex.ToString)
            'Console.WriteLine("Exception occurred: {0}", wex.ToString())
            httpResponse = CType(wex.Response, HttpWebResponse)
        Finally
            ' Close any remaining resources
            '''Console.WriteLine("Closing any remaining resources...")
            ''' 
            iret = httpResponse.StatusCode
            szResponse = szResponse + "  -  httpResponse.StatusDescription: " + httpResponse.StatusDescription
            If (Not IsNothing(httpResponse)) Then
                httpResponse.Close()
            End If
            If (Not IsNothing(localFile)) Then
                localFile.Close()
            End If
        End Try
        HttpMethodPost = szResponse


    End Function

    'Function GetFileInString(ByVal szfile As String) As String
    '    Dim utf8 As Encoding = Encoding.UTF8
    '    Dim szTmp As String
    '    'Dim i As Integer

    '    szTmp = My.Computer.FileSystem.ReadAllText(szfile, utf8)
    '    'Dim value As Byte() = My.Computer.FileSystem.ReadAllBytes(szfile)
    '    'szTmp = ""
    '    'For i = 0 To UBound(value) - 1
    '    '    szTmp = szTmp + value(i)
    '    '    'szTmp = CStr(My.Computer.FileSystem.ReadAllBytes(szfile))

    '    'Next
    '    'PrintLine(1, Trim(Hora) + ": " + Texto)
    '    'FileClose(1)

    '    GetFileInString = szTmp


    'End Function

    ' This is the main routine that parses the command line and calls routines to
    ' issue the POST request and receive the response.
    ' <param name="args">Command line arguments</param>
    Shared Sub Main()
        Dim proxyServer As IWebProxy
        Dim uriToPost As String = "http://search.msdn.microsoft.com/"
        Dim proxyName As String = Nothing
        Dim postData As String = "Default.aspx?Query=web.dll"
        Dim fileName As String = Nothing
        ' Parse the command line
        Dim args As String() = Environment.GetCommandLineArgs()
        Dim i As Integer

        usage()

        For i = 1 To args.GetUpperBound(0)
            Try
                Dim CurArg() As Char = args(i).ToCharArray(0, args(i).Length)
                If (CurArg(0) = "-") Or (CurArg(0) = "/") Then
                    Select Case Char.ToLower(CurArg(1), System.Globalization.CultureInfo.CurrentCulture)
                        Case "u"
                            ' URI to post to
                            i = i + 1
                            uriToPost = args(i)
                        Case "p"
                            ' Name of proxy server to use
                            i = i + 1
                            proxyName = args(i)
                        Case "d"
                            ' Retrieve all referenced images and text on the same host
                            i = i + 1
                            postData = args(i)
                        Case "s"
                            ' Local save path to append to retrieved resources
                            i = i + 1
                            fileName = args(i)
                        Case Else
                            usage()
                            Exit Sub
                    End Select
                End If
            Catch e As Exception
                usage()
                Exit Sub
            End Try
        Next
        Try
            Dim httpPost As HttpPostRequest = New HttpPostRequest()

            ' Set the proxy if supplied or use the default IE static proxy
            Console.WriteLine("Setting the proxy if supplied or use the default IE static proxy...")
            If (IsNothing(proxyName)) Then
                proxyServer = WebRequest.DefaultWebProxy
            Else
                ' Must cast it to IWebProxy if needed, not done here
                proxyServer = New WebProxy(proxyName)
            End If
            ' Post the request and write the response to the file
            Console.WriteLine("Posting the request and write the response to the file...")
            '            httpPost.HttpMethodPost(uriToPost, postData, proxyServer)
        Catch ex As Exception
            Console.WriteLine("Exception occurred: {0}", ex.Message)
        End Try
    End Sub


End Class
