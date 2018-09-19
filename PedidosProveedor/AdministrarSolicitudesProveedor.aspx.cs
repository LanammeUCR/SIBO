using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Articulo
{
    public partial class AdministrarSolicitudesProveedor : System.Web.UI.Page
    {
        #region variables globales       
        private FuncionarioServicios funcionarioServicio = new FuncionarioServicios();
        private PedidoProveedorServicios pedidoProveedorServicios = new PedidoProveedorServicios();
        #endregion
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Session["idPedidoProveedor"] = null;
                Session["listaPedidos"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiar();", true);
                cargardatos();
            }
        }
        #endregion

        #region logica
        private void cargardatos()
        {
            List<Pedido> listaPedidos = pedidoProveedorServicios.getPedidosProveedores();
            Session["listaPedidos"] = listaPedidos;
            rpPedidosProveedor.DataSource = listaPedidos;
            rpPedidosProveedor.DataBind();
        }
        #endregion

        #region eventos
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla que puede ver una solicitud
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnVer_Click(object sender, EventArgs e)
        {
            int idPedidoProveedor = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idPedidoProveedor"] = idPedidoProveedor;
            String url = Page.ResolveUrl("~/PedidosProveedor/VerPedidoProveedor.aspx");
            Response.Redirect(url);

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita una articulo.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idPedidoProveedor = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idPedidoProveedor"] = idPedidoProveedor;
            String url = Page.ResolveUrl("~/PedidosProveedor/EditarPedidoProveedor.aspx");
            Response.Redirect(url);
        }
       
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita una articulo.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnnar_Click(object sender, EventArgs e)
        {
            int idPedidoProveedor = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idPedidoProveedor"] = idPedidoProveedor;
            String url = Page.ResolveUrl("~/PedidosProveedor/narPedidoProveedor.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita una articulo.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnNuevo_Pedido_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/PedidosProveedor/SolicitudProveedor.aspx");
            Response.Redirect(url);
        }

        #endregion
    }
}