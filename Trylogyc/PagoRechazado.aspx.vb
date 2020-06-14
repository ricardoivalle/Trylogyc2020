﻿Imports Trylogyc.DAL
Imports Trylogyc.DAL.TrylogycContext

Public Class PagoRechazado
    Inherits System.Web.UI.Page

    Public IdComprobante As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim collection As String = Request.Params("collection_id")
            Dim merchantOrder As String = Request.Params("merchant_order_id")
            Dim preference As String = Request.Params("preference_id")
            Dim myContext As New TrylogycContext
            Dim pagoRegistrado As Boolean = myContext.UpdPago(preference, EstadosPagos.Rechazado, collection, merchantOrder)
        End If
    End Sub

End Class