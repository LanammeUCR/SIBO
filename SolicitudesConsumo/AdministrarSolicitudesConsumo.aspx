<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarSolicitudesConsumo.aspx.cs" Inherits="SIBO.Articulo.AdministrarSolicitudesConsumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblAdministrarSolicitudesUnidades" runat="server" Text="Solicitudes de Consumo" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>

            <%-- tabla--%>
            <%-- botones --%>
            <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10">
                <asp:Button ID="btnNuevoArriba" runat="server" Text="Nueva Solicitud" CssClass="btn btn-primary" OnClick="btnNueva_Solicitud_Click" />
            </div>
            <%-- fin botones --%>
            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; top: 10px; left: 30px;">

                <asp:Repeater ID="rpOrdenesConsumo" runat="server">
                    <HeaderTemplate>
                        <table id="tbOrdenes" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Numero de Solicitud</th>
                                    <th>Recepcion Solicitante</th>
                                    <th>Funcionario</th>
                                    <th>Fecha Solicitud</th>
                                    <th>Estado</th>
                                    <th>Descripción</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnVer" runat="server" ToolTip="Ver" OnClick="btnVer_Click" CommandArgument='<%# Eval("idConsumo") %>'><span class="btn glyphicon glyphicon-eye-open"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Eval("idConsumo") %>'><span class="btn glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("idConsumo") %>'><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                            </td>
                            <td>
                                <span class="btn glyphicon glyphicon-alert hide"></span><%# Eval("idConsumo") %>
                            </td>
                            <td>
                                <%# Eval("unidadSolicitante.nombre") %>
                            </td>
                            <td>
                                <%# Eval("persona.nombre") %>
                            </td>
                            <td>
                                <%#Convert.ToDateTime(Eval("fechaSolicitud")).ToShortDateString()%>
                            </td>
                            <td>
                                <%# Eval("estado") %>
                            </td>
                             <td>
                                <%# Eval("decripcion") %>
                            </td>
                        </tr>

                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Numero de Solicitud</th>
                                <th>Recepcion Solicitante</th>
                                <th>Funcionario</th>
                                <th>Fecha Entrega</th>
                                <th>Estado</th>
                                <th>Descripción</th>
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

            <%-- botones --%>
            <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10">
                <asp:Button ID="btnEnviar" runat="server" Text="Nueva Solicitud" CssClass="btn btn-primary" OnClick="btnNueva_Solicitud_Click" />
            </div>
            <%-- fin botones --%>
            <%-- paneles que se muestran a la derecha que indican el significado de los colores de las muestras --%>
            <div class="panel-group">
                <div class="affix col-md-2 col-xs-2 col-sm-2" style="left: 82%">
                    <div class="panel-body panel" style="background-color: #005da4; color: #fff; opacity: 0.6">
                        Solicitud Enviada
                    </div>
                    <div class="panel-body" style="background-color: #2EA716; color: #fff; opacity: 0.6">
                        Entrega de Solicitud Completa
                    </div>
                    <div class="panel-body" style="background-color: #FF9900; color: #fff; opacity: 0.6">
                        Entrega de Solicitud Parcial
                    </div>
                </div>
            </div>
            <%-- paneles que se muestran a la derecha que indican el significado de los colores de las muestras --%>
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

        $('#tbOrdenes thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbOrdenes thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });
        // DataTable
        var tbOrdenes = $('#tbOrdenes').DataTable({
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
            "rowCallback": function (row, data, index) {

                if (data[5] == "Enviada") {
                    $("td:eq(1)", row).css("background-color", "#005da4").css("color", "#FFFFFF").css("opacity", "0.6");
                } else if (data[5] == "Completa") {
                    $("td:eq(1)", row).css("background-color", "#2EA716").css("color", "#FFFFFF").css("opacity", "0.6");
                } else if (data[5] == "Parcial") {
                    $("td:eq(1)", row).css("background-color", "#FF9900").css("color", "#FFFFFF").css("opacity", "0.6");
                }
            },
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
        $("#tbOrdenes thead input").on('keyup change', function () {
            tbOrdenes
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        function limpiar() {
            $("#tbOrdenes thead input").keyup();
        }

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

