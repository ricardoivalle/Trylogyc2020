﻿Imports Trylogyc.Models
Imports Trylogyc.DAL
Imports System.Net
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Net.WebRequestMethods
Imports System.Xml

Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("logueado") = False
        Dim directoryName As String = Server.MapPath("/_tmp")
        If (System.IO.Directory.Exists(Server.MapPath("~/_tmp"))) Then
            For Each deleteFile In Directory.GetFiles(directoryName, "*.*", SearchOption.TopDirectoryOnly)
                System.IO.File.Delete(deleteFile)
            Next
        End If

    End Sub
    Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Dim myContext As New TrylogycContext
        Dim daUser As DataSet = myContext.GetUsuarioNombre(Me.txtemail.Text)
        Dim myUser = New Usuario
        If daUser IsNot Nothing Then
            If daUser.Tables(0).TableName = "Error" Then
                lblError.Text = "Error: " & daUser.Tables(0).Rows(0).Item(0)
            Else
                If daUser.Tables(0).Rows.Count > 0 Then 'Existe un usuario para dicho codigo de socio
                    myUser.username = daUser.Tables(0).Rows(0).Item("userName")
                    myUser.passWord = daUser.Tables(0).Rows(0).Item("userPass")
                    myUser.IDUsuario = daUser.Tables(0).Rows(0).Item("IDUsuario")
                    myUser.XmlSocio = daUser.Tables(0).Rows(0).Item("xmlSocio")
                    myUser.email = daUser.Tables(0).Rows(0).Item("userEmail")
                    myUser.foto = daUser.Tables(0).Rows(0).Item("foto")
                    myUser.ruta = daUser.Tables(0).Rows(0).Item("ruta")
                    If daUser.Tables(0).Rows(0).Item("EnviaFacturaMail") IsNot DBNull.Value AndAlso Convert.ToBoolean(daUser.Tables(0).Rows(0).Item("EnviaFacturaMail")) = True Then
                        myUser.EnviarFacturaEmail = True
                    Else
                        myUser.EnviarFacturaEmail = False
                    End If

                    If myUser.username.ToLower = Me.txtemail.Text.ToLower And myUser.passWord = Me.txtpassword.Text Then 'la contraseña coincide
                        Session("logueado") = True
                        Session("IDUsuario") = myUser.IDUsuario
                        Session("xmlSocio") = myUser.XmlSocio
                        Session("nomUsuario") = myUser.username
                        Session("userEmail") = myUser.email
                        Session("aceptaFacturaMail") = myUser.EnviarFacturaEmail

                        ' aca me fijo que conexiones tiene realcionadas. Esto no estaba agrego Marcelo
                        Dim daConexiones As DataSet = myContext.GetSocioConexion(myUser.IDUsuario)
                        Dim filtroConexiones As String = "", I As Integer = 0
                        If daConexiones IsNot Nothing Then
                            If daConexiones.Tables(0).TableName = "Error" Then
                                lblError.Text = "Error: " & daUser.Tables(0).Rows(0).Item(0)
                            Else
                                If daConexiones.Tables(0).Rows.Count > 0 Then
                                    For I = 0 To daConexiones.Tables(0).Rows.Count - 1
                                        If filtroConexiones = "" Then
                                            filtroConexiones = "CONEXION = " & daConexiones.Tables(0).Rows(I)("idConexion").ToString & " OR "
                                        Else
                                            filtroConexiones = filtroConexiones & "CONEXION = " & daConexiones.Tables(0).Rows(I)("idConexion").ToString & " OR "
                                        End If
                                    Next
                                End If
                                If filtroConexiones <> "" Then filtroConexiones = Left(filtroConexiones, Len(filtroConexiones) - 4)
                            End If
                        End If
                        '----------------------------------------------------------------------------------

                        Dim xmlFile2 As XmlReader = XmlReader.Create(Server.MapPath("/xmlfiles/SALDOS.xml"), New XmlReaderSettings())
                        Dim ds2 As New DataSet()
                        ds2.ReadXml(xmlFile2)
                        Dim dtSaldos As DataTable = ds2.Tables(0)
                        Dim dvSaldos As New DataView(dtSaldos)
                        dvSaldos.RowFilter = "Socio = " & Session("xmlSocio") & " AND (" & filtroConexiones & ")"
                        Session("dtSaldo") = dvSaldos.ToTable
                        Dim ds As New DataSet()
                        Dim xmlFile = XmlReader.Create(Server.MapPath("/xmlfiles/SOCIOS.xml"), New XmlReaderSettings())
                        ds.ReadXml(xmlFile)
                        'ds2.ReadXml(xmlFile2)
                        Dim dtSocios As DataTable = ds.Tables(0)
                        Dim dvSocios As New DataView(dtSocios)
                        dvSocios.RowFilter = "SOCIO = '" & Session("xmlSocio") & "' AND (" & filtroConexiones & ")"
                        Dim conCount As Int32 = dvSocios.Count
                        Session("conCount") = conCount
                        Session("dtSocio") = dvSocios.ToTable
                        Response.Redirect(myUser.ruta)
                    Else
                        lblError.Text = "Datos de Ingreso erróneos"
                        lblError.Visible = True
                    End If
                Else  'Chequear si el codigo de socio ingresado existe en listado de socios, caso afirmativo redireccionar a registro.
                    lblError.Text = "No existe Usuario"
                    lblError.Visible = True
                End If
            End If


        End If


    End Sub

    'Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
    '    Dim ex1 As New Exception
    '    Dim myByte As Byte() = DownloadSingleFile("ftp://ftp.w1181780.ferozo.com/public_html/PDF/",
    '                                                "w1181780@w1181780.ferozo.com", "RUdilisu40", "0000010001000002449953.pdf",
    '                                                "C:\Intel\", False, ex1)



    '    Dim myContext As New TrylogycContext
    '    Dim daUser As DataSet = myContext.GetUsuarioNombre(Me.txtemail.Text)
    '    Dim myUser = New Usuario
    '    If daUser IsNot Nothing Then
    '        If daUser.Tables(0).TableName = "Error" Then
    '            lblError.Text = "Error: " & daUser.Tables(0).Rows(0).Item(0)
    '        Else
    '            If daUser.Tables(0).Rows.Count > 0 Then 'Existe un usuario para dicho codigo de socio
    '                myUser.username = daUser.Tables(0).Rows(0).Item(3)
    '                myUser.passWord = daUser.Tables(0).Rows(0).Item(4)
    '                myUser.IDUsuario = daUser.Tables(0).Rows(0).Item(0)
    '                myUser.XmlSocio = daUser.Tables(0).Rows(0).Item(1)
    '                myUser.email = daUser.Tables(0).Rows(0).Item(2)
    '                myUser.foto = daUser.Tables(0).Rows(0).Item(5)
    '                myUser.ruta = daUser.Tables(0).Rows(0).Item(6)
    '                If myUser.username.ToLower = Me.txtemail.Text.ToLower And myUser.passWord = Me.txtpassword.Text Then 'la contraseña coincide
    '                    Session("logueado") = True
    '                    Session("IDUsuario") = myUser.IDUsuario
    '                    Session("xmlSocio") = myUser.XmlSocio
    '                    Session("nomUsuario") = myUser.username
    '                    Response.Redirect(myUser.ruta)
    '                Else
    '                    lblError.Text = "Datos de Ingreso erróneos"
    '                    lblError.Visible = True
    '                End If
    '            Else  'Chequear si el codigo de socio ingresado existe en listado de socios, caso afirmativo redireccionar a registro.
    '                lblError.Text = "No existe Usuario"
    '                lblError.Visible = True
    '            End If
    '        End If


    '    End If


    'End Sub

    'Public Shared Function DownloadSingleFile(ftpAddress As String,
    '                                      ftpUser As String,
    '                                      ftpPassword As String,
    '                                      fileToDownload As String,
    '                                      downloadTargetFolder As String,
    '                                      deleteAfterDownload As Boolean,
    '                                      ExceptionInfo As Exception) As Byte()

    '    Dim FileDownloaded As Boolean = False
    '    Dim Request As FtpWebRequest = DirectCast(WebRequest.Create(Convert.ToString(ftpAddress & fileToDownload)), FtpWebRequest)
    '    Request.Method = WebRequestMethods.Ftp.DownloadFile
    '    Request.Credentials = New NetworkCredential(ftpUser, ftpPassword)
    '    Dim Response As FtpWebResponse = DirectCast(Request.GetResponse(), FtpWebResponse)

    '    Dim responseStream As Stream = Response.GetResponseStream()
    '    Dim Reader As StreamReader = New StreamReader(responseStream)
    '    Dim fileContents As Byte() = Encoding.UTF8.GetBytes(Reader.ReadToEnd())
    '    'Dim fileContents As Byte() = Encoding.Default.GetBytes(Reader.ReadToEnd())
    '    ' Create the file. 
    '    'File.WriteAllBytes((HttpContext.Current.Server.MapPath("/_tmp") & "/0000010001000002449953.pdf"), fileContents)
    '    Dim myDocument As Document = New Document(PageSize.LETTER)
    '    'PdfWriter.GetInstance(myDocument, New FileStream(HttpContext.Current.Server.MapPath("/_tmp") & "/0000010001000002449953.pdf", FileMode.Create))
    '    myDocument.Open()
    '    myDocument.Add(New Paragraph(Encoding.UTF8.GetString(fileContents)))
    '    myDocument.OpenDocument()
    '    myDocument.Open()
    '    myDocument.Close()

    '    Dim myrequest As New WebClient()
    '    ' Confirm the Network credentials based on the user name and password passed in.
    '    myrequest.Credentials = New NetworkCredential(ftpUser, ftpPassword)
    '    'Read the file data into a Byte array
    '    Dim bytes() As Byte = myrequest.DownloadData(ftpAddress & fileToDownload)
    '    Try
    '        '  Create a FileStream to read the file into
    '        Dim DownloadStream As FileStream = System.IO.File.Create(HttpContext.Current.Server.MapPath("/_tmp") & "/0000010001000002449953.pdf")
    '        '  Stream this data into the file
    '        DownloadStream.Write(bytes, 0, bytes.Length)

    '        '  Close the FileStream
    '        DownloadStream.Close()
    '        HttpContext.Current.Session("filename") = "0000010001000002449953.pdf"
    '        HttpContext.Current.Response.Redirect("~/showPdf.aspx")

    '    Catch ex As Exception

    '    End Try


    '    'Guardar en carpeta TMP y luego hacer un redirect a un nuevo webform con un iframe que muestre el pdf descargado.

    '    Dim pdfReader As PdfReader = New PdfReader(fileContents)
    '    Dim stream As MemoryStream = New MemoryStream()
    '    Dim stamper As PdfStamper = New PdfStamper(pdfReader, stream)
    '    pdfReader.Close()
    '    'stamper.Close()
    '    stream.Flush()
    '    stream.Close()
    '    Dim pdfByte As Byte() = stream.ToArray()

    '    HttpContext.Current.Response.Clear()
    '    HttpContext.Current.Response.ClearHeaders()
    '    HttpContext.Current.Response.ClearContent()
    '    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    'HttpContext.Current.Response.ContentEncoding = Reader.CurrentEncoding
    '    HttpContext.Current.Response.ContentType = "application/pdf"
    '    ' HttpContext.Current.Response.AddHeader("Content-Disposition", "inline filename=" & "0000010001000002449953.pdf")
    '    HttpContext.Current.Response.AddHeader("Content-Length", pdfByte.Length)
    '    HttpContext.Current.Response.BinaryWrite(stream.ToArray)
    '    HttpContext.Current.Response.Flush()
    '    HttpContext.Current.Response.End()


    'End Function


    'Private Sub SendFileToClient()


    'End Sub
End Class