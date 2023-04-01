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
        LabelXaxis = New Label()
        LabelZaxis = New Label()
        LabelUaxis = New Label()
        LabelYaxis = New Label()
        LabelRaxis = New Label()
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        ' 
        ' LabelDPad
        ' 
        LabelDPad.AutoSize = True
        LabelDPad.Location = New Point(12, 114)
        LabelDPad.Name = "LabelDPad"
        LabelDPad.Size = New Size(95, 25)
        LabelDPad.TabIndex = 0
        LabelDPad.Text = "LabelDPad"
        ' 
        ' LabelButtons
        ' 
        LabelButtons.AutoSize = True
        LabelButtons.Location = New Point(301, 47)
        LabelButtons.Name = "LabelButtons"
        LabelButtons.Size = New Size(114, 25)
        LabelButtons.TabIndex = 1
        LabelButtons.Text = "LabelButtons"
        ' 
        ' LabelXaxis
        ' 
        LabelXaxis.AutoSize = True
        LabelXaxis.Location = New Point(12, 47)
        LabelXaxis.Name = "LabelXaxis"
        LabelXaxis.Size = New Size(93, 25)
        LabelXaxis.TabIndex = 3
        LabelXaxis.Text = "LabelXaxis"
        ' 
        ' LabelZaxis
        ' 
        LabelZaxis.AutoSize = True
        LabelZaxis.Location = New Point(12, 9)
        LabelZaxis.Name = "LabelZaxis"
        LabelZaxis.Size = New Size(92, 25)
        LabelZaxis.TabIndex = 4
        LabelZaxis.Text = "LabelZaxis"
        ' 
        ' LabelUaxis
        ' 
        LabelUaxis.AutoSize = True
        LabelUaxis.Location = New Point(301, 89)
        LabelUaxis.Name = "LabelUaxis"
        LabelUaxis.Size = New Size(94, 25)
        LabelUaxis.TabIndex = 5
        LabelUaxis.Text = "LabelUaxis"
        ' 
        ' LabelYaxis
        ' 
        LabelYaxis.AutoSize = True
        LabelYaxis.Location = New Point(12, 72)
        LabelYaxis.Name = "LabelYaxis"
        LabelYaxis.Size = New Size(90, 25)
        LabelYaxis.TabIndex = 6
        LabelYaxis.Text = "LabelYaxis"
        ' 
        ' LabelRaxis
        ' 
        LabelRaxis.AutoSize = True
        LabelRaxis.Location = New Point(301, 114)
        LabelRaxis.Name = "LabelRaxis"
        LabelRaxis.Size = New Size(93, 25)
        LabelRaxis.TabIndex = 7
        LabelRaxis.Text = "LabelRaxis"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(LabelRaxis)
        Controls.Add(LabelYaxis)
        Controls.Add(LabelUaxis)
        Controls.Add(LabelZaxis)
        Controls.Add(LabelXaxis)
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
    Friend WithEvents LabelXaxis As Label
    Friend WithEvents LabelZaxis As Label
    Friend WithEvents LabelUaxis As Label
    Friend WithEvents LabelYaxis As Label
    Friend WithEvents LabelRaxis As Label
End Class
