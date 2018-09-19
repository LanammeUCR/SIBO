using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Articulo
{
    public partial class AdministrarArticulo : System.Web.UI.Page
    {
        #region variables globales
        ArticuloServicios articuloServicios = new ArticuloServicios();
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
                Session["articuloEditar"] = null;
                Session["articuloELiminar"] = null;
                Session["articuloUbicacion"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiar();", true);
                cargarDatosTblArticuloes();
            }
        }
        #endregion

        #region logica
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Llenar la tabla con los datos de las articulos que se encuantran en la base de datos
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        public void cargarDatosTblArticuloes()
        {
            List<Entidades.Articulo> listaArticulos = new List<Entidades.Articulo>();
            listaArticulos = articuloServicios.getArticulos();
            rpArticulo.DataSource = listaArticulos;
            rpArticulo.DataBind();

            Session["listaArticulos"] = listaArticulos;
        }
        #endregion

        #region eventos

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/AgregarArticulo.aspx");
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
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            Entidades.Articulo articuloEditar = listaArticulos.Find(x => x.idArticulo == idArticulo );
            
            Session["articuloEditar"] = articuloEditar;

            String url = Page.ResolveUrl("~/Articulo/EditarArticulo.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se elimina una articulo
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            Entidades.Articulo articuloEliminar = listaArticulos.Find(x => x.idArticulo == idArticulo);

            Session["articuloELiminar"] = articuloEliminar;

            String url = Page.ResolveUrl("~/Articulo/EliminarArticulo.aspx");
            Response.Redirect(url);

        }       

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde administran las ubicaciones del articulo
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnUbicaciones_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            Entidades.Articulo articuloUbicacion = listaArticulos.Find(x => x.idArticulo == idArticulo);

            Session["articuloUbicacion"] = articuloUbicacion;

            String url = Page.ResolveUrl("~/Articulo/ArticuloUbicaciones.aspx");
            Response.Redirect(url);
        }
        #endregion
    }
}