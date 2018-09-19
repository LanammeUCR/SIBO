<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="narUbicacionArticulo.aspx.cs" Inherits="SIBO.Articulo.narUbicacionArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblUbicacion" runat="server" Text="Ubicación del Articulo" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>
           
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-1 col-xs-1">
                    <asp:Label ID="lbArticulo" runat="server" Text="Articulo: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3">
                    <div class="input-group">
                        <asp:Label ID="lbNombre" runat="server" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>                       
                    </div>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
               <div class="col-md-12 col-xs-12 col-sm-12 ">
                <div class="col-md-2 col-xs-2">
                    <asp:Label ID="lblBodegas" runat="server" Text="Bodega: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>                   
                </div>
                   <div class="col-md-4 col-xs-4">                   
                    <asp:TextBox class="form-control" ID="txbUbicacion" runat="server" ReadOnly="true" ></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
             <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbFila" runat="server" Text="Fila: " Font-Size="Medium" ForeColor="Black"  CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                     <asp:TextBox class="form-control" ID="txbFila" runat="server" ReadOnly="true"></asp:TextBox>
                </div>              
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbEstante" runat="server" Text="Estante: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                     <asp:TextBox class="form-control" ID="txbEstante" runat="server" ReadOnly="true"></asp:TextBox>
                </div>                
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbPiso" runat="server" Text="Piso: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                     <asp:TextBox class="form-control" ID="txbPiso" runat="server" ReadOnly="true"></asp:TextBox>
                </div>              
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbCantidad" runat="server" Text="Cantidad: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                     <asp:TextBox class="form-control" ID="txbCantidad" runat="server" ReadOnly="true"></asp:TextBox>
                </div>              
                <br />
            </div>   
            

            <%-- botones --%>
            <div class="col-md-6 col-xs-6 col-sm-6 col-md-offset-10 col-xs-offset-10 col-sm-offset-10">
                <asp:Button ID="btnAgregar" runat="server" Text="nar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
            </div>
            <%-- fin botones --%>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">    
</asp:Content>
