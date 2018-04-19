using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Bodega
{
    public partial class EditarBodega : System.Web.UI.Page
    {
        #region variables globales
        BodegaServicios bodegaServicios = new BodegaServicios();

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
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarBodegaes");

            if (!permisos[1])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }

            if (!Page.IsPostBack)
            {
                Entidades.Bodega bodegaEditar = (Entidades.Bodega)Session["bodegaEditar"];
                txtNombreBodega.Text = bodegaEditar.nombre;
                txtNombreBodega.Attributes.Add("oninput", "validarTexto(this)");
                txbDireccionBodega.Text = bodegaEditar.direccion;
                txbDireccionBodega.Attributes.Add("oninput", "validarTexto(this)");
                txbTelefonoBodega.Text = bodegaEditar.telefono;
                txbTelefonoBodega.Attributes.Add("oninput", "validarTexto(this)");
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
            divNombreBodegaIncorrecto.Style.Add("display", "none");
            divTelefonoBodegaIncorrecto.Style.Add("display", "none");

            txtNombreBodega.CssClass = "form-control";
            txbTelefonoBodega.CssClass = "form-control";

            #region validacion nombre bodega
            String nombreBodega = txtNombreBodega.Text;

            if (nombreBodega.Trim() == "")
            {
                txtNombreBodega.CssClass = "form-control alert-danger";
                divNombreBodegaIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            #region validacion telefono bodega
            String telefonoBodega = txbTelefonoBodega.Text;

            if (telefonoBodega.Trim() == "")
            {
                txbTelefonoBodega.CssClass = "form-control alert-danger";
                divTelefonoBodegaIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            return validados;
        }
        #endregion
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Bodega esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Bodegaes
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Entidades.Bodega bodegaEditar = (Entidades.Bodega)Session["bodegaEditar"];
                bodegaEditar.nombre = txtNombreBodega.Text;
                bodegaEditar.direccion = txbDireccionBodega.Text;
                bodegaEditar.telefono = txbTelefonoBodega.Text;

                bodegaServicios.actualizarBodega(bodegaEditar,(String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Bodega/AdministrarBodega.aspx");
                Response.Redirect(url);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Bodega/AdministrarBodega.aspx");
            Response.Redirect(url);
        }
    }
}