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
        Public dwXpos As Integer
        Public dwYpos As Integer
        Public dwZpos As Integer 'Xbox: Trigger
        Public dwRpos As Integer
        Public dwUpos As Integer
        Public dwVpos As Integer
        Public dwButtons As Integer
        Public dwButtonNumber As Integer
        Public dwPOV As Integer 'D-Pad
        Public dwReserved1 As Integer
        Public dwReserved2 As Integer
    End Structure

    Dim JI As JOYINFOEX

    Dim JNum As Long = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        JI.dwSize = 64
        JI.dwFlags = &HFF ' All information

        Timer1.Interval = 32
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        For JNum = 0 To 15 'Up to 16 controllers

            If joyGetPosEx(JNum, JI) = 0 Then

                With JI

                    'Buttons
                    Select Case .dwButtons
                        Case 1
                            '                                         XBox / PlayStation
                            Text = "Joystick: " & CStr(JNum) & " Button: A / Square"
                        Case 2
                            Text = "Joystick: " & CStr(JNum) & " Button: B / X "
                        Case 4
                            Text = "Joystick: " & CStr(JNum) & " Button: X / Circle"
                        Case 8
                            Text = "Joystick: " & CStr(JNum) & " Button: Y / Triangle"
                        Case 16
                            Text = "Joystick: " & CStr(JNum) & " Button: Left Bumper"
                        Case 32
                            Text = "Joystick: " & CStr(JNum) & " Button: Right Bumper"
                        Case 64
                            Text = "Joystick: " & CStr(JNum) & " Button: Back / Left Trigger"
                        Case 128
                            Text = "Joystick: " & CStr(JNum) & " Button: Start / Right Trigger"
                    End Select

                    If .dwButtons = 0 Then

                        Select Case Text
                            Case "Joystick: " & CStr(JNum) & " Button: A / Square"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: B / X "
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: X / Circle"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: Y / Triangle"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: A"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: Left Bumper"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: Right Bumper"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: Back / Left Trigger"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: Start / Right Trigger"
                                Text = ""
                        End Select

                    End If

                    'D-Pad
                    If .dwPOV = 9000 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Right"
                    End If
                    If .dwPOV = 27000 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Left"
                    End If
                    If .dwPOV = 0 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Up"
                    End If
                    If .dwPOV = 18000 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Down"
                    End If
                    If .dwPOV = 31500 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Up Left"
                    End If
                    If .dwPOV = 4500 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Up Right"
                    End If
                    If .dwPOV = 22500 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Down Left"
                    End If
                    If .dwPOV = 13500 Then
                        Text = "Joystick: " & CStr(JNum) & " D-Pad: Down Right"
                    End If

                    If .dwPOV = 65535 Then
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Up" Then
                            Text = ""
                        End If
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Up Left" Then
                            Text = ""
                        End If
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Up Right" Then
                            Text = ""
                        End If
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Down" Then
                            Text = ""
                        End If
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Down Left" Then
                            Text = ""
                        End If
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Down Right" Then
                            Text = ""
                        End If
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Left" Then
                            Text = ""
                        End If
                        If Text = "Joystick: " & CStr(JNum) & " D-Pad: Right" Then
                            Text = ""
                        End If
                    End If

                    'Xbox trigger buttons
                    If (.dwZpos - 32767) / 32768.0 > 0 Then

                        Text = "Joystick: " & CStr(JNum) & " Button: Left Trigger"

                    End If
                    If (.dwZpos - 32767) / 32768.0 < 0 Then

                        Text = "Joystick: " & CStr(JNum) & " Button: Right Trigger"

                    End If

                    If (.dwZpos - 32767) / 32768.0 = 0 Then
                        Select Case Text
                            Case "Joystick: " & CStr(JNum) & " Button: Right Trigger"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Button: Left Trigger"
                                Text = ""
                        End Select
                    End If




                    'Xbox left stick
                    If (.dwYpos - 32767) / 32768.0 = 1 Then

                        Text = "Joystick: " & CStr(JNum) & " Left Stick: Down"

                    End If
                    If (.dwYpos - 32767) / 32768.0 < 0 Then

                        Text = "Joystick: " & CStr(JNum) & " Left Stick: Up"

                    End If

                    If CInt((.dwYpos - 32767) / 32768.0) = 0 Then
                        Select Case Text
                            Case "Joystick: " & CStr(JNum) & " Left Stick: Down"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Left Stick: Up"
                                Text = ""
                        End Select
                    End If


                    'Text = (.dwXpos - 32767) / 32768.0

                    If (.dwXpos - 32767) / 32768.0 > 0 Then

                        Text = "Joystick: " & CStr(JNum) & " Left Stick: Right"

                    End If
                    If (.dwXpos - 32767) / 32768.0 < 0 Then

                        Text = "Joystick: " & CStr(JNum) & " Left Stick: Left"

                    End If

                    If CInt((.dwXpos - 32767) / 32768.0) = 0 Then
                        Select Case Text
                            Case "Joystick: " & CStr(JNum) & " Left Stick: Right"
                                Text = ""
                            Case "Joystick: " & CStr(JNum) & " Left Stick: Left"
                                Text = ""
                        End Select
                    End If


                End With

            End If

        Next

    End Sub

End Class
