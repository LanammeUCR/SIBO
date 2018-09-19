using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Articulo
{
    public partial class narUbicacionArticulo : System.Web.UI.Page
    {
        #region variables globales
        BodegaServicios bodegaServicios = new BodegaServicios();
        DetalleBodegaServicios detalleBodegaServicio = new DetalleBodegaServicios();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                DetalleBodega detalleEditar = (DetalleBodega)Session["detalleEditar"];
                                              
                txbEstante.Text = detalleEditar.estante;              
                txbPiso.Text = detalleEditar.piso;                
                txbCantidad.Text = detalleEditar.cantidadArticulo.ToString();
                txbUbicacion.Text = detalleEditar.bodega.nombre;
            }

        }         

        #region eventos
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
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
           
                DetalleBodega detalleBodega = (DetalleBodega)Session["detalleEditar"];
                                                            
                Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloUbicacion"];
                detalleBodegaServicio.eliminarBodega(detalleBodega, articulo.idArticulo, (String)Session["nombreCompleto"]);
                String url = Page.ResolveUrl("~/Articulo/ArticuloUbicaciones.aspx");
                Response.Redirect(url);
            
        }      
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/ArticuloUbicaciones.aspx");
            Response.Redirect(url);
        }
        #endregion
    
    }
}