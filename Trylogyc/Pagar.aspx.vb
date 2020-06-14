Imports MercadoPago
Imports MercadoPago.Resources
Imports MercadoPago.DataStructures.Preference
Imports MercadoPago.Common
Imports System.Globalization
Imports Trylogyc.DAL

Public Class Pagar
    Inherits System.Web.UI.Page

    Private _myUri As String
    Private _myHost As String
    Private _isSandbox As Boolean
    Private _sandboxPostBackUrl As String
    Private _productionPostBackUrl As String
    Protected originalUri As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _isSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings("isSandbox"))
            _sandboxPostBackUrl = ConfigurationManager.AppSettings("sandboxPostbackUrl")
            _productionPostBackUrl = ConfigurationManager.AppSettings("productionPostbackUrl")
            _myUri = HttpContext.Current.Request.Url.AbsoluteUri
            _myHost = HttpContext.Current.Request.Url.Host
            ReEvaluarYSetearMercadoPagoToken()
            Dim nroFactura As String = Request.Params("numfact")
            Dim strImport As String = Request.Params("importe")
            Dim idSocio As Int32 = Request.Params("idSocio")
            Dim idConexion As Int32 = Request.Params("idConexion")
            'Nos aseguramos que nadie cambie los valores de la query string manualmente. Si lo hacen lo redirigimos a default.

            If IsNothing(HttpContext.Current.Request.UrlReferrer) Then
                Response.Redirect("~/Default.aspx")
            Else
                If Not IsNothing(HttpContext.Current.Request.UrlReferrer.AbsolutePath) Then
                    If HttpContext.Current.Request.UrlReferrer.AbsolutePath.Equals("/Pagar") Then
                        Response.Redirect("~/Default.aspx")
                    End If
                End If
            End If


            'Seteamos el importe en decimal
            Dim importeFactura As Decimal
            If (Decimal.TryParse(Request.Params("importe"),
                                 NumberStyles.AllowDecimalPoint,
                                 CultureInfo.CreateSpecificCulture("fr-FR"),
                                 importeFactura) = False) Then
                Response.Redirect("~/Default.aspx")
            End If
            Dim preferencia As String = GenerarPreferenciaPagoMercadoPago(nroFactura, importeFactura)
            If CrearRegistroPagoEnCurso(nroFactura, importeFactura, preferencia, idSocio, idConexion) = True Then
            Else
                Session("txtError") = "Se produjo un error durante el proceso de pago y su pago no pudo ser realizado."
                Try
                    Response.Redirect("~/Error.aspx")
                Catch ex As Exception
                    Throw ex
                End Try

            End If
        Catch ex As Exception
            If String.IsNullOrEmpty(Session("txtError")) = True Then
                Response.Redirect("~/Default.aspx")
            Else
                Response.Redirect("~/Error.aspx")
            End If

        End Try

    End Sub
    Private Sub ReEvaluarYSetearMercadoPagoToken()
        Dim token As String
        If (MercadoPago.SDK.AccessToken Is Nothing) Then
            If _isSandbox = True Then
                token = ConfigurationManager.AppSettings("tokenMpagoSandBox")
            Else
                token = ConfigurationManager.AppSettings("tokenMpagoProduccion")
            End If
            MercadoPago.SDK.SetAccessToken(token)
        End If
        'Dim sdk As New MercadoPago.SDK
        'Dim obj As Object = MercadoPago.SDK.Get("https://api.mercadopago.com/v1/payments/search" & "?id=" & "229476782-9671c3e9-e960-43c3-bdeb-fc761ba4e3df")
    End Sub
    Private Function GenerarPreferenciaPagoMercadoPago(nroFactura As String, importeFactura As Decimal) As String

        Dim myPreference As New Preference()
        'myPreference.ClientId = "580564279"
        myPreference.Payer = RegistrarDatosClienteMercadoPago()
        Dim itemToAdd As New Item()
        itemToAdd.Title = nroFactura
        itemToAdd.Quantity = 1
        itemToAdd.CurrencyId = CurrencyId.ARS
        itemToAdd.UnitPrice = importeFactura
        myPreference.Items.Add(itemToAdd)
        Dim _backUrls As New BackUrls()
        _backUrls.Success = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/PagoExitoso.aspx")
        _backUrls.Failure = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/PagoRechazado.aspx")
        _backUrls.Pending = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/PagoPendiente.aspx")
        myPreference.BackUrls = _backUrls
        myPreference.AutoReturn = AutoReturnType.approved
        myPreference.BinaryMode = True

        If _isSandbox = True Then
            'myPreference.NotificationUrl = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/" & _sandboxPostBackUrl)
        Else
            'myPreference.NotificationUrl = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/" & _productionPostBackUrl)
        End If

        myPreference.Save()

        If _isSandbox = True Then
            preferenceID.Value = myPreference.SandboxInitPoint

        Else
            preferenceID.Value = myPreference.InitPoint
        End If

        Return myPreference.Id
    End Function

    Private Function RegistrarDatosClienteMercadoPago() As Payer
        Dim payer As New Payer()
        payer.Email = Session("userEmail").ToString()
        'payer.Identification = Session("xmlSocio")
        payer.Name = Session("nomUsuario")
        Return payer
    End Function

    Private Function CrearRegistroPagoEnCurso(ByVal nroFactura As String, ByVal importe As Decimal, ByVal preferenceId As String, ByVal idSocio As Int32, ByVal idConexion As Int32) As Boolean
        Dim myContext As New TrylogycContext
        Dim pagoRegistrado As Boolean = myContext.PutPago(nroFactura, importe, preferenceId, idSocio, idConexion)
        Return pagoRegistrado
    End Function
End Class