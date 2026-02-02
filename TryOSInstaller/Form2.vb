Imports System.IO.Compression

Public Class Form2
    '1. Get Newst Version
    '2. Download TryOS Zip from Github
    '3. Extract TryOS Zip

    Public WayInSetup As Int64 = 0

    Public InstallPathInternet As String = "https://github.com/sebastian2007bro/TryOS/releases/download/TryOS_0.1.0.282/TryOS.0.1.0.282.zip"
    Public InstallPath As String = Nothing

    Public Sub GetNewstVersion()
        Try
            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\InstallPathInternet.txt") Then
                WayInSetup = 1
                ProgressBar1.Style = ProgressBarStyle.Marquee
                Label1.Text = "Info: Reading ""InstallPathInternet.txt"""
                InstallPathInternet = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\InstallPathInternet.txt")
                WaitPause.Start()
            Else
                WayInSetup = 1
                ProgressBar1.Style = ProgressBarStyle.Marquee
                Label1.Text = "Info: Getting newst version"
                Dim VersionPlace As String = ""
                Dim WebDownloader As New Net.WebClient
                WebDownloader.DownloadFile("https://raw.githubusercontent.com/sebastian2007bro/TryOSInstaller/refs/heads/main/UpdateData/UpdateURL.txt", My.Application.Info.DirectoryPath & "\Version.txt")
                VersionPlace = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\Version.txt")
                InstallPathInternet = VersionPlace
                My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Version.txt")
                WaitPause.Start()
            End If

        Catch ex1 As Net.WebException
            MsgBox("I think your internet isn't working right.")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetInstallFolder()
        WayInSetup = 2
        Label1.Text = "Info: Getting installation folder"
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            InstallPath = FolderBrowserDialog1.SelectedPath
            WaitPause.Start()
        Else
            MsgBox("Failed to give installation folder")
            End
        End If
    End Sub

    Public Sub DownloadTryOS()
        WayInSetup = 3
        Label1.Text = "Info: Downloading TryOS"
        Try
            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\InternetDownloader.exe") = False Then
                My.Computer.FileSystem.WriteAllBytes(My.Application.Info.DirectoryPath & "\InternetDownloader.exe", My.Resources.InternetDownloader, False)
            End If

            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\TryZip.zip") Then
                My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\TryZip.zip")
            End If

            Dim psi As New ProcessStartInfo(My.Application.Info.DirectoryPath & "\InternetDownloader.exe", "/Address:" & InstallPathInternet & " /fileName:" & My.Application.Info.DirectoryPath & "\TryZip.zip")
            psi.RedirectStandardOutput = True
            psi.UseShellExecute = False
            psi.CreateNoWindow = True
            Dim process As Process = Process.Start(psi)

            Dim output As String = process.StandardOutput.ReadToEnd()
            process.WaitForExit()

            If output.Contains("1") Then
            Else

            End If

            'Dim WebDownloader As New Net.WebClient
            'WebDownloader.DownloadFile(InstallPathInternet, InstallPath & "\TryZip.zip")
            WaitPause.Start()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ExtractTryOS()
        WayInSetup = 4
        Label1.Text = "Info: Extracting TryOS into it's installation folder"
        If My.Computer.FileSystem.FileExists(InstallPath & "\TryZip.zip") Then
            ZipFile.ExtractToDirectory(InstallPath & "\TryZip.zip", InstallPath)
            My.Computer.FileSystem.DeleteFile(InstallPath & "\TryZip.zip")
            WaitPause.Start()
        Else

        End If
    End Sub

    Public Sub CloseSetupProgram(DidProgramCloseLikeNormal As Boolean, Optional ex As String = Nothing)
        WayInSetup = 5
        If DidProgramCloseLikeNormal = True Then
            Label1.Text = "Info: Closing TryOS Setup"
            WaitPause.Start()
        ElseIf DidProgramCloseLikeNormal = False Then
            MsgBox(ex, MsgBoxStyle.Critical)
            End
        End If
    End Sub

    Private Sub WaitPause_Tick(sender As Object, e As EventArgs) Handles WaitPause.Tick
        If WayInSetup = 0 Then
            WaitPause.Stop()
            GetNewstVersion()
        ElseIf WayInSetup = 1 Then
            WaitPause.Stop()
            GetInstallFolder()
        ElseIf WayInSetup = 2 Then
            WaitPause.Stop()
            DownloadTryOS()
        ElseIf WayInSetup = 3 Then
            WaitPause.Stop()
            ExtractTryOS()
        ElseIf WayInSetup = 4 Then
            WaitPause.Stop()
            CloseSetupProgram(True)
        ElseIf WayInSetup = 5 Then
            End
        End If
    End Sub


End Class