'Game Controller
'This is an example app showing how to use Xbox And PlayStation controllers in VB.NET.
'Written this year 2023 actually works on Windows 10 64-Bit.
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

    Dim ControllerData As JOYINFOEX

    Dim ControllerNumber As Long = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Text = "Game Controller"

        LabelButtons.Text = ""
        LabelDPad.Text = ""
        LabelLeftStick.Text = ""
        LabelRightStick.Text = ""
        LabelXboxTriggers.Text = ""

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

                    GetRightStickData()

                    GetLeftStickData()

                    GetTriggerData()

                    GetButtonData()

                    GetDPadData()

                End If

            Catch ex As Exception

                MsgBox(ex.ToString)

                Exit Sub

            End Try

        Next

    End Sub

    Private Sub GetButtonData()
        'The range of buttons is 0 to 255.

        With ControllerData

            '                                                               XBox / PlayStation
            'What buttons are down?
            Select Case .dwButtons
                Case 1
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: A / Square"
                Case 2
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: B / X "
                Case 4
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: X / Circle"
                Case 8
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: Y / Triangle"
                Case 16
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: Left Bumper"
                Case 32
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: Right Bumper"
                Case 64
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: Back / Left Trigger"
                Case 128
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: Start / Right Trigger"
                Case 3
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: A+B / Square+X"
                Case 5
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: A+X / Square+Circle"
                Case 9
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: A+Y / Square+Triangle"
                Case 6
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: B+X / X+Circle"
                Case 10
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: B+Y / X+Triangle"
                Case 12
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: X+Y / Circle+Triangle"
                Case 192
                    LabelButtons.Text = "Controller: " & CStr(ControllerNumber) & " Button: Back+Start / Left Trigger+Right Trigger"
            End Select

            'Are all the buttons up?
            If .dwButtons = 0 Then
                'Yes, all the buttons are up.

                Select Case LabelButtons.Text
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A / Square"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: B / X "
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: X / Circle"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Y / Triangle"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Left Bumper"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Right Bumper"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Back / Left Trigger"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Start / Right Trigger"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A+B / Square+X"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A+X / Square+Circle"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A+Y / Square+Triangle"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: B+X / X+Circle"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: B+Y / X+Triangle"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: X+Y / Circle+Triangle"
                        LabelButtons.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Back+Start / Left Trigger+Right Trigger"
                        LabelButtons.Text = ""
                End Select

            End If

        End With

    End Sub

    Private Sub GetDPadData()
        'The range of POV is 0 to 65535.
        '0 through 31500 is used to represent the angle.
        'degrees = POV \ 100  315° = 31500 \ 100

        With ControllerData

            'What buttons are down?
            If .dwPOV = 9000 Then '90°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Right"
            End If
            If .dwPOV = 27000 Then '270°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Left"
            End If
            If .dwPOV = 0 Then '0°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Up"
            End If
            If .dwPOV = 18000 Then '180°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Down"
            End If
            If .dwPOV = 31500 Then '315°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Left"
            End If
            If .dwPOV = 4500 Then '45°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Right"
            End If
            If .dwPOV = 22500 Then '225°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Left"
            End If
            If .dwPOV = 13500 Then '135°
                LabelDPad.Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Right"
            End If

            'Has the POV pad returned to it neutral position?
            If .dwPOV = 65535 Then
                'Yes, the POV pad has returned to it neutral position.

                Select Case LabelDPad.Text
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Up"
                        LabelDPad.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Left"
                        LabelDPad.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Right"
                        LabelDPad.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Down"
                        LabelDPad.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Left"
                        LabelDPad.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Right"
                        LabelDPad.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Left"
                        LabelDPad.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Right"
                        LabelDPad.Text = ""
                End Select

            End If

        End With

    End Sub

    Private Sub GetLeftStickData()
        'The range on the y-axis is 0 to 65535.
        'The range on the x-axis is 0 to 65535.

        With ControllerData

            'Is the left stick in the up position?
            If .dwYpos >= 0 Then
                If .dwYpos <= 10000 Then
                    'Yes, the left stick is in the up position.

                    LabelLeftStick.Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Up"

                End If
            End If

            'Is the left stick in the down position?
            If .dwYpos >= 55000 Then
                'Yes, the left stick is in the down position.

                LabelLeftStick.Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Down"

            End If

            'Has the left stick returned to it neutral position on the y-axis?
            If .dwYpos > 10000 Then
                If .dwYpos < 55000 Then
                    'Yes, the left stick has returned to it neutral position y-axis.

                    Select Case LabelLeftStick.Text
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Down"
                            LabelLeftStick.Text = ""
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Up"
                            LabelLeftStick.Text = ""
                    End Select

                End If
            End If

            'Is the left stick in the left position?
            If .dwXpos >= 0 Then
                If .dwXpos <= 10000 Then
                    'Yes, the left stick is in the left position.

                    LabelLeftStick.Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Left"

                End If
            End If

            'Is the left stick in the right position?
            If .dwXpos >= 55000 Then
                'Yes, the left stick is in the right position.

                LabelLeftStick.Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Right"

            End If

            'Has the left stick returned to it neutral position on the x-axis?
            If .dwXpos > 10000 Then
                If .dwXpos < 55000 Then
                    'Yes, the left stick has returned to it neutral position on the x-axis.

                    Select Case LabelLeftStick.Text
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Left"
                            LabelLeftStick.Text = ""
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Right"
                            LabelLeftStick.Text = ""
                    End Select

                End If
            End If

        End With

    End Sub

    Private Sub GetRightStickData()
        'The range on the r-axis is 0 to 65535.
        'The range on the u-axis is 0 to 65535.

        With ControllerData

            'Are the PlayStation trigger buttons up?
            If .dwButtons <> 128 And .dwButtons <> 192 Then 'PS:Triggers
                'Yes, the PlayStation trigger buttons are up.

                'Is the right stick in the left position?
                If .dwUpos >= 1 And .dwUpos <= 15000 Then
                    'Yes, the right stick is in the left position.

                    LabelRightStick.Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Left"

                End If

                'Is the right stick in the right position?
                If .dwUpos >= 50000 Then
                    'Yes, the right stick is in the right position.

                    LabelRightStick.Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Right"

                End If

                'Has the right stick returned to it neutral position on the x-axis?
                If .dwUpos > 15000 Then
                    If .dwUpos < 50000 Then
                        'Yes, the right stick has returned to it neutral position on the x-axis.

                        Select Case LabelRightStick.Text
                            Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Left"
                                LabelRightStick.Text = ""
                            Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Right"
                                LabelRightStick.Text = ""
                        End Select
                    End If
                End If

            Else
                'No, the PlayStation trigger buttons are not up.

                Select Case LabelRightStick.Text
                    Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Left"
                        LabelRightStick.Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Right"
                        LabelRightStick.Text = ""
                End Select

            End If

            'Is the right stick in the up position?
            If .dwRpos >= 0 Then
                If .dwRpos <= 15000 Then
                    'Yes, the right stick is in the up position.

                    LabelRightStick.Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Up"

                End If
            End If

            'Is the right stick in the down position?
            If .dwRpos >= 50000 Then
                'Yes, the right stick is in the down position.

                LabelRightStick.Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Down"

            End If

            'Has the right stick returned to it neutral position on the y-axis?
            If .dwRpos > 15000 Then
                If .dwRpos < 50000 Then
                    'Yes, the right stick has returned to it neutral position y-axis.

                    Select Case LabelRightStick.Text
                        Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Down"
                            LabelRightStick.Text = ""
                        Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Up"
                            LabelRightStick.Text = ""
                    End Select

                End If
            End If

        End With

    End Sub

    Private Sub GetTriggerData()
        'The range on the z-axis is 0 to 65535.

        With ControllerData

            'Xbox Triggers
            'Is the right trigger down?
            If .dwZpos >= 0 Then
                If .dwZpos <= 10000 Then
                    'Yes, the right trigger is down.

                    LabelXboxTriggers.Text = "Controller: " & CStr(ControllerNumber) & " Right Trigger / Right Stick: Left"

                End If
            End If

            'Is the left trigger down?
            If .dwZpos >= 55000 Then
                'Yes, the left trigger is down.

                LabelXboxTriggers.Text = "Controller: " & CStr(ControllerNumber) & " Left Trigger / Right Stick: Right"

            End If

            'Have the triggers returned to their neutral positions?
            If .dwZpos > 10000 Then
                If .dwZpos < 55000 Then
                    'Yes, the triggers have returned to their neutral positions.

                    Select Case LabelXboxTriggers.Text
                        Case "Controller: " & CStr(ControllerNumber) & " Left Trigger / Right Stick: Right"

                            LabelXboxTriggers.Text = ""

                        Case "Controller: " & CStr(ControllerNumber) & " Right Trigger / Right Stick: Left"

                            LabelXboxTriggers.Text = ""

                    End Select

                End If
            End If

        End With

    End Sub

End Class
