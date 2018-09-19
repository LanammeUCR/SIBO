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
    public partial class VerSolicitudConsumoFuncionario : System.Web.UI.Page
    {   
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();       
        private UnidadServicios unidadServicios = new UnidadServicios();
        private SolicitudesConsumoServicios solicitudesConsumoServicio = new SolicitudesConsumoServicios();
        #endregion
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 13 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Session["listaArticulos"] = null;
                Session["listaArticulosSolicitud"] = null;
                cargardatos();
            }
        }
        #endregion

        #region logica
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/05/2018
        /// Efecto: Llena los componentes con los datos de la solicitud seleccionada que se encuantran en la base de datos
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        public void cargardatos()
        {
            String userName = Session["login"].ToString();
                    

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
        /// 09/05/2018
        /// Efecto: redirecciona a la pantalla administrar solicitudes
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnVolverSolicitud(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesFuncionario.aspx");
            Response.Redirect(url);
        }
      
        #endregion   

    }
}