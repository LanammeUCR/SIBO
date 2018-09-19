using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.SolicitudesConsumo
{
    public partial class EliminarSolicitudConsumo : System.Web.UI.Page
    {   
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private FuncionarioServicios funcionarioServicio = new FuncionarioServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private SolicitudesConsumoServicios solicitudesConsumoServicio = new SolicitudesConsumoServicios();
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
                Session["listaArticulos"] = null;
                Session["listaArticulosSolicitud"] = null;
                cargardatos();
            }
        }

        public void cargardatos()
        {
            String userName = Session["login"].ToString();
            Entidades.Funcionario solicitante = funcionarioServicio.getFuncionarioPorUsuario(userName);                       

            int idConsumo = Convert.ToInt32(Session["idOrdenConsumo"].ToString());
            List<DetalleConsumo> listaArticulosSolicitud = solicitudesConsumoServicio.getDetalleOrdenesConsumo(idConsumo);
            Session["listaArticulosSolicitud"] = listaArticulosSolicitud;

            lbIDConsumo.Text = idConsumo.ToString();
            rpSolicitud.DataSource = listaArticulosSolicitud;
            rpSolicitud.DataBind();

        }
        #endregion  

        #region eventos   
        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: na de forma logica la solicitud en la bd
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEliminarSolicitud(object sender, EventArgs e)
        {
            Consumo ordenConsumo = new Consumo();
            ordenConsumo.idConsumo = Convert.ToInt32(Session["idOrdenConsumo"].ToString());
            solicitudesConsumoServicio.eliminarSolicitudConsumo(ordenConsumo, (String)Session["nombreCompleto"]);

            String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesConsumo.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Redirecciona a la pagina administrar solicitudes de consumo
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesConsumo.aspx");
            Response.Redirect(url);
        }
        #endregion

    }
}