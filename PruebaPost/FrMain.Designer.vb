<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrMain
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrMain))
        Me.Frm1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TxtFichero = New System.Windows.Forms.TextBox()
        Me.BtnSel = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbTipoEnvio = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbCliM = New System.Windows.Forms.ComboBox()
        Me.CbCliente = New System.Windows.Forms.ComboBox()
        Me.BtnLimpiar = New System.Windows.Forms.Button()
        Me.BtnPost = New System.Windows.Forms.Button()
        Me.TxtRes = New System.Windows.Forms.TextBox()
        Me.TxtPass = New System.Windows.Forms.TextBox()
        Me.TxtUser = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtURL = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFile1 = New System.Windows.Forms.OpenFileDialog()
        Me.Frm1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frm1
        '
        Me.Frm1.Controls.Add(Me.Button1)
        Me.Frm1.Controls.Add(Me.TxtFichero)
        Me.Frm1.Controls.Add(Me.BtnSel)
        Me.Frm1.Location = New System.Drawing.Point(12, 3)
        Me.Frm1.Name = "Frm1"
        Me.Frm1.Size = New System.Drawing.Size(566, 110)
        Me.Frm1.TabIndex = 0
        Me.Frm1.TabStop = False
        Me.Frm1.Text = "Selecciona Fichero a Enviar"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(460, 46)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 19)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TxtFichero
        '
        Me.TxtFichero.Location = New System.Drawing.Point(42, 69)
        Me.TxtFichero.Name = "TxtFichero"
        Me.TxtFichero.Size = New System.Drawing.Size(380, 20)
        Me.TxtFichero.TabIndex = 1
        '
        'BtnSel
        '
        Me.BtnSel.Location = New System.Drawing.Point(42, 19)
        Me.BtnSel.Name = "BtnSel"
        Me.BtnSel.Size = New System.Drawing.Size(215, 32)
        Me.BtnSel.TabIndex = 0
        Me.BtnSel.Text = "Selecciona archivo a enviar"
        Me.BtnSel.UseVisualStyleBackColor = True
        '
        'Frame2
        '
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.cbTipoEnvio)
        Me.Frame2.Controls.Add(Me.Frame3)
        Me.Frame2.Controls.Add(Me.BtnLimpiar)
        Me.Frame2.Controls.Add(Me.BtnPost)
        Me.Frame2.Controls.Add(Me.TxtRes)
        Me.Frame2.Controls.Add(Me.TxtPass)
        Me.Frame2.Controls.Add(Me.TxtUser)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me.TxtURL)
        Me.Frame2.Location = New System.Drawing.Point(12, 119)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Size = New System.Drawing.Size(567, 471)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Parametros del Post"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 139)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(127, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Seleccionar tipo de envio"
        '
        'cbTipoEnvio
        '
        Me.cbTipoEnvio.FormattingEnabled = True
        Me.cbTipoEnvio.Location = New System.Drawing.Point(25, 155)
        Me.cbTipoEnvio.Name = "cbTipoEnvio"
        Me.cbTipoEnvio.Size = New System.Drawing.Size(488, 21)
        Me.cbTipoEnvio.TabIndex = 11
        '
        'Frame3
        '
        Me.Frame3.Controls.Add(Me.Label6)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Controls.Add(Me.cbCliM)
        Me.Frame3.Controls.Add(Me.CbCliente)
        Me.Frame3.Location = New System.Drawing.Point(25, 19)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Size = New System.Drawing.Size(523, 117)
        Me.Frame3.TabIndex = 9
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Seleccionar datos de cliente (Http) de la base de datos"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Cliente (URL)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Cliente Master"
        '
        'cbCliM
        '
        Me.cbCliM.FormattingEnabled = True
        Me.cbCliM.Location = New System.Drawing.Point(109, 27)
        Me.cbCliM.Name = "cbCliM"
        Me.cbCliM.Size = New System.Drawing.Size(394, 21)
        Me.cbCliM.TabIndex = 1
        '
        'CbCliente
        '
        Me.CbCliente.FormattingEnabled = True
        Me.CbCliente.Location = New System.Drawing.Point(109, 76)
        Me.CbCliente.Name = "CbCliente"
        Me.CbCliente.Size = New System.Drawing.Size(394, 21)
        Me.CbCliente.TabIndex = 0
        '
        'BtnLimpiar
        '
        Me.BtnLimpiar.Location = New System.Drawing.Point(313, 312)
        Me.BtnLimpiar.Name = "BtnLimpiar"
        Me.BtnLimpiar.Size = New System.Drawing.Size(225, 24)
        Me.BtnLimpiar.TabIndex = 8
        Me.BtnLimpiar.Text = "Limpiar"
        Me.BtnLimpiar.UseVisualStyleBackColor = True
        '
        'BtnPost
        '
        Me.BtnPost.Location = New System.Drawing.Point(14, 312)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(180, 24)
        Me.BtnPost.TabIndex = 7
        Me.BtnPost.Text = "Post"
        Me.BtnPost.UseVisualStyleBackColor = True
        '
        'TxtRes
        '
        Me.TxtRes.Location = New System.Drawing.Point(6, 344)
        Me.TxtRes.Multiline = True
        Me.TxtRes.Name = "TxtRes"
        Me.TxtRes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtRes.Size = New System.Drawing.Size(555, 121)
        Me.TxtRes.TabIndex = 6
        '
        'TxtPass
        '
        Me.TxtPass.Location = New System.Drawing.Point(375, 272)
        Me.TxtPass.Name = "TxtPass"
        Me.TxtPass.Size = New System.Drawing.Size(163, 20)
        Me.TxtPass.TabIndex = 5
        '
        'TxtUser
        '
        Me.TxtUser.Location = New System.Drawing.Point(60, 269)
        Me.TxtUser.Name = "TxtUser"
        Me.TxtUser.Size = New System.Drawing.Size(180, 20)
        Me.TxtUser.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(316, 275)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 272)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Usuario"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 227)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "URL"
        '
        'TxtURL
        '
        Me.TxtURL.Location = New System.Drawing.Point(10, 243)
        Me.TxtURL.Name = "TxtURL"
        Me.TxtURL.Size = New System.Drawing.Size(531, 20)
        Me.TxtURL.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'OpenFile1
        '
        Me.OpenFile1.FileName = "OpenFileDialog1"
        '
        'FrMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 594)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frm1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrMain"
        Me.Text = "Prueba Post"
        Me.Frm1.ResumeLayout(False)
        Me.Frm1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Frm1 As System.Windows.Forms.GroupBox
    Friend WithEvents Frame2 As System.Windows.Forms.GroupBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OpenFile1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TxtFichero As System.Windows.Forms.TextBox
    Friend WithEvents BtnSel As System.Windows.Forms.Button
    Friend WithEvents TxtPass As System.Windows.Forms.TextBox
    Friend WithEvents TxtUser As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtURL As System.Windows.Forms.TextBox
    Friend WithEvents BtnLimpiar As System.Windows.Forms.Button
    Friend WithEvents BtnPost As System.Windows.Forms.Button
    Friend WithEvents TxtRes As System.Windows.Forms.TextBox
    Friend WithEvents Frame3 As System.Windows.Forms.GroupBox
    Friend WithEvents CbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbTipoEnvio As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbCliM As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
