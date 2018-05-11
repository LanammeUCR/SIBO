using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Servicios;

namespace SIBO.Unidad
{
    public partial class AdministradorUnidades : System.Web.UI.Page
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

            //devuelve los permisos de la pantalla en el siguiente orden:
            //[0]=ver
            //[1]=Nuevo
            //[2]=Editar
            //[3]=Eliminar
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarUnidades");

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
                Session["listaUnidades"] = null;
                Session["unidadEditar"] = null;
                Session["unidadEliminar"] = null;
                Session["unidadAsociar"] = null;                                                       
                    
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
            List<Entidades.Unidad> listaUniades = new List<Entidades.Unidad>();
            listaUniades = unidadServicios.getUnidades();
            rpUnidad.DataSource = listaUniades;
            rpUnidad.DataBind();

            Session["listaUnidades"] = listaUniades;
        }
        #endregion

        #region eventos
       
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AgregarUnidad.aspx");
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

            List<Entidades.Unidad> listaUniades = (List<Entidades.Unidad>)Session["listaUnidades"];

            Entidades.Unidad unidadEditar = new Entidades.Unidad();

            foreach (Entidades.Unidad unidad in listaUniades)
            {
                if (unidad.idUnidad == idUnidad)
                {
                    unidadEditar = unidad;
                    break;
                }
            }

            Session["unidadEditar"] = unidadEditar;

            String url = Page.ResolveUrl("~/Unidad/EditarUnidad.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se elimina una unidad
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idUnidad = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Unidad> listaUniades = (List<Entidades.Unidad>)Session["listaUnidades"];

            Entidades.Unidad unidadEliminar = new Entidades.Unidad();

            foreach (Entidades.Unidad unidad in listaUniades)
            {
                if (unidad.idUnidad == idUnidad)
                {
                    unidadEliminar = unidad;
                    break;
                }
            }

            Session["unidadEliminar"] = unidadEliminar;

            String url = Page.ResolveUrl("~/Unidad/EliminarUnidad.aspx");
            Response.Redirect(url);

        }

        protected void btnAsociarRecepcionistas_Click(object sender, EventArgs e)
        {
            int idUnidad = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Unidad> listaUniades = (List<Entidades.Unidad>)Session["listaUnidades"];

            Entidades.Unidad unidadEliminar = new Entidades.Unidad();

            foreach (Entidades.Unidad unidad in listaUniades)
            {
                if (unidad.idUnidad == idUnidad)
                {
                    unidadEliminar = unidad;
                    break;
                }
            }

            Session["unidadAsociar"] = unidadEliminar;

            String url = Page.ResolveUrl("~/Unidad/FuncionariosPorUnidad.aspx");
            Response.Redirect(url);

        }

        protected void rpUnidad_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarUnidades");

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