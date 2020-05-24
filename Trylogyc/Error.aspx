<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc.Master" CodeBehind="Error.aspx.vb" Inherits="Trylogyc._Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h2>Se ha Producido un Error</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box box-danger with-border">
    <div class="box-header with-border" style="border-bottom:1px solid silver ">
       <h3 class="box-title" style="font-family:verdana;font-size:large"><span><i class="fa fa-alert"></i></span>&nbsp&nbsp Código de Error</h3>
        
    </div>
    <div class="box-body with-border">
        <div class="row">
        <h4 style="color: #CC0000">Texto del Error</h4>
            <asp:Label ID="Label1" runat="server" Text="Se ha producido un error. Por favor intente nuevamente." style="font-weight:bold;color: #CC0000"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Se ha producido un error. Por favor intente nuevamente." style="color: #CC0000; display:none"></asp:Label>
    <div>
        </div>
            </div>
        </div>
      
</div>
        
</asp:Content>
