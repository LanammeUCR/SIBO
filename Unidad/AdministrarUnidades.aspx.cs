using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Servicios;

namespace SIBO.Unidad
{
    public partial class AdministradorUnidades : System.Web.UI.Page
    {
        #region variables globales
        UnidadServicios unidadServicios = new UnidadServicios();
        #endregion


        #region page load
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

            if (!permisos[0])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!permisos[1])
            {
                btnNuevo.Visible = false;

            }

            if (!Page.IsPostBack)
            {
                Session["listaUnidades"] = null;
                Session["unidadEditar"] = null;                
                Session["unidadSeleccionada"] = null;              
                txtAutorizadoPor.Text = (String)Session["nombreCompleto"];

                cargarDatosTblUnidades();              
            }
        }
        #endregion

        #region logica
        public void cargarDatosTblUnidades() 
        {
            List<Entidades.Unidad> listasUniades = new List<Entidades.Unidad>();
            listasUniades = unidadServicios.getUnidades();
            rpUnidad.DataBind();
            Session["listaUnidades"] = listasUniades;

        }
        #endregion

        #region eventos
       
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            
        }

        
        protected void btnEditar_Click(object sender, EventArgs e)
        {
          
        }

       
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            

        }
        protected void rpUnidad_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEditar = e.Item.FindControl("btnEditar") as LinkButton;
                LinkButton btnEliminar = e.Item.FindControl("btnEliminar") as LinkButton;
                //devuelve los permisos de la pantalla en el siguiente orden:
                //[0]=ver
                //[1]=Nuevo
                //[2]=Editar
                //[3]=Eliminar
                Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarUnidades");

                if (!permisos[0])
                {
                    String url = Page.ResolveUrl("~/Default.aspx");
                    Response.Redirect(url);
                }

                if (!permisos[2])
                {
                    btnEditar.Visible = false;
                }

                if (!permisos[3])
                {
                    btnEliminar.Visible = false;
                }
            }
        }
        #endregion

    }
}