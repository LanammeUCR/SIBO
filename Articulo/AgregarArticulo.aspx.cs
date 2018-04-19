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
        ArticuloServicios recepcionistaServicios = new ArticuloServicios();
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
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarArticuloes");

            if (!permisos[1])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!Page.IsPostBack)
            {
             
                               
                txbNombreArticulo.Attributes.Add("oninput", "validarTexto(this)");
                txbDescripcion.Attributes.Add("oninput", "validarTexto(this)");
                txbExistenciasArticulo.Attributes.Add("oninput", "validarTexto(this)");
                txbCriticaArticulo.Attributes.Add("oninput", "validarTexto(this)");
                cldFechaIngresoArticulo.Attributes.Add("oninput", "validarTexto(this)");
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
            divExistenciasArticuloIncorrecto.Style.Add("display", "none");
            divCriticaArticuloIncorrecto.Style.Add("display", "none");
            divFechaArticuloIncorrecto.Style.Add("display", "none");

            txbNombreArticulo.CssClass = "form-control";
            txbDescripcion.CssClass = "form-control";
            txbExistenciasArticulo.CssClass = "form-control";
            txbCriticaArticulo.CssClass = "form-control";
            

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
            String descripcionArticulo = txbNombreArticulo.Text;

            if (descripcionArticulo.Trim() == "")
            {
                txbNombreArticulo.CssClass = "form-control alert-danger";
                divNombreArticuloIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion cantidad existencias Articulo
            String cantidadExistencias = txbExistenciasArticulo.Text;

            if (cantidadExistencias.Trim() == "")
            {
                txbExistenciasArticulo.CssClass = "form-control alert-danger";
                divExistenciasArticuloIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion cantidad critica Articulo
            String criticaArticulo = txbCriticaArticulo.Text;

            if (criticaArticulo.Trim() == "")
            {
                txbCriticaArticulo.CssClass = "form-control alert-danger";
                divCriticaArticuloIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            return validados;
        }
        #endregion

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

                articuloNuevo.nombreArticulo = txbNombreArticulo.Text;
                articuloNuevo.descripcion = txbDescripcion.Text;
                articuloNuevo.cantidadTotal = Convert.ToInt32(txbExistenciasArticulo.Text);
                articuloNuevo.cantidadCritica = Convert.ToInt32(txbCriticaArticulo.Text);
                articuloNuevo.fechaIngreso = cldFechaIngresoArticulo.SelectedDate;
                

                recepcionistaServicios.insertarArticulo(articuloNuevo);

                String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
                Response.Redirect(url);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
            Response.Redirect(url);
        }
    }
}