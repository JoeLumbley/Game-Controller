'Game Controller
'This is an example app showing how to use Xbox And PlayStation controllers in VB.NET.
'Written this year 2023 actually works on Windows 10 and 11 64-Bit.
'I'm working on a video to explain the code on my YouTube channel.
'https://www.youtube.com/@codewithjoe6074
'
'MIT License
'Copyright(c) 2023 Joseph Lumbley

'Permission Is hereby granted, free Of charge, to any person obtaining a copy
'of this software And associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
'copies of the Software, And to permit persons to whom the Software Is
'furnished to do so, subject to the following conditions:

'The above copyright notice And this permission notice shall be included In all
'copies Or substantial portions of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
'IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
'LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
'OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
'SOFTWARE.

Imports System.Runtime.InteropServices

Public Class Form1

    Private Declare Function joyGetPosEx Lib "winmm.dll" (ByVal uJoyID As Integer, ByRef pji As JOYINFOEX) As Integer

    <StructLayout(LayoutKind.Sequential)> Public Structure JOYINFOEX
        Public dwSize As Integer
        Public dwFlags As Integer
        Public dwXpos As Integer 'Left Stick: Left / Right
        Public dwYpos As Integer 'Left Stick: Up / Down
        Public dwZpos As Integer 'Xbox: Triggers
        Public dwRpos As Integer 'Right Stick: Up / Down
        Public dwUpos As Integer 'Right Stick: Left / Right
        Public dwVpos As Integer
        Public dwButtons As Integer
        Public dwButtonNumber As Integer
        Public dwPOV As Integer 'D-Pad
        Public dwReserved1 As Integer
        Public dwReserved2 As Integer
    End Structure

    Private Const NeutralStart = 21845
    Private Const NeutralEnd = 43690

    Private ControllerData As JOYINFOEX

    Private ControllerNumber As Integer = 0

    Private Connected(0 To 15) As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Text = "Game Controller - Code with Joe"

        LabelButtons.Text = ""
        LabelDPad.Text = ""

        LabelXaxis.Text = ""
        LabelYaxis.Text = ""

        LabelUaxis.Text = ""
        LabelRaxis.Text = ""

        LabelZaxis.Text = ""

        ControllerData.dwSize = 64
        ControllerData.dwFlags = 255 ' Get all the data.

        Timer1.Interval = 32
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        GetControllerData()

    End Sub

    Private Sub GetControllerData()

        For ControllerNumber = 0 To 15 'Up to 16 controllers

            Try

                If joyGetPosEx(ControllerNumber, ControllerData) = 0 Then

                    UpdateButtonPosition()

                    UpdateDPadPosition()

                    UpdateLeftThumbstickPosition()

                    UpdateTriggerPosition()

                    UpdateRightThumbstickPosition()

                    Connected(ControllerNumber) = True

                Else

                    Connected(ControllerNumber) = False

                End If

            Catch ex As Exception

                MsgBox(ex.ToString)

                Exit Sub

            End Try

        Next

    End Sub

    Private Sub UpdateButtonPosition()
        'The range of buttons is 0 to 255.
        '                                                                            XBox / PlayStation
        'What buttons are down?
        Select Case ControllerData.dwButtons
            Case 0 'All the buttons are up.
                LabelButtons.Text = ""
            Case 1 'A / Square button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: A / Square"
            Case 2 'B / X button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: B / X "
            Case 4 'X / Circle button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: X / Circle"
            Case 8 'Y / Triangle button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Y / Triangle"
            Case 16 'Left Bumper is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Left Bumper"
            Case 32 'Right Bumper is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Right Bumper"
            Case 64 'Back / Left Trigger is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Back / Left Trigger"
            Case 128 'Start / Right Trigger is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Start / Right Trigger"
            Case 3 'A+B / Square+X buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+B / Square+X"
            Case 5 'A+X / Square+Circle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+X / Square+Circle"
            Case 9 'A+Y / Square+Triangle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+Y / Square+Triangle"
            Case 6 'B+X / X+Circle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: B+X / X+Circle"
            Case 10 'B+Y / X+Triangle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: B+Y / X+Triangle"
            Case 12 'X+Y / Circle+Triangle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: X+Y / Circle+Triangle"
            Case 48 'Left Bumper+Right Bumper buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Left Bumper+Right Bumper"
            Case 192 'Back+Start / Left Trigger+Right Trigger are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Back+Start / Left Trigger+Right Trigger"
        End Select

    End Sub

    Private Sub UpdateDPadPosition()
        'The range of POV is 0 to 65535.
        '0 through 31500 is used to represent the angle.
        'degrees = POV \ 100  315° = 31500 \ 100

        'What position is the D-Pad in?
        Select Case ControllerData.dwPOV
            Case 0 '0° Up
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Up"
            Case 4500 '45° Up Right
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Up Right"
            Case 9000 '90° Right
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Right"
            Case 13500 '135° Down Right
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Down Right"
            Case 18000 '180° Down
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Down"
            Case 22500 '225° Down Left
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Down Left"
            Case 27000 '270° Left
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Left"
            Case 31500 '315° Up Left
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Up Left"
            Case 65535 'Neutral
                LabelDPad.Text = ""
        End Select

    End Sub

    Private Sub UpdateLeftThumbstickPosition()
        'The range on the X-axis is 0 to 65535.
        'The range on the Y-axis is 0 to 65535.

        'What position is the left thumbstick in on the X-axis?
        If ControllerData.dwXpos <= NeutralStart Then
            'The left thumbstick is in the left position.

            LabelXaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Left"

        ElseIf ControllerData.dwXpos >= NeutralEnd Then
            'The left thumbstick is in the right position.

            LabelXaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Right"

        Else
            'The left thumbstick is in the neutral position.

            LabelXaxis.Text = ""

        End If

        'What position is the left thumbstick in on the Y-axis?
        If ControllerData.dwYpos <= NeutralStart Then
            'The left thumbstick is in the up position.

            LabelYaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Up"

        ElseIf ControllerData.dwYpos >= NeutralEnd Then
            'The left thumbstick is in the down position.

            LabelYaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Down"

        Else
            'The left thumbstick is in the neutral position.

            LabelYaxis.Text = ""

        End If

    End Sub

    Private Sub UpdateRightThumbstickPosition()
        'The range on the U-axis is 0 to 65535.
        'The range on the R-axis is 0 to 65535.

        'What position is the right thumbstick in on the U-axis?
        If ControllerData.dwUpos <= NeutralStart Then
            'The right thumbstick is in the left position.

            LabelUaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Left"

        ElseIf ControllerData.dwUpos >= NeutralEnd Then
            'The right thumbstick is in the right position.

            LabelUaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Right"

        Else
            'The right thumbstick is in the neutral position.

            LabelUaxis.Text = ""

        End If

        'What position is the right thumbstick in on the R-axis?
        If ControllerData.dwRpos <= NeutralStart Then
            'The right thumbstick is in the up position.

            LabelRaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Up"

        ElseIf ControllerData.dwRpos >= NeutralEnd Then
            'The right thumbstick is in the down position.

            LabelRaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Down"

        Else
            'The right thumbstick is in the neutral position.

            LabelRaxis.Text = ""

        End If

    End Sub

    Private Sub UpdateTriggerPosition()
        'The range on the Z-axis is 0 to 65535.

        'Is one of the Xbox triggers down?
        If ControllerData.dwZpos <= NeutralStart Then
            'The right trigger is down only.

            LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Trigger / Right Thumbstick: Left"

        ElseIf ControllerData.dwZpos >= NeutralEnd Then
            'The left trigger is down only.

            LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Trigger / Right Thumbstick: Right"

        Else
            'The triggers are either both up or down.

            LabelZaxis.Text = ""

        End If

    End Sub

End Class
