Imports Trylogyc.DAL
Imports System.Xml
Imports System.Linq
Imports System.IO
Imports System.Net

Public Class _Default
    Inherits System.Web.UI.Page
    Dim myContext = New TrylogycContext
    Dim sumtotal As Double
    Protected Sub page_preinit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Session("logueado") = True Then
        Else
            Response.Redirect("~/login.aspx")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("logueado") = True Then
            Me.divError.Visible = False
            If Not Page.IsPostBack Then

                Dim daUser As DataSet = myContext.GetUsuario(Session("IDUsuario"))
                If daUser.Tables(0).TableName = "Error" Then
                    Session("codError") = daUser.Tables(0).Rows(0).Item(0)
                    Session("txtError") = daUser.Tables(0).Rows(0).Item(1)
                    Response.Redirect("~/Error.aspx")
                Else
                    ''traer listado de socio-conexion de la tabla
                    'traer listado de conexiones para el socio
#Region "NUEVO METODO"


                    lstConexiones.Items.Insert(0, " Todas")
                    lstConexiones.Items.Item(0).Value = 0
                    Dim dtSocio As DataTable = Session("dtSocio")

                    'Llenar el Combo
                    Dim lenSocio = 6 - Len(Session("xmlSocio").ToString)
                    Dim strSocio As String = ""
                    For s = 0 To lenSocio - 1
                        strSocio = "0" & strSocio
                    Next
                    strSocio = strSocio & Session("xmlSocio").ToString

                    For r = 0 To dtSocio.Rows.Count - 1
                        Dim miItem As New ListItem
                        Dim itConexion = dtSocio.Rows(r).Item("Conexion")
                        Dim itcnt As Int32
                        Dim strConexion As String = ""
                        Dim lenConexion = 4 - Len(itConexion.ToString)
                        For t = 0 To lenConexion - 1
                            strConexion = "0" & strConexion
                        Next
                        strConexion = strConexion & itConexion.ToString
                        itcnt = lstConexiones.Items.Count
                        miItem.Value = itConexion
                        miItem.Text = strSocio & "-" & strConexion
                        lstConexiones.Items.Insert(r + 1, miItem)
                        lstConexiones.Items.Item(r + 1).Value = itConexion
                        lstConexiones.Items.Item(r + 1).Text = strSocio & "-" & strConexion
                    Next r
                    lstConexiones.SelectedValue = 0
                    readFacturas(Session("xmlSocio"), lstConexiones.SelectedValue)
                    'Me.txtDeudatotal.Text = Format((sumtotal), "0.00")
