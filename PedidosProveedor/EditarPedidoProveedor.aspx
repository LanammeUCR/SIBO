<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPedidoProveedor.aspx.cs" Inherits="SIBO.PedidosProveedor.EditarPedidoProveedor" MaintainScrollPositionOnPostback="true"%>
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
                    <asp:TextBox ID="txbNumeroPedido" AutoPostBack="true" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>                                            
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
                    <asp:Label ID="lblArticulo" runat="server" Text="Proveedor: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
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
                <div class="col-sm-6 col-md-6 col-xs-6 col-md-offset-2 col-xs-offset-2 col-sm-offset-2">
                    <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="btn btn-default dropdown-toggle"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-12 col-xs-12 col-sm-12">
                    <br />
                </div>
            <div>
                <div class="col-md-12 col-xs-12 col-sm-12 ">
                    <div class="col-md-2 col-xs-2">
                        <asp:Label ID="Label1" runat="server" Text="Articulo: " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 col-xs-3">
                        <div class="input-group">
                            <asp:TextBox ID="tbxNombreArticulo" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="tbNombreArticulo_TextChanged"></asp:TextBox>
                            <span class="input input-group-addon"><span class="glyphicon glyphicon-search"></span>&nbsp;&nbsp;</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <br />
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <div class="col-md-6 col-xs-6">
                        <asp:ListBox ID="lbArticulos" runat="server" class="input input-group-addon" CssClass="form-control" Height="150px"></asp:ListBox>
                    </div>
                    <div class="col-md-3 col-xs-3">
                        <asp:Button ID="btnAgregar" runat="server" class="input input-group-addon" Text="Agregar Articulo" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                    </div>
                </div>
            </div>
            <%-- tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; top: 10px; left: 30px;">

                <asp:Repeater ID="rpEditarSolicitudProveedor" runat="server" OnItemDataBound="rpEditarSolicitudProveedor_ItemDataBound">
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
                            <td>
                                <asp:LinkButton ID="btnnar" runat="server" ToolTip="nar" OnClick="btnnarArticulo_Click" CommandArgument='<%# Eval("articulo.idArticulo") %>'><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                            </td>
                            <td>
                                <%# Eval("articulo.nombreArticulo") %>
                            </td>                           
                            <td>
                                <asp:TextBox class="form-control" ID="cantidadArticulo" runat="server" Width="70px" TextMode="Number" min="1" OnTextChanged="cantidadArticuloTextChanged" Height="25px" Text='<%# Eval("cantidad") %>' AutoPostBack="True"></asp:TextBox>
                            </td>
                             <td>
                                <asp:CheckBox  class="form-control" ID="chkEntregado" runat="server" OnCheckedChanged="chkEntregado_CheckedChanged" Checked='<%# Eval("entregado") %>' AutoPostBack="true"/>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Nombre Articulo</th>                               
                                <td></td>
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
                <asp:Button ID="btnEnviar" runat="server" Text="Actualizar Pedido" CssClass="btn btn-primary" OnClick="btnEnviarSolicitud" />
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
            "bPaginate": false,
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


