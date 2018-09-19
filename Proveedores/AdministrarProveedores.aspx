﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarProveedores.aspx.cs" Inherits="SIBO.Proveedores.AdministrarProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblAdministrarProveedores" runat="server" Text="Catálogo Proveedores" Font-Size="Large" ForeColor="Black"></asp:Label>
            </center>
            <%-- fin titulo pantalla --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>
               <%-- botones --%>
            <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10">
                <asp:Button ID="btnNuevoArriba" runat="server" Text="Agregar Proveedor" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />
            </div>
            <%-- fin botones --%>     
            <%-- tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto; top: 0px; left: 0px;">

                <asp:Repeater ID="rpProveedor" runat="server">
                    <HeaderTemplate>
                        <table id="tblProveedor" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Cédula</th>
                                    <th>Nombre </th>   
                                    <th>Teléfono </th>                                
                                    <th>Correo Electrónico</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>                               
                                <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Eval("idProveedor") %>'><span class="btn glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="nar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("idProveedor") %>'><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                            </td>
                            <td>
                                <%# Eval("cedula") %>
                            </td>
                             <td>
                                <%# Eval("nombre") %>
                            </td>                    
                              <td>
                                <%# Eval("telefono") %>
                            </td>       
                            <td>
                                <%# Eval("correo") %>
                            </td>                           
                        </tr>

                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Cédula</th>
                                <th>Nombre </th>   
                                <th>Teléfono </th>                            
                                <th>Correo Electrónico</th>                           
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
                <asp:Button ID="btnNuevoAbajo" runat="server" Text="Agregar Proveedor" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />
            </div>
            <%-- fin botones --%>          
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

        $('#tblProveedor thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblProveedor thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        // DataTable
        var table = $('#tblProveedor').DataTable({
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
        $("#tblProveedor thead input").on('keyup change', function () {
            table
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
