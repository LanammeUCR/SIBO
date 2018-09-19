﻿using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Bodega
{
    public partial class AgregarBodega : System.Web.UI.Page
    {
        #region variables globales
        BodegaServicios bodegaServicios = new BodegaServicios();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {

                txtNombreBodega.Attributes.Add("oninput", "validarTexto(this)");
                txbDireccionBodega.Attributes.Add("oninput", "validarTexto(this)");               
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
            divNombreBodegaIncorrecto.Style.Add("display", "none");
            divDireccionBodegaIncorrecto.Style.Add("display", "none");
          

            txtNombreBodega.CssClass = "form-control";
            txbDireccionBodega.CssClass = "form-control";

            #region validacion nombre bodega
            String nombreBodega = txtNombreBodega.Text;

            if (nombreBodega.Trim() == "")
            {
                txtNombreBodega.CssClass = "form-control alert-danger";
                divNombreBodegaIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            #region validacion direccion bodega
            String direccionBodega = txbDireccionBodega.Text;

            if (direccionBodega.Trim() == "")
            {
                txbDireccionBodega.CssClass = "form-control alert-danger";
                divDireccionBodegaIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
           
            return validados;
        }
        #endregion
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Bodega esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Bodegas
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Entidades.Bodega bodega = new Entidades.Bodega();
                bodega.nombre = txtNombreBodega.Text;
                bodega.direccion = txbDireccionBodega.Text;               

                bodegaServicios.insertarBodega(bodega);

                String url = Page.ResolveUrl("~/Bodega/AdministrarBodega.aspx");
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla de Administrar Bodegas
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Bodega/AdministrarBodega.aspx");
            Response.Redirect(url);
        }
    }
}