<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Timer1 = New Timer(components)
        LabelDPad = New Label()
        LabelButtons = New Label()
        LabelLeftStick = New Label()
        LabelXboxTriggers = New Label()
        LabelRightStick = New Label()
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        ' 
        ' LabelDPad
        ' 
        LabelDPad.AutoSize = True
        LabelDPad.Location = New Point(12, 213)
        LabelDPad.Name = "LabelDPad"
        LabelDPad.Size = New Size(95, 25)
        LabelDPad.TabIndex = 0
        LabelDPad.Text = "LabelDPad"' 
        ' LabelButtons
        ' 
        LabelButtons.AutoSize = True
        LabelButtons.Location = New Point(300, 86)
        LabelButtons.Name = "LabelButtons"
        LabelButtons.Size = New Size(114, 25)
        LabelButtons.TabIndex = 1
        LabelButtons.Text = "LabelButtons"' 
        ' LabelLeftStick
        ' 
        LabelLeftStick.AutoSize = True
        LabelLeftStick.Location = New Point(12, 86)
        LabelLeftStick.Name = "LabelLeftStick"
        LabelLeftStick.Size = New Size(118, 25)
        LabelLeftStick.TabIndex = 3
        LabelLeftStick.Text = "LabelLeftStick"' 
        ' LabelXboxTriggers
        ' 
        LabelXboxTriggers.AutoSize = True
        LabelXboxTriggers.Location = New Point(12, 9)
        LabelXboxTriggers.Name = "LabelXboxTriggers"
        LabelXboxTriggers.Size = New Size(156, 25)
        LabelXboxTriggers.TabIndex = 4
        LabelXboxTriggers.Text = "LabelXboxTriggers"' 
        ' LabelRightStick
        ' 
        LabelRightStick.AutoSize = True
        LabelRightStick.Location = New Point(300, 213)
        LabelRightStick.Name = "LabelRightStick"
        LabelRightStick.Size = New Size(63, 25)
        LabelRightStick.TabIndex = 5
        LabelRightStick.Text = "Label1"' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(LabelRightStick)
        Controls.Add(LabelXboxTriggers)
        Controls.Add(LabelLeftStick)
        Controls.Add(LabelButtons)
        Controls.Add(LabelDPad)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelDPad As Label
    Friend WithEvents LabelButtons As Label
    Friend WithEvents LabelLeftStick As Label
    Friend WithEvents LabelXboxTriggers As Label
    Friend WithEvents LabelRightStick As Label
End Class
