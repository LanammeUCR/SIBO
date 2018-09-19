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
    public partial class AgregarUbicacionArticulo : System.Web.UI.Page
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
                txbEstante.Attributes.Add("onpinput", "validarTexto(this)");
                txbPiso.Attributes.Add("onpinput", "validarTexto(this)");                
                cargarDatosTblArticuloes();
            }

        }

        private void cargarDatosTblArticuloes()
        {
            Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloUbicacion"];
            lbNombre.Text = articulo.nombreArticulo;
           
            List<Entidades.Bodega> listaBodegas = bodegaServicios.getBodegas();
            ddlBodegas.DataSource = listaBodegas;
            ddlBodegas.DataTextField = "nombre";
            ddlBodegas.DataValueField = "idBodega";
            ddlBodegas.DataBind();
        }
          


        private bool validarCampos()
        {
            Boolean validados = true;
          
            divEstanteIncorrecto.Style.Add("display", "none");
            divPisoIncorrecto.Style.Add("display", "none");

            txbEstante.CssClass = "form-control";
            txbPiso.CssClass = "form-control";                     

            #region validacion Estante
            String estante = txbEstante.Text;

            if (estante.Trim() == "")
            {
                txbEstante.CssClass = "form-control alert-danger";
                divEstanteIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion piso
            String piso = txbPiso.Text;

            if (piso.Trim() == "")
            {
                txbPiso.CssClass = "form-control alert-danger";
                divPisoIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion         
            return validados;
        }

        #region eventos
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Articulo esten completos y realiza un actualizar lógico en la base de datos
        /// redirecciona a la pantalla de Administrar Articulos
        /// Requiere:-
        /// Modifica: Articulo
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //if (validarCampos())
            //{
            //    DetalleBodega detalleBodega = new DetalleBodega();
            //    Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloUbicacion"];

            //    detalleBodega.bodega.idBodega = Convert.ToInt32(ddlBodegas.SelectedValue);               
            //    detalleBodega.estante = txbEstante.Text;
            //    detalleBodega.piso = txbPiso.Text;
            //    detalleBodega.cantidadArticulo = Convert.ToInt32(txbCantidad.Text);

            //    detalleBodegaServicio.insertarBodega(detalleBodega, articulo.idArticulo);
            //    String url = Page.ResolveUrl("~/Articulo/ArticuloUbicaciones.aspx");
            //    Response.Redirect(url);
            //}

       
         
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/ArticuloUbicaciones.aspx");
            Response.Redirect(url);
        }
        #endregion
    }
}