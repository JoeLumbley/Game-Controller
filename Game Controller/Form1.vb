'Game Controller
'This is an example program for using Xbox and PlayStation controllers.
'I'm making a video to explain the code on my YouTube channel.
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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

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

                    GetButtonData()

                    GetDPadData()

                    GetLeftStickData()

                    GetRightStickData()

                    GetTriggerData()

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
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: A / Square"
                Case 2
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: B / X "
                Case 4
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: X / Circle"
                Case 8
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: Y / Triangle"
                Case 16
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: Left Bumper"
                Case 32
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: Right Bumper"
                Case 64
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: Back / Left Trigger"
                Case 128
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: Start / Right Trigger"
                Case 3
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: A+B / Square+X"
                Case 5
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: A+X / Square+Circle"
                Case 9
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: A+Y / Square+Triangle"
                Case 6
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: B+X / X+Circle"
                Case 10
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: B+Y / X+Triangle"
                Case 12
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: X+Y / Circle+Triangle"
                Case 192
                    Text = "Controller: " & CStr(ControllerNumber) & " Button: Back+Start / Left Trigger+Right Trigger"
            End Select

            'Are all the buttons up?
            If .dwButtons = 0 Then
                'Yes, all the buttons are up.

                Select Case Text
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A / Square"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: B / X "
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: X / Circle"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Y / Triangle"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Left Bumper"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Right Bumper"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Back / Left Trigger"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Start / Right Trigger"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A+B / Square+X"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A+X / Square+Circle"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: A+Y / Square+Triangle"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: B+X / X+Circle"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: B+Y / X+Triangle"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: X+Y / Circle+Triangle"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " Button: Back+Start / Left Trigger+Right Trigger"
                        Text = ""
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
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Right"
            End If
            If .dwPOV = 27000 Then '270°
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Left"
            End If
            If .dwPOV = 0 Then '0°
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Up"
            End If
            If .dwPOV = 18000 Then '180°
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Down"
            End If
            If .dwPOV = 31500 Then '315°
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Left"
            End If
            If .dwPOV = 4500 Then '45°
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Right"
            End If
            If .dwPOV = 22500 Then '225°
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Left"
            End If
            If .dwPOV = 13500 Then '135°
                Text = "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Right"
            End If

            'Has the POV pad returned to it neutral position?
            If .dwPOV = 65535 Then
                'Yes, the POV pad has returned to it neutral position.

                Select Case Text
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Up"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Left"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Up Right"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Down"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Left"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Down Right"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Left"
                        Text = ""
                    Case "Controller: " & CStr(ControllerNumber) & " D-Pad: Right"
                        Text = ""
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

                    Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Up"

                End If
            End If

            'Is the left stick in the down position?
            If .dwYpos >= 55000 Then
                'Yes, the left stick is in the down position.

                Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Down"

            End If

            'Has the left stick returned to it neutral position on the y-axis?
            If .dwYpos > 10000 Then
                If .dwYpos < 55000 Then
                    'Yes, the left stick has returned to it neutral position y-axis.

                    Select Case Text
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Down"
                            Text = ""
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Up"
                            Text = ""
                    End Select

                End If
            End If

            'Is the left stick in the left position?
            If .dwXpos >= 0 Then
                If .dwXpos <= 10000 Then
                    'Yes, the left stick is in the left position.

                    Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Left"

                End If
            End If

            'Is the left stick in the right position?
            If .dwXpos >= 55000 Then
                'Yes, the left stick is in the right position.

                Text = "Controller: " & CStr(ControllerNumber) & " Left Stick: Right"

            End If

            'Has the left stick returned to it neutral position on the x-axis?
            If .dwXpos > 10000 Then
                If .dwXpos < 55000 Then
                    'Yes, the left stick has returned to it neutral position on the x-axis.

                    Select Case Text
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Left"
                            Text = ""
                        Case "Controller: " & CStr(ControllerNumber) & " Left Stick: Right"
                            Text = ""
                    End Select

                End If
            End If

        End With

    End Sub

    Private Sub GetRightStickData()
        'The range on the r-axis is 0 to 65535.
        'The range on the u-axis is 0 to 65535.

        With ControllerData

            'Is the right stick in the up position?
            If .dwRpos >= 0 Then
                If .dwRpos <= 10000 Then
                    'Yes, the right stick is in the up position.

                    Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Up"

                End If
            End If

            'Is the right stick in the down position?
            If .dwRpos >= 55000 Then
                'Yes, the right stick is in the down position.

                Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Down"

            End If

            'Has the right stick returned to it neutral position on the y-axis?
            If .dwRpos > 10000 Then
                If .dwRpos < 55000 Then
                    'Yes, the right stick has returned to it neutral position y-axis.

                    Select Case Text
                        Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Down"
                            Text = ""
                        Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Up"
                            Text = ""
                    End Select

                End If
            End If

            'Is the right stick in the left position?
            If .dwUpos >= 0 Then
                If .dwUpos <= 10000 Then
                    'Yes, the right stick is in the left position.

                    Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Left"

                End If
            End If

            'Is the right stick in the right position?
            If .dwUpos >= 55000 Then
                'Yes, the right stick is in the right position.

                Text = "Controller: " & CStr(ControllerNumber) & " Right Stick: Right"

            End If

            'Has the right stick returned to it neutral position on the x-axis?
            If .dwUpos > 10000 Then
                If .dwUpos < 55000 Then
                    'Yes, the right stick has returned to it neutral position on the x-axis.

                    Select Case Text
                        Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Left"
                            Text = ""
                        Case "Controller: " & CStr(ControllerNumber) & " Right Stick: Right"
                            Text = ""
                    End Select

                End If
            End If

        End With

    End Sub

    Private Sub GetTriggerData()
        'The range on the z-axis is 0 to 65535.

        With ControllerData

            'Is the right trigger down?
            If .dwZpos >= 0 Then
                If .dwZpos <= 10000 Then
                    'Yes, the right trigger is down.

                    Text = "Controller: " & CStr(ControllerNumber) & " Right Trigger"

                End If
            End If

            'Is the left trigger down?
            If .dwZpos >= 55000 Then
                'Yes, the left trigger is down.

                Text = "Controller: " & CStr(ControllerNumber) & " Left Trigger"

            End If

            'Have the triggers returned to their neutral positions?
            If .dwZpos > 10000 Then
                If .dwZpos < 55000 Then
                    'Yes, the triggers have returned to their neutral positions.

                    Select Case Text
                        Case "Controller: " & CStr(ControllerNumber) & " Left Trigger"

                            Text = ""

                        Case "Controller: " & CStr(ControllerNumber) & " Right Trigger"

                            Text = ""

                    End Select

                End If
            End If

        End With

    End Sub

End Class
