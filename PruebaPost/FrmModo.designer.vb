<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmModo
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
        Me.Rb1 = New System.Windows.Forms.RadioButton
        Me.Rb2 = New System.Windows.Forms.RadioButton
        Me.Btn1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Rb1
        '
        Me.Rb1.AutoSize = True
        Me.Rb1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rb1.Location = New System.Drawing.Point(91, 51)
        Me.Rb1.Name = "Rb1"
        Me.Rb1.Size = New System.Drawing.Size(243, 29)
        Me.Rb1.TabIndex = 0
        Me.Rb1.TabStop = True
        Me.Rb1.Text = "Trabajar en servidor"
        Me.Rb1.UseVisualStyleBackColor = True
        '
        'Rb2
        '
        Me.Rb2.AutoSize = True
        Me.Rb2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rb2.Location = New System.Drawing.Point(91, 105)
        Me.Rb2.Name = "Rb2"
        Me.Rb2.Size = New System.Drawing.Size(208, 29)
        Me.Rb2.TabIndex = 1
        Me.Rb2.TabStop = True
        Me.Rb2.Text = "Trabajar en local"
        Me.Rb2.UseVisualStyleBackColor = True
        '
        'Btn1
        '
        Me.Btn1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn1.Location = New System.Drawing.Point(318, 185)
        Me.Btn1.Name = "Btn1"
        Me.Btn1.Size = New System.Drawing.Size(121, 28)
        Me.Btn1.TabIndex = 2
        Me.Btn1.Text = "Aceptar"
        Me.Btn1.UseVisualStyleBackColor = True
        '
        'FrmModo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 225)
        Me.ControlBox = False
        Me.Controls.Add(Me.Btn1)
        Me.Controls.Add(Me.Rb2)
        Me.Controls.Add(Me.Rb1)
        Me.Name = "FrmModo"
        Me.Text = "Seleccionar sitio de trabajo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Rb1 As System.Windows.Forms.RadioButton
    Friend WithEvents Rb2 As System.Windows.Forms.RadioButton
    Friend WithEvents Btn1 As System.Windows.Forms.Button
End Class
