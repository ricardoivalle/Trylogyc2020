﻿Imports Trylogyc.DAL
Imports System.IO
Imports System.Net
Imports MercadoPago.Resources
Imports MercadoPago.DataStructures.Preference
Imports MercadoPago.Common
Imports System.Globalization
Imports System.Linq

Public Class _Default
    Inherits System.Web.UI.Page
    Dim myContext = New TrylogycContext
    Dim sumtotal As Double
    Public preferenceIDMP As String

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
                'Ocultar/Mostrar columna "Pagar"
                Dim displayPagar As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("displayPagar"))
                CType(GridView1.Columns.Cast(Of DataControlField)().Where(Function(fld) fld.HeaderText = "Pagar") _
                .SingleOrDefault(), DataControlField).Visible = displayPagar

            Else
                SelectedPreferenceId = String.Empty
                If Session("logueado") = True Then
                Else
                    Response.Redirect("~/login.aspx")
                End If
            End If
        Else
            Response.Redirect("~/login.aspx")
        End If

    End Sub


#Region "Eventos"
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
            'Crear 1 preferencia de pago por fila:

        End If
    End Sub
    Protected Sub gridview1_rowcommand(sender As Object, e As CommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "cmdPDF2") Then
            Dim index1 As Integer = Convert.ToInt32(e.CommandArgument) 'captura el valor del índice de la fila
            Dim row1 As GridViewRow = GridView1.Rows(index1) 'crea un objeto row que contiene la fila del botón presionado.
            Dim numfact As String = CType(row1.Cells(10), DataControlFieldCell).Text
            Dim filePDF As String = "~/PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            Dim filePDF2 As String = "PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            Try
                Dim myrequest As New WebClient()
                ' Establecer Credenciales para acceder a la carpeta
                myrequest.Credentials = New NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("ftpUser").ToString(), System.Configuration.ConfigurationManager.AppSettings("ftpPassWord").ToString())
                'Read the file data into a Byte array

                Dim myAddress As String = ""
                If System.Configuration.ConfigurationManager.AppSettings("ftpAddress").ToString().Length <1 Then
                    myAddress= Server.MapPath("/PDF/")
                Else
                    myAddress= System.Configuration.ConfigurationManager.AppSettings("ftpAddress").ToString()
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
        Else
            If (e.CommandName.Equals("btnPagar")) Then
                Dim index1 As Integer = Convert.ToInt32(e.CommandArgument) 'captura el valor del índice de la fila
                Dim row1 As GridViewRow = GridView1.Rows(index1) 'crea un objeto row que contiene la fila del botón presionado.
                Dim numfact As String = CType(row1.Cells(2), DataControlFieldCell).Text
                Dim importe As String = CType(row1.Cells(9), DataControlFieldCell).Text
                importeFactura = ConvertirCampoImporteADecimal(importe)
                Dim Master As Trylogyc = CType(Me.Master, Trylogyc)
                Dim idSocio As String = Convert.ToInt32(Session("xmlSocio")).ToString()
                Dim idConexion As String = CType(row1.Cells(10), DataControlFieldCell).Text
                Dim pagoEnProceso As Boolean = VerificarPagoEnProceso(idSocio, idConexion, numfact, importeFactura)
                If pagoEnProceso = True Then
                    Response.Redirect("~/ConfirmarPago.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & idSocio & "&idConexion=" & idConexion)
                Else
                    Response.Redirect("~/Pagar.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & idSocio & "&idConexion=" & idConexion)
                End If
            End If
        End If


    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
#End Region

#Region "Métodos Privados"

    Private Sub readFacturas(ByVal xmlsocio As String, ByVal conexion As Int32)
        Try
            Dim dvFacturas As New DataView(Session("dtSaldo"))
            Dim dtFacturas As New DataTable
            Dim dvdtFacturas As New DataTable
            Dim importeFactura As Decimal

            dtFacturas.Columns.Add("Nro_Factura")
            dtFacturas.Columns.Add("Pto_Venta")
            dtFacturas.Columns.Add("Letra")
            dtFacturas.Columns.Add("Periodo")
            dtFacturas.Columns.Add("Fecha_Emision")
            dtFacturas.Columns.Add("Fecha_Vto")
            dtFacturas.Columns.Add("Grupo_Fact")
            dtFacturas.Columns.Add("Importe")
            dtFacturas.Columns.Add("Factura")
            dtFacturas.Columns.Add("Conexion")
            dtFacturas.Columns.Add("Pagada")
            dtFacturas.Columns.Add("IdPreferenciaPago")

            If conexion = 0 Then
                'dvFacturas.RowFilter = "Socio =" & xmlsocio
                dvFacturas.Sort = "PERIODO DESC"
                dvdtFacturas = dvFacturas.ToTable
                For u = 0 To dvdtFacturas.Rows.Count - 1
                    dtFacturas.Rows.Add()
                    dtFacturas.Rows(u).Item("Nro_Factura") = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                    dtFacturas.Rows(u).Item("Pto_Venta") = dvdtFacturas.Rows(u).Item("Pto_Venta")
                    dtFacturas.Rows(u).Item("Letra") = dvdtFacturas.Rows(u).Item("Letra")
                    dtFacturas.Rows(u).Item("Periodo") = dvdtFacturas.Rows(u).Item("Periodo")
                    dtFacturas.Rows(u).Item("Fecha_Emision") = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                    dtFacturas.Rows(u).Item("Fecha_Vto") = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                    dtFacturas.Rows(u).Item("Grupo_Fact") = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                    dtFacturas.Rows(u).Item("Importe") = dvdtFacturas.Rows(u).Item("Importe")
                    dtFacturas.Rows(u).Item("Factura") = dvdtFacturas.Rows(u).Item("Factura")
                    dtFacturas.Rows(u).Item("Conexion") = dvdtFacturas.Rows(u).Item("Conexion")
                    dtFacturas.Rows(u).Item("Pagada") = dvdtFacturas.Rows(u).Item("Pagada")
                    'TODO: Para la fila 10, si la factura está en la tabla nueva a crear "Pagos", mostrar "Pago en proceso" y quizás darle un color.
                    sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                Next
                'Me.txtDeudatotal.Text = Format((sumtotal), "0.00")

            Else
                dvFacturas.RowFilter = "Socio =" & xmlsocio
                dvFacturas.RowFilter = "Conexion =" & conexion
                dvFacturas.Sort = "periodo DESC"
                dvdtFacturas = dvFacturas.ToTable
                For u = 0 To dvdtFacturas.Rows.Count - 1
                    dtFacturas.Rows.Add()
                    dtFacturas.Rows(u).Item("Nro_Factura") = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                    dtFacturas.Rows(u).Item("Pto_Venta") = dvdtFacturas.Rows(u).Item("Pto_Venta")
                    dtFacturas.Rows(u).Item("Letra") = dvdtFacturas.Rows(u).Item("Letra")
                    dtFacturas.Rows(u).Item("Periodo") = dvdtFacturas.Rows(u).Item("Periodo")
                    dtFacturas.Rows(u).Item("Fecha_Emision") = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                    dtFacturas.Rows(u).Item("Fecha_Vto") = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                    dtFacturas.Rows(u).Item("Grupo_Fact") = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                    dtFacturas.Rows(u).Item("Importe") = dvdtFacturas.Rows(u).Item("Importe")
                    dtFacturas.Rows(u).Item("Factura") = dvdtFacturas.Rows(u).Item("Factura")
                    dtFacturas.Rows(u).Item("Conexion") = dvdtFacturas.Rows(u).Item("Conexion")
                    dtFacturas.Rows(u).Item("Pagada") = "Información del Pago MP."
                    'TODO: Para la fila 10, si la factura está en la tabla nueva a crear "Pagos", mostrar "Pago en proceso" y quizás darle un color.
                    sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                Next


            End If

            Me.GridView1.DataSource = ""
            Me.GridView1.DataSource = dtFacturas
            Me.GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ReEvaluarYSetearMercadoPagoToken()
        If (MercadoPago.SDK.AccessToken Is Nothing) Then
            MercadoPago.SDK.SetAccessToken("TEST-803559796931547-060612-07b906157e8808e7d657beabf35fdc1d-229476782")
        End If
    End Sub
    Private Function GenerarPreferenciaPagoMercadoPago(nroFactura As String, importeFactura As Decimal) As String
        Dim importeAPagar As Decimal
        Dim myPreference As New Preference()
        myPreference.ClientId = "580430370"
        Dim itemToAdd As New Item()
        itemToAdd.Title = nroFactura
        itemToAdd.Quantity = 1
        itemToAdd.CurrencyId = CurrencyId.ARS
        itemToAdd.UnitPrice = importeFactura
        myPreference.Items.Add(itemToAdd)
        Dim _backUrls As New BackUrls()
        _backUrls.Success = Request.Url.GetLeftPart(UriPartial.Path) & "/PagoExitoso.aspx"
        _backUrls.Failure = Request.Url.GetLeftPart(UriPartial.Path) & "/PagoRechazado.aspx"
        _backUrls.Pending = Request.Url.GetLeftPart(UriPartial.Path) & "/PagoPendiente.aspx"
        myPreference.BackUrls = _backUrls
        myPreference.AutoReturn = AutoReturnType.approved
        myPreference.Save()
        GenerarPreferenciaPagoMercadoPago = myPreference.Id
        Return myPreference.Id
    End Function
    Private Function ConvertirCampoImporteADecimal(ByVal cell As Object) As Decimal
        Dim importeFactura As Decimal

        If cell Is Nothing Or cell Is DBNull.Value Then
            Session("txtError") = "Se produjo un error. Los montos de las facturas no tienen el formato correcto."
            Response.Redirect("~/Error.aspx")
        Else
            If (Decimal.TryParse(cell,
            NumberStyles.AllowDecimalPoint,
            CultureInfo.CreateSpecificCulture("fr-FR"),
            importeFactura) = False) Then
                Session("txtError") = "Se produjo un error. Los montos de las facturas no tienen el formato correcto."
                Response.Redirect("~/Error.aspx")
            End If
        End If

        ConvertirCampoImporteADecimal = importeFactura
        Return importeFactura
    End Function

    Private Function VerificarPagoEnProceso(ByVal idSocio As Int32, ByVal idConexion As Int32, ByVal numFact As String, ByVal importe As Decimal) As Boolean
        Dim myContext As New TrylogycContext
        Dim existePago As Boolean = myContext.GetPago(idSocio, idConexion, numFact, importe)
        Return existePago
    End Function
#End Region
End Class