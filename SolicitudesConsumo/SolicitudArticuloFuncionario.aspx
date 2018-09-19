<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SolicitudArticuloFuncionario.aspx.cs" Inherits="SIBO.Articulo.SolicitudArticuloFuncionario" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblAdministrarUnidades" runat="server" Text="Solicitud de Articulos Para Suministros" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>

            <div class="col-md-12 col-xs-12 col-sm-12 ">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblRecepciones" runat="server" Text="Recepción: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:DropDownList ID="ddlUnidad" runat="server" Width="130px" class="btn btn-default dropdown-toggle"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: <span style='color:red'>*</span> " Font-Size="Large" ForeColor="Black"></asp:Label>
                </div>
                <div class="col-md-5 col-xs-5 col-sm-5">
                    <asp:TextBox class="form-control" ID="txbDescripcion" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                   <div id="divDescripcionIncorrecto" runat="server" style="display: none" class="col-md-3 col-xs-3 col-sm-3">
                    <asp:Label ID="lblDescripcionIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
           
            <div class="col-md-6 col-xs-6 col-sm-6 col-md-offset-8 col-xs-offset-8 col-sm-offset-8">
                <asp:Button ID="btnAgregar" runat="server" class="input input-group-addon" Text="Agregar Articulo" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
                &nbsp;
                <asp:Button ID="btnEnviarArriba" runat="server" Text="Enviar Solicitud" CssClass="btn btn-primary" OnClick="btnEnviarSolicitud" />
                &nbsp;
                <asp:Button ID="btnCancelarArriba" runat="server" Text="Cancelar" OnClick="btnCancelar" CssClass="btn btn-danger" />
            </div>
            
            <%-- tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; top: 10px; left: 30px;">
                <asp:Repeater ID="rpSolicitud" runat="server">
                    <HeaderTemplate>
                        <table id="tbArticulosSolicitud" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Nombre Articulo</th>
                                    <th>Cantidad Solicitada</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnnar" runat="server" ToolTip="nar" CommandArgument='<%# Eval("articulo.idArticulo") %>' OnClick="btnnarArticulo_Click" ><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                            </td>
                            <td>
                                <%# Eval("articulo.nombreArticulo") %>
                            </td>
                            <td>
                                <%# Eval("cantidadConsumo") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Nombre Articulo</th>
                                <td></td>
                            </tr>
                        </thead>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <%-- fin tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <br />
            </div>

            <%-- botones --%>
            <div class="col-md-5 col-xs-5 col-sm-5 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar Solicitud" CssClass="btn btn-primary" OnClick="btnEnviarSolicitud" />
                &nbsp;
                <asp:Button ID="btnCancelarAbajo" runat="server" Text="Cancelar" OnClick="btnCancelar" CssClass="btn btn-danger" />
            </div>
            <%-- fin botones --%>
        </div>
    </div>   
    <!-- Modal de Articulos -->
    <div id="modalArticulos" class="modal fade" role="alertdialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <!-- Modal header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Agregar Articulos</h4>
                </div>
                <!-- Fin Modal header -->

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3">
                                <asp:Label ID="lblArticuloSeleccionado" runat="server" Text="Articulo Seleccionado: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-5 col-xs-5 col-sm-5">
                                <asp:TextBox class="form-control" ID="txbArticuloSeleccionado" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3">
                                <asp:Label ID="lbCantidad" runat="server" Text="Cantidad a Solicitar: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:TextBox class="form-control" ID="txbCantidad" runat="server" TextMode="Number" min="1" OnTextChanged="txbCantidad_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                <asp:Button ID="btnAgregarArriba" runat="server" Text="Agregar" OnClick="btnAgregarArticuloLista" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>                     
                        <div class="col-md-12 col-xs-12 col-sm-12" >
                             <br />
                            <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Repeater ID="rpArticulos" runat="server">
                                        <HeaderTemplate>
                                            <table id="tbArticulos" class="row-border table-striped">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th>Nombre Articulo</th>
                                                        <th>Disponible</th>
                                                       <%-- <th></th>--%>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <asp:HiddenField ID="hfIdArticulo" runat="server" Value='<%# Eval("idArticulo") %>' />
                                                <td>
                                                    <%--<asp:CheckBox ID="ChckBxSeleccionado" runat="server" OnCheckedChanged="ChckBxSeleccionado" AutoPostBack="true" />--%>
                                                    <asp:LinkButton ID="btnSeleccionar" runat="server" ToolTip="Selecionar" OnClick="ChckBxSeleccionado" CommandArgument='<%# Eval("idArticulo") %>'> <span class="btn glyphicon glyphicon-ok"></span></asp:LinkButton>
                                                </td>

                                                <td>
                                                    <%# Eval("nombreArticulo") %>
                                                </td>
                                                <td>
                                                    <%# Eval("cantidadTotal") %>
                                                </td>
                                              <%--  <td>
                                                    <asp:TextBox class="form-control" ID="txbCantidad" runat="server" TextMode="Number" min="1" max='<%# Eval("cantidadTotal") %>' OnTextChanged="txbCantidad_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <thead>
                                                <tr id="filterrow">
                                                    <td></td>
                                                    <th>Nombre Articulo</th>
                                                    <td>Disponible</td>
                                                   <%-- <td></td>--%>
                                                </tr>
                                            </thead>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>

                    </div>
                </div>
                <!-- Fin Modal body -->

                <!-- Modal footer -->
                <div class="modal-footer">
                    <asp:Button ID="btnAgregarAbajo" runat="server" Text="Agregar" OnClick="btnAgregarArticuloLista" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
                <!-- Fin Modal footer -->
            </div>
            <!-- Fin Modal content -->
        </div>
    </div>
    <!-- Fin Modal Articulos -->



    <script src="../Scripts/moment.js"></script>
    <script src="../Scripts/transition.js"></script>
    <script src="../Scripts/collapse.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>

    <script type="text/javascript" src="../Scripts/dataSourcePlugins.js"></script>
    <!-- script tabla jquery -->
    <script type="text/javascript">

        function validarTexto(txtBox) {
            var id = txtBox.id.substring(12);

            if (id == "txbDescripcion") {
                var nombreArticuloIncorrecto = document.getElementById('<%= divDescripcionIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";
                    nombreArticuloIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    nombreArticuloIncorrecto.style.display = 'block';
                }
            }
        }

        $('#tbArticulosSolicitud thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbArticulosSolicitud thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });
        // DataTable
        var tbArticulosSolicitud = $('#tbArticulosSolicitud').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 10,
            "aLengthMenu": [[2, 5, 10, -1], [2, 5, 10, "All"]],
            "colReorder": true,
            "select": false,
            "bSort": false,
            "stateSave": true,
            "dom": 'Bfrtip',
            "buttons": [
                'pdf', 'excel', 'copy', 'print'
            ],
            "aoColumnDefs": [{
                'bSortable': false,
            }],
            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "decimal": ",",
                "thousands": ".",
                "sSelect": "1 fila seleccionada",
                "select": {
                    rows: {
                        _: "Ha seleccionado %d filas",
                        0: "Dele click a una fila para seleccionarla",
                        1: "1 fila seleccionada"
                    }
                },
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                }
            }
        });

        // aplicar filtro
        $("#tbArticulosSolicitud thead input").on('keyup change', function () {
            tbArticulosSolicitud
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        function limpiarSolcititud() {
            $("#tbArticulosSolicitud thead input").keyup();
        }

        /****************************** Tabla Modal Articulos **********************************/
        $('#tbArticulos thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbArticulos thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        // DataTable
        var tblArticulos = $('#tbArticulos').DataTable({
            orderCellsTop: true,
            "bPaginate": false,          
            "aLengthMenu": [[2, 5, 10, -1], [2, 5, 10, "All"]],
            "colReorder": true,
            "select": false,
            "stateSave": true,
            "dom": 'Bfrtip',
            "buttons": [
                'pdf', 'excel', 'copy', 'print'
            ],
            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "decimal": ",",
                "thousands": ".",
                "sSelect": "1 fila seleccionada",
                "select": {
                    rows: {
                        _: "Ha seleccionado %d filas",
                        0: "Dele click a una fila para seleccionarla",
                        1: "1 fila seleccionada"
                    }
                },
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                }
            }
        });

        // aplicar filtro
        $("#tbArticulos thead input").on('keyup change', function () {
            tbArticulos
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        function limpiarArticulos() {
            $("#tbArticulos thead input").keyup();
        }

        /*************************** Fin Tabla Modal Articulos *********************************/

        // Tabla de clientes
        function levantarModalUbicacion() {
            $('#modalArticulos').modal('show');
        };

        function cerrarModalUbicacion() {
            $('#modalArticulos').modal('hide');
        };

        function stopPropagation(evt) {
            if (evt.stopPropagation !== undefined) {
                evt.stopPropagation();
            } else {
                evt.cancelBubble = true;
            }
        };
    </script>
    <!-- fin script tabla jquery -->
</asp:Content>

