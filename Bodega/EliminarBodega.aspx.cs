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

            if (!Page.IsPostBack)
            {
                Entidades.Bodega bodegaEliminar = (Entidades.Bodega)Session["bodegaEliminar"];
                txtNombreBodega.Text = bodegaEliminar.nombre;
                txtNombreBodega.Attributes.Add("oninput", "validarTexto(this)");
                txbDireccionBodega.Text = bodegaEliminar.direccion;
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
        protected void btnnar_Click(object sender, EventArgs e)
        {

            Entidades.Bodega bodeganar = (Entidades.Bodega)Session["bodegaEliminar"];               

                bodegaServicios.eliminarBodega(bodeganar,(String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Bodega/AdministrarBodega.aspx");
                Response.Redirect(url);
           
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