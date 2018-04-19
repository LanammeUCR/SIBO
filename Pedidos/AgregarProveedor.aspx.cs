﻿using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Pedidos
{
    public partial class AgregarProveedor : System.Web.UI.Page
    {
        #region variables globales
        ProveedorServicios proveedorServicios = new ProveedorServicios();
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
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarProveedores");

            if (!permisos[1])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!Page.IsPostBack)
            {

                txbCedulaProveedor.Attributes.Add("oninput", "validarTexto(this)");
                txbNombreProveedor.Attributes.Add("oninput", "validarTexto(this)");
                txbTelefonoProveedor.Attributes.Add("oninput", "validarTexto(this)");
                txbCorreoProveedor.Attributes.Add("oninput", "validarTexto(this)");
                
                cargaTipoCedula();
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
            divCedulaProveedorIncorrecto.Style.Add("display", "none");
            divNombreProveedorIncorrecto.Style.Add("display", "none");
            divTelefonoProveedorIncorrecto.Style.Add("display", "none");
            divCorreoProveedorIncorrecto.Style.Add("display", "none");

            txbCedulaProveedor.CssClass = "form-control";
            txbNombreProveedor.CssClass = "form-control";
            txbTelefonoProveedor.CssClass = "form-control";
            txbCorreoProveedor.CssClass = "form-control";

            #region validacion cedula Proveedor
            String cedulaProveedor = txbCedulaProveedor.Text;

            if (cedulaProveedor.Trim() == "")
            {
                txbCedulaProveedor.CssClass = "form-control alert-danger";
                divCedulaProveedorIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion nombre Proveedor
            String nombreProveedor = txbNombreProveedor.Text;

            if (nombreProveedor.Trim() == "")
            {
                txbNombreProveedor.CssClass = "form-control alert-danger";
                divNombreProveedorIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion apellidos Proveedor
            String telefonoProveedor = txbTelefonoProveedor.Text;

            if (telefonoProveedor.Trim() == "")
            {
                txbTelefonoProveedor.CssClass = "form-control alert-danger";
                divTelefonoProveedorIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion correo Proveedor
            String correoProveedor = txbCorreoProveedor.Text;

            if (correoProveedor.Trim() == "")
            {
                txbCorreoProveedor.CssClass = "form-control alert-danger";
                divCorreoProveedorIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            return validados;
        }
        #endregion

        private void cargaTipoCedula() 
        {
            ListItem personaFisica = new ListItem("Cédula persona física", "1");
            ListItem personaJuridica = new ListItem("Cédula persona jurídica", "2");
            ListItem nite = new ListItem("Número de Identificación Tributario Especial (NITE)", "3");
            ListItem dimex = new ListItem("Documento de Identificación Migratorio para Extranjeros (DIMEX)", "4");
            // Lo agrega a la colección de Items del DropDownList
            ddlTipoCedula.Items.Add(personaFisica);
            ddlTipoCedula.Items.Add(personaJuridica);
            ddlTipoCedula.Items.Add(nite);
            ddlTipoCedula.Items.Add(dimex);

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Proveedor esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Proveedores
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Proveedor proveedor = new Entidades.Proveedor();
                proveedor.cedula = txbCedulaProveedor.Text;
                proveedor.tipoCedula = Convert.ToInt32(ddlTipoCedula.SelectedItem.Value);
                proveedor.nombre = txbNombreProveedor.Text;
                proveedor.telefono = txbTelefonoProveedor.Text;
                proveedor.correo = txbCorreoProveedor.Text;

                proveedorServicios.insertarProveedor(proveedor);

                String url = Page.ResolveUrl("~/Pedidos/AdministrarProveedores.aspx");
                Response.Redirect(url);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Pedidos/AdministrarProveedores.aspx");
            Response.Redirect(url);
        }
    }
}