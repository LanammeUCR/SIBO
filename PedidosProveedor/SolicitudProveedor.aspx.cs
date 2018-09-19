using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Pedidos
{
    public partial class SolicitudProveedor : System.Web.UI.Page
    {
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private ProveedorServicios proveedorServicio = new ProveedorServicios();
        private BodegaServicios bodegaServicios = new BodegaServicios();
        private DetalleBodegaServicios detalleBodegaServicio = new DetalleBodegaServicios();
        private PedidoProveedorServicios pedidoProveedorServicio = new PedidoProveedorServicios();


        #endregion
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Session["listaProveedores"] = null;
                Session["listaArticulos"] = null;
                Session["listaArticulosIngreso"] = null;
                Session["articuloIngreso"] = null;
                Session["articuloSeleccionado"] = null;
                Session["listaUbicaciones"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarIngreso();", true);
                cargardatos();
            }
        }
        #endregion

        private void cargardatos()
        {
            List<Entidades.Articulo> articulos = articuloServicio.getArticulos();
            Session["listaArticulos"] = articulos;

            List<DetallePedido> listaArticulosIngreso = new List<DetallePedido>();
            Session["listaArticulosIngreso"] = listaArticulosIngreso;

            List<Proveedor> proveedores = proveedorServicio.getProveedores();
            Session["listaProveedores"] = proveedores;

            List<DetalleBodega> listaUbicaciones = new List<DetalleBodega>();
            Session["listaUbicaciones"] = listaUbicaciones;

            ddlProveedores.DataSource = proveedores;
            ddlProveedores.DataTextField = "nombre";
            ddlProveedores.DataValueField = "idProveedor";
            ddlProveedores.DataBind();

            ddlBodegas.DataSource = bodegaServicios.getBodegas();
            ddlBodegas.DataTextField = "nombre";
            ddlBodegas.DataValueField = "idBodega";
            ddlBodegas.DataBind();

            rpSolicitudProveedor.DataSource = listaArticulosIngreso;
            rpSolicitudProveedor.DataBind();
        }

        #region logica
        /// <summary>
        /// Fabián Quirós Masís
        /// 13/04/2018
        /// Efecto: Valida los campos que debe de ingresar el usuario
        /// devuelve true si todos se encuentran correctos, de lo contrario 
        /// devuelve false y marca los campos para que el usuario vea cuales son.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        public Boolean validarCampos()
        {
            Boolean validados = true;
            divNumeroPedidoIncorrecto.Style.Add("display", "none");
            txbNumeroPedido.CssClass = "form-control";

            #region validacion numero de pedido
            String numeroPedido = txbNumeroPedido.Text;

            if (numeroPedido.Trim() == "")
            {
                txbNumeroPedido.CssClass = "form-control alert-danger";
                divNumeroPedidoIncorrecto.Style.Add("display", "block");
                validados = false;
            }
            #endregion
            return validados;
        }
        #endregion

        #region eventos

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Filtra el dropdwonlist de proveedores según el texto ingresado en la textbox Proveedor
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void tbxNombreProveedor_TextChanged(object sender, EventArgs e)
        {
            List<Entidades.Proveedor> listaProveedores = (List<Entidades.Proveedor>)Session["listaProveedores"];
            List<Entidades.Proveedor> listaFiltrados = new List<Entidades.Proveedor>();

            String busqueda = tbxNombreProveedor.Text;
            listaFiltrados = listaProveedores.FindAll(x => x.nombre.ToLower().Contains(busqueda.ToLower()));

            ddlProveedores.DataSource = listaFiltrados;
            ddlProveedores.DataTextField = "nombre";
            ddlProveedores.DataValueField = "idProveedor";
            ddlProveedores.DataBind();
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Verifica que la cantidad ingresada no sea negativa 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void txbCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txbCantidad = (TextBox)sender;
            if (!String.IsNullOrWhiteSpace(txbCantidad.Text))
            {
                int cantidad = Convert.ToInt32(txbCantidad.Text);
                if (cantidad > 0)
                {
                    if (txbCantidad.ID == "txbCantidad")
                    {
                        Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloSeleccionado"];

                        if (articulo != null)
                        {
                            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

                            //int cantidadTotal = listaSolicitud.Find(x => x.articulo.idArticulo == articulo.idArticulo).articulo.cantidadTotal;

                            DetallePedido detalleArticulo = new DetallePedido();
                            detalleArticulo.articulo = articulo;
                            detalleArticulo.cantidad = cantidad;
                            detalleArticulo.articulo.cantidadUbicable += cantidad;
                            listaArticulos.Remove(articulo);
                            Session["articuloIngreso"] = detalleArticulo;
                            Session["listaArticulos"] = listaArticulos;

                        }
                        else
                        {
                            (this.Master as SiteMaster).Mensaje("Debe seleccionar un articulo primero", "¡Alerta!");
                            txbCantidad.Text = "";
                        }
                    }
                }
                else if (txbCantidad.ID == "txbCantidadUbicar")
                {
                    (this.Master as SiteMaster).Mensaje("La cantidad del articulo a ubicar tiene que ser mayor a 1", "¡Alerta!");
                    txbCantidad.Text = "";
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado tiene que ser mayor a 1", "¡Alerta!");
                    txbCantidad.Text = "";
                }
            }
            if (txbCantidad.ID == "txbCantidadSolicitada")
            {
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
            }
            else if (txbCantidad.ID == "txbCantidad")
            {
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
            }            
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Selecciona un articulo para despues administrar las ubicaciones de este
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)(sender);
            int idArticulo = Convert.ToInt32(linkButton.CommandArgument.ToString());

            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosIngreso"];

            Entidades.Articulo articulo = listaArticulos.Find(x => x.articulo.idArticulo == idArticulo).articulo;
            int cantidadUbicar = listaArticulos.Find(x => x.articulo.idArticulo == idArticulo).cantidad;
            Session["articulo"] = articulo;

            if (articulo != null)
            {
                articulo.detalleUbicacionBodega = detalleBodegaServicio.getDetalleBodegas(articulo);
                List<DetalleBodega> listaUbicaciones = (List<DetalleBodega>)Session["listaUbicaciones"];
                if (listaUbicaciones.Count == 0)
                {
                    Session["listaUbicaciones"] = articulo.detalleUbicacionBodega;
                    rpArticuloUbicaciones.DataSource = articulo.detalleUbicacionBodega;
                }
                else
                {
                    rpArticuloUbicaciones.DataSource = listaUbicaciones;
                }

                rpArticuloUbicaciones.DataBind();

                lblNombreArticulo.Text = articulo.nombreArticulo;
                txbCantidadUbicable.Text = articulo.cantidadUbicable.ToString();
                // ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarArticuloUbicaciones();", true);
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
            }
        }


        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: na de la tabla el articulo seleccionado 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregarArticuloLista(object sender, EventArgs e)
        {
            DetallePedido articuloIngreso = (DetallePedido)Session["articuloIngreso"];
            List<DetallePedido> listaSolicitud = (List<DetallePedido>)Session["listaArticulosIngreso"];
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            if (!listaSolicitud.Exists(x => x.articulo.idArticulo == articuloIngreso.articulo.idArticulo))
            {
                listaSolicitud.Add(articuloIngreso);
                listaArticulos.Remove(articuloIngreso.articulo);
                Session["listaArticulosIngreso"] = listaSolicitud;
                Session["listaArticulos"] = listaArticulos;

                rpArticulos.DataSource = listaArticulos;
                rpArticulos.DataBind();

                rpSolicitudProveedor.DataSource = listaSolicitud;
                rpSolicitudProveedor.DataBind();

                txbCantidad.Text = "";
                txbArticuloSeleccionado.Text = "";
            }
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Agrega una ubicación del articulo según bodega, estante y piso, 
        /// verifica que esta misma ya no se encuetra registrada
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnAgregar_Ubicacion_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txbCantidadUbicar.Text))
            {
                DetalleBodega ubicacionArticulo = new DetalleBodega();
                ubicacionArticulo.bodega.idBodega = Convert.ToInt32(ddlBodegas.SelectedValue);
                ubicacionArticulo.bodega.nombre = ddlBodegas.SelectedItem.Text;

                if (!String.IsNullOrWhiteSpace(txbEstante.Text))
                    ubicacionArticulo.estante = txbEstante.Text;
                else
                    ubicacionArticulo.estante = "";

                if (!String.IsNullOrWhiteSpace(txbPiso.Text))
                    ubicacionArticulo.piso = txbPiso.Text;
                else
                    ubicacionArticulo.piso = "";

                Entidades.Articulo articulo = (Entidades.Articulo)Session["articulo"];
                int cantidadUbicar = Convert.ToInt32(txbCantidadUbicar.Text);

                List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosIngreso"];
                int cantidad = listaArticulos.Find(x => x.articulo.idArticulo == articulo.idArticulo).cantidad;

                if (cantidadUbicar <= (articulo.cantidadUbicable))
                {
                    List<DetalleBodega> listaUbicaciones = (List<DetalleBodega>)Session["listaUbicaciones"];
                    DetalleBodega detalleUbicacion = listaUbicaciones.Find(
                        x => x.bodega.idBodega == ubicacionArticulo.bodega.idBodega
                            && x.estante == ubicacionArticulo.estante && x.piso == ubicacionArticulo.piso);

                    if (detalleUbicacion != null)
                    {
                        listaUbicaciones.Remove(detalleUbicacion);
                        articulo.cantidadUbicable = articulo.cantidadUbicable - cantidadUbicar;
                        detalleUbicacion.cantidadArticulo = cantidadUbicar;
                        listaUbicaciones.Add(detalleUbicacion);
                    }
                    else
                    {
                        articulo.cantidadUbicable = articulo.cantidadUbicable - cantidadUbicar;
                        ubicacionArticulo.cantidadArticulo = cantidadUbicar;
                        listaUbicaciones.Add(ubicacionArticulo);
                    }

                    Session["articulo"] = articulo;
                    Session["listaUbicaciones"] = listaUbicaciones;

                    txbCantidadUbicable.Text = articulo.cantidadUbicable.ToString();
                    rpArticuloUbicaciones.DataSource = listaUbicaciones;
                    rpArticuloUbicaciones.DataBind();
                }
                else
                {
                    txbCantidad.Text = "";
                    (this.Master as SiteMaster).Mensaje("La cantidad a ubicar del articulo no puede ser menor a la cantidad disponible para ubicar!", "¡Alerta!");
                }
            }
       
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Agrega las ubicaciones a la base de datos.
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnAceptar_Ubicacion_Click(object sender, EventArgs e)
        {
            List<DetalleBodega> listaUbicaciones = (List<DetalleBodega>)Session["listaUbicaciones"];

            Entidades.Articulo articulo = (Entidades.Articulo)Session["articulo"];
            articulo.detalleUbicacionBodega = listaUbicaciones;

            List<DetallePedido> listaPedido = (List<DetallePedido>)Session["listaArticulosIngreso"];
            listaPedido.Find(x => x.articulo.idArticulo == articulo.idArticulo).articulo.detalleUbicacionBodega = listaUbicaciones;
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: na una ubicación del articulo según el id de la bodega
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminarUbicacion_Click(object sender, EventArgs e)
        {
            int idBodega = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            String estante = ((HiddenField)((LinkButton)sender).Parent.FindControl("hdfEstante")).Value;
            List<DetalleBodega> listaUbicaciones = (List<DetalleBodega>)Session["listaUbicaciones"];
            DetalleBodega detalleUbicacion = listaUbicaciones.Find(x => x.bodega.idBodega == idBodega && x.estante == estante);
            listaUbicaciones.Remove(detalleUbicacion);
            Session["listaUbicaciones"] = listaUbicaciones;
            rpArticuloUbicaciones.DataSource = listaUbicaciones;
            rpArticuloUbicaciones.DataBind();

            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
        }


        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Asigna a la variable de session el articulo seleccionado por el usuario 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnSeleccionarModal(object sender, EventArgs e)
        {

            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            Entidades.Articulo articulo = listaArticulos.Find(x => x.idArticulo == idArticulo);

            txbArticuloSeleccionado.Text = articulo.nombreArticulo;
            Session["articuloSeleccionado"] = articulo;

            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Agrega a la lista de la solicitud el articulo seleccionado por el usuario
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            List<Entidades.Articulo> listaArticulos = articuloServicio.getArticulos();
            listaArticulos = articuloServicio.getArticulos();
            Session["listaArticulos"] = listaArticulos;
            rpArticulos.DataSource = listaArticulos;
            rpArticulos.DataBind();
            //ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarArticulos();", true);
            txbArticuloSeleccionado.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: na de la lista de la solicitud el articulo seleccionado por el usuario
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosIngreso"];

            listaArticulos.RemoveAll(x => x.articulo.idArticulo == idArticulo);
            rpSolicitudProveedor.DataSource = listaArticulos;
            rpSolicitudProveedor.DataBind();
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: verifica que la cantidad del articulo no sea negativa
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void cantidadArticuloTextChanged(object sender, EventArgs e)
        {
            LinkButton lbID = (LinkButton)((TextBox)sender).Parent.FindControl("btnEliminar");
            int idArticulo = Convert.ToInt32((lbID.CommandArgument).ToString());
            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosIngreso"];
            DetallePedido articuloIngreso = listaArticulos.Find(x => x.articulo.idArticulo == idArticulo);
            int indexArticulo = listaArticulos.FindIndex(x => x.articulo.idArticulo == idArticulo);
            int cantidad = Convert.ToInt32(((TextBox)sender).Text);
            if (cantidad > 1)
            {

                listaArticulos.ElementAt(indexArticulo).cantidad = cantidad;
            }
            else
            {
                (this.Master as SiteMaster).Mensaje("La cantidad del Articulo es menor a cero, no se permiten cantidades negativas.", "¡Alerta!");
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Envía la solicitud a la base de datos para ser ingresada a los registros
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEnviarSolicitud(object sender, EventArgs e)
        {
            Pedido pedidoProveedor = new Pedido();
            pedidoProveedor.usuario = Session["nombreCompleto"].ToString();
            pedidoProveedor.numeroPedido = txbNumeroPedido.Text;
            pedidoProveedor.proveedor.idProveedor = Convert.ToInt32(ddlProveedores.SelectedValue);
            pedidoProveedor.detallePedido = (List<DetallePedido>)Session["listaArticulosIngreso"];
            if (validarCampos())
            {
                if (pedidoProveedor.detallePedido.Count() > 0)
                {
                    if (isArticulosUbicados())
                    {
                        pedidoProveedorServicio.insertarPedidoProveedor(pedidoProveedor);
                        foreach (DetallePedido detallePedido in pedidoProveedor.detallePedido)
                        {
                            detalleBodegaServicio.actualizarBodega(detallePedido.articulo.detalleUbicacionBodega, detallePedido.articulo.idArticulo, (String)Session["nombreCompleto"]);
                        }
                        string url = Page.ResolveUrl("~/PedidosProveedor/AdministrarSolicitudesProveedor.aspx");
                        Response.Redirect(url);
                    }
                    else
                    {
                        (this.Master as SiteMaster).Mensaje("Todos las existencias de los articulos de la solicitud deben estar ubicadas.", "¡alerta!");
                    }

                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("Debe de agregar al menos un articulo para realizar la solicitud.", "¡alerta!");
                }
            }

        }

        private Boolean isArticulosUbicados()
        {
            Boolean ubicados = true;

            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosIngreso"];

            foreach (DetallePedido articulo in listaArticulos)
            {
                if (articulo.articulo.cantidadUbicable != 0)
                {
                    ubicados = false;
                }
            }
            return ubicados;
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: regresa a la pantalla para administrar solicitudes
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar(object sender, EventArgs e)
        {

            String url = Page.ResolveUrl("~/PedidosProveedor/AdministrarSolicitudesProveedor.aspx");
            Response.Redirect(url);
        }

        protected void rpDetalleUbicaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfIdArticulo = e.Item.FindControl("hfIdUbicacionesArticulo") as HiddenField;

                Entidades.Articulo articulo = new Entidades.Articulo();
                articulo.idArticulo = Convert.ToInt32(hfIdArticulo.Value);
                articulo.detalleUbicacionBodega = detalleBodegaServicio.getDetalleBodegas(articulo);

                Repeater rpUbicaciones = e.Item.FindControl("rpUbicaciones") as Repeater;
                rpUbicaciones.DataSource = articulo.detalleUbicacionBodega;
                rpUbicaciones.DataBind();

                LinkButton btnEditar = e.Item.FindControl("btnEditar") as LinkButton;
                LinkButton btnEliminar = e.Item.FindControl("btnEliminar") as LinkButton;

            }
        }
        #endregion
    }
}