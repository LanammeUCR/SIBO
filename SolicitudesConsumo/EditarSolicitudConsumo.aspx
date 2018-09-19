<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarSolicitudConsumo.aspx.cs" Inherits="SIBO.SolicitudesConsumo.EditarSolicitudConsumo" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblAdministrarUnidades" runat="server" Text="Editar Solicitud de Articulos" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblIdSolicitud" runat="server" Text="Id Solicitud: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lbIDConsumo" runat="server" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <br />
                </div>
                <div class="col-md-2 col-xs-2 col-sm-2">
                    <asp:Label ID="lblUnidad" runat="server" Text="Recepción: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-2 col-xs-2 col-sm-2">

                    <asp:Label ID="lblUnidadNombre" runat="server" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>

                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-4 col-xs-4 col-sm-4 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                    <asp:Button ID="btnAgregar" runat="server" class="input input-group-addon" Text="Agregar Articulo" CssClass="btn btn-success" OnClick="btnAgregar_Click" />
                    &nbsp;
                    <asp:Button ID="btnAceptarArriba" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnEnviarSolicitud" />
                    &nbsp;                                                                                
                    <asp:Button ID="btnCancelarArriba" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                </div>
            </div>
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>--%>
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <%--tabla--%>
            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; top: 10px; left: 30px;">

                <asp:Repeater ID="rpSolicitud" runat="server">
                    <HeaderTemplate>
                        <table id="tbArticulosSolicitud" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Nombre Articulo</th>
                                    <th>Cantidad Disponieble</th>
                                    <th>Cantidad Solicitada</th>
                                    <th>Cantidad Entregada</th>
                                    <th>Cantidad a Entregar</th>
                                    <th>Cantidad Pendiente</th>
                                    <th>Entrega Total</th>
                                    <th>Entrega Parcial</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Eval("articulo.idArticulo") %>' Visible='<%# !Convert.ToBoolean(Eval("entregado")) %>'><span class="btn glyphicon glyphicon-pencil"></span></asp:LinkButton>
                            </td>
                            <td>
                                <%# Eval("articulo.nombreArticulo") %>
                            </td>
                            <td>
                                <%# Eval("articulo.cantidadTotal") %>
                            </td>
                            <td>
                                <%#  Eval("cantidadConsumo") %>
                            </td>
                            <td>
                                <%#  Eval("cantidadEntregada") %>
                            </td>
                            <td>
                                <%#  Eval("cantidadEntregar") %>
                            </td>
                            <td>
                                <%#  Eval("cantidadPendiente") %>
                            </td>

                            <td>
                                <asp:CheckBox class="form-control" ID="chkEntregaTotal" runat="server" OnCheckedChanged="chkEntregaTotal_CheckedChanged" AutoPostBack="true" Visible='<%# !Convert.ToBoolean(Eval("entregado")) %>' />
                            </td>
                            <td>
                                <asp:CheckBox class="form-control" ID="chkEntregaParcial" runat="server" OnCheckedChanged="chkEntregaParcial_CheckedChanged" AutoPostBack="true" Visible='<%# !Convert.ToBoolean(Eval("entregado")) %>'/>
                            </td>

                        </tr>

                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Nombre Articulo</th>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </thead>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <%-- fin tabla--%>
            <%-- </asp:UpdatePanel>--%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- botones --%>
            <div class="col-md-5 col-xs-5 col-sm-5 col-md-offset-10 col-xs-offset-10 col-sm-offset-10">
                <asp:Button ID="btnEnviar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnEnviarSolicitud" />
                &nbsp;                                                                                
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
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
                                <asp:Label ID="lblCantidadSolicitar" runat="server" Text="Cantidad a Solicitar: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
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

    <!-- Modal de Administrador -->
    <div id="modalAdministrar" class="modal fade" role="alertdialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <!-- Modal header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Administrar Articulo</h4>
                </div>
                <!-- Fin Modal header -->

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lbArticulo" runat="server" Text="Articulo: " Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                            <div class="col-md-5 col-xs-5 col-sm-5">
                                <asp:Label ID="lbNombreArticulo" runat="server" Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lbCantidadDisponible" runat="server" Text="Cantidad Disponible: " Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:Label ID="lbCantidad" runat="server" Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lbCantidadSolicitada" runat="server" Text="Cantidad Solicitada:" Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:TextBox class="form-control" ID="txbCantidadSolicitada" OnTextChanged="txbCantidad_TextChanged" AutoPostBack="true" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-5 col-xs-5 col-sm-5">
                                &nbsp;
                            <asp:CheckBox ID="chkRechazar" runat="server" OnCheckedChanged="chkRechazar_CheckedChanged" Text="Rechazar" AutoPostBack="true" CssClass="checkbox" Font-Size="Large" ForeColor="Black" />
                                &nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chkModificar" runat="server" OnCheckedChanged="chkModificar_CheckedChanged" Text="Modificar" AutoPostBack="true" CssClass="checkbox" Font-Size="Large" ForeColor="Black" />
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lblComentario" runat="server" Text="Comentario:" Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                            <div class="col-md-5 col-xs-5 col-sm-5">
                                <asp:TextBox class="form-control" ID="txbComentario" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                        </div>
                    </div>
                </div>
                <!-- Fin Modal body -->

                <!-- Modal footer -->
                <div class="modal-footer">
                    <asp:Button ID="btnAceptarAdministrar" runat="server" Text="Aceptar" OnClick="btnAceptarAdministrar_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
                <!-- Fin Modal footer -->
            </div>
            <!-- Fin Modal content -->
        </div>
    </div>
    <!-- Fin Modal de Administrador -->

    <!-- Modal de Cantidad Entrega -->
    <div id="modalCantidadEntrega" class="modal fade" role="alertdialog">
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
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lblCantidadSolicitada" runat="server" Text="Cantidad Solicitada:" Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:TextBox class="form-control" ID="txbCantidadSolicitadaParcial" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <br />
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="col-md-3 col-xs-3 col-sm-3">
                                <asp:Label ID="lbCantidadParcial" runat="server" Text="Cantidad a Entregar:" Font-Size="Large" ForeColor="Black"></asp:Label>
                            </div>
                            <div class="col-md-2 col-xs-2 col-sm-2">
                                <asp:TextBox class="form-control" ID="txbCantidadEntregar" OnTextChanged="txbCantidad_TextChanged" runat="server" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <br />
                            </div>
                        </div>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <asp:Repeater ID="rpUbicacionesParcial" runat="server">
                                <HeaderTemplate>
                                    <table id="tbUbicaciones" class="row-border table-striped">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Bodega</th>
                                                <th>Estante</th>
                                                <th>Piso</th>
                                                <th>Cantidad</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr>
                                        <%--<asp:HiddenField ID="hfIdArticuloSeleccionado" runat="server" Value='<%# Eval("idArticulo") %>' />--%>
                                        <td>
                                            <asp:LinkButton ID="btnSeleccionarUbicacionParcial" runat="server" ToolTip="Selecionar" CommandArgument='<%# Eval("bodega.idBodega") +","+Eval("estante")+","+ Eval("piso")+","+ Eval("cantidadArticulo")%>' OnClick="btnSeleccionarUbicacion_Click"><span class="btn glyphicon glyphicon-ok"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("bodega.nombre") %>
                                        </td>
                                        <td>
                                            <%# Eval("estante") %>
                                        </td>
                                        <td>
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
                                            <th>Bodega/Estante/Piso</th>
                                            <td></td>
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
                    <!-- Fin Modal body -->
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptarEntrega_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
                <!-- Fin Modal footer -->
            </div>
            <!-- Fin Modal content -->
        </div>
    </div>
    <!-- Fin Modal Cantidad Entrega -->


    <!-- Modal de Ubicacion -->
    <div id="modalEntregaUbicacion" class="modal fade" role="alertdialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <!-- Modal header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Selección Entrega Bodega</h4>
                </div>
                <!-- Fin Modal header -->

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <br />
                            <asp:Repeater ID="rpUbicaciones" runat="server">
                                <HeaderTemplate>
                                    <table id="tbUbicaciones" class="row-border table-striped">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Bodega</th>
                                                <th>Estante</th>
                                                <th>Piso</th>
                                                <th>Cantidad</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr>
                                        <%--<asp:HiddenField ID="hfIdArticuloSeleccionado" runat="server" Value='<%# Eval("idArticulo") %>' />--%>
                                        <td>
                                            <asp:LinkButton ID="btnSeleccionarUbicacionTotal" runat="server" ToolTip="Selecionar" CommandArgument='<%# Eval("bodega.idBodega") +","+Eval("estante")+","+ Eval("piso")+","+ Eval("cantidadArticulo")%>' OnClick="btnSeleccionarUbicacion_Click"><span class="btn glyphicon glyphicon-ok"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("bodega.nombre") %>
                                        </td>
                                        <td>
                                            <%# Eval("estante") %>
                                        </td>
                                        <td>
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
                                            <th>Bodega/Estante/Piso</th>
                                            <td></td>
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
                    <asp:Button ID="Button1" runat="server" Text="Aceptar" OnClick="btnAceptarEntrega_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
                <!-- Fin Modal footer -->
            </div>
            <!-- Fin Modal content -->
        </div>
    </div>
    <!-- Fin Modal Cantidad Entrega -->

    <script src="../Scripts/moment.js"></script>
    <script src="../Scripts/transition.js"></script>
    <script src="../Scripts/collapse.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>

    <script type="text/javascript" src="../Scripts/dataSourcePlugins.js"></script>
    <!-- script tabla jquery -->
    <script type="text/javascript">

        $('#tbArticulosSolicitud thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbArticulosSolicitud thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });
        // DataTable
        var tbArticulos = $('#tbArticulosSolicitud').DataTable({
            orderCellsTop: true,
            "bPaginate": false,
            "aLengthMenu": [[2, 5, 10, -1], [2, 5, 10, "All"]],
            "colReorder": true,
            "select": false,
            "bSort": true,
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

        function limpiarSolicitud() {
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

        /****************************** Tabla Modal Ubicaciones **********************************/
        $('#tbUbicaciones thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbUbicaciones thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        // DataTable
        var tblArticulos = $('#tbUbicaciones').DataTable({
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
        $("#tbUbicaciones thead input").on('keyup change', function () {
            tbUbicaciones
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        function limpiarArticulos() {
            $("#tbUbicaciones thead input").keyup();
        }

        /*************************** Fin Tabla Modal Articulos *********************************/

        // Tabla de articulos
        function levantarModalArticulos() {
            $('#modalArticulos').modal('show');
        };

        function cerrarModalArticulos() {
            $('#modalArticulos').modal('hide');
        };


        // Modal Administrar
        function levantarModalAdministrador() {
            $('#modalAdministrar').modal('show');
        };

        function cerrarModalAdministrador() {
            $('#modalAdministrar').modal('hide');
        };

        // Modal Cantidad Entrega
        function levantarModalCantidad() {
            $('#modalCantidadEntrega').modal('show');
        };

        function cerrarModalCantidad() {
            $('#modalCantidadEntrega').modal('hide');
        };

        // Modal Cantidad Entrega
        function levantarModalUbicacion() {
            $('#modalEntregaUbicacion').modal('show');
        };

        function cerrarModalUbicacion() {
            $('#modalEntregaUbicacion').modal('hide');
        };

        // aplicar filtro
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


