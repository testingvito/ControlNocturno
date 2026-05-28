Imports System.Diagnostics

Public Class FormCreditos
    ' URL de donación real de Víctor integrada perfectamente
    Private urlDonacion As String = "https://www.paypal.com/paypalme/vcarreno1983"

    Public Sub New()
        ' Llamada requerida por el diseñador.
        InitializeComponent()

        ' Enlazamos dinámicamente el botón en el diseño para la donación
        ' Evita errores si el botón tiene nombres genéricos en el .Designer
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Button Then
                AddHandler ctrl.Click, AddressOf BotonDonar_Click
                Exit For
            End If
        Next
    End Sub

    Private Sub FormCreditos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Acerca de la Aplicación"
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub

    Private Sub BotonDonar_Click(sender As Object, e As EventArgs)
        Try
            Dim sInfo As New ProcessStartInfo(urlDonacion) With {
                .UseShellExecute = True
            }
            Process.Start(sInfo)
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir el enlace de PayPal: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class