using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Servicios;

namespace SIBO.Recepcion
{
    public partial class AdministrarRecepcion : System.Web.UI.Page
    {
        #region variables globales
        UnidadServicios unidadServicios = new UnidadServicios();
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
                Session["listaRecepciones"] = null;
                Session["recepcionEditar"] = null;
                Session["recepcionEliminar"] = null;
                //Session["unidadAsociar"] = null;
                //ClientScript.RegisterStartupScript(GetType(), "activar", "limpiar();", true);
                cargarDatosTblUnidades();              
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
        public void cargarDatosTblUnidades() 
        {
            List<Entidades.Recepcion> listaUniades = new List<Entidades.Recepcion>();
            listaUniades = unidadServicios.getUnidades();
            
            rpRecepcion.DataSource = listaUniades;
            rpRecepcion.DataBind();

            Session["listaRecepciones"] = listaUniades;
        }
        #endregion

        #region eventos

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se crea una unidad.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Recepcion/AgregarRecepcion.aspx");
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
            int idUnidad = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Recepcion> listaUniades = (List<Entidades.Recepcion>)Session["listaRecepciones"];

            Entidades.Recepcion recepcionEditar = listaUniades.Find(x => x.idRecepcion == idUnidad);         

            Session["recepcionEditar"] = recepcionEditar;

            String url = Page.ResolveUrl("~/Recepcion/EditarRecepcion.aspx");
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
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idUnidad = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Recepcion> listaUniades = (List<Entidades.Recepcion>)Session["listaRecepciones"];

            Entidades.Recepcion recepcionEliminar = listaUniades.Find(x => x.idRecepcion == idUnidad);

            Session["recepcionEliminar"] = recepcionEliminar;

            String url = Page.ResolveUrl("~/Recepcion/EliminarRecepcion.aspx");
            Response.Redirect(url);

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se asocia un funcionario con una unidad
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnAsociarRecepcionistas_Click(object sender, EventArgs e)
        {
            int idUnidad = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Recepcion> listaUniades = (List<Entidades.Recepcion>)Session["listaRecepciones"];

            Entidades.Recepcion recepcionEliminar = new Entidades.Recepcion();

            foreach (Entidades.Recepcion unidad in listaUniades)
            {
                if (unidad.idRecepcion == idUnidad)
                {
                    recepcionEliminar = unidad;
                    break;
                }
            }

            Session["unidadAsociar"] = recepcionEliminar;

            String url = Page.ResolveUrl("~/Unidad/FuncionariosPorUnidad.aspx");
            Response.Redirect(url);

        }

        #endregion

    }
}