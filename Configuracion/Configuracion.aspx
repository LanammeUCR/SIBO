<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SIBO.Configuracion.Configuracion" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="divRedondo">
        <div class="row">

            <%-- titulo accion--%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <center>
                        <asp:Label ID="lblConfiguracion" runat="server" Text="Configuración" Font-Size="Large" ForeColor="Black"></asp:Label>
                    </center>
            </div>
            <%-- fin titulo accion --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- campos a llenar Primera BD--%>
            <div class="col-md-12 col-xs-12 col-sm-12 ">
                <asp:Label ID="Label1" runat="server" Text="Base de Control de Trabajos de Laboratorio:" Font-Size="Large" ForeColor="Black" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                 <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreServidorSIBO" runat="server" Text="Servidor:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtNombreServidorSIBO" runat="server"></asp:TextBox>
                </div>
             </div>
             <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreBdSIBO" runat="server" Text="Nombre:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtNombreBdSIBO" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreUsuarioBdSIBO" runat="server" Text="Usuario:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtNombreUsuarioBdSIBO" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblContrasenaBdSIBO" runat="server" Text="Contraseña:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtContrasenaBdSIBO" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <%-- fin campos a llenar --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

           
           <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <%-- campos a llenar BD Login--%>
            <div class="col-md-12 col-xs-12 col-sm-12 ">
                <asp:Label ID="Label2" runat="server" Text="Base de Datos Login:" Font-Size="Large" ForeColor="Black" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                 <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreServidorLogin" runat="server" Text="Servidor:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtNombreServidorLogin" runat="server"></asp:TextBox>
                </div>
             </div>
             <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreBdLogin" runat="server" Text="Nombre:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtNombreBdLogin" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreUsuarioBdLogin" runat="server" Text="Usuario:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtNombreUsuarioBdLogin" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblContrasenaBdLogin" runat="server" Text="Contraseña:" Font-Size="Medium" ForeColor="Black" CssClass="label" ></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtContrasenaBdLogin" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <%-- fin campos a llenar --%>
            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>
            
            <%-- botones --%>
            <div class="col-md-4 col-xs-4 col-sm-4 col-md-offset-8 col-xs-offset-8 col-sm-offset-8">
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
           </div>
            <%-- fin botones --%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
