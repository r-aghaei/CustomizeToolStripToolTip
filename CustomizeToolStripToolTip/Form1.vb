Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupCustomDraw(MenuStrip1)
        SetupCustomDraw(ToolStrip1)
    End Sub
    Sub SetupCustomDraw(MyToolStrip As ToolStrip)
        Dim MyField = MyToolStrip.GetType().GetProperty("ToolTip",
            Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        Dim MyToolTip = CType(MyField.GetValue(MyToolStrip), ToolTip)
        MyToolTip.OwnerDraw = True
        MyToolTip.BackColor = Color.Red
        MyToolTip.ForeColor = Color.White
        AddHandler MyToolTip.Popup,
            Sub(obj, args)
                args.ToolTipSize = New Size(args.ToolTipSize.Width * 2, args.ToolTipSize.Height * 2)
            End Sub
        AddHandler MyToolTip.Draw,
            Sub(obj, args)
                Dim F = New Font(MyToolStrip.Font.FontFamily, MyToolStrip.Font.Size * 2)
                args.DrawBackground()
                TextRenderer.DrawText(args.Graphics, args.ToolTipText,
                    F, args.Bounds, MyToolTip.ForeColor)
                args.DrawBorder()
            End Sub

        'Passing each sub menu to the function
        For Each mnuItem As ToolStripMenuItem In MyToolStrip.Items.OfType(Of ToolStripMenuItem)
            If mnuItem.DropDown IsNot Nothing Then
                SetupCustomDraw(mnuItem.DropDown)
            End If
        Next
    End Sub
End Class
