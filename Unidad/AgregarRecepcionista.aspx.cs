﻿using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Recepcionista
{
    public partial class AgregarRecepcionista : System.Web.UI.Page
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
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarRecepcionistaes");

            if (!permisos[1])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!Page.IsPostBack)
            {
                
                txtNombreRecepcionista.Attributes.Add("oninput", "validarTexto(this)");                
                txbTelefonoRecepcionista.Attributes.Add("oninput", "validarTexto(this)");
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
            divNombreRecepcionistaIncorrecto.Style.Add("display", "none");
            divTelefonoRecepcionistaIncorrecto.Style.Add("display", "none");

            txtNombreRecepcionista.CssClass = "form-control";
            txbTelefonoRecepcionista.CssClass = "form-control";

            #region validacion nombre Recepcionista
            String nombreRecepcionista = txtNombreRecepcionista.Text;

            if (nombreRecepcionista.Trim() == "")
            {
                txtNombreRecepcionista.CssClass = "form-control alert-danger";
                divNombreRecepcionistaIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            #region validacion telefono Recepcionista
            String telefonoRecepcionista = txbTelefonoRecepcionista.Text;

            if (telefonoRecepcionista.Trim() == "")
            {
                txbTelefonoRecepcionista.CssClass = "form-control alert-danger";
                divTelefonoRecepcionistaIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            return validados;
        }
        #endregion
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Recepcionista esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Recepcionistaes
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                PersonaRecepcionista recepcionista = new Entidades.PersonaRecepcionista();
                recepcionista.nombre = txtNombreRecepcionista.Text;
                recepcionista.telefono = txbTelefonoRecepcionista.Text;

                recepcionistaServicios.insertarRecepcionista(recepcionista);

                String url = Page.ResolveUrl("~/Unidad/AdministrarRecepcionistaes.aspx");
                Response.Redirect(url);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AdministrarRecepcionistaes.aspx");
            Response.Redirect(url);
        }
    }
}