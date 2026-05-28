<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCreditos
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
        Label1 = New Label()
        Button1 = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(66, 67)
        Label1.Name = "Label1"
        Label1.Size = New Size(550, 20)
        Label1.TabIndex = 0
        Label1.Text = "Control Nocturno v1.0" & ChrW(8221) & " " & ChrW(8220) & "Desarrollado por: Víctor" & ChrW(8221) & " " & ChrW(8220) & "Todos los derechos reservados."
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(122, 153)
        Button1.Name = "Button1"
        Button1.Size = New Size(251, 29)
        Button1.TabIndex = 1
        Button1.Text = "Invitame un Cafe ☕"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' FormCreditos
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(725, 295)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Name = "FormCreditos"
        Text = "FormCreditos"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
End Class
