using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Articulo
{
    public partial class EditarArticulo : System.Web.UI.Page
    {
        #region variables globales
        static ArticuloServicios articuloServicios = new ArticuloServicios();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {

                Entidades.Articulo articuloEditar = (Entidades.Articulo) Session["articuloEditar"];

                txbNombreArticulo.Text = articuloEditar.nombreArticulo;
                txbNombreArticulo.Attributes.Add("oninput", "validarTexto(this)");
                txbDescripcion.Text = articuloEditar.descripcion;
                txbDescripcion.Attributes.Add("oninput", "validarTexto(this)");               
                txbCantidadCritica.Text = articuloEditar.cantidadCritica.ToString();
                txbCantidadCritica.Attributes.Add("oninput", "validarTexto(this)");
                txbFecha.Text = articuloEditar.fechaIngreso.ToShortDateString();
                txbFecha.Attributes.Add("oninput", "validarFecha(this)");
                txbTiempoEntrega.Text = articuloEditar.tiempoEntrega.ToString();
                txbGastoAnual.Text = articuloEditar.gastoAnualAproximado.ToString();
            }
        }

        #region logica
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Valida los campos que debe de ingresar el usuario
        /// devuelve true si todos se encuentran correctos, de lo contrario 
        /// devuelve false y marca los campos para que el usuario vea cuales son.
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        public Boolean validarCampos()
        {
            Boolean validados = true;
            divNombreArticuloIncorrecto.Style.Add("display", "none");
            divDescripcionArticuloIncorrecto.Style.Add("display", "none");         
            divCriticaArticuloIncorrecto.Style.Add("display", "none");
            divFechaArticuloIncorrecto.Style.Add("display", "none");

            txbNombreArticulo.CssClass = "form-control";
            txbDescripcion.CssClass = "form-control";         
            txbCantidadCritica.CssClass = "form-control";

            #region validacion nombre Articulo
            String nombreArticulo = txbNombreArticulo.Text;

            if (nombreArticulo.Trim() == "")
            {
                txbNombreArticulo.CssClass = "form-control alert-danger";
                divNombreArticuloIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion descripcion Articulo
            String descripcionArticulo = txbDescripcion.Text;

            if (descripcionArticulo.Trim() == "")
            {
                txbDescripcion.CssClass = "form-control alert-danger";
                divNombreArticuloIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion           
        
            #region validacion cantidad critica Articulo
            String criticaArticulo = txbCantidadCritica.Text;

            if (criticaArticulo.Trim() == "")
            {
                txbCantidadCritica.CssClass = "form-control alert-danger";
                divCriticaArticuloIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            return validados;
        }
        #endregion

        #region eventos

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que el gasto anual y tiempo de entre no sean nulos o negativos y calcula la cantidad critica
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void chkCritica_CheckedChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txbGastoAnual.Text))
            {
                int gastoAnual = Convert.ToInt32(txbGastoAnual.Text);
                Double[] variables = articuloServicios.getVariables();
                if (!String.IsNullOrWhiteSpace(txbTiempoEntrega.Text))
                {
                    Double tiempoEntrega = Convert.ToDouble(txbTiempoEntrega.Text);
                    //[0] = dias habiles por año
                    Double cantidadCritica = (gastoAnual / variables[0]) * tiempoEntrega;
                    txbCantidadCritica.Text = "" + Math.Ceiling(cantidadCritica);
                }
                else
                {
                    //[0] = dias habiles por año, [1] tiempo de entrega por defecto
                    txbCantidadCritica.Text = "" + Math.Ceiling((gastoAnual / variables[0]) * variables[1]);
                }
            }
            else
            {
                chkCritica.Checked = false;
                (this.Master as SiteMaster).Mensaje("Para calcular la cantidad critica es necesario al menos ingresar el gasto aproximado anual.", "¡Alerta!");
            }
        }
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que el tiempo de entre no sean nulos o negativos
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void txbTiempoEntrega_TextChanged(object sender, EventArgs e)
        {
            if (txbTiempoEntrega.Text != null && txbTiempoEntrega.Text != "")
            {
                int tiempoEntrega = Convert.ToInt32(txbTiempoEntrega.Text);
                if (tiempoEntrega < 0)
                {
                    txbTiempoEntrega.Text = "";
                    (this.Master as SiteMaster).Mensaje("El tiempo de entrega no puede ser un numero negativo.", "¡Alerta!");
                }
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que el gasto anual no sean nulos o negativos
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void txbGastoAnual_TextChanged(object sender, EventArgs e)
        {
            if (txbGastoAnual.Text != null && txbGastoAnual.Text != "")
            {
                int gastoAnual = Convert.ToInt32(txbGastoAnual.Text);
                if (gastoAnual < 0)
                {
                    txbGastoAnual.Text = "";
                    (this.Master as SiteMaster).Mensaje("El gasto anual no puede ser un numero negativo.", "¡Alerta!");
                }
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
            if (validarCampos())
            {
                Entidades.Articulo articuloEditar = (Entidades.Articulo)Session["articuloEditar"];

                articuloEditar.nombreArticulo = txbNombreArticulo.Text;
                articuloEditar.descripcion = txbDescripcion.Text;             
                articuloEditar.cantidadCritica = Convert.ToInt32(txbCantidadCritica.Text);
                articuloEditar.fechaIngreso = Convert.ToDateTime(txbFecha.Text);
                articuloServicios.actualizarArticulo(articuloEditar, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
                Response.Redirect(url);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
            Response.Redirect(url);
        }
        #endregion
    }
}