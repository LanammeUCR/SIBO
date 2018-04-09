﻿using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Unidad
{
    public partial class EditarUnidad : System.Web.UI.Page
    {
        #region variables globales
        UnidadServicios unidadServicios = new UnidadServicios();

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

            if (!permisos[2])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }         

            if (!Page.IsPostBack)
            {
                Entidades.Unidad unidad = (Entidades.Unidad)Session["unidadEditar"];
                txtNombreUnidad.Text = unidad.nombre;
                txtNombreUnidad.Attributes.Add("oninput", "validarTexto(this)");
                txbTelefonoUnidad.Text = unidad.telefono;
                txbTelefonoUnidad.Attributes.Add("oninput", "validarTexto(this)");
            }
        }
        #region logica
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Valida los campos que debe de ingresar el usuario
        /// devuelve true si todos se encuentran correctos, de lo contrario 
        /// devuelve false y marca los campos para que el usuario vea cuales son.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        public Boolean validarCampos()
        {
            Boolean validados = true;
            divNombreUnidadIncorrecto.Style.Add("display","none");
            divTelefonoUnidadIncorrecto.Style.Add("display","none");

            txtNombreUnidad.CssClass = "form-control";
            txbTelefonoUnidad.CssClass = "form-control";

            #region validacion nombre unidad
            String nombreUnidad = txtNombreUnidad.Text;

            if(nombreUnidad.Trim()=="")
            {
                txtNombreUnidad.CssClass = "form-control alert-danger";
                divNombreUnidadIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            #region validacion telefono unidad
            String telefonoUnidad = txbTelefonoUnidad.Text;

            if (telefonoUnidad.Trim() == "")
            {
                txbTelefonoUnidad.CssClass = "form-control alert-danger";
                divTelefonoUnidadIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            return validados;
        }
        #endregion
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Unidad esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Unidades
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Entidades.Unidad unidad = (Entidades.Unidad)Session["unidadEditar"];
                unidad.nombre = txtNombreUnidad.Text;
                unidad.telefono = txbTelefonoUnidad.Text;
               
                unidadServicios.actualizarUnidad(unidad, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
                Response.Redirect(url);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
            Response.Redirect(url);
        }
    }
}