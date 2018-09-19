﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="narPedidoProveedor.aspx.cs" Inherits="SIBO.PedidosProveedor.narPedidoProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblAdministrarUnidades" runat="server" Text="Solicitud de Articulos a Proveedor" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>

            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-2 col-xs-2">
                    <asp:Label ID="Label2" runat="server" Text="Numero de Pedido: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-3">
                    <asp:TextBox ID="txbNumeroPedido" AutoPostBack="true" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
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
                        <asp:TextBox ID="tbxNombreProveedor" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <br />
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                <br />
            </div>
            <%-- tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; top: 10px; left: 30px;">

                <asp:Repeater ID="rpVerSolicitudProveedor" runat="server" OnItemDataBound="rpVerSolicitudProveedor_ItemDataBound">
                    <HeaderTemplate>
                        <table id="tbArticulos" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Nombre Articulo</th>
                                    <th>Cantidad Solicitada</th>
                                    <th>Entregado</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td></td>
                            <td>
                                <%# Eval("articulo.nombreArticulo") %>
                            </td>
                            <td>
                                <%# Eval("cantidad") %>
                            </td>
                            <td>
                                <asp:CheckBox class="form-control" ID="chkEntregado" runat="server" Enabled="false" Checked='<%# Eval("entregado") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Nombre Articulo</th>
                                <th>Cantidad Disponible</th>
                                <th>Entregado</th>
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
            <div class="col-md-5 col-xs-5 col-sm-5 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                <asp:Button ID="btnEnviar" runat="server" Text="nar Pedido" CssClass="btn btn-primary" OnClick="btnAceptar_Pedido_Click" />
                &nbsp;
                <asp:Button ID="tbnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar" CssClass="btn btn-danger" />

            </div>
            <%-- fin botones --%>
        </div>
    </div>

    <script type="text/javascript" src="../Scripts/dataSourcePlugins.js"></script>
    <!-- script tabla jquery -->
    <script type="text/javascript">

        $('#tbArticulos thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tbArticulos thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });
        // DataTable
        var tbArticulos = $('#tbArticulos').DataTable({
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
        $("#tbArticulos thead input").on('keyup change', function () {
            tbArticulos
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });
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
