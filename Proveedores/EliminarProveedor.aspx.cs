using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Proveedores
{
    public partial class EliminarProveedor : System.Web.UI.Page
    {
        #region variables globales
        ProveedorServicios proveedorServicios = new ProveedorServicios();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Proveedor proveedornar = (Proveedor)Session["proveedorEliminar"];
                txbCedulaProveedor.Text = proveedornar.cedula;               
                txbNombreProveedor.Text = proveedornar.nombre;            
                txbTelefonoProveedor.Text = proveedornar.telefono;           
                txbCorreoProveedor.Text = proveedornar.correo;        
                cargaTipoCedula(proveedornar.tipoCedula);
                
            }
        }
        #endregion
       

        private void cargaTipoCedula(int tipo)
        {
            if(tipo == 1)
            {
                txbTipoCedula.Text = "Cédula persona física";
            }
            if(tipo == 2)
            {
                txbTipoCedula.Text = "Cédula persona jurídica";
            }
            if(tipo == 3)
            {
                txbTipoCedula.Text = "Número de Identificación Tributario Especial (NITE)";
            }
            if (tipo == 4)
            {
                txbTipoCedula.Text = "Documento de Identificación Migratorio para Extranjeros (DIMEX)";
            }                                   
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 13/04/2018
        /// Efecto: Verifica que los datos de una Proveedor esten completos y los actualiza en la base de datos
        /// redirecciona a la pantalla de Administrar Proveedores
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Proveedor proveedorEditar = (Proveedor)Session["proveedorEliminar"];

            proveedorServicios.eliminarProveedor(proveedorEditar, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Proveedores/AdministrarProveedores.aspx");
                Response.Redirect(url);            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Proveedores/AdministrarProveedores.aspx");
            Response.Redirect(url);
        }
    }
}