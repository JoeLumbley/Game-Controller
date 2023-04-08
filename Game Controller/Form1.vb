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

    <DllImport("winmm.dll", EntryPoint:="joyGetDevCapsW")> Private Shared Function joyGetDevCapsW(ByVal uJoyID As Integer, ByRef pjc As JOYCAPSW, ByVal cbjc As Integer) As UInteger
    End Function

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure JOYCAPSW
        Public wMid As UShort
        Public wPid As UShort
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public szPname As String
        Public wXmin As UInteger
        Public wXmax As UInteger
        Public wYmin As UInteger
        Public wYmax As UInteger
        Public wZmin As UInteger
        Public wZmax As UInteger
        Public wNumButtons As UInteger
        Public wPeriodMin As UInteger
        Public wPeriodMax As UInteger
        Public wRmin As UInteger
        Public wRmax As UInteger
        Public wUmin As UInteger
        Public wUmax As UInteger
        Public wVmin As UInteger
        Public wVmax As UInteger
        Public wCaps As JoyCapOpts
        Public wMaxAxes As UInteger
        Public wNumAxes As UInteger
        Public wMaxButtons As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public szRegKey As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public szOEMVxD As String
    End Structure

    Public Enum JoyCapOpts As UInteger
        Has_Z_Axis = &H1
        Has_R_Axis = &H2
        Has_U_Axis = &H4
        Has_V_Axis = &H8
        Has_PointOfView = &H10
        Has_PointOfView_4Direction = &H20
        Has_PointOfView_Continuous = &H40
    End Enum

    Public Const PlayStation As Integer = 1356

    'Private Declare Function joyGetPosEx Lib "winmm.dll" (ByVal uJoyID As Integer, ByRef pji As JOYINFOEX) As Integer

    <DllImport("winmm.dll", EntryPoint:="joyGetPosEx")> Private Shared Function joyGetPosEx(ByVal uJoyID As Integer, ByRef pji As JOYINFOEX) As Integer
    End Function

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

    Private IsPlayStation(0 To 15) As Boolean

    Private ControllerCapabilities As New JOYCAPSW


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


        Timer2.Interval = 400

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        GetControllerData()

    End Sub

    Private Sub GetControllerData()

        For ControllerNumber = 0 To 15 'Up to 16 controllers

            Try

                If joyGetPosEx(ControllerNumber, ControllerData) = 0 Then

                    UpdateControllerManufacturer()

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

    Private Sub UpdateControllerManufacturer()

        If joyGetDevCapsW(ControllerNumber, ControllerCapabilities, Marshal.SizeOf(ControllerCapabilities)) = 0 Then

            If ControllerCapabilities.wMid = PlayStation Then

                IsPlayStation(ControllerNumber) = True

            Else

                IsPlayStation(ControllerNumber) = False

            End If

        End If

    End Sub

    Private Sub UpdateButtonPosition()
        'The range of buttons is 0 to 255.

        If IsPlayStation(ControllerNumber) = True Then
            'PS
            'What buttons are down?
            Select Case ControllerData.dwButtons
                Case 0 'All the buttons are up.
                    'LabelButtons.Text = ""
                Case 1 'A / Square button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Square"
                    Timer2.Start()
                Case 2 'B / X button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: X "
                    Timer2.Start()
                Case 4 'X / Circle button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Circle"
                    Timer2.Start()
                Case 8 'Y / Triangle button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Triangle"
                    Timer2.Start()
                Case 16 'Left Bumper is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Left Bumper"
                    Timer2.Start()
                Case 32 'Right Bumper is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Right Bumper"
                    Timer2.Start()
                Case 64 'Back / Left Trigger is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Left Trigger"
                    Timer2.Start()
                Case 128 'Start / Right Trigger is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Right Trigger"
                    Timer2.Start()
                Case 3 'A+B / Square+X buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Square+X"
                    Timer2.Start()
                Case 5 'A+X / Square+Circle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Square+Circle"
                    Timer2.Start()
                Case 9 'A+Y / Square+Triangle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Square+Triangle"
                    Timer2.Start()
                Case 6 'B+X / X+Circle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: X+Circle"
                    Timer2.Start()
                Case 10 'B+Y / X+Triangle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: X+Triangle"
                    Timer2.Start()
                Case 12 'X+Y / Circle+Triangle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Circle+Triangle"
                    Timer2.Start()
                Case 48 'Left Bumper+Right Bumper buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Left Bumper+Right Bumper"
                    Timer2.Start()
                Case 192 'Back+Start / Left Trigger+Right Trigger are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Left Trigger+Right Trigger"
                    Timer2.Start()
            End Select

        Else
            'XB
            'What buttons are down?
            Select Case ControllerData.dwButtons
                Case 0 'All the buttons are up.
                    'LabelButtons.Text = ""
                Case 1 'A / Square button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: A"
                    Timer2.Start()
                Case 2 'B / X button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: B "
                    Timer2.Start()
                Case 4 'X / Circle button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: X"
                    Timer2.Start()
                Case 8 'Y / Triangle button is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Y"
                    Timer2.Start()
                Case 16 'Left Bumper is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Left Bumper"
                    Timer2.Start()
                Case 32 'Right Bumper is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Right Bumper"
                    Timer2.Start()
                Case 64 'Back / Left Trigger is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Back"
                    Timer2.Start()
                Case 128 'Start / Right Trigger is down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Start"
                    Timer2.Start()
                Case 3 'A+B / Square+X buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+B"
                    Timer2.Start()
                Case 5 'A+X / Square+Circle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+X"
                    Timer2.Start()
                Case 9 'A+Y / Square+Triangle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+Y"
                    Timer2.Start()
                Case 6 'B+X / X+Circle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: B+X"
                    Timer2.Start()
                Case 10 'B+Y / X+Triangle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: B+Y"
                    Timer2.Start()
                Case 12 'X+Y / Circle+Triangle buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: X+Y"
                    Timer2.Start()
                Case 48 'Left Bumper+Right Bumper buttons are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Left Bumper+Right Bumper"
                    Timer2.Start()
                Case 192 'Back+Start / Left Trigger+Right Trigger are down.
                    LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Back+Start"
                    Timer2.Start()
            End Select

        End If

    End Sub

    Private Sub UpdateDPadPosition()
        'The range of POV is 0 to 65535.
        '0 through 31500 is used to represent the angle.
        'degrees = POV \ 100  315° = 31500 \ 100

        'What position is the D-Pad in?
        Select Case ControllerData.dwPOV
            Case 0 '0° Up
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Up"
                Timer2.Start()
            Case 4500 '45° Up Right
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Up Right"
                Timer2.Start()
            Case 9000 '90° Right
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Right"
                Timer2.Start()
            Case 13500 '135° Down Right
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Down Right"
                Timer2.Start()
            Case 18000 '180° Down
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Down"
                Timer2.Start()
            Case 22500 '225° Down Left
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Down Left"
                Timer2.Start()
            Case 27000 '270° Left
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Left"
                Timer2.Start()
            Case 31500 '315° Up Left
                LabelDPad.Text = "Controller: " & ControllerNumber.ToString & " D-Pad: Up Left"
                Timer2.Start()
            Case 65535 'Neutral
                'LabelDPad.Text = ""
        End Select

    End Sub

    Private Sub UpdateLeftThumbstickPosition()
        'The range on the X-axis is 0 to 65535.
        'The range on the Y-axis is 0 to 65535.

        'What position is the left thumbstick in on the X-axis?
        If ControllerData.dwXpos <= NeutralStart Then
            'The left thumbstick is in the left position.

            LabelXaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Left"
            Timer2.Start()


        ElseIf ControllerData.dwXpos >= NeutralEnd Then
            'The left thumbstick is in the right position.

            LabelXaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Right"
            Timer2.Start()

        Else
            'The left thumbstick is in the neutral position.

            'LabelXaxis.Text = ""

        End If

        'What position is the left thumbstick in on the Y-axis?
        If ControllerData.dwYpos <= NeutralStart Then
            'The left thumbstick is in the up position.

            LabelYaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Up"
            Timer2.Start()

        ElseIf ControllerData.dwYpos >= NeutralEnd Then
            'The left thumbstick is in the down position.

            LabelYaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Down"
            Timer2.Start()

        Else
            'The left thumbstick is in the neutral position.

            'LabelYaxis.Text = ""

        End If

    End Sub

    Private Sub UpdateRightThumbstickPosition()
        'The range on the U-axis is 0 to 65535.
        'The range on the R-axis is 0 to 65535.

        If IsPlayStation(ControllerNumber) = False Then

            'What position is the right thumbstick in on the U-axis?
            If ControllerData.dwUpos <= NeutralStart Then
                'The right thumbstick is in the left position.

                LabelUaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Left"
                Timer2.Start()


            ElseIf ControllerData.dwUpos >= NeutralEnd Then
                'The right thumbstick is in the right position.

                LabelUaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Right"
                Timer2.Start()

            Else
                'The right thumbstick is in the neutral position.

                'LabelUaxis.Text = ""

            End If


        End If

        'What position is the right thumbstick in on the R-axis?
        If ControllerData.dwRpos <= NeutralStart Then
            'The right thumbstick is in the up position.

            LabelRaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Up"
            Timer2.Start()

        ElseIf ControllerData.dwRpos >= NeutralEnd Then
            'The right thumbstick is in the down position.

            LabelRaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Down"
            Timer2.Start()

        Else
            'The right thumbstick is in the neutral position.

            'LabelRaxis.Text = ""

        End If

    End Sub

    Private Sub UpdateTriggerPosition()
        'The range on the Z-axis is 0 to 65535.

        If IsPlayStation(ControllerNumber) = True Then
            'PS
            'What position is the right thumbstick in on the R-axis?
            If ControllerData.dwZpos <= NeutralStart Then
                'The right thumbstick is in the up position.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Left"
                Timer2.Start()

            ElseIf ControllerData.dwZpos >= NeutralEnd Then
                'The right thumbstick is in the down position.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Right"
                Timer2.Start()

            Else
                'The right thumbstick is in the neutral position.

                'LabelZaxis.Text = ""

            End If

        Else
            'XB
            'Is one of the Xbox triggers down?
            If ControllerData.dwZpos <= NeutralStart Then
                'The right trigger is down only.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Trigger"
                Timer2.Start()

            ElseIf ControllerData.dwZpos >= NeutralEnd Then
                'The left trigger is down only.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Trigger"
                Timer2.Start()

            Else
                'The triggers are either both up or down.

                'LabelZaxis.Text = ""

            End If

        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        LabelButtons.Text = ""

        LabelDPad.Text = ""

        LabelXaxis.Text = ""

        LabelYaxis.Text = ""

        LabelUaxis.Text = ""

        LabelRaxis.Text = ""

        LabelZaxis.Text = ""

        Timer2.Stop()

    End Sub

End Class
