using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Unidad
{
    public partial class EliminarRecepcionista : System.Web.UI.Page
    {
         #region variables globales
        PersonaRecepcionistaServicios recepcionistaServicios = new PersonaRecepcionistaServicios();
        PersonaRecepcionista recepcionista;
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

            if (!permisos[3])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!Page.IsPostBack)
            {
                recepcionista = (PersonaRecepcionista)Session["recepcionistaEliminar"];
                txbCedulaRecepcionista.Text = recepcionista.cedula;            
                txbNombreRecepcionista.Text = recepcionista.nombre;             
                txbApellidosRecepcionista.Text = recepcionista.apellidos;            
                txbCorreoRecepcionista.Text = recepcionista.correo;             
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Realiza un borrado lógico de la base de datos
        /// redirecciona a la pantalla de Administrar Recepcionistaes
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
                recepcionista = (PersonaRecepcionista)Session["recepcionistaEliminar"];
                recepcionistaServicios.eliminarRecepcionista(recepcionista, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Unidad/AdministrarRecepcionistas.aspx");
                Response.Redirect(url);
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AdministrarRecepcionistas.aspx");
            Response.Redirect(url);
        }
    }
}