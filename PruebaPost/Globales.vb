Module Globales

    Public iModoTrabajo As Short
    Public Nivel_Usuario As Integer

    Public Const MODO_TRABAJO_EN_LOCAL As Short = 1
    Public Const MODO_TRABAJO_EN_SERVIDOR As Short = 2
    Public Unidad_Servidor As String

    Public AdoBDAccess As ADOconnection
    Public bdDeporte As ADODB.Connection
    Public Const DIRECTORIO_CONFIGURACIONES_XML As String = "\Infofutbol\Config\"
    Public Const FICHERO_XML_CLAVES As String = "CFG_CLV.Xml"
    ' Public Const INFOFUTBOL_CENTROS As String = "\infofutbol\BasesDatos\infofutbolcentros.mdb"
    Public DiscoBase As String
    Public bdCentros As ADODB.Connection

    Public Const boundary As String = "AaB03x"
    Public Const boundary2 As String = "BbC04y"

    'Tipos de conexiones a bases de datos
    Public Const BBDD_ACCESS As Integer = 1
    Public Const BBDD_ACCESS97 As Integer = 2
    Public Const BBDD_SQLSERVER As Integer = 3
    Public Const BBDD_MYSQL As Integer = 4


    Public bdMySQL As ADODB.Connection
    Public bdMySQL_local As ADODB.Connection
    Public Const DIR_CONFIG As String = "\Infofutbol\Config\"
    Public Const FICHERO_CONFIG As String = "CFG_MYSQL_CONNECTION.Xml"
    Public Const FICHERO_CONFIG_OLIMPICOS As String = "IDM_OLIMPICOS_CFG.Xml"
    Public bdADO As ADOconnection
End Module
