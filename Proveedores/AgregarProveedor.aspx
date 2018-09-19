<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarProveedor.aspx.cs" Inherits="SIBO.Proveedores.AgregarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divRedondo">
        <div class="row">

            <%-- titulo accion--%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <center>
                        <asp:Label ID="lblAgregarProveedor" runat="server" Text="Agregar Proveedor" Font-Size="Large" ForeColor="Black"></asp:Label>
                    </center>
            </div>
            <%-- fin titulo accion --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- campos a llenar --%>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblTipoCedula" runat="server" Text="Tipo Identificación <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:DropDownList class="btn btn-default dropdown-toggle" ID="ddlTipoCedula" runat="server"></asp:DropDownList>
                </div>
                <div id="div1" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblTipoCedulaProveedorIncorrecta" runat="server" Font-Size="Small" class="label alert-danger" Text="Debe  Seleccionar un tipo de identificación" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbCedulaProveedor" runat="server" Text="Cédula <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbCedulaProveedor" runat="server"></asp:TextBox>
                </div>
                <div id="divCedulaProveedorIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblCedulaPersonaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreProveedor" runat="server" Text="Nombre Completo <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbNombreProveedor" runat="server"></asp:TextBox>
                </div>
                <div id="divNombreProveedorIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblNombreProveedorIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblTelefonoProveedor" runat="server" Text="Teléfono" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbTelefonoProveedor" runat="server"></asp:TextBox>
                </div>               
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblCorreoProveedor" runat="server" Text="Correo Eletrónico" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbCorreoProveedor" runat="server"></asp:TextBox>
                </div>    
                  <div id="divCorreoProveedorIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblCorreoProveedorIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" ForeColor="Red"></asp:Label>
                </div>         
            </div>

            <div class="col-xs-12">
                <br />
                <div class="col-xs-12">
                    <h6 style="text-align: left">Los campos marcados con <span style='color: red'>*</span> son requeridos.</h6>
                </div>
            </div>

            <%-- fin campos a llenar --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- botones --%>
            <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
            </div>
            <%-- fin botones --%>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        /*
        Evalúa de manera inmediata los campos de texto que va ingresando el usuario.
        */
        function validarTexto(txtBox) {
            var id = txtBox.id.substring(12);

            if (id == "txbCedulaProveedor") {
                var cedulaProveedorIncorrecto = document.getElementById('<%= divCedulaProveedorIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    cedulaProveedorIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    cedulaProveedorIncorrecto.style.display = 'block';
                }
            }

            if (id == "txbNombreProveedor") {
                var nombreProveedorIncorrecto = document.getElementById('<%= divNombreProveedorIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    nombreProveedorIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    nombreProveedorIncorrecto.style.display = 'block';
                }
            }

            if (id == "txbCorreoProveedor") {
                var correoProveedorIncorrecto = document.getElementById('<%= divCorreoProveedorIncorrecto.ClientID %>');
                 var mensajeCorreo = document.getElementById('<%= lblCorreoProveedorIncorrecto.ClientID %>');
                 var validarCorreo = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/;
                 if (txtBox.value != "") {
                     if (validarCorreo.test(txtBox.value)) {
                         txtBox.className = "form-control";
                         correoProveedorIncorrecto.style.display = 'none';
                     } else {
                         mensajeCorreo.innerHTML = 'Formato de correo incorrecto.';
                         txtBox.className = "form-control alert-danger";
                         correoProveedorIncorrecto.style.display = 'block';
                     }
                 }
             }
        }
    </script>
</asp:Content>