#End Region
                End If
                GridView1.DataBind()
            Else
                If Session("logueado") = True Then
                Else
                    Response.Redirect("~/login.aspx")
                End If
            End If
        Else
            Response.Redirect("~/login.aspx")
        End If

    End Sub
    Private Sub readFacturas(ByVal xmlsocio As String, ByVal conexion As Int32)
        Try
            Dim dvFacturas As New DataView(Session("dtSaldo"))
            Dim dtFacturas As New DataTable
            Dim dvdtFacturas As New DataTable
            For t = 0 To 10
                dtFacturas.Columns.Add()
            Next
            If conexion = 0 Then
                'dvFacturas.RowFilter = "Socio =" & xmlsocio
                dvFacturas.Sort = "PERIODO DESC"
                dvdtFacturas = dvFacturas.ToTable
                For u = 0 To dvdtFacturas.Rows.Count - 1
                    dtFacturas.Rows.Add()
                    dtFacturas.Rows(u).Item(0) = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                    dtFacturas.Rows(u).Item(1) = dvdtFacturas.Rows(u).Item("Pto_Venta")
                    dtFacturas.Rows(u).Item(2) = dvdtFacturas.Rows(u).Item("Letra")
                    dtFacturas.Rows(u).Item(3) = dvdtFacturas.Rows(u).Item("Periodo")
                    dtFacturas.Rows(u).Item(4) = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                    dtFacturas.Rows(u).Item(5) = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                    dtFacturas.Rows(u).Item(6) = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                    dtFacturas.Rows(u).Item(7) = dvdtFacturas.Rows(u).Item("Importe")
                    dtFacturas.Rows(u).Item(8) = dvdtFacturas.Rows(u).Item("Factura")
                    dtFacturas.Rows(u).Item(9) = dvdtFacturas.Rows(u).Item("Conexion")
                    dtFacturas.Rows(u).Item(10) = "Información del Pago MP."
                    'TODO: Para la fila 10, si la factura está en la tabla nueva a crear "Pagos", mostrar "Pago en proceso" y quizás darle un color.
                    sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                Next
                'Me.txtDeudatotal.Text = Format((sumtotal), "0.00")
                Me.GridView1.DataSource = ""
                Me.GridView1.DataSource = dtFacturas
                Me.GridView1.DataBind()
                'Me.GridView1.Columns(9).Visible = False
                'Me.GridView1.Columns(10).Visible = False
            Else
                dvFacturas.RowFilter = "Socio =" & xmlsocio
                dvFacturas.RowFilter = "Conexion =" & conexion
                dvFacturas.Sort = "periodo DESC"
                dvdtFacturas = dvFacturas.ToTable
                For u = 0 To dvdtFacturas.Rows.Count - 1
                    dtFacturas.Rows.Add()
                    dtFacturas.Rows(u).Item(0) = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                    dtFacturas.Rows(u).Item(1) = dvdtFacturas.Rows(u).Item("Pto_Venta")
                    dtFacturas.Rows(u).Item(2) = dvdtFacturas.Rows(u).Item("Letra")
                    dtFacturas.Rows(u).Item(3) = dvdtFacturas.Rows(u).Item("Periodo")
                    dtFacturas.Rows(u).Item(4) = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                    dtFacturas.Rows(u).Item(5) = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                    dtFacturas.Rows(u).Item(6) = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                    dtFacturas.Rows(u).Item(7) = dvdtFacturas.Rows(u).Item("Importe")
                    dtFacturas.Rows(u).Item(8) = dvdtFacturas.Rows(u).Item("Factura")
                    dtFacturas.Rows(u).Item(9) = dvdtFacturas.Rows(u).Item("Conexion")
                    dtFacturas.Rows(u).Item(10) = "Información del Pago MP."
                    'TODO: Para la fila 10, si la factura está en la tabla nueva a crear "Pagos", mostrar "Pago en proceso" y quizás darle un color.
                    sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                Next
                Me.GridView1.DataSource = ""
                Me.GridView1.DataSource = dtFacturas
                Me.GridView1.DataBind()
                'Me.GridView1.Columns(9).Visible = False
                'Me.GridView1.Columns(10).Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lstConexiones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstConexiones.SelectedIndexChanged
        Me.divError.Visible = False
        Dim intSocio As Int32
        Dim intConexion As Int32
        intSocio = Session("xmlSocio")
        intConexion = lstConexiones.SelectedValue
        readFacturas(intSocio, intConexion)
        Me.GridView1.Caption = "Facturas Conexión " & lstConexiones.SelectedItem.ToString
        Dim dv As DataView = New DataView(Session("dtsocio"))
        For Each row As DataRowView In dv
            If row("CONEXION") = intConexion Then
                Me.txtNombre.Text = row("NOMBRE")
                Me.txtDireccion.Text = row("DIRECION")
                Me.txtLocalidad.Text = row("LOCALIDAD")
                'Me.txtDeudatotal.Text = Format((sumtotal), "0.00")
                Exit For
            End If
        Next
        lstConexiones.Focus()
    End Sub
    Protected Sub cmdPDF_Click(sender As Object, e As ImageClickEventArgs)

    End Sub
    Protected Sub cmdPDF2_Click(sender As Object, e As ImageClickEventArgs)

    End Sub

    Protected Sub gridview1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType <> DataControlRowType.Footer And GridView1.Rows.Count > 0 Then

            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = "Imprime"
            e.Row.Cells(1).Text = "Pagar"
            e.Row.Cells(2).Text = "Factura"
            e.Row.Cells(3).Text = "Pto Venta"
            e.Row.Cells(4).Text = "Letra"
            e.Row.Cells(5).Text = "Período"
            e.Row.Cells(6).Text = "Fecha Emisión"
            e.Row.Cells(7).Text = "Fecha Vto"
            e.Row.Cells(8).Text = "Grupo"
            e.Row.Cells(9).Text = "Importe"
            e.Row.Cells(10).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Text = "Info Pago"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(9).CssClass = "badge bg-red money"
            e.Row.Cells(10).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
        End If
    End Sub
    Protected Sub gridview1_rowcommand(sender As Object, e As CommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "cmdPDF2") Then
            Dim index1 As Integer = Convert.ToInt32(e.CommandArgument) 'captura el valor del índice de la fila
            Dim row1 As GridViewRow = GridView1.Rows(index1) 'crea un objeto row que contiene la fila del botón presionado.
            Dim numfact As String = CType(row1.Cells(9), DataControlFieldCell).Text
            Dim filePDF As String = "~/PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            Dim filePDF2 As String = "PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            Try
                Dim myrequest As New WebClient()
                ' Establecer Credenciales para acceder a la carpeta
                myrequest.Credentials = New NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("ftpUser").ToString(), System.Configuration.ConfigurationManager.AppSettings("ftpPassWord").ToString())
                'Read the file data into a Byte array

                Dim myAddress As String = ""
                If System.Configuration.ConfigurationManager.AppSettings("ftpAddress").ToString().Length < 1 Then
                    myAddress = Server.MapPath("/PDF/")
                Else
                    myAddress = System.Configuration.ConfigurationManager.AppSettings("ftpAddress").ToString()
                End If
                If (Not System.IO.Directory.Exists(Server.MapPath("~/_tmp"))) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/_tmp"))
                End If

