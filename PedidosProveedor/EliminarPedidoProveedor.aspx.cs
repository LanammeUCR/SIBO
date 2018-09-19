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
    public partial class narPedidoProveedor : System.Web.UI.Page
    {
         #region variables globales       
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
                Session["pedidonar"] = null;
                cargardatos();
            }
        }

        private void cargardatos()
        {
            int idPedidoProveedor = Convert.ToInt32(Session["idPedidoProveedor"]);
            Pedido pedidoProveedor = pedidoProveedorServicio.getPedido(idPedidoProveedor);
            Session["pedidonar"] = pedidoProveedor;

            txbNumeroPedido.Text = pedidoProveedor.numeroPedido;
            tbxNombreProveedor.Text = pedidoProveedor.proveedor.nombre;

            pedidoProveedor.detallePedido = pedidoProveedorServicio.getDetallePedidoProveedor(idPedidoProveedor);

            rpVerSolicitudProveedor.DataSource = pedidoProveedor.detallePedido;
            rpVerSolicitudProveedor.DataBind();
        }
        #endregion

        #region eventos
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita una articulo.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAceptar_Pedido_Click(object sender, EventArgs e)
        {
            Pedido pedidoProveedor = (Pedido)  Session["pedidonar"] ;
            pedidoProveedorServicio.eliminarPedidoProveedor(pedidoProveedor);
            String url = Page.ResolveUrl("~/PedidosProveedor/AdministrarSolicitudesProveedor.aspx");
            Response.Redirect(url);
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