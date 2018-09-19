using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.PedidosProveedor
{
    public partial class EditarPedidoProveedor : System.Web.UI.Page
    {
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private ProveedorServicios proveedorServicio = new ProveedorServicios();
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
                Session["listaArticulosPedido"] = null;
                cargardatos();
            }
        }
        #endregion

        private void cargardatos()
        {
            List<Entidades.Articulo> articulos = articuloServicio.getArticulos();
            Session["listaArticulos"] = articulos;

            int idPedido = Convert.ToInt32(Session["idPedidoProveedor"].ToString());
            Pedido pedido = pedidoProveedorServicio.getPedido(idPedido);

            List<DetallePedido> listaArticulosPedido = pedidoProveedorServicio.getDetallePedidoProveedor(idPedido); ;
            Session["listaArticulosPedido"] = listaArticulosPedido;

            List<Proveedor> proveedores = proveedorServicio.getProveedores();
            Session["listaProveedores"] = proveedores;

            txbNumeroPedido.Text = pedido.numeroPedido;

            ddlProveedores.DataSource = proveedores;
            ddlProveedores.DataTextField = "nombre";
            ddlProveedores.DataValueField = "idProveedor";
            ddlProveedores.DataBind();
            ListItem item = ddlProveedores.Items.FindByValue(pedido.proveedor.idProveedor.ToString());
            int index = ddlProveedores.Items.IndexOf(item);
            ddlProveedores.SelectedIndex=  index;

            lbArticulos.DataSource = articuloServicio.getArticulos();
            lbArticulos.DataTextField = "nombreArticulo";
            lbArticulos.DataValueField = "idArticulo";
            lbArticulos.DataBind();

            rpEditarSolicitudProveedor.DataSource = listaArticulosPedido;
            rpEditarSolicitudProveedor.DataBind();
        }

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
        /// Efecto: Filtra el listbox de articulos según el texto ingresado en la textbox
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void tbNombreArticulo_TextChanged(object sender, EventArgs e)
        {
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];
            List<Entidades.Articulo> listaFiltrados = new List<Entidades.Articulo>();

            String busqueda = tbxNombreArticulo.Text;
            listaFiltrados = listaArticulos.FindAll(x => x.nombreArticulo.ToLower().Contains(busqueda.ToLower()));

            lbArticulos.Items.Clear();
            lbArticulos.DataSource = listaFiltrados;
            lbArticulos.DataTextField = "nombreArticulo";
            lbArticulos.DataValueField = "idArticulo";
            lbArticulos.DataBind();
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Agrega un articulo a la lista de articulos para la solicitud de Proveedor 
        /// Requiere:-
        /// Modifica: List<DetallePedido> listaArticulos
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosPedido"];
            DetallePedido articuloDetalle = new DetallePedido();
            if (lbArticulos.SelectedItem != null)
            {
                articuloDetalle.articulo = articuloServicio.getArticulo(Convert.ToInt32(lbArticulos.SelectedValue));
                if (!listaArticulos.Exists(x => x.articulo.idArticulo == articuloDetalle.articulo.idArticulo))
                {
                    articuloDetalle.cantidad = 1;
                    listaArticulos.Add(articuloDetalle);
                    rpEditarSolicitudProveedor.DataSource = listaArticulos;
                    rpEditarSolicitudProveedor.DataBind();
            }
            }
            else
            {
                (this.Master as SiteMaster).Mensaje("Debe selecionar un Articulo de la lista", "¡Alerta!");
            }
           

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: na un articulo de la lista de articulos para la solicitud de Proveedor 
        /// Requiere:-
        /// Modifica: List<DetallePedido> listaArticulos
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnnarArticulo_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosPedido"];

            listaArticulos.RemoveAll(x => x.articulo.idArticulo == idArticulo);
            rpEditarSolicitudProveedor.DataSource = listaArticulos;
            rpEditarSolicitudProveedor.DataBind();
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica la cantidad del articulo
        /// Requiere:-
        /// Modifica: List<DetallePedido> listaArticulos
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void cantidadArticuloTextChanged(object sender, EventArgs e)
        {
            LinkButton lbID = (LinkButton)((TextBox)sender).Parent.FindControl("btnnar");
            int idArticulo = Convert.ToInt32((lbID.CommandArgument).ToString());
            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosPedido"];
            DetallePedido articuloConsumo = listaArticulos.Find(x => x.articulo.idArticulo == idArticulo);
            int indexArticulo = listaArticulos.FindIndex(x => x.articulo.idArticulo == idArticulo);
            int cantidad = Convert.ToInt32(((TextBox)sender).Text);
            if (cantidad > 1 )
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
        /// 09/04/2018
        /// Efecto: Cambia el rechazado del articulo a entregado
        /// Requiere:-
        /// Modifica: List<DetallePedido> listaArticulos
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void chkEntregado_CheckedChanged(object sender, EventArgs e)
        {
            LinkButton lbID = (LinkButton)((CheckBox)sender).Parent.FindControl("btnnar");
            int idArticulo = Convert.ToInt32((lbID.CommandArgument).ToString());
            List<DetallePedido> listaArticulos = (List<DetallePedido>)Session["listaArticulosPedido"];

            int indexArticulo = listaArticulos.FindIndex(x => x.articulo.idArticulo == idArticulo);
            CheckBox entregadoCheck = (CheckBox)sender;

            listaArticulos.ElementAt(indexArticulo).entregado = entregadoCheck.Checked;
            Session["listaArticulosPedido"] = listaArticulos;
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
            pedidoProveedor.idPedido = Convert.ToInt32(Session["idPedidoProveedor"].ToString());
            pedidoProveedor.usuario = Session["nombreCompleto"].ToString();
            pedidoProveedor.numeroPedido = txbNumeroPedido.Text;
            pedidoProveedor.proveedor.idProveedor = Convert.ToInt32(ddlProveedores.SelectedValue);
            pedidoProveedor.detallePedido = (List<DetallePedido>)Session["listaArticulosPedido"];           
            if (pedidoProveedor.detallePedido.Count > 0)
            {
                pedidoProveedorServicio.actualizarPedidoProveedor(pedidoProveedor);
                string url = Page.ResolveUrl("~/PedidosProveedor/AdministrarSolicitudesProveedor.aspx");
                Response.Redirect(url);
            }
            else
            {
                (this.Master as SiteMaster).Mensaje("debe de agregar al menos un articulo para realizar la solicitud", "¡alerta!");
            }            
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

        #endregion
    }
}