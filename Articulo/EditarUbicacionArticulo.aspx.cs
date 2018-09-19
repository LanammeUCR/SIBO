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
    public partial class EditarUbicacionArticulo : System.Web.UI.Page
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

              
                txbEstante.Attributes.Add("onpinput", "validarTexto(this)");
                txbEstante.Text = detalleEditar.estante;
                txbPiso.Attributes.Add("onpinput","validarTexto(this)");
                txbPiso.Text = detalleEditar.piso;
                
                txbCantidad.Text = detalleEditar.cantidadArticulo.ToString();
                txbUbicacion.Text = detalleEditar.bodega.nombre;
            }

        }     

        private bool validarCampos()
        {
            Boolean validados = true;
            divFilaIncorrecto.Style.Add("display", "none");
            divEstanteIncorrecto.Style.Add("display", "none");
            divPisoIncorrecto.Style.Add("display", "none");


            txbFila.CssClass = "form-control";
            txbEstante.CssClass = "form-control";
            txbPiso.CssClass = "form-control";           

            #region validacion fila
            String fila = txbFila.Text;

            if (fila.Trim() == "")
            {
                txbFila.CssClass = "form-control alert-danger";
                divFilaIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

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
        /// redirecciona a la pantalla de Administrar Articuloes
        /// Requiere:-
        /// Modifica: Articulo
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                DetalleBodega detalleBodega = (DetalleBodega)Session["detalleEditar"];
                                              
                detalleBodega.estante = txbEstante.Text;
                detalleBodega.piso = txbPiso.Text;
                detalleBodega.cantidadArticulo = Convert.ToInt32(txbCantidad.Text);
                
                //Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloUbicacion"];
                //detalleBodegaServicio.actualizarBodega(detalleBodega, articulo.idArticulo, (String) Session["nombreCompleto"]);
                //String url = Page.ResolveUrl("~/Articulo/ArticuloUbicaciones.aspx");
                //Response.Redirect(url);
            }
        }      
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/ArticuloUbicaciones.aspx");
            Response.Redirect(url);
        }
        #endregion
    
    }
}