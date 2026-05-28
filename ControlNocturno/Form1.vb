Imports Microsoft.Win32
Imports System.IO
Imports System.Threading

Public Class Form1
    Private tiempoInicio As TimeSpan = New TimeSpan(23, 59, 0)
    Private tiempoFin As TimeSpan = New TimeSpan(6, 0, 0)
    Private servicioActivo As Boolean = False
    Private iniciarConSistema As Boolean = False

    Private rutaConfig As String = Path.Combine(AppContext.BaseDirectory, "config.ini")
    Private apagadoIniciado As Boolean = False
    Private Shared m_Mutex As Mutex = Nothing

    Public Sub New()
        Dim esUnicaInstancia As Boolean = False
        m_Mutex = New Mutex(True, "Global\ControlNocturnoApp_Victor_UniqueKey", esUnicaInstancia)

        If Not esUnicaInstancia Then
            MessageBox.Show("La aplicación ya se encuentra ejecutándose en segundo plano junto al reloj del sistema.",
                            "Servicio de Sistema",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            m_Mutex.Close()
            m_Mutex = Nothing
            Application.Exit()
            Environment.Exit(0)
            Exit Sub
        End If

        InitializeComponent()
        CargarConfiguracion()

        ' Enlace de eventos manuales de tus controles
        AddHandler btnAplicar.Click, AddressOf btnAplicar_Click
        AddHandler btnCreditos.Click, AddressOf btnCreditos_Click
        AddHandler notifyIcon1.MouseDoubleClick, AddressOf notifyIcon1_MouseDoubleClick
        AddHandler timer2.Tick, AddressOf timer2_Tick

        Try
            notifyIcon1.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Environment.ProcessPath)
        Catch ex As Exception
            notifyIcon1.Icon = Me.Icon
        End Try

        notifyIcon1.Text = "Control de Horario Activo"
        notifyIcon1.Visible = True

        timer2.Interval = 5000
        timer2.Start()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        chkActivo.Checked = servicioActivo
        chkInicioWindows.Checked = iniciarConSistema
        Me.Text = "Servicio de Sistema"

        Me.Size = New System.Drawing.Size(0, 0)
        Me.ShowInTaskbar = False
        Me.WindowState = FormWindowState.Minimized
        Me.Hide()
    End Sub

    Private Sub btnCreditos_Click(sender As Object, e As EventArgs)
        Using ventanaCreditos As New FormCreditos()
            ventanaCreditos.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs)
        Try
            servicioActivo = chkActivo.Checked
            iniciarConSistema = chkInicioWindows.Checked

            Dim nuevasLineas() As String = {
                "[Configuracion]",
                "HoraInicio=" & tiempoInicio.ToString("hh\:mm"),
                "HoraFin=" & tiempoFin.ToString("hh\:mm"),
                "Activo=" & servicioActivo.ToString(),
                "IniciarConWindows=" & iniciarConSistema.ToString()
            }
            File.WriteAllLines(rutaConfig, nuevasLineas)

            GestionarRegistroWindows(iniciarConSistema)

            MessageBox.Show("Configuración aplicada correctamente. El programa seguirá protegiendo el equipo junto al reloj.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.ShowInTaskbar = False
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub notifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        Me.Size = New System.Drawing.Size(420, 240)
        Me.ShowInTaskbar = True
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.BringToFront()
    End Sub

    Private Sub timer2_Tick(sender As Object, e As EventArgs)
        If Not servicioActivo Then
            apagadoIniciado = False
            Exit Sub
        End If

        Dim ahora As TimeSpan = DateTime.Now.TimeOfDay
        Dim dentroDelHorario As Boolean = False

        If tiempoInicio > tiempoFin Then
            If ahora >= tiempoInicio Or ahora < tiempoFin Then
                dentroDelHorario = True
            End If
        Else
            If ahora >= tiempoInicio AndAlso ahora < tiempoFin Then
                dentroDelHorario = True
            End If
        End If

        If dentroDelHorario Then
            If Not apagadoIniciado Then
                apagadoIniciado = True
                Dim infoApagado As New ProcessStartInfo("shutdown.exe")
                infoApagado.Arguments = "/s /f /t 0"
                infoApagado.UseShellExecute = True
                Process.Start(infoApagado)
            End If
        Else
            apagadoIniciado = False
        End If
    End Sub

    Private Sub GestionarRegistroWindows(ByVal activar As Boolean)
        Try
            Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            If activar Then
                regKey.SetValue("ControlNocturnoJimmy", Environment.ProcessPath)
            Else
                regKey.DeleteValue("ControlNocturnoJimmy", False)
            End If
            regKey.Close()
        Catch ex As Exception
            MessageBox.Show("Nota sobre el inicio con Windows: " & ex.Message, "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub CargarConfiguracion()
        Try
            If Not File.Exists(rutaConfig) Then
                Dim lineasPorDefecto() As String = {
                    "[Configuracion]",
                    "HoraInicio=23:59",
                    "HoraFin=06:00",
                    "Activo=False",
                    "IniciarConWindows=False"
                }
                File.WriteAllLines(rutaConfig, lineasPorDefecto)
                tiempoInicio = New TimeSpan(23, 59, 0)
                tiempoFin = New TimeSpan(6, 0, 0)
                servicioActivo = False
                iniciarConSistema = False
            Else
                Dim lineas() As String = File.ReadAllLines(rutaConfig)
                For Each linea As String In lineas
                    Dim texto As String = linea.Trim()

                    If texto.StartsWith("HoraInicio=") Then
                        Dim valor As String = texto.Replace("HoraInicio=", "").Trim()
                        Dim partes() As String = valor.Split(":"c)
                        If partes.Length = 2 Then
                            Dim h As Integer, m As Integer
                            If Integer.TryParse(partes(0), h) AndAlso Integer.TryParse(partes(1), m) Then
                                tiempoInicio = New TimeSpan(h, m, 0)
                            End If
                        End If
                    ElseIf texto.StartsWith("HoraFin=") Then
                        Dim valor As String = texto.Replace("HoraFin=", "").Trim()
                        Dim partes() As String = valor.Split(":"c)
                        If partes.Length = 2 Then
                            Dim h As Integer, m As Integer
                            If Integer.TryParse(partes(0), h) AndAlso Integer.TryParse(partes(1), m) Then
                                tiempoFin = New TimeSpan(h, m, 0)
                            End If
                        End If
                    ElseIf texto.StartsWith("Activo=") Then
                        Dim valor As String = texto.Replace("Activo=", "").Trim()
                        Boolean.TryParse(valor, servicioActivo)
                    ElseIf texto.StartsWith("IniciarConWindows=") Then
                        Dim valor As String = texto.Replace("IniciarConWindows=", "").Trim()
                        Boolean.TryParse(valor, iniciarConSistema)
                    End If
                Next
            End If
        Catch ex As Exception
            tiempoInicio = New TimeSpan(23, 59, 0)
            tiempoFin = New TimeSpan(6, 0, 0)
            servicioActivo = False
            iniciarConSistema = False
        End Try
    End Sub
End Class