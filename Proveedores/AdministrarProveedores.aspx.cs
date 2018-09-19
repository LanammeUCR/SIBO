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

            if (!Page.IsPostBack)
            {
                Session["listaProveedores"] = null;
                Session["proveedorEditar"] = null;
                Session["proveedornar"] = null;
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

            String url = Page.ResolveUrl("~/Proveedores/EditarProveedor.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se na una proveedor
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Proveedor> listaProveedores = (List<Proveedor>)Session["listaProveedores"];

            Proveedor proveedornar = new Proveedor();

            foreach (Proveedor proveedor in listaProveedores)
            {
                if (proveedor.idProveedor == id)
                {
                    proveedornar = proveedor;
                    break;
                }
            }

            Session["proveedorEliminar"] = proveedornar;

            String url = Page.ResolveUrl("~/Proveedores/EliminarProveedor.aspx");
            Response.Redirect(url);

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Redirecciona a la pagina para agregar un nuevo proveedor
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Proveedores/AgregarProveedor.aspx");
            Response.Redirect(url);
        }

        #endregion

    }
}