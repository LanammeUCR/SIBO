using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Bodega
{
    public partial class AdministrarBodega : System.Web.UI.Page
    {
        #region variables globales
        BodegaServicios bodegaServicios = new BodegaServicios();
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
                Session["listaBodegas"] = null;
                Session["bodegaEditar"] = null;
                Session["bodeganar"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiar();", true);
                cargarDatosTblBodegas();
            }
        }
        #endregion

        #region logica
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Llenar la tabla con los datos de las unidades que se encuantran en la base de datos
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        public void cargarDatosTblBodegas()
        {
            List<Entidades.Bodega> listaBodega = new List<Entidades.Bodega>();
            listaBodega = bodegaServicios.getBodegas();
            rpBodega.DataSource = listaBodega;
            rpBodega.DataBind();

            Session["listaBodegas"] = listaBodega;
        }
        #endregion

        #region eventos

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Bodega/AgregarBodega.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita una unidad.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idBodega = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Bodega> listaBodegas = (List<Entidades.Bodega>)Session["listaBodegas"];

            Entidades.Bodega bodegaEditar = new Entidades.Bodega();

            foreach (Entidades.Bodega bodega in listaBodegas)
            {
                if (bodega.idBodega == idBodega)
                {
                    bodegaEditar = bodega;
                    break;
                }
            }

            Session["bodegaEditar"] = bodegaEditar;

            String url = Page.ResolveUrl("~/Bodega/EditarBodega.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se na una unidad
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnnar_Click(object sender, EventArgs e)
        {
            int idBodega = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Bodega> listaBodegas = (List<Entidades.Bodega>)Session["listaBodegas"];

            Entidades.Bodega bodeganar = new Entidades.Bodega();

            foreach (Entidades.Bodega bodega in listaBodegas)
            {
                if (bodega.idBodega == idBodega)
                {
                    bodeganar = bodega;
                    break;
                }
            }

            Session["bodegaEliminar"] = bodeganar;

            String url = Page.ResolveUrl("~/Bodega/EliminarBodega.aspx");
            Response.Redirect(url);

        }
        #endregion

    }
}