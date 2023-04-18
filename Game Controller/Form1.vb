'Game Controller
'
'This is an example application that demonstrates how to use Xbox and
'PlayStation controllers in VB.NET. It was written in 2023 and works on
'Windows 10 and 11. I’m currently working on a video that explains the code in
'more detail on my YouTube channel at https://www.youtube.com/@codewithjoe6074.
'
'MIT License
'Copyright(c) 2023 Joseph W. Lumbley
'
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

    Private ControllerNumber As Integer = 0

    Private Const PlayStation As Integer = 1356 'Manufacturer ID

    Private ReadOnly IsPlayStation(0 To 15) As Boolean

    <DllImport("winmm.dll", EntryPoint:="joyGetDevCapsW")>
    Private Shared Function joyGetDevCapsW(ByVal uJoyID As Integer, ByRef pjc As JOYCAPSW, ByVal cbjc As Integer) As UInteger
    End Function

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure JOYCAPSW
        Public wMid As UShort 'Manufacturer ID
        Public wPid As UShort
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public szPname As String
        Public wXmin As UInteger
        Public wXmax As UInteger
        Public wYmin As UInteger
        Public wYmax As UInteger
        Public wZmin As UInteger
        Public wZmax As UInteger
        Public wNumButtons As UInteger
        Public wPeriodMin As UInteger 'Smallest polling frequency supported in milliseconds.
        Public wPeriodMax As UInteger 'Largest polling frequency supported in milliseconds.
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

    Private ControllerCapabilities As New JOYCAPSW

    'The start of the thumbstick neutral zone.
    Private Const NeutralStart = 21845

    'The end of the thumbstick neutral zone.
    Private Const NeutralEnd = 43690

    Private ReadOnly Connected(0 To 15) As Boolean

    <DllImport("winmm.dll", EntryPoint:="joyGetPosEx")>
    Private Shared Function joyGetPosEx(ByVal uJoyID As Integer, ByRef pji As JOYINFOEX) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Public Structure JOYINFOEX
        Public dwSize As Integer
        Public dwFlags As Integer
        Public dwXpos As Integer 'Left Stick: Left / Right
        Public dwYpos As Integer 'Left Stick: Up / Down
        Public dwZpos As Integer 'Xbox Triggers: Up / Down - PlayStation Right Stick: Left / Right
        Public dwRpos As Integer 'Right Stick: Up / Down
        Public dwUpos As Integer 'Right Stick: Left / Right
        Public dwVpos As Integer
        Public dwButtons As Integer
        Public dwButtonNumber As Integer
        Public dwPOV As Integer 'D Pad
        Public dwReserved1 As Integer
        Public dwReserved2 As Integer
    End Structure

    Private ControllerPosition As JOYINFOEX

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeApp()

    End Sub

    Private Sub InitializeApp()

        Text = "Game Controller - Code with Joe"

        ClearLabels()

        InitializeControllerData()

        InitializeTimer1()

        InitializeTimer2()

    End Sub

    Private Sub InitializeTimer1()

        Timer1.Interval = 32 'Polling frequency in milliseconds.

        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        GetControllerData()

    End Sub

    Private Sub InitializeControllerData()

        ControllerPosition.dwSize = 64

        ControllerPosition.dwFlags = 255 ' Get all the data.

    End Sub

    Private Sub GetControllerData()

        For ControllerNumber = 0 To 15 'Up to 16 controllers

            Try

                If joyGetPosEx(ControllerNumber, ControllerPosition) = 0 Then

                    UpdateManufacturer()

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

    Private Sub UpdateManufacturer()

        If joyGetDevCapsW(ControllerNumber, ControllerCapabilities, Marshal.SizeOf(ControllerCapabilities)) = 0 Then

            If ControllerCapabilities.wMid = PlayStation Then

                IsPlayStation(ControllerNumber) = True

            Else

                IsPlayStation(ControllerNumber) = False

            End If

        End If

    End Sub

    Private Sub UpdateButtonPosition()

        If IsPlayStation(ControllerNumber) = False Then

            UpdateXboxButtonPosition()

        Else

            UpdatePlayStationButtonPosition()

        End If

    End Sub

    Private Sub UpdateXboxButtonPosition()
        'The range of buttons is 0 to 255.

        'What buttons are down?
        Select Case ControllerPosition.dwButtons
            Case 0 'All the buttons are up.
            Case 1 'A button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: A"
                Timer2.Start()
            Case 2 'B button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: B"
                Timer2.Start()
            Case 4 'X button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: X"
                Timer2.Start()
            Case 8 'Y button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Y"
                Timer2.Start()
            Case 16 'Left bumper is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Left Bumper"
                Timer2.Start()
            Case 32 'Right bumper is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Right Bumper"
                Timer2.Start()
            Case 64 'Back button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Back"
                Timer2.Start()
            Case 128 'Start button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Start"
                Timer2.Start()
            Case 3 'A and b buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+B"
                Timer2.Start()
            Case 5 'A and x buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+X"
                Timer2.Start()
            Case 9 'A and y buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: A+Y"
                Timer2.Start()
            Case 6 'B and x buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: B+X"
                Timer2.Start()
            Case 10 'B and y buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: B+Y"
                Timer2.Start()
            Case 12 'X and y buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: X+Y"
                Timer2.Start()
            Case 48 'Left and right bumpers are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Left+Right Bumpers"
                Timer2.Start()
            Case 192 'Back and start buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Back+Start"
                Timer2.Start()
        End Select

    End Sub

    Private Sub UpdatePlayStationButtonPosition()
        'The range of buttons is 0 to 255.

        'What buttons are down?
        Select Case ControllerPosition.dwButtons
            Case 0 'All the buttons are up.
            Case 1 'Square button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Square"
                Timer2.Start()
            Case 2 'X button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: X"
                Timer2.Start()
            Case 4 'Circle button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Circle"
                Timer2.Start()
            Case 8 'Triangle button is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Triangle"
                Timer2.Start()
            Case 16 'Left bumper is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Left Bumper"
                Timer2.Start()
            Case 32 'Right bumper is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Right Bumper"
                Timer2.Start()
            Case 64 'Left trigger is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Left Trigger"
                Timer2.Start()
            Case 128 'Right trigger is down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Button: Right Trigger"
                Timer2.Start()
            Case 3 'Square and x buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Square+X"
                Timer2.Start()
            Case 5 'Square and circle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Square+Circle"
                Timer2.Start()
            Case 9 'Square and triangle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Square+Triangle"
                Timer2.Start()
            Case 6 'X and circle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: X+Circle"
                Timer2.Start()
            Case 10 'X and triangle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: X+Triangle"
                Timer2.Start()
            Case 12 'Circle and triangle buttons are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Circle+Triangle"
                Timer2.Start()
            Case 48 'Left and right bumpers are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Left+Right Bumpers"
                Timer2.Start()
            Case 192 'Left and right triggers are down.
                LabelButtons.Text = "Controller: " & ControllerNumber.ToString & " Buttons: Left+Right Triggers"
                Timer2.Start()
        End Select

    End Sub

    Private Sub UpdateDPadPosition()
        'The range of POV is 0 to 65535.
        '0 through 31500 is used to represent the angle.
        'degrees = POV \ 100  315° = 31500 \ 100

        'What position is the D-Pad in?
        Select Case ControllerPosition.dwPOV
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

        End Select

    End Sub

    Private Sub UpdateLeftThumbstickPosition()
        'The range on the X-axis is 0 to 65535.
        'The range on the Y-axis is 0 to 65535.

        'What position is the left thumbstick in on the X-axis?
        If ControllerPosition.dwXpos <= NeutralStart Then
            'The left thumbstick is in the left position.

            LabelXaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Left"
            Timer2.Start()


        ElseIf ControllerPosition.dwXpos >= NeutralEnd Then
            'The left thumbstick is in the right position.

            LabelXaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Right"
            Timer2.Start()

        Else
            'The left thumbstick is in the neutral position.

        End If

        'What position is the left thumbstick in on the Y-axis?
        If ControllerPosition.dwYpos <= NeutralStart Then
            'The left thumbstick is in the up position.

            LabelYaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Up"
            Timer2.Start()

        ElseIf ControllerPosition.dwYpos >= NeutralEnd Then
            'The left thumbstick is in the down position.

            LabelYaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Thumbstick: Down"
            Timer2.Start()

        Else
            'The left thumbstick is in the neutral position.

        End If

    End Sub

    Private Sub UpdateRightThumbstickPosition()
        'The range on the U-axis is 0 to 65535.
        'The range on the R-axis is 0 to 65535.

        If IsPlayStation(ControllerNumber) = False Then

            'What position is the right thumbstick in on the U-axis?
            If ControllerPosition.dwUpos <= NeutralStart Then
                'The right thumbstick is in the left position.

                LabelUaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Left"
                Timer2.Start()

            ElseIf ControllerPosition.dwUpos >= NeutralEnd Then
                'The right thumbstick is in the right position.

                LabelUaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Right"
                Timer2.Start()

            Else
                'The right thumbstick is in the neutral position.

            End If

        End If

        'What position is the right thumbstick in on the R-axis?
        If ControllerPosition.dwRpos <= NeutralStart Then
            'The right thumbstick is in the up position.

            LabelRaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Up"
            Timer2.Start()

        ElseIf ControllerPosition.dwRpos >= NeutralEnd Then
            'The right thumbstick is in the down position.

            LabelRaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Down"
            Timer2.Start()

        Else
            'The right thumbstick is in the neutral position.

        End If

    End Sub

    Private Sub UpdateTriggerPosition()
        'The range on the Z-axis is 0 to 65535.

        If IsPlayStation(ControllerNumber) = True Then

            'What position is the right thumbstick in on the Z-axis?
            If ControllerPosition.dwZpos <= NeutralStart Then
                'The right thumbstick is in the left position.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Left"
                Timer2.Start()

            ElseIf ControllerPosition.dwZpos >= NeutralEnd Then
                'The right thumbstick is in the right position.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Thumbstick: Right"
                Timer2.Start()

            Else
                'The right thumbstick is in the neutral position.

            End If

        Else

            'Is one of the Xbox triggers down?
            If ControllerPosition.dwZpos <= NeutralStart Then
                'The right trigger is down only.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Right Trigger"
                Timer2.Start()

            ElseIf ControllerPosition.dwZpos >= NeutralEnd Then
                'The left trigger is down only.

                LabelZaxis.Text = "Controller: " & ControllerNumber.ToString & " Left Trigger"
                Timer2.Start()

            Else
                'The triggers are either both up or down.

            End If

        End If

    End Sub

    Private Sub InitializeTimer2()

        Timer2.Interval = 400 'Label display time in milliseconds.

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        ClearLabels()

        Timer2.Stop()

    End Sub

    Private Sub ClearLabels()

        LabelButtons.Text = ""

        LabelDPad.Text = ""

        LabelXaxis.Text = ""

        LabelYaxis.Text = ""

        LabelZaxis.Text = ""

        LabelUaxis.Text = ""

        LabelRaxis.Text = ""

    End Sub

