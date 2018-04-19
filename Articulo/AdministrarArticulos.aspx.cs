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

            //devuelve los permisos de la pantalla en el siguiente orden:
            //[0]=ver
            //[1]=Nuevo
            //[2]=Editar
            //[3]=Eliminar
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarArticulos");

            if (!permisos[0])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!permisos[1])
            {
                btnNuevo.Visible = false;

            }

            if (!Page.IsPostBack)
            {
                Session["listaArticulos"] = null;
                Session["articuloEditar"] = null;
                Session["articuloELiminar"] = null;
                cargarDatosTblArticuloes();
            }
        }
        #endregion

        #region logica
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Llenar la tabla con los datos de las articuloes que se encuantran en la base de datos
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

            Entidades.Articulo articuloEditar = new Entidades.Articulo();

            foreach (Entidades.Articulo articulo in listaArticulos)
            {
                if (articulo.idArticulo == idArticulo)
                {
                    articuloEditar = articulo;
                    break;
                }
            }

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

            Entidades.Articulo articuloEliminar = new Entidades.Articulo();

            foreach (Entidades.Articulo articulo in listaArticulos)
            {
                if (articulo.idArticulo == idArticulo)
                {
                    articuloEliminar = articulo;
                    break;
                }
            }

            Session["articuloELiminar"] = articuloEliminar;

            String url = Page.ResolveUrl("~/Articulo/EliminarArticulo.aspx");
            Response.Redirect(url);

        }
        protected void rpArticulo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEditar = e.Item.FindControl("btnEditar") as LinkButton;
                LinkButton btnEliminar = e.Item.FindControl("btnEliminar") as LinkButton;
                //devuelve los permisos de la pantalla en el siguiente orden:
                //[0]=ver
                //[1]=Nuevo
                //[2]=Editar
                //[3]=Eliminar
                Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarArticulos");

                if (!permisos[0])
                {
                    String url = Page.ResolveUrl("~/Default.aspx");
                    Response.Redirect(url);
                }

                if (!permisos[2])
                {
                    btnEditar.Visible = false;
                }

                if (!permisos[3])
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        #endregion

    }
}