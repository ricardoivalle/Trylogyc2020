<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc.Master" CodeBehind="Default.aspx.vb" Inherits="Trylogyc._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table th {
            text-align:center !important;
            border:1px;

        } 
        table tbody {
            padding:5px !important;
        }
        .badge-bg-red {
            vertical-align:middle !important;
            padding-top:10px !important;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
          <asp:ScriptManager runat="server">

      </asp:ScriptManager>
    <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: white; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loader.gif" AlternateText="Cargando ..." ToolTip="Cargando ..." style="padding: 10px;position:fixed;top:5%; left:30%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
          <div class="row" style="overflow: visible; width:100%; padding-left:10px !important" >
               <div class="col-sm-4" style="padding-left:20px !important">
    <div class="box box-success with-border">
    <div class="box-header with-border" style="border-bottom:1px solid silver ">
       <h3 class="box-title" style="font-family:verdana;font-size:large"><span><i class="fa fa-plug"></i></span>&nbsp&nbsp Mis Conexiones</h3>
    </div>
    <div class="box-body with-border">
        <div class="row">
             <div class="border col-md-12">

    <asp:ListBox ID="lstConexiones" runat="server" Width="95%"  AutoPostBack="True" height="100px" style="margin-left:4%    "></asp:ListBox>
               
               </div>
        </div>
         
        <br />
                                <div class="col-md-12">
                                     <div class="input-group" style="margin-left: 0.5%">
                                        <span class="input-group-addon" style="width: 100px">Nombre&nbsp&nbsp&nbsp</span>
                                        <asp:TextBox ID="txtNombre" runat="server" Enabled="false" CssClass="form-control" Style="width: 270px"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group" style="margin-left: 0.5%">
                                        <span class="input-group-addon" style="width: 100px">Dirección</span>
                                        <asp:TextBox ID="txtDireccion" runat="server" Enabled="false" CssClass="form-control" Style="width: 270px"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group" style="margin-left: 0.5%">
                                        <span class="input-group-addon" style="width: 100px">Localidad</span>
                                        <asp:TextBox ID="txtLocalidad" runat="server" Enabled="false" CssClass="form-control" Style="width: 270px"></asp:TextBox>
                                    </div>
                                    <br />
                                   
                                </div>
            <div class="row">
           <div class="col-md-3">
           </div>
           <div class="col-md-6">
            <a  href="relaciones.aspx" ID="Button1" Class="btn btn-default"/>Agregar Relación</a>
            </div>
           </div>
        </div>
    
</div>
   </div>
   <div class="col-sm-8">
              <div class="box box-success with-border">
    <div class="box-header with-border" style="border-bottom:1px solid silver ">
       <h3 class="box-title" style="font-family:verdana;font-size:large"><span><i class="fa fa-clipboard"></i></span>&nbsp&nbsp Mis Facturas</h3>
       <h3 class="box-title" style="font-family:verdana;font-size:8px"><span><i class="fa fa-clipboard"></i></span> ESTA APLICACIÓN SÓLO PERMITE REIMPRIMIR LAS ULTIMAS FACTURAS ADEUDADAS, NO CONSTITUYE UN INFORME DE DEUDA; SI DESEA CONOCER EL ESTADO COMPLETO DE SU DEUDA POR SERVICIOS PRESTADOS, DEBERA REQUERIR INFORME EN LAS OFICINAS DE LA COOPERATIVA</h3>
    </div>
    <div class="box-body with-border">
        <div class="row" style="text-align:center">
    <div class="box-body table-responsive no-padding" style="margin-left:3%">
        <div class="alert alert-danger alert-dismissible" id="divError" runat="server" style="width:80%; margin-left:-2% !important">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <h4><i class="icon fa fa-ban"></i> Atención!</h4>
                <asp:Label ID="lblError" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
              </div>
        

       
        <asp:GridView ID="GridView1" runat="server" Caption="Facturas" EmptyDataText="No hay Facturas para Mostrar" Font-Size="Small" 
            Style="font-size: x-small" width="96%" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" PageSize="50"
            CellSpacing="1" HorizontalAlign="Left" CssClass="table table-hover" CaptionAlign="Top" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:imageButton ID="cmdPDF2" commandname = "cmdPDF2" runat="server"   Text=""  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                                Width="30px" onclick="cmdPDF2_Click" ImageUrl="~/Images/pdf.jpg" />
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Pagar">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" Text="Pagar" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
        </div> 
        <br />
        <div class="box-footer with-border">
            <div class="row">
                <div class="col-md-3">
                </div>
            <div class="col-md-6" style="margin-left:70px">
            <div style="float:right">
                <span><img alt="" src="images/Users/aguasunch.jpg" width="100px" />
                </span>
            </div>
                 </div>
                <div class="col-md-3">

                </div>
            
          </div>
        </div>
      </div>  
      </div> 
        </a> 

</div>
</div>
    </ContentTemplate>
    </asp:UpdatePanel>
        <asp:SqlDataSource ID="sqlFacturas" runat="server" ConnectionString="<%$ ConnectionStrings:TrylogycContext %>" SelectCommand="SEL_FACTURAS_SOCIO_CONEXION" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="XmlSocio" SessionField="xmlSocio" Type="Int32" />
                <asp:ControlParameter ControlID="lstConexiones" Name="XmlConexion" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>



    <asp:LinqDataSource ID="LinqDataSource1" runat="server">
    </asp:LinqDataSource>



</asp:Content>
