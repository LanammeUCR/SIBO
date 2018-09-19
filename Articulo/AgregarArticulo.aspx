
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarArticulo.aspx.cs" Inherits="SIBO.Articulo.AgregarArticulo"  MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divRedondo">
        <div class="row">


            <%-- titulo accion--%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <center>
                        <asp:Label ID="lblEditarArticulo" runat="server" Text="Agregar Articulo" Font-Size="Large" ForeColor="Black"></asp:Label>
                </center>
            </div>
            <%-- fin titulo accion --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- campos a llenar --%>

            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblNombreArticulo" runat="server" Text="Nombre <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                    <asp:TextBox class="form-control" ID="txbNombreArticulo"  runat="server"></asp:TextBox>
                </div>
                <div id="divNombreArticuloIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblNombreArticuloIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbDescripcionArticulo" runat="server" Text="Descripción <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-4 col-sm-4">
                     <asp:TextBox class="form-control" ID="txbDescripcion" runat="server"></asp:TextBox>
                </div>
                <div id="divDescripcionArticuloIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lbDescripcionArticuloIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>

            </div>
              <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblCodigo" runat="server" Text="Código Articulo <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:TextBox class="form-control" ID="txbCodigo" runat="server"></asp:TextBox>
                </div> 
                <div id="divCodigoArticuloIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lbCodigoArticuloIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>             
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblTiempoEntrega" runat="server" Text="Tiempo de Entrega" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:TextBox class="form-control" ID="txbTiempoEntrega" runat="server" TextMode="Number" Min="1" Max="360" AutoPostBack="true" OnTextChanged="txbTiempoEntrega_TextChanged"></asp:TextBox>
                </div>              
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">

                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblGastoAnual" runat="server" Text="Gasto Aproximado anual" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:TextBox class="form-control" ID="txbGastoAnual" runat="server" TextMode="Number" Min="1" AutoPostBack="true" OnTextChanged="txbGastoAnual_TextChanged"></asp:TextBox>
                </div>              
            </div>
             <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblCantidadCritica" runat="server" Text="Cantidad Critica" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3 col-sm-3">                  
                    <asp:TextBox CssClass="form-control" ID="txbCantidadCritica" runat="server" ReadOnly="true"></asp:TextBox>
                </div>   
                 <div class="col-md-3 col-xs-3 col-sm-3">                  
                    <asp:CheckBox CssClass="form-control" ID="chkCritica" runat="server" Text="Calcular" AutoPostBack="true" OnCheckedChanged="chkCritica_CheckedChanged" BorderStyle="None" BorderWidth="0"></asp:CheckBox>
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

    <script src="../Scripts/moment.js"></script>
    <script src="../Scripts/transition.js"></script>
    <script src="../Scripts/collapse.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">      
        /*
        Evalúa de manera inmediata los campos de texto que va ingresando el usuario.
        */
        function validarTexto(txtBox) {
            var id = txtBox.id.substring(12);

            if (id == "txbNombreArticulo") {
                var nombreArticuloIncorrecto = document.getElementById('<%= divNombreArticuloIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";                                                         
                    nombreArticuloIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    nombreArticuloIncorrecto.style.display = 'block';
                }
            }

            if (id == "txbDescripcion") {
                var descripcionArticuloIncorrecto = document.getElementById('<%= divDescripcionArticuloIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    descripcionArticuloIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    descripcionArticuloIncorrecto.style.display = 'block';
                }
            }
            if (id == "txbCodigo") {
                var codigoArticuloIncorrecto = document.getElementById('<%= divCodigoArticuloIncorrecto.ClientID %>');
                  if (txtBox.value != "") {
                      txtBox.className = "form-control";

                      codigoArticuloIncorrecto.style.display = 'none';
                  } else {
                      txtBox.className = "form-control alert-danger";
                      codigoArticuloIncorrecto.style.display = 'block';
                  }
              }
        }               
    </script>
</asp:Content>

