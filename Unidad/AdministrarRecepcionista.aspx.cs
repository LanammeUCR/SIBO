using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Unidad
{
    public partial class AdministradorRecepcionista : System.Web.UI.Page
    {
        #region variables globales
        PersonaRecepcionistaServicios recepcionistaServicios = new PersonaRecepcionistaServicios();
        #endregion
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
                Session["listaRecepcionistas"] = null;
                Session["recepcionistaEditar"] = null;              


                cargarDatosTblRecepcionistas();
            }
        }
        #region logica
        private void cargarDatosTblRecepcionistas()
        {
            List<PersonaRecepcionista> listaRecepcionistas = new List<PersonaRecepcionista>();
            listaRecepcionistas = recepcionistaServicios.getRecepcionistas();
            rpRecepcionista.DataSource = listaRecepcionistas;
            rpRecepcionista.DataBind();

            Session["listaRecepcionistas"] = listaRecepcionistas;
        }
        #endregion

        #region eventos
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

            List<Entidades.Unidad> listasUniades = (List<Entidades.Unidad>)Session["listaRecepcionistas"];

            Entidades.Unidad unidadEditar = new Entidades.Unidad();

            foreach (Entidades.Unidad unidad in listasUniades)
            {
                if (unidad.idUnidad == idUnidad)
                {
                    unidadEditar = unidad;
                    break;
                }
            }

            Session["recepcionistaEditar"] = unidadEditar;

            String url = Page.ResolveUrl("~/Unidad/EditarRecepcionista.aspx");
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

            List<Entidades.Unidad> listasUniades = (List<Entidades.Unidad>)Session["listaRecepcionistas"];

            Entidades.Unidad unidadEliminar = new Entidades.Unidad();

            foreach (Entidades.Unidad unidad in listasUniades)
            {
                if (unidad.idUnidad == idUnidad)
                {
                    unidadEliminar = unidad;
                    break;
                }
            }

            Session["recepcionistaEditar"] = unidadEliminar;

            String url = Page.ResolveUrl("~/Unidad/EliminarRecepcionista.aspx");
            Response.Redirect(url);

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AgregarRecepcionista.aspx");
            Response.Redirect(url);
        }

        protected void rpRecepcionista_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarRecepcionista");

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