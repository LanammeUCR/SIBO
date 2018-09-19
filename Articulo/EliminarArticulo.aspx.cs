using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Articulo
{
    public partial class narArticulo : System.Web.UI.Page
    {
        #region variables globales
        ArticuloServicios recepcionistaServicios = new ArticuloServicios();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {

                Entidades.Articulo articulonar = (Entidades.Articulo)Session["articuloEliminar"];

                txbNombreArticulo.Text = articulonar.nombreArticulo;
            
                txbDescripcion.Text = articulonar.descripcion;
            
                txbExistenciasArticulo.Text = articulonar.cantidadTotal.ToString();
              
                txbCriticaArticulo.Text = articulonar.cantidadCritica.ToString();

                txbFechaIngresoArticulo.Text = articulonar.fechaIngreso.ToString(); ;
               
            }
        }

       
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Articulo esten completos y realiza un actualizar lógico en la base de datos
        /// redirecciona a la pantalla de Administrar Articuloes
        /// Requiere:-
        /// Modifica: Articulo
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnActualizar_Click(object sender, EventArgs e)
        {

            Entidades.Articulo articulonar = (Entidades.Articulo)Session["articuloEliminar"];
                recepcionistaServicios.eliminarArticulo(articulonar, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
                Response.Redirect(url);           
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla de Administrar Articuloes
        /// Requiere:-
        /// Modifica: Articulo
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
            Response.Redirect(url);
        }
    }
}