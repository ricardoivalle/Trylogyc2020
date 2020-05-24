<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc.Master" CodeBehind="UsuarioModif.aspx.vb" Inherits="Trylogyc.UsuarioModif" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box box-info with-border">
    <div class="box-header with-border" style="border-bottom:1px solid silver ">
       <h3 class="box-title" style="font-family:verdana;font-size:large"><span><i class="fa fa-user"></i></span>&nbsp&nbsp Mis Datos de Usuario</h3>

    </div>

    <div class="box-body with-border">
        <div class="box-md-6">
          <div class="input-group"  style="margin-left:0.5%">
          <span class="input-group-addon" style="width:170px">Contraseña Actual</span><span >
          <asp:TextBox ID="txtpassWordOld" runat="server"  Enabled="true" CssClass="form-control" style="width:400px" TextMode="Password"></asp:TextBox></span>
          </div>
           <br />
          <div class="input-group"  style="margin-left:0.5%">
          <span class="input-group-addon" style="width:170px">Nueva Contraseña</span><span >
          <asp:TextBox ID="txtPassWordNew" runat="server"  Enabled="true" CssClass="form-control" style="width:400px" TextMode="Password"></asp:TextBox></span>
          </div>
          <br />
          <div class="input-group"  style="margin-left:0.5%">
          <span class="input-group-addon" style="width:170px">Confirmar Contraseña</span><span >
          <asp:TextBox ID="txtPassWordNewCnf" runat="server"  Enabled="true" CssClass="form-control" style="width:400px" TextMode="Password"></asp:TextBox></span>
          </div>
        <br />
            <div class="alert alert-danger alert-dismissible" id="divError" runat="server" style="width:50%;text-align:center">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <h4><i class="icon fa fa-ban"></i> Atención!</h4>
                <asp:Label ID="lblError" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
            </div>   
            <div class="alert alert-success alert-dismissible" id="divSuccess" runat="server" style="width:50%;text-align:center">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <h4><i class="icon fa fa-check"></i> OK!</h4>
                <asp:Label ID="lblSuccess" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
            </div>   
            </div>
        <div class="box-md-6">

        </div>
    </div>
    <div class="box-footer with-border">
        
        <asp:Button ID="btnSavePass" runat="server" Text="Guardar" class="btn btn-primary" />
        
       

    </div>
</div>

</asp:Content>
