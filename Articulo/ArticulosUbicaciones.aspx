<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticulosUbicaciones.aspx.cs" Inherits="SIBO.Articulo.ArticulosUbicaciones" MaintainScrollPositionOnPostback="true" %>

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
            <%-- botones --%>
            <%-- fin botones --%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <%-- tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto; top: 0px; left: 0px;">

                <asp:Repeater ID="rpUbicacionesArticulos" runat="server" OnItemDataBound="rpDetalleUbicaciones_ItemDataBound">
                    <HeaderTemplate>
                        <table id="tblUbicaciones" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Articulo</th>
                                    <td>Cantidad Disponible</td>
                                    <td>Cantidad Para Ubicar</td>
                                    <th>Ubicaciones</th>

                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnSeleccionar" runat="server" ToolTip="Selecionar" OnClick="btnSeleccionar_Click" CommandArgument='<%# Eval("idArticulo") %>'> <span class="btn glyphicon glyphicon-ok"></span></asp:LinkButton>
                            </td>
                            <td>
                                <%# Eval("nombreArticulo") %>
                            </td>
                            <td>
                                <%# Eval("cantidadTotal") %>
                            </td>
                            <td>
                                <%# Eval("cantidadUbicable") %>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfIdUbicacionesArticulo" runat="server" Value='<%# Eval("idArticulo") %>' />
                                <asp:Repeater ID="rpUbicaciones" runat="server">
                                    <HeaderTemplate>
                                        <div class="col-xs-12">
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="btnVerArchivo" runat="server" Text='<%# Eval("nombreArchivo") %>' OnClick="btnVerArchivo_Click" CommandArgument='<%# Eval("idArchivoMuestra")+","+Eval("nombreArchivo")+","+Eval("rutaArchivo") %>'></asp:LinkButton>--%>
                                        <%# Eval("bodega.nombre")+", cantidad "+ Eval("cantidadArticulo") %>
                                        <br />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Articulo</th>
                                <td></td>
                                <td></td>
                                <th>Ubicaciones</th>
                            </tr>
                        </thead>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <%-- fin tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

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
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                    <ContentTemplate>
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
                                                <asp:Label ID="lbEstante" runat="server" Text="Estante <span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txbEstante" runat="server"></asp:TextBox>
                                            </div>
                                            <div id="divEstanteIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                                                <asp:Label ID="lbEstanteIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                            </div>
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-md-2 col-xs-2 col-sm-2">
                                                <asp:Label ID="lbPiso" runat="server" Text="Piso <span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txbPiso" runat="server"></asp:TextBox>
                                            </div>
                                            <div id="divPisoIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                                                <asp:Label ID="lbPisoIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                            </div>
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-md-2 col-xs-2 col-sm-2">
                                                <asp:Label ID="lbCantidad" runat="server" Text="Cantidad <span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txbCantidad" runat="server" TextMode="Number" min="1" OnTextChanged="txbCantidad_TextChanged"></asp:TextBox>
                                            </div>
                                            <div id="divCantidadIncorrecto" runat="server" style="display: none" class="col-md-6 col-xs-6 col-sm-6">
                                                <asp:Label ID="lbCantidadIncorrecto" runat="server" Font-Size="Small" class="label alert-danger" Text="Espacio Obligatorio" ForeColor="Red"></asp:Label>
                                            </div>
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                        <div class="col-md-6 col-xs-6 col-sm-6 col-md-offset-6 col-xs-offset-6 col-sm-offset-6">
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
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
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    </div>
                    <!-- Fin Modal footer -->
                </div>
                <!-- Fin Modal content -->
            </div>
        </div>
        <!-- Fin Modal Ubicaciones -->

    </div>
    </div>




    <script src="../Scripts/moment.js"></script>
    <script src="../Scripts/transition.js"></script>
    <script src="../Scripts/collapse.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>

    <script type="text/javascript" src="../Scripts/dataSourcePlugins.js"></script>
    <!-- script tabla jquery -->
    <script type="text/javascript">
        /****************************** Tabla Articulos **********************************/
        $('#tblUbicaciones thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblUbicaciones thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        // DataTable
        var table = $('#tblUbicaciones').DataTable({
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
        $("#tblUbicaciones thead input").on('keyup change', function () {
            table
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

       

        function keyUp() {
            $("#tblUbicaciones thead input").keyup();
        }

        // aplicar filtro
        function stopPropagation(evt) {
            if (evt.stopPropagation !== undefined) {
                evt.stopPropagation();
            } else {
                evt.cancelBubble = true;
            }
        };

        function limpiarUbicaciones() {
            $("#tblUbicaciones thead input").keyup();
        }
        /*************************** Fin Tabla Articulos *********************************/

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

        /*************************** Fin Tabla Modal Ubicaciones *********************************/

        // Tabla de clientes
        function levantarModalUbicacion() {
            $('#modalUbicaciones').modal('show');
        };

        function cerrarModalUbicacion() {
            $('#modalUbicaciones').modal('hide');
        };

        /*
       Evalúa de manera inmediata los campos de texto que va ingresando el usuario.
       */
        function validarTexto(txtBox) {
            var id = txtBox.id.substring(12);

            if (id == "txbEstante") {
                var nombreArticuloIncorrecto = document.getElementById('<%= divEstanteIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    nombreArticuloIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    nombreArticuloIncorrecto.style.display = 'block';
                }
            }

            if (id == "txbPiso") {
                var apellidosArticuloIncorrecto = document.getElementById('<%= divPisoIncorrecto.ClientID %>');
                if (txtBox.value != "") {
                    txtBox.className = "form-control";

                    apellidosArticuloIncorrecto.style.display = 'none';
                } else {
                    txtBox.className = "form-control alert-danger";
                    apellidosArticuloIncorrecto.style.display = 'block';
                }
            }
        }
    </script>
</asp:Content>


