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
    public partial class AdministrarSolicitudesConsumo : System.Web.UI.Page
    {
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private FuncionarioServicios funcionarioServicio = new FuncionarioServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private SolicitudesConsumoServicios solicitudesConsumoServicio = new SolicitudesConsumoServicios();
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
                Session["idOrdenConsumo"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiar();", true);
                cargardatos();
            }
        }
        #endregion

        #region logica
        private void cargardatos()
        {
            List<Consumo> listaOrdenesConsumo = solicitudesConsumoServicio.getOrdenesConsumo();
            Session["listaOrdenesConsumo"] = listaOrdenesConsumo;
            rpOrdenesConsumo.DataSource = listaOrdenesConsumo;
            rpOrdenesConsumo.DataBind();
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
            int idOrdenConsumo = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idOrdenConsumo"] = idOrdenConsumo;
            String url = Page.ResolveUrl("~/SolicitudesConsumo/VerSolicitudConsumo.aspx");
            Response.Redirect(url);

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita la solicitud de consumo.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idOrdenConsumo = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idOrdenConsumo"] = idOrdenConsumo;
            String url = Page.ResolveUrl("~/SolicitudesConsumo/EditarSolicitudConsumo.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se na una solicitud de consumo.
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idOrdenConsumo = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idOrdenConsumo"] = idOrdenConsumo;
            String url = Page.ResolveUrl("~/SolicitudesConsumo/EliminarSolicitudConsumo.aspx");
            Response.Redirect(url);

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se crea una nueva solicitud de articulo
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnNueva_Solicitud_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/SolicitudesConsumo/SolicitudArticulo.aspx");
            Response.Redirect(url);
        }

        #endregion
    }
}