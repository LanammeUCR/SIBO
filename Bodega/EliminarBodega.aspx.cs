using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Bodega
{
    public partial class EliminarBodega : System.Web.UI.Page
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

            //devuelve los permisos de la pantalla en el siguiente orden:
            //[0]=ver
            //[1]=Nuevo
            //[2]=Eliminar
            //[3]=Eliminar
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarBodegaes");

            if (!permisos[1])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!Page.IsPostBack)
            {
                Entidades.Bodega bodegaEliminar = (Entidades.Bodega)Session["bodegaEliminar"];
                txtNombreBodega.Text = bodegaEliminar.nombre;
                txtNombreBodega.Attributes.Add("oninput", "validarTexto(this)");
                txbDireccionBodega.Text = bodegaEliminar.direccion;
                txbDireccionBodega.Attributes.Add("oninput", "validarTexto(this)");
                txbTelefonoBodega.Text = bodegaEliminar.telefono;
                txbTelefonoBodega.Attributes.Add("oninput", "validarTexto(this)");
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
       
        #endregion
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Bodega esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Bodegaes
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            
                Entidades.Bodega bodegaEliminar = (Entidades.Bodega)Session["bodegaEliminar"];               

                bodegaServicios.eliminarBodega(bodegaEliminar,(String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Bodega/AdministrarBodega.aspx");
                Response.Redirect(url);
           
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Bodega/AdministrarBodega.aspx");
            Response.Redirect(url);
        }
    }
}