End Class

'Learn more:
'
'Consuming Unmanaged DLL Functions
'https://learn.microsoft.com/en-us/dotnet/framework/interop/consuming-unmanaged-dll-functions
'
'DllImportAttribute.EntryPoint Field
'https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.dllimportattribute.entrypoint?view=net-7.0
'
'Passing Structures
'https://learn.microsoft.com/en-us/dotnet/framework/interop/passing-structures
'
'Strings used in Structures
'https://learn.microsoft.com/en-us/dotnet/framework/interop/default-marshalling-for-strings#strings-used-in-structures
'
'joyGetDevCapsW Function
'https://learn.microsoft.com/en-us/windows/win32/api/joystickapi/nf-joystickapi-joygetdevcapsw
'
'JOYCAPSW Structure
'https://learn.microsoft.com/en-us/windows/win32/api/joystickapi/ns-joystickapi-joycapsw
'
'joyGetPosEx Function
'https://learn.microsoft.com/en-us/windows/win32/api/joystickapi/nf-joystickapi-joygetposex
'
'JOYINFOEX Structure
'https://learn.microsoft.com/en-us/windows/win32/api/joystickapi/ns-joystickapi-joyinfoex
'
'Multimedia Input
'https://learn.microsoft.com/en-us/windows/win32/Multimedia/multimedia-input
'
'Windows Multimedia
'https://learn.microsoft.com/en-us/windows/win32/multimedia/windows-multimedia-start-page
'
'Reading Input Data From Joystick in Visual Basic
'https://social.msdn.microsoft.com/Forums/en-US/af28b35b-d756-4d87-94c6-ced882ab20a5/reading-input-data-from-joystick-in-visual-basic?forum=vbgeneral
