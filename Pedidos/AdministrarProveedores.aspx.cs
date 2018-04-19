using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Pedidos
{
    public partial class AdministrarProveedores : System.Web.UI.Page
    {

        #region Variables Globales
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
                Session["listaProveedores"] = null;
                Session["proveedorEditar"] = null;
                Session["proveedorEliminar"] = null;


                cargarDatosTblProveedor();
            }
        }
        #region logica
        private void cargarDatosTblProveedor()
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            listaProveedores = proveedorServicios.getProveedores();
            rpProveedor.DataSource = listaProveedores;
            rpProveedor.DataBind();

            Session["listaProveedores"] = listaProveedores;
        }
        #endregion

        #region eventos
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita una proveedor.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Proveedor> listaProveedores = (List<Proveedor>)Session["listaProveedores"];

            Proveedor proveedorEditar = new Proveedor();

            foreach (Proveedor proveedor in listaProveedores)
            {
                if (proveedor.idProveedor == id)
                {
                    proveedorEditar = proveedor;
                    break;
                }
            }

            Session["proveedorEditar"] = proveedorEditar;

            String url = Page.ResolveUrl("~/Pedidos/EditarProveedor.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se elimina una proveedor
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Proveedor> listaProveedores = (List<Proveedor>)Session["listaProveedores"];

            Proveedor proveedorEliminar = new Proveedor();

            foreach (Proveedor proveedor in listaProveedores)
            {
                if (proveedor.idProveedor == id)
                {
                    proveedorEliminar = proveedor;
                    break;
                }
            }

            Session["proveedorEliminar"] = proveedorEliminar;

            String url = Page.ResolveUrl("~/Pedidos/EliminarProveedor.aspx");
            Response.Redirect(url);

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Pedidos/AgregarProveedor.aspx");
            Response.Redirect(url);
        }

        protected void rpProveedor_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarProveedor");

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