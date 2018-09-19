<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticulosUbicaciones.aspx.cs" Inherits="SIBO.Inventario.ArticulosUbicaciones" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblUbicacion" runat="server" Text="Actualizar Existencias" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>
            <%-- botones --%>
            <%-- fin botones --%>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12 ">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblBodega" runat="server" Text="Bodega: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:DropDownList ID="ddlBodegas" runat="server" class="btn btn-default dropdown-toggle" OnSelectedIndexChanged="ddlBodegas_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <%-- tabla--%>
            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto; top: 0px; left: 0px;">

                <asp:Repeater ID="rpUbicacionesArticulos" runat="server">
                    <HeaderTemplate>
                        <table id="tblUbicaciones" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Articulo</th>
                                    <td>Cantidad Disponible</td>
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
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Articulo</th>
                                <td></td>
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
        </div>
    </div>

    <!-- Modal de Ubicacion -->
    <div id="modalArticulo" class="modal fade" role="alertdialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <!-- Modal header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Actualizar Existencias</h4>
                </div>
                <!-- Fin Modal header -->
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <%-- campos a llenar --%>

                        <div class="col-md-12 col-xs-12 col-sm-12">

                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lblNombreArticulo" runat="server" Text="Nombre  " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4 col-xs-4 col-sm-4">
                                <asp:TextBox class="form-control" ID="txbNombreArticulo" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lblCodigo" runat="server" Text="Codigo" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:TextBox class="form-control" ID="txbCodigo" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lbDescripcionArticulo" runat="server" Text="Descripción  " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4 col-xs-4 col-sm-4">
                                <asp:TextBox class="form-control" ID="txbDescripcion" runat="server" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lblCantidadTotal" runat="server" Text="Cantidad" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:TextBox class="form-control" ID="txbCantidadTotal" runat="server" TextMode="Number" Min="1" AutoPostBack="true" OnTextChanged="txbCantidadTotal_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                    </div>
                    <%-- fin campos a llenar --%>
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

        //Tabla de Articulos
        function levantarModalArticulo() {
            $('#modalArticulo').modal('show');
        };

        function cerrarModalArticulo() {
            $('#modalArticulo').modal('hide');
        };
    </script>
</asp:Content>


