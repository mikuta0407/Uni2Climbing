Option Explicit
Dim objWshShell
Dim yn

Set objWshShell = WScript.CreateObject("WScript.Shell")

yn = MsgBox("フルスクリーンモードで起動しますか?",vbYesNo,"Uni2Climbing")

If yn = vbYes Then objWshShell.Run "Uni2Climbing.exe -screen-fullscreen 1"
If yn = vbNo Then objWshShell.Run "Uni2Climbing.exe -screen-fullscreen 0"