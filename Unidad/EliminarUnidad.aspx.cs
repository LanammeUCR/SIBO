using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Unidad
{
    public partial class EliminarUnidad : System.Web.UI.Page
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
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            
                Entidades.Unidad unidad = (Entidades.Unidad)Session["unidadEditar"];
                unidad.nombre = txtNombreUnidad.Text;
                unidad.telefono = txbTelefonoUnidad.Text;
               
                unidadServicios.eliminarUnidad(unidad, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
                Response.Redirect(url);
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
            Response.Redirect(url);
        }
    
    }
}