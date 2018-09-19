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
    public partial class VerPedidoProveedor : System.Web.UI.Page
    {
        #region variables globales       
        private PedidoProveedorServicios pedidoProveedorServicio = new PedidoProveedorServicios();
        private DetalleBodegaServicios detalleBodegaServicios = new DetalleBodegaServicios();
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
                cargardatos();
            }
        }

        private void cargardatos()
        {
            int idPedidoProveedor = Convert.ToInt32(Session["idPedidoProveedor"]);
            Pedido pedidoProveedor = pedidoProveedorServicio.getPedido(idPedidoProveedor);

            txbNumeroPedido.Text = pedidoProveedor.numeroPedido;
            tbxNombreProveedor.Text = pedidoProveedor.proveedor.nombre;

            pedidoProveedor.detallePedido = pedidoProveedorServicio.getDetallePedidoProveedor(idPedidoProveedor);       

            rpVerSolicitudProveedor.DataSource = pedidoProveedor.detallePedido;
            rpVerSolicitudProveedor.DataBind();

        }

        protected void rpDetalleUbicaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfIdArticulo = e.Item.FindControl("hfIdUbicacionesArticulo") as HiddenField;

                Entidades.Articulo articulo = new Entidades.Articulo();
                articulo.idArticulo = Convert.ToInt32(hfIdArticulo.Value);
                articulo.detalleUbicacionBodega = detalleBodegaServicios.getDetalleBodegas(articulo);

                Repeater rpUbicaciones = e.Item.FindControl("rpUbicaciones") as Repeater;
                rpUbicaciones.DataSource = articulo.detalleUbicacionBodega;
                rpUbicaciones.DataBind();            
            }
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
            String url = Page.ResolveUrl("~/PedidosProveedor/AdministrarSolicitudesProveedor.aspx");
            Response.Redirect(url);
        }

        #endregion
    }
}