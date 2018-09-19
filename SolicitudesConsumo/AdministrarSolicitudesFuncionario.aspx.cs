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
    public partial class AdministrarSolicitudesFuncionario : System.Web.UI.Page
    {
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private FuncionarioServicios funcionarioServicio = new FuncionarioServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private SolicitudesConsumoServicios solicitudesConsumoServicio = new SolicitudesConsumoServicios();
        #endregion
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 13 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Session["idOrdenConsumo"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiar();", true);
                cargardatos();
            }
        }
        #endregion
        #region logica
        public void cargardatos()
        {
                   
            List<Consumo> listaOrdenesConsumo = solicitudesConsumoServicio.getOrdenesConsumoFuncionario();
            Session["listaOrdenesConsumo"] = listaOrdenesConsumo;
            rpOrdenesConsumo.DataSource = listaOrdenesConsumo;
            rpOrdenesConsumo.DataBind();
        }
        #endregion

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla que puede ver una solicitud
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnVer_Click(object sender, EventArgs e)
        {
            int idOrdenConsumo = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idOrdenConsumo"] = idOrdenConsumo;
            String url = Page.ResolveUrl("~/SolicitudesConsumo/VerSolicitudConsumoFuncionario.aspx");
            Response.Redirect(url);

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Redirecciona a la pantalla donde se edita una solicitud de articulo.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idOrdenConsumo = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
            Session["idOrdenConsumo"] = idOrdenConsumo;
            String url = Page.ResolveUrl("~/SolicitudesConsumo/EditarSolicitudConsumoFuncionario.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se na una solicitud de articulo
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        //protected void btnnar_Click(object sender, EventArgs e)
        //{
        //    int idOrdenConsumo = Convert.ToInt32((((LinkButton)sender).CommandArgument).ToString());
        //    Session["idOrdenConsumo"] = idOrdenConsumo;
        //    String url = Page.ResolveUrl("~/SolicitudesConsumo/narSolicitudConsumoFuncionario.aspx");
        //    Response.Redirect(url);

        //}
        protected void rpOrdenesConsumo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEditar = e.Item.FindControl("btnEditar") as LinkButton;
                List<Consumo> listaOrdenesConsumo = (List<Consumo>)Session["listaOrdenesConsumo"];
                
                HiddenField hfIdConsumo = e.Item.FindControl("hfIdConsumo") as HiddenField;

                int idConsumo = Convert.ToInt32(hfIdConsumo.Value);
              //  btnEditar.Visible = listaOrdenesConsumo.Find(x => x.idConsumo == idConsumo).rechazado;

                Repeater rpComentarios = e.Item.FindControl("rpComentarios") as Repeater;
                List<ComentarioSolicitud> comentarios = solicitudesConsumoServicio.getComentariosSolicitud(idConsumo);
                if(comentarios.Count != 0 ){
                    rpComentarios.DataSource = comentarios;
                    rpComentarios.DataBind();
                }
                   
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirecciona a la pantalla donde se crea una nueva solicitud de articulo
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnNueva_Solicitud_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/SolicitudesConsumo/SolicitudArticuloFuncionario.aspx");
            Response.Redirect(url);
        }
    }
}