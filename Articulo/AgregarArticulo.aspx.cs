using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Articulo
{
    public partial class AgregarArticulo : System.Web.UI.Page
    {

        #region variables globales
        ArticuloServicios articuloServicios = new ArticuloServicios();
        #endregion

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                txbNombreArticulo.Attributes.Add("oninput", "validarTexto(this)");
                txbNombreArticulo.AutoCompleteType = AutoCompleteType.DisplayName;
                txbDescripcion.Attributes.Add("oninput", "validarTexto(this)");
            }
        }
        #endregion

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
            divCodigoArticuloIncorrecto.Style.Add("display", "none");

            txbNombreArticulo.CssClass = "form-control";
            txbDescripcion.CssClass = "form-control";
            txbCodigo.CssClass = "form-control";

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

            #region validacion descripcion Articulo
            String codigoArticulo = txbCodigo.Text;

            if (codigoArticulo.Trim() == "")
            {
                txbCodigo.CssClass = "form-control alert-danger";
                divCodigoArticuloIncorrecto.Style.Add("display", "block");

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
                Entidades.Articulo articuloNuevo = new Entidades.Articulo();
                articuloNuevo.codigoArticulo = txbCodigo.Text;

                if (!articuloServicios.existeArticulo(articuloNuevo.codigoArticulo))
                {
                    articuloNuevo.nombreArticulo = txbNombreArticulo.Text;
                    articuloNuevo.descripcion = txbDescripcion.Text;
                    articuloNuevo.fechaIngreso = DateTime.Now;
                    if (chkCritica.Checked)
                    {
                        articuloNuevo.cantidadCritica = Convert.ToInt32(txbCantidadCritica.Text);
                    }
                    if (txbTiempoEntrega.Text != null && txbTiempoEntrega.Text != "")
                    {
                        articuloNuevo.tiempoEntrega = Convert.ToInt32(txbTiempoEntrega.Text);
                    }
                    if (txbGastoAnual.Text != null && txbGastoAnual.Text != "")
                    {
                        articuloNuevo.gastoAnualAproximado = Convert.ToInt32(txbGastoAnual.Text);
                    }

                    articuloServicios.insertarArticulo(articuloNuevo);

                    String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
                    Response.Redirect(url);
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("El codigo del articulo ya se encuentra registrado. Por favor verifiquelo!", "¡Alerta!");
                }
            }
        }
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Devuelve a la página de administración de articulos 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
            Response.Redirect(url);
        }

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
            if (!String.IsNullOrEmpty(txbGastoAnual.Text))
            {
                int gastoAnual = Convert.ToInt32(txbGastoAnual.Text);
                Double[] variables = articuloServicios.getVariables();
                if (String.IsNullOrEmpty(txbTiempoEntrega.Text))
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
        /// Efecto: Verifica que el gasto anual no sean nulos o negativos
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void txbTiempoEntrega_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txbTiempoEntrega.Text))
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
            if (!String.IsNullOrEmpty(txbGastoAnual.Text))
            {
                int gastoAnual = Convert.ToInt32(txbGastoAnual.Text);
                if (gastoAnual < 0)
                {
                    txbGastoAnual.Text = "";
                    (this.Master as SiteMaster).Mensaje("El gasto anual no puede ser un numero negativo.", "¡Alerta!");
                }
            }
        }
        #endregion

    }
}