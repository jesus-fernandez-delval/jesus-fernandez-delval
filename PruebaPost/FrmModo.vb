Public Class FrmModo

    Private Sub FrmModo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        iModoTrabajo = MODO_TRABAJO_EN_LOCAL
        Rb1.Checked = False
        Rb2.Checked = True
    End Sub

    Private Sub Btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn1.Click

        If Rb1.Checked = True Then
            iModoTrabajo = MODO_TRABAJO_EN_SERVIDOR
        End If

        If Rb2.Checked = True Then
            iModoTrabajo = MODO_TRABAJO_EN_LOCAL
        End If

        Me.Close()
    End Sub
End Class