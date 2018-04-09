<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarRecepcionista.aspx.cs" Inherits="SIBO.Recepcionista.AgregarRecepcionista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divRedondo">
        <div class="row">

            <%-- titulo accion--%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <center>
                        <asp:Label ID="lblAgregarRecepcionista" runat="server" Text="Editar Recepcionista" Font-Size="Large" ForeColor="Black"></asp:Label>
                    </center>
            </div>
            <%-- fin titulo accion --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- campos a llenar --%>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbCedulaRecepcionista" runat="server" Text="Cédula <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbCedulaRecepcionista" runat="server"></asp:TextBox>
                </div>
                <div id="divCedulaRecepcionistaIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblCedulaPersonaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreRecepcionista" runat="server" Text="Nombre <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbNombreRecepcionista" runat="server"></asp:TextBox>
                </div>
                <div id="divNombreRecepcionistaIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblNombreRecepcionistaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblApellidosRecepcionista" runat="server" Text="Apellidos <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbApellidosRecepcionista" runat="server"></asp:TextBox>
                </div>
                <div id="divApellidosRecepcionistaIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblApellidosRecepcionistaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblTelefonoRecepcionista" runat="server" Text="Teléfono <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbTelefonoRecepcionista" runat="server"></asp:TextBox>
                </div>
                <div id="divTelefonoRecepcionistaIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblTeléfonoRecepcionistaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblCorreoRecepcionista" runat="server" Text="Correo Eletrónico <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbCorreoRecepcionista" runat="server"></asp:TextBox>
                </div>
                <div id="divCorreoRecepcionistaIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblCorreoRecepcionistaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
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



            if (id == "txtNombreRecepcionista") {
                var nombreRecepcionistaIncorrecto = document.getElementById('<%= divNombreRecepcionistaIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    nombreRecepcionistaIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    nombreRecepcionistaIncorrecto.style.display = 'block';
                }
            }

            if (id == "txbTelefonoRecepcionista") {
                var nombreRecepcionistaIncorrecto = document.getElementById('<%= divNombreRecepcionistaIncorrecto.ClientID %>');
                if (numero != "" && /^([0-9])*[.]?[0-9]*$/.test(numero)) {
                    txtBox.className = "form-control";

                    nombreRecepcionistaIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    nombreRecepcionistaIncorrecto.style.display = 'block';
                }
            }
        }
    </script>
</asp:Content>
