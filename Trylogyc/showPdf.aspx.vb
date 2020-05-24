Public Class showPdf
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim url As String = "/_tmp/" & Session("filename")
        Dim output As String
        output = "<form id=form1 runat=server style=height:100%>" &
            "<iframe src='" & url & "'" & " style='width:100%;height:100%'</iframe>" & "</form>"
        Me.Controls.Add(New LiteralControl(output))
    End Sub

End Class