#Region "Prueba Ricardo FtpWebRequest, funciona OK localmente, igual que el caso siguiente."
                'PRUEBA RICARDO
                'Dim _request As System.Net.FtpWebRequest = System.Net.WebRequest.Create(myAddress)
                Dim _request As Net.FtpWebRequest = Net.FtpWebRequest.Create(myAddress & LTrim(RTrim(numfact)) & ".pdf")
                _request.KeepAlive = False
                _request.UsePassive = True
                _request.Timeout = 6000000
                _request.Method = System.Net.WebRequestMethods.Ftp.DownloadFile
                _request.Credentials = New NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("ftpUser").ToString(), System.Configuration.ConfigurationManager.AppSettings("ftpPassWord").ToString())
                Dim _response As System.Net.FtpWebResponse = _request.GetResponse()
                Using fs As FileStream = File.Create(HttpContext.Current.Server.MapPath("/_tmp/") & LTrim(RTrim(numfact)) & ".pdf")
                    _response.GetResponseStream.CopyTo(fs)
                End Using
                'Dim responseStream As System.IO.Stream = _response.GetResponseStream().CopyTo(File.Create())

                'Dim bytes() As Byte = myrequest.DownloadData(myAddress & LTrim(RTrim(numfact)) & ".pdf")
                'Dim DownloadStream As FileStream = System.IO.File.Create(HttpContext.Current.Server.MapPath("~/_tmp/") & LTrim(RTrim(numfact)) & ".pdf")
                'DownloadStream.Write(bytes, 0, bytes.Length)
                'DownloadStream.Close()
                ''Dim fs As New System.IO.FileStream(HttpContext.Current.Server.MapPath("~/_tmp/") & LTrim(RTrim(numfact)) & ".pdf", System.IO.FileMode.Create)
                ''responseStream.CopyTo(fs)
                ''responseStream.Close()
                '_response.Close()
                'FIN PRUEBA RICARDO
#End Region
#Region "Descomentar, funcionaba OK, no era por esto"
                'INICIO DESCOMENTAR
                'Dim bytes() As Byte = myrequest.DownloadData(myAddress & LTrim(RTrim(numfact)) & ".pdf")
                '''  Crear FileStream para leer el archivo
                'Dim DownloadStream As FileStream = System.IO.File.Create(HttpContext.Current.Server.MapPath("~/_tmp/") & LTrim(RTrim(numfact)) & ".pdf")
                '''  Stream al archivo
                'DownloadStream.Write(bytes, 0, bytes.Length)
                'DownloadStream.Close()
                'FIN DESCOMENTAR
#End Region
#Region "Prueba WebRequest"
                'Dim objResponse As WebResponse
                'Dim objRequest As WebRequest
                'Dim result As String
                'objRequest = System.Net.HttpWebRequest.Create("http://www.capsunchales.com/PDF/" & LTrim(RTrim(numfact)) & ".pdf")
                'objRequest.Credentials = New NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("ftpUser").ToString(), System.Configuration.ConfigurationManager.AppSettings("ftpPassWord").ToString())
                'objResponse = objRequest.GetResponse()
                'Dim remoteStream As Stream = Nothing
                'Dim localStream As Stream = Nothing
                'localStream = File.Create(Server.MapPath("~/_tmp/" & LTrim(RTrim(numfact)) & ".pdf"))
                'Dim sr As New StreamReader(objResponse.GetResponseStream())
                'Dim stream As Stream = objResponse.GetResponseStream()
                'Dim buffer As Byte() = New Byte(1023) {}
                'Dim bg1 As Int64 = buffer.Length
                'Dim bytesread As Byte = stream.Read(buffer, 0, bg1)
                'localStream.Write(buffer, 0, bytesread)
                ''result = sr.ReadToEnd()
                ''Dim mst As MemoryStream = sr.ReadToEnd().ToArray
                ''HttpContext.Current.Response.ClearContent()
                ''HttpContext.Current.Response.ClearHeaders()
                ''HttpContext.Current.Response.ContentType = "application/pdf"
                ''HttpContext.Current.Response.OutputStream.Write(mst.ToArray(), 0, mst.ToArray().Length)
                ''HttpContext.Current.Response.Flush()
                ''HttpContext.Current.Response.Close()

                'sr.Close()

                '' Create a request for the URL. 
                'Dim request As WebRequest = WebRequest.Create("http://www.capsunchales.com/PDF/" & LTrim(RTrim(numfact)) & ".pdf")
                '' If required by the server, set the credentials.
                'request.Credentials = New NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("ftpUser").ToString(), System.Configuration.ConfigurationManager.AppSettings("ftpPassWord").ToString())
                '' Get the response.
                'Dim response As WebResponse = request.GetResponse()
                'Dim outFile As String = "c:\google.pdf"
                '' Get the stream containing content returned by the server.
                'Dim dataStream As Stream = response.GetResponseStream()
                '' Open the stream using a StreamReader for easy access.
                'Dim reader As New StreamReader(dataStream)
                '' Read the content.
                'Dim responseFromServer As String = reader.ReadToEnd()
                '' Display the content.
                'Console.WriteLine(responseFromServer)
                '' Clean up the streams and the response.
                'reader.Close()
                'response.Close()
#End Region

                Session("filename") = LTrim(RTrim(numfact)) & ".pdf"
                Response.Redirect("~/showPdf.aspx")
            Catch ex As Exception
                Me.lblError.Text = "No se pudo recuperar el documento PDF."
                Me.divError.Visible = True
            End Try

        End If


    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class