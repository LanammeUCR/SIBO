using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Articulo
{
    public partial class EliminarArticulo : System.Web.UI.Page
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

            //devuelve los permisos de la pantalla en el siguiente orden:
            //[0]=ver
            //[1]=Nuevo
            //[2]=Editar
            //[3]=Eliminar
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarArticuloes");

            if (!permisos[2])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!Page.IsPostBack)
            {

                Entidades.Articulo articuloEliminar = (Entidades.Articulo)Session["articuloELiminar"];

                txbNombreArticulo.Text = articuloEliminar.nombreArticulo;
            
                txbDescripcion.Text = articuloEliminar.descripcion;
            
                txbExistenciasArticulo.Text = articuloEliminar.cantidadTotal.ToString();
              
                txbCriticaArticulo.Text = articuloEliminar.cantidadCritica.ToString();

                txbFechaIngresoArticulo.Text = articuloEliminar.fechaIngreso.ToString("d"); ;
               
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
           
                Entidades.Articulo articuloEliminar = (Entidades.Articulo)Session["articuloEliminar"];
                recepcionistaServicios.eliminarArticulo(articuloEliminar, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
                Response.Redirect(url);           
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
            Response.Redirect(url);
        }
    }
}