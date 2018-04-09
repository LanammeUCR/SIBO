<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarUnidades.aspx.cs" Inherits="SIBO.Unidad.AdministradorUnidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- titulo pantalla --%>
    <div class="divRedondo">
        <div class="row">
            <center>
            <asp:Label ID="lblAdministrarUnidades" runat="server" Text="Administrar Unidades" Font-Size="Large" ForeColor="Black"></asp:Label>
        </center>
            <%-- fin titulo pantalla --%>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                <hr />
            </div>

            <%-- tabla--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto; top: 0px; left: 0px;">

                <asp:Repeater ID="rpUnidad" runat="server" OnItemDataBound="rpUnidad_ItemDataBound">
                    <HeaderTemplate>
                        <table id="tblUnidad" class="row-border table-striped">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Nombre Unidad</th>
                                    <th>Numero Teléfono</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Eval("idUnidad") %>'><span class="btn glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("idUnidad") %>'><span class="btn glyphicon glyphicon-trash"></span></asp:LinkButton>
                            </td>
                            <td>
                                <%# Eval("nombre") %>
                            </td>
                            <td>
                                <%# Eval("telefono") %>
                            </td>                           
                        </tr>

                    </ItemTemplate>

                    <FooterTemplate>
                        <thead>
                            <tr id="filterrow">
                                <td></td>
                                <th>Nombre Unidad</th>
                                <th>Numero Teléfono</th>                               
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
                <asp:Button ID="btnNuevo" runat="server" Text="Nueva Unidad" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />
            </div>
            <%-- fin botones --%>          
        </div>
    </div>

    <!-- Modal de Enviar -->
    <div id="modalEnviar" class="modal fade" role="alertdialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <!-- Modal header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Enviar</h4>
                </div>
                <!-- Fin Modal header -->

                <!-- Modal body -->
                <div class="modal-body">

                        <div class="row">

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <center>
                                    <asp:Label ID="Label5" runat="server" Text="Correo(s) destinatarios" Font-Size="Large" ForeColor="Black" ></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <asp:TextBox class="form-control" ID="txtCorreos" runat="server" Width="100%" onkeydown="cambiarEspacio(this,event)"></asp:TextBox>
                            </div>

                            <div class="col-xs-12">
                                <div class="col-xs-12">
                                    <h6 style="text-align: left">Los correos se separan por <span style='color: red;font:bold'>;</span></h6>
                                </div>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <center>
                                    <asp:Label ID="Label1" runat="server" Text="Con copia a" Font-Size="Large" ForeColor="Black" ></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <asp:TextBox class="form-control" ID="txtConCopiaA" runat="server" Width="100%" onkeydown="cambiarEspacio(this,event)"></asp:TextBox>
                            </div>

                            <div class="col-xs-12">
                                <div class="col-xs-12">
                                    <h6 style="text-align: left">Los correos se separan por <span style='color: red;font:bold'>;</span></h6>
                                </div>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <center>
                                    <asp:Label ID="Label2" runat="server" Text="Con copia oculta a" Font-Size="Large" ForeColor="Black" ></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <asp:TextBox class="form-control" ID="txtConCopiaOcultaA" runat="server" Width="100%" onkeydown="cambiarEspacio(this,event)"></asp:TextBox>
                            </div>

                            <div class="col-xs-12">
                                <div class="col-xs-12">
                                    <h6 style="text-align: left">Los correos se separan por <span style='color: red;font:bold'>;</span></h6>
                                </div>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <center>
                                    <asp:Label ID="Label6" runat="server" Text="Cuerpo del correo" Font-Size="Large" ForeColor="Black" ></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <asp:TextBox class="form-control" ID="txtAsunto" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <hr />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <center>
                                    <asp:Label ID="Label3" runat="server" Text="Seleccionar archivos" Font-Size="Large" ForeColor="Black" ></asp:Label>
                                </center>
                            </div>
                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <br />
                            </div>
                        </div>
                </div>
                <!-- Fin Modal body -->

              
            </div>
            <!-- Fin Modal content -->
        </div>
    </div>
    <!-- Fin Modal de Enviar -->

    <!-- Modal de generar pdf -->
    <div id="modalGenerarPDF" class="modal fade" role="alertdialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content -->
            <div class="modal-content">
                <!-- Modal header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Seleccionar notas</h4>
                </div>
                <!-- Fin Modal header -->

            
                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <hr />
                            </div>
                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <center>
                                    <asp:Label ID="Label7" runat="server" Text="Notas que aparecerán en el reporte" Font-Size="Large" ForeColor="Black" ></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                <asp:TextBox class="form-control" ID="txtNotas" TextMode="MultiLine" runat="server" Width="100%" Height="400px"></asp:TextBox>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <div class="col-md-3 col-xs-3 col-sm-3">
                                    <asp:Label ID="lblFecha" runat="server" Text="Fecha de entrega estimada <span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-3 col-xs-4 col-sm-4 input-group date" id="divFecha">
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                    <asp:TextBox CssClass="form-control" ID="txtFecha" runat="server" onInput="validarFecha(this)" onChange="validarFecha(this)" onFocus="validarFecha(this)" placeholder="dd/mm/yyyy"></asp:TextBox>
                                </div>
                                <div class="col-md-5 col-xs-5 col-sm-5" id="divFechaIncorrecta" runat="server" style="display: none;">
                                    <asp:Label ID="lblFechaIncorrecta" runat="server" Font-Size="Small" CssClass="label alert-danger" Text="Fecha incorrecta" Visible="false" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">
                                <br />
                            </div>

                            <div class="col-md-12 col-xs-12 col-sm-12">

                                <div class="col-md-3 col-xs-3 col-sm-3">
                                    <asp:Label ID="lblCostoDesayuno" runat="server" Text="Autorizado por" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-3 col-xs-3 col-sm-3">
                                    <asp:TextBox class="form-control" ID="txtAutorizadoPor" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-xs-12">
                                <br />
                                <div class="col-xs-12">
                                    <h6 style="text-align: left">Los campos marcados con <span style='color: red'>*</span> son requeridos.</h6>
                                </div>
                            </div>

                        </div>
                </div>
                <!-- Fin Modal body -->
              
            </div>
            <!-- Fin Modal content -->
        </div>
    </div>
    <!-- Fin Modal de generar pdf -->

     <script src="../Scripts/moment.js"></script>
    <script src="../Scripts/transition.js"></script>
    <script src="../Scripts/collapse.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>

    <script type="text/javascript" src="../Scripts/dataSourcePlugins.js"></script>
    <!-- script tabla jquery -->
    <script type="text/javascript">

        $('#tblSolicitud thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblSolicitud thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        // DataTable
        var table = $('#tblSolicitud').DataTable({
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

                if (data[4] == "Aprobación técnica") {
                    $("td:eq(1)", row).css("background-color", "#1C7908").css("color", "#FFFFFF").css("opacity", "0.6");
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
        $("#tblSolicitud thead input").on('keyup change', function () {
            table
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        /*tabla notas*/

        $('#tblNota thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblNota thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        var tblNota = $('#tblNota').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 5,
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
        $("#tblNota thead input").on('keyup change', function () {
            tblNota
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        /*tabla notas enviar*/

        $('#tblNotaEnviar thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblNotaEnviar thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        var tblNotaEnviar = $('#tblNotaEnviar').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 5,
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
        $("#tblNotaEnviar thead input").on('keyup change', function () {
            tblNotaEnviar
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        /*tabla archivos*/

        $('#tblArchivos thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblArchivos thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        var tblArchivos = $('#tblArchivos').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 5,
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
        $("#tblArchivos thead input").on('keyup change', function () {
            tblArchivos
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        /*tabla archivos seleccionados*/

        $('#tblArchivosSeleccionados thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblArchivosSeleccionados thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        var tblArchivosSeleccionados = $('#tblArchivosSeleccionados').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 5,
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
        $("#tblArchivosSeleccionados thead input").on('keyup change', function () {
            tblArchivosSeleccionados
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        /*tabla notas generar pdf*/

        $('#tblNotaGenerarPDF thead tr#filterrow th').each(function () {
            var campoBusqueda = $('#tblNotaGenerarPDF thead th').eq($(this).index()).text();
            $(this).html('<input type="text" style="text-align: center" onclick="stopPropagation(event);" placeholder="Buscar ' + campoBusqueda + '" />');
        });

        var tblNotaGenerarPDF = $('#tblNotaGenerarPDF').DataTable({
            orderCellsTop: true,
            "iDisplayLength": 5,
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
        $("#tblNotaGenerarPDF thead input").on('keyup change', function () {
            tblNotaGenerarPDF
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
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">

        $(function () {
            // Fechas
            $('#divFecha').datetimepicker({
                format: 'DD/MM/YYYY',
                locale: moment.locale('es')
            });
        });

        function validarFecha(txtFecha) {
            patron = /^\d{2}\/\d{2}\/\d{4}$/;

            var id = txtFecha.id.substring(12);
            var fechaIncorrecta;

            fechaIncorrecta = document.getElementById('<%= divFechaIncorrecta.ClientID %>');

            if (txtFecha.value != "" && patron.test(txtFecha.value)) {
                txtFecha.className = "From-Date form-control";
                fechaIncorrecta.style.display = 'none';
            } else {
                txtFecha.className = "From-Date form-control alert-danger";
                fechaIncorrecta.style.display = 'block';
            }
        };
    </script>
</asp:Content>
