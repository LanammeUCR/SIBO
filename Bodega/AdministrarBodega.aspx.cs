﻿using Servicios;
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

            //devuelve los permisos de la pantalla en el siguiente orden:
            //[0]=ver
            //[1]=Nuevo
            //[2]=Editar
            //[3]=Eliminar
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarBodegas");

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
                Session["listaBodegas"] = null;
                Session["bodegaEditar"] = null;
                Session["bodegaEliminar"] = null;

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
        /// Efecto: redirecciona a la pantalla donde se elimina una unidad
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idBodega = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Bodega> listaBodegas = (List<Entidades.Bodega>)Session["listaBodegas"];

            Entidades.Bodega bodegaEliminar = new Entidades.Bodega();

            foreach (Entidades.Bodega bodega in listaBodegas)
            {
                if (bodega.idBodega == idBodega)
                {
                    bodegaEliminar = bodega;
                    break;
                }
            }

            Session["bodegaEliminar"] = bodegaEliminar;

            String url = Page.ResolveUrl("~/Bodega/EliminarBodega.aspx");
            Response.Redirect(url);

        }
        protected void rpBodega_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarBodegas");

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