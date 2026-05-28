<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        btnAplicar = New Button()
        chkActivo = New CheckBox()
        chkInicioWindows = New CheckBox()
        timer2 = New Timer(components)
        notifyIcon1 = New NotifyIcon(components)
        btnCreditos = New Button()
        SuspendLayout()
        ' 
        ' btnAplicar
        ' 
        btnAplicar.Location = New Point(39, 133)
        btnAplicar.Name = "btnAplicar"
        btnAplicar.Size = New Size(94, 29)
        btnAplicar.TabIndex = 0
        btnAplicar.Text = "Aplicar"
        btnAplicar.UseVisualStyleBackColor = True
        ' 
        ' chkActivo
        ' 
        chkActivo.AutoSize = True
        chkActivo.Location = New Point(39, 81)
        chkActivo.Name = "chkActivo"
        chkActivo.Size = New Size(73, 24)
        chkActivo.TabIndex = 1
        chkActivo.Text = "Activo"
        chkActivo.UseVisualStyleBackColor = True
        ' 
        ' chkInicioWindows
        ' 
        chkInicioWindows.AutoSize = True
        chkInicioWindows.Location = New Point(39, 42)
        chkInicioWindows.Name = "chkInicioWindows"
        chkInicioWindows.Size = New Size(161, 24)
        chkInicioWindows.TabIndex = 2
        chkInicioWindows.Text = "Iniciar con windows"
        chkInicioWindows.UseVisualStyleBackColor = True
        ' 
        ' notifyIcon1
        ' 
        notifyIcon1.Text = "NotifyIcon1"
        notifyIcon1.Visible = True
        ' 
        ' btnCreditos
        ' 
        btnCreditos.Location = New Point(157, 133)
        btnCreditos.Name = "btnCreditos"
        btnCreditos.Size = New Size(33, 29)
        btnCreditos.TabIndex = 3
        btnCreditos.Text = "?"
        btnCreditos.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(390, 296)
        Controls.Add(btnCreditos)
        Controls.Add(chkInicioWindows)
        Controls.Add(chkActivo)
        Controls.Add(btnAplicar)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnAplicar As Button
    Friend WithEvents chkActivo As CheckBox
    Friend WithEvents chkInicioWindows As CheckBox
    Friend WithEvents timer2 As Timer
    Friend WithEvents notifyIcon1 As NotifyIcon
    Friend WithEvents btnCreditos As Button

End Class
