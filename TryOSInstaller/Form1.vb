Imports System
Imports System.Net
Imports System.IO.Compression

Public Class Form1
    Public InstallPath As String = "https://github.com/sebastian2007bro/CV/releases/download/3.0.0.200/Sebs.SW.CV.3.0.zip"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Environment.CommandLine.Contains("/UseOldCV") Then
        Else
            Form2.Show()
            Close()
        End If

        Try
            Dim sd As String = ""
            Dim your_mom As New Net.WebClient
            your_mom.DownloadFile("https://raw.githubusercontent.com/sebastian2007bro/CV/master/Info/Version_Now.swfiles", My.Application.Info.DirectoryPath & "\Version")
            sd = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\Version")
            If sd.Contains("https://github.com/sebastian2007bro/CV/releases/download") = True Then
                InstallPath = sd
            End If
            My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\Version")
        Catch ex As Exception
            MsgBox("Is Internet working?")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
        Else

            'Try
            'TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1)
            'Catch ex As Exception
            'End Try
            If TextBox2.Text.Contains("https://github.com/sebastian2007bro/CV/releases/download") = True Then
                Try
                    Dim your_mom As New Net.WebClient
                    your_mom.DownloadFile(TextBox2.Text, TextBox1.Text & "\CVZip.zip")
                Catch ex As Exception

                End Try
                If My.Computer.FileSystem.FileExists(TextBox1.Text & "\CVZip.zip") Then
                    ZipFile.ExtractToDirectory(TextBox1.Text & "\CVZip.zip", TextBox1.Text & "\")
                Else
                    MsgBox("This Failed")
                    Exit Sub
                End If
            Else
                If TextBox2.Text = "" Then
                    Try
                        Dim your_mom As New Net.WebClient
                        your_mom.DownloadFile(InstallPath, TextBox1.Text & "\CVZip.zip")
                    Catch ex As Exception

                    End Try
                    If My.Computer.FileSystem.FileExists(TextBox1.Text & "\CVZip.zip") Then
                        ZipFile.ExtractToDirectory(TextBox1.Text & "\CVZip.zip", TextBox1.Text & "\")
                    Else
                        MsgBox("This Failed")
                        Exit Sub
                    End If
                End If
            End If

                'Installer()
            End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub
    Private Sub Installer()
        If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Apps") Then
            My.Computer.FileSystem.CreateDirectory("Apps")
            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Apps\picview.swfiles", "3.0.0", False)
            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Apps\quickedit.swfiles", "3.0.0", False)
            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Apps\swstore.swfiles", "3.0.0", False)
            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\Apps\swtaskp.swfiles", "3.0.0", False)
        End If
        If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Settings") Then
            My.Computer.FileSystem.CreateDirectory("Settings")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
        Else
            If My.Computer.FileSystem.FileExists(TextBox1.Text & "\CVZip.zip") Then
                ZipFile.ExtractToDirectory(TextBox1.Text & "\CVZip.zip", TextBox1.Text & "\")
            Else
                MsgBox("This Failed")
                Exit Sub
            End If
            Installer()
        End If
    End Sub
End Class
