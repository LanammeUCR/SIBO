<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FuncionariosPorUnidad.aspx.cs" Inherits="SIBO.Unidad.RecepcionistasPorUnidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ViewContactos" runat="server" style="display: block">

        <div class="divRedondo">
            <div class="row">
                  <%-- variables hidden --%><input type="hidden" id="hdIdContacto" runat="server" /><%-- fin variables hidden --%><%-- titulo accion--%><div class="col-md-12 col-xs-12 col-sm-12">
                    <center>
                        <asp:Label ID="lbltitulo" runat="server" Text="Asociar Funcionarios" Font-Size="Large" ForeColor="Black" ></asp:Label>
                       </center>
                </div>
                <%-- fin titulo accion --%>
                <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-1 col-xs-offset-1 col-sm-offset-1">
                     <asp:Label ID="lblNombreUnidad" runat="server" Font-Size="Medium" ForeColor="Black" CssClass="label">Unidad:</asp:Label>                   
                </div>
                            
                <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10">                   
                    
                </div>

                <!-- contactos en la base de datos -->

                <div class="col-md-5 col-xs-5 col-sm-5 col-md-offset-1 col-xs-offset-1 col-sm-offset-1">
                    <asp:Label ID="lblContactos" runat="server" Text="Funcionarios Asociados" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-6 col-xs-6 col-sm-6">

                    <asp:Label ID="lblContactosAsociados" runat="server" Text="Funcionarios" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>

                <div class="col-md-4 col-xs-4 col-sm-4 col-md-offset-1 col-xs-offset-1 col-sm-offset-1">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="txtBuscarFuncionariosAsociados" AutoPostBack="true" class="form-control"></asp:TextBox>
                        <span class="input input-group-addon"><span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;</span>
                    </div>
                </div>


                <div class="col-md-4 col-xs-4 col-sm-4 col-md-offset-1 col-xs-offset-1 col-sm-offset-1">
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="txtBuscarFuncionarios" AutoPostBack="true" class="form-control" OnTextChanged="txtBuscarFuncionarios_TextChanged" ></asp:TextBox>
                        <span class="input input-group-addon"><span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;</span>
                    </div>
                </div>

                <div class="col-md-4 col-xs-4 col-sm-4 col-md-offset-1 col-xs-offset-1 col-sm-offset-1">
                    <asp:ListBox ID="LbFuncionariosAsociados" runat="server" SelectionMode="Multiple" Height="190px" CssClass="form-control" style="overflow:auto"></asp:ListBox>
                </div>
                <div class="col-md-1 col-xs-1 btn-group-vertical">
                    <br />
                    <asp:LinkButton ID="btnPasarDerecha" CssClass="btn btn-default" runat="server" OnClick="btnPasarDerecha_Click" ><span class="glyphicon glyphicon-chevron-right"></span></asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="btnPasarIzquierda" CssClass="btn btn-default" runat="server" OnClick="btnPasarIzquierda_Click" ><span class="glyphicon glyphicon-chevron-left"></span></asp:LinkButton>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:ListBox ID="LbFuncionarios" runat="server" SelectionMode="Multiple" Height="190px" CssClass="form-control" style="overflow:auto"></asp:ListBox>
                </div>

                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <hr />
                </div>

                <%-- botones --%>
                <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click"  />
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <br />
                </div>
            </div>
        </div>
        <%-- fin botones --%>

        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
            <br />
        </div>

    </div>
    <!-- ------------------------ FIN VISTA CONTACTOS --------------------------- -->
</asp:Content>

