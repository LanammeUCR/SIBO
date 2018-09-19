using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Inventario
{
    public partial class ArticulosUbicaciones : System.Web.UI.Page
    {
        #region variables globales
        ArticuloServicios articuloServicios = new ArticuloServicios();
        BodegaServicios bodegaServicios = new BodegaServicios();
        DetalleBodegaServicios detalleBodegaServicio = new DetalleBodegaServicios();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Session["articulo"] = null;

                cargarDatosTblArticulos();
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarUbicaciones();", true);
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "keyUp();", true);
            }
            //ScriptManager1.RegisterAsyncPostBackControl(btnAgregar);
            //ScriptManager1.RegisterAsyncPostBackControl(rpArticuloUbicaciones);
        }
        #endregion

        private void cargarDatosTblArticulos()
        {
            ddlBodegas.DataSource = bodegaServicios.getBodegas();
            ddlBodegas.DataTextField = "nombre";
            ddlBodegas.DataValueField = "idBodega";
            ddlBodegas.DataBind();

            rpUbicacionesArticulos.DataSource = articuloServicios.getArticulos();
            rpUbicacionesArticulos.DataBind();
        }

        #region eventos

        protected void ddlBodegas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Entidades.Bodega bodega = new Entidades.Bodega();
            bodega.idBodega = Convert.ToInt32(ddlBodegas.SelectedValue);

            rpUbicacionesArticulos.DataSource = articuloServicios.getArticulosBodega(bodega);
            rpUbicacionesArticulos.DataBind();
        }
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Selecciona un articulo para despues administrar las ubicaciones de este
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            Entidades.Articulo articulo = articuloServicios.getArticulo(idArticulo);

            Session["articulo"] = articulo;

            if (articulo != null)
            {
                txbNombreArticulo.Text = articulo.nombreArticulo;
                txbCodigo.Text = articulo.codigoArticulo;
                txbDescripcion.Text = articulo.descripcion;
                txbCantidadTotal.Text = articulo.cantidadTotal.ToString();
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulo();", true);
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Agrega las ubicaciones a la base de datos.
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Entidades.Articulo articulo = (Entidades.Articulo)Session["articulo"];
            if (!String.IsNullOrWhiteSpace(txbCantidadTotal.Text))
            {
                articuloServicios.actualizarArticuloExistencias(articulo, (String)Session["nombreCompleto"]);
            }else
            {
                (this.Master as SiteMaster).Mensaje("Debe de ingresar una cantidad mayor a 0", "¡Alerta!");
                txbCantidadTotal.Text = "";
            }
            cargarDatosTblArticulos();
        }

        #endregion

        protected void txbCantidadTotal_TextChanged(object sender, EventArgs e)
        {
            TextBox txbCantidad = (TextBox)sender;
            if (!String.IsNullOrWhiteSpace(txbCantidad.Text))
            {
                int cantidad = Convert.ToInt32(txbCantidad.Text);
                Entidades.Articulo articulo = (Entidades.Articulo)Session["articulo"];
                if (cantidad > 0)
                {
                    articulo.cantidadTotal = cantidad;
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("La cantidad del articulo tiene que ser mayor a 1", "¡Alerta!");
                    txbCantidad.Text = "";
                }
            }
            else
            {
                (this.Master as SiteMaster).Mensaje("Debe de ingresar una cantidad mayor a 0", "¡Alerta!");
                txbCantidad.Text = "";
            }
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulo();", true);
        }
    }
}