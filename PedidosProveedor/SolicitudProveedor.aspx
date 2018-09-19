<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SolicitudProveedor.aspx.cs" Inherits="SIBO.Pedidos.SolicitudProveedor" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblAdministrarUnidades" runat="server" Text="Ingreso de Articulos de Proveedor" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2">
                    <asp:Label ID="Label2" runat="server" Text="Numero de Pedido: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3">
                    <asp:TextBox ID="txbNumeroPedido" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div id="divNumeroPedidoIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                    <asp:Label ID="lblNumeroPedidoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <br />
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2">
                    <asp:Label ID="lblProveedor" runat="server" Text="Proveedor: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3">
                    <div class="input-group">
                        <asp:TextBox ID="tbxNombreProveedor" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="tbxNombreProveedor_TextChanged"></asp:TextBox>
                        <span class="input input-group-addon"><span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;</span>
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <br />
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-sm-6 col-md-6 col-xs-6  col-md-offset-2 col-xs-offset-2 col-sm-offset-2">
                    <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="btn btn-default dropdown-toggle"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-5 col-xs-5 col-md-offset-8 col-xs-offset-8 col-sm-offset-8">
                    <asp:Button ID="btnAgregar" runat="server" class="input input-group-addon" Text="Agregar Articulo" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
                    &nbsp;
                        <asp:Button ID="btnEnviarArriba" runat="server" Text="Enviar Ingreso" CssClass="btn btn-primary" OnClick="btnEnviarSolicitud" />
                    &nbsp;
                        <asp:Button ID="btnCancelarArriba" runat="server" Text="Cancelar" OnClick="btnCancelar" CssClass="btn btn-danger" />

                </div>
            </div>
            <%-- tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; top: 10px; left: 30px;">

                <asp:Repeater ID="rpSolicitudProveedor" runat="server">
                    <HeaderTemplate>
                        <table id="tbArticulosIngreso" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Nombre Articulo</th>
                                    <th>Cantidad en Inventario</th>
                                    <th>Cantidad a Ingresar</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="nar" OnClick="btnEliminarArticulo_Click" CommandArgument='<%# Eval("articulo.idArticulo") %>'><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnSeleccionar" runat="server" ToolTip="Selecionar" OnClick="btnSeleccionar_Click" CommandArgument='<%# Eval("articulo.idArticulo") %>'> <span class="btn glyphicon glyphicon-ok"></span></asp:LinkButton>
                            </td>
                            <td>
                                <%# Eval("articulo.nombreArticulo") %>
                            </td>
                            <td>
                                <%# Eval("articulo.cantidadTotal") %>
                            </td>
                            <td>
                                <%# Eval("cantidad") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Nombre Articulo</th>
                                <th>Cantidad Disponible</th>
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
                <asp:Button ID="btnEnviarAbajo" runat="server" Text="Enviar Ingreso" CssClass="btn btn-primary" OnClick="btnEnviarSolicitud" />
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
                                <asp:Label ID="lbCantidad" runat="server" Text="Cantidad a Ingresar: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:TextBox class="form-control" ID="txbCantidad" runat="server" TextMode="Number" min="1" OnTextChanged="txbCantidad_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                <asp:Button ID="btnAgregarArriba" runat="server" Text="Agregar" OnClick="btnAgregarArticuloLista" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
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
                                            <%--<asp:CheckBox ID="btnSeleccionado" runat="server" OnCheckedChanged="btnSeleccionado" AutoPostBack="true" />--%>
                                            <asp:LinkButton ID="btnSeleccionarModal" runat="server" ToolTip="Selecionar" OnClick="btnSeleccionarModal" CommandArgument='<%# Eval("idArticulo") %>'> <span class="btn glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        </td>

                                        <td>
                                            <%# Eval("nombreArticulo") %>
                                        </td>
                                        <td>
                                            <%# Eval("cantidadTotal") %>
                                        </td>                                
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

    <!-- Modal de Ubicacion -->
    <div id="modalUbicaciones" class="modal fade" role="alertdialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <!-- Modal header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Agregar Ubicación</h4>
                </div>
                <!-- Fin Modal header -->
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-1 col-xs-1 col-sm-1">
                                <asp:Label ID="lblArticulo" runat="server" Text="Articulo: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-6 col-xs-6 col-sm-6">
                                <div class="input-group">
                                    <asp:Label ID="lblNombreArticulo" runat="server" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lblCantidadUbicable" runat="server" Text="Cantidad para Ubicar: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-5 col-xs-5 col-sm-5">
                                <asp:TextBox class="form-control" ID="txbCantidadUbicable" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12 ">
                            <div class="col-md-2 col-xs-2">
                                <asp:Label ID="lblBodegas" runat="server" Text="Bodega: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-6 col-xs-6 col-sm-12 ">
                                <asp:DropDownList ID="ddlBodegas" runat="server" class="btn btn-default dropdown-toggle"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lbEstante" runat="server" Text="Estante:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4 col-xs-4 col-sm-4">
                                <asp:TextBox class="form-control" ID="txbEstante" runat="server"></asp:TextBox>
                            </div>
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lbPiso" runat="server" Text="Piso:" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4 col-xs-4 col-sm-4">
                                <asp:TextBox class="form-control" ID="txbPiso" runat="server"></asp:TextBox>
                            </div>
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lbCantidadUbicar" runat="server" Text="Cantidad <span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4 col-xs-4 col-sm-4">
                                <asp:TextBox class="form-control" ID="txbCantidadUbicar" runat="server" TextMode="Number" min="1" OnTextChanged="txbCantidad_TextChanged"></asp:TextBox>
                            </div>
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-6 col-xs-6 col-sm-6 col-md-offset-6 col-xs-offset-6 col-sm-offset-6">
                            <asp:Button ID="btnAgregarUbicacion" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregar_Ubicacion_Click" />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <asp:Repeater ID="rpArticuloUbicaciones" runat="server">
                                <HeaderTemplate>
                                    <table id="tbArticuloUbicaciones" class="row-border table-striped">
                                        <thead>
                                            <tr>
                                                <td></td>
                                                <th>Bodega</th>
                                                <th>Estante</th>
                                                <th>Piso</th>
                                                <th>Cantidad</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btnEliminarUbicacion" runat="server" OnClick="btnEliminarUbicacion_Click" CommandArgument='<%# Eval("bodega.idBodega") %>' ToolTip="Eliminar"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("bodega.nombre") %>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdfEstante" runat="server" Value='<%# Eval("estante") %>' />
                                            <%# Eval("estante") %>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdfPiso" runat="server" Value='<%# Eval("piso") %>' />
                                            <%# Eval("piso") %>
                                        </td>
                                        <td>
                                            <%# Eval("cantidadArticulo") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <thead>
                                        <tr id="filterrow">
                                            <td></td>
                                            <th>Bodega</th>
                                            <th>Estante</th>
                                            <th>Piso</th>
                                            <td>Cantidad</td>
                                        </tr>
                                    </thead>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                    </div>
                </div>
                <!-- Fin Modal body -->

                <!-- Modal footer -->
                <div class="modal-footer">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Ubicacion_Click" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                </div>
                <!-- Fin Modal footer -->
            </div>
            <!-- Fin Modal content -->
        </div>
    </div>
    <!-- Fin Modal Ubicaciones -->


    <script type="text/javascript" src="../Scripts/dataSourcePlugins.js"></script>
    <script src="../Scripts/moment.js"></script>
    <script src="../Scripts/transition.js"></script>
    <script src="../Scripts/collapse.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>
    <!-- script tabla jquery -->
    <script type="text/javascript">

        $('#tbArticulosIngreso thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbArticulosIngreso thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });
        // DataTable
        var tbArticulosIngreso = $('#tbArticulosIngreso').DataTable({
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
        $("#tbArticulosIngreso thead input").on('keyup change', function () {
            tbArticulosIngreso
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        function limpiarIngreso() {
            $("#tbArticulosIngreso thead input").keyup();
        }

        /****************************** Tabla Modal Articulos **********************************/
        $('#tbArticulos thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbArticulos thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });
        // DataTable
        var tbArticulos = $('#tbArticulos').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 10,
            "bPaginate": false,
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
        $("#tbArticulos thead input").on('keyup change', function () {
            tbArticulos
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        function limpiarArticulos() {
            $("#tbArticulos thead input").keyup();
        }

        // Modal Articulos
        function levantarModalArticulos() {
            $('#modalArticulos').modal('show');
        };

        function cerrarModalArticulos() {
            $('#modalArticulos').modal('hide');
        };
        /*************************** Fin Tabla Modal Articulos *********************************/

        /****************************** Tabla Modal Ubicaciones **********************************/
        $('#tbArticuloUbicaciones thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbArticuloUbicaciones thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        // DataTable
        var tbArticuloUbicaciones = $('#tbArticuloUbicaciones').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 10,
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
        $("#tbArticuloUbicaciones thead input").on('keyup change', function () {
            tbArticuloUbicaciones
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        function limpiarArticuloUbicaciones() {
            $("#tbArticuloUbicaciones thead input").keyup();
        }

        // Modal Ubicaciones
        function levantarModalUbicacion() {
            $('#modalUbicaciones').modal('show');
        };

        function cerrarModalUbicacion() {
            $('#modalUbicaciones').modal('hide');
        }
        /*************************** Fin Tabla Modal Ubicaciones *********************************/

        // aplicar filtro
        function validarTexto(txtBox) {
            var id = txtBox.id.substring(12);

            if (id == "txbNumeroPedido") {
                var numeroPedidoIncorrecto = document.getElementById('<%= divNumeroPedidoIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";
                    cedulaProveedorIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    numeroPedidoIncorrecto.style.display = 'block';
                }
            }
        }

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


