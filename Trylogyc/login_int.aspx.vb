Imports System.Xml
Public Class Login_Int
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("logueado") = False
        Me.lblError.Visible = False
    End Sub

    Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click

        Dim ds As New DataSet()
        Dim ds2 As New DataSet()
        Dim xmlFile = XmlReader.Create(Server.MapPath("/xmlfiles/SOCIOS.xml"), New XmlReaderSettings())
        ds.ReadXml(xmlFile)
        'ds2.ReadXml(xmlFile2)
        Dim dtSocios As DataTable = ds.Tables(0)

        Try
            Dim usrInput As String = Me.HiddenField1.Value
            Dim intSocio As Int32
            Dim encontrado As Boolean = False
            Dim dvSocios As New DataView(dtSocios)

            If DropDownList1.SelectedValue = 0 Then
                Dim dni = Convert.ToString(Me.HiddenField1.Value)
                'dv.Sort = "Socio ASC, Conexion ASC"
                dvSocios.RowFilter = "Documento = '" & dni.ToString & "'"
                Session("dtSocio") = dvSocios.ToTable
                If Session("dtSocio").Rows.Count > 0 Then
                    For r = 0 To Session("dtSocio").Rows.Count - 1
                        If Session("dtSocio").Rows(r).Item("Documento") = dni Then
                            encontrado = True
                            Session("xmlSocio") = Session("dtSocio").Rows(r).Item("Socio")
                            Session("conexiones") = Session("dtSocio").Rows.Count
                            Dim xmlFile2 As XmlReader = XmlReader.Create(Server.MapPath("/xmlfiles/SALDOS.xml"), New XmlReaderSettings())
                            ds2.ReadXml(xmlFile2)
                            Dim dtSaldos As DataTable = ds2.Tables(0)
                            Dim dvSaldos As New DataView(dtSaldos)
                            dvSaldos.RowFilter = "Socio = " & Session("xmlSocio")
                            Session("dtSaldo") = dvSaldos.ToTable
                            Exit For
                        End If
                    Next
                Else
                    encontrado = False
                End If

                If encontrado = True Then
                    Session("logueado") = True
                Else
                    Me.lblError.Visible = True
                    Me.lblError.Text = "No se ha encontrado socio con ese DNI"
                    Response.Write("<script type='text/javascript'>alert('No se encontró socio con ese DNI');</script>")
                End If



            Else
                If Len(usrInput) < 1 Then
                    Me.lblError.Visible = True
                    Me.lblError.Text = "No se ha encontrado socio con ese nro."
                    Response.Write("<script type='text/javascript'>alert('No se encontró socio con ese número');</script>")
                Else
                    intSocio = Convert.ToInt32(Me.HiddenField1.Value)
                    dvSocios.RowFilter = "Socio = " & intSocio.ToString
                    Session("dtSocio") = dvSocios.ToTable
                    If Session("dtSocio").Rows.Count > 0 Then
                        For r = 0 To Session("dtSocio").Rows.Count - 1
                            If Session("dtSocio").Rows(r).Item("Socio") = intSocio Then
                                encontrado = True
                                Session("xmlSocio") = Session("dtSocio").Rows(r).Item("Socio")
                                Session("conexiones") = Session("dtSocio").Rows.Count
                                Dim xmlFile2 As XmlReader = XmlReader.Create(Server.MapPath("/xmlfiles/SALDOS.xml"), New XmlReaderSettings())
                                ds2.ReadXml(xmlFile2)
                                Dim dtSaldos As DataTable = ds2.Tables(0)
                                Dim dvSaldos As New DataView(dtSaldos)
                                dvSaldos.RowFilter = "Socio = " & Session("xmlSocio")
                                Session("dtSaldo") = dvSaldos.ToTable
                                Exit For
                            End If
                        Next
                    Else
                        encontrado = False
                    End If
                    If encontrado = True Then
                        Session("logueado") = True

                    Else
                        Me.lblError.Visible = True
                        Me.lblError.Text = "No se ha encontrado socio con ese nro."
                    End If
                End If

            End If
        Catch ex As Exception
            Response.Redirect("~/login_int.aspx")
        End Try
        If Session("logueado") = True Then
            Response.Redirect("~/default_int.aspx")
        End If
    End Sub

End Class