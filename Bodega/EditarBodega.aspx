<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarBodega.aspx.cs" Inherits="SIBO.Bodega.EditarBodega" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divRedondo">
        <div class="row">

            <%-- titulo accion--%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <center>
                        <asp:Label ID="lblEditarBodega" runat="server" Text="Editar Bodega" Font-Size="Large" ForeColor="Black"></asp:Label>
                    </center>
            </div>
            <%-- fin titulo accion --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- campos a llenar --%>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreBodega" runat="server" Text="Nombre <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txtNombreBodega" runat="server"></asp:TextBox>
                </div>
                <div id="divNombreBodegaIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblNombreBodegaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblDireccionBodega" runat="server" Text="Dirección &lt;span style='color:red'&gt;*&lt;/span&gt; " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbDireccionBodega" runat="server"></asp:TextBox>
                </div>
                <div id="divDireccionBodegaIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblDireccionBodegaIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>   
            <div class="col-xs-12">             
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
                <asp:Button ID="btnEditar" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnEditar_Click" />
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



            if (id == "txtNombreBodega") {
                var nombreBodegaIncorrecto = document.getElementById('<%= divNombreBodegaIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    nombreBodegaIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    nombreBodegaIncorrecto.style.display = 'block';
                }
            }

            if (id == "txbDireccionBodega") {
                var direccionBodegaIncorrecto = document.getElementById('<%= divDireccionBodegaIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    direccionBodegaIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    direccionBodegaIncorrecto.style.display = 'block';
                }
            }        
        }
    </script>
</asp:Content>
