using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Unidad
{
    public partial class EditarUnidad : System.Web.UI.Page
    {
        #region variables globales
        UnidadServicios unidadServicios = new UnidadServicios();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Entidades.Recepcion unidad = (Entidades.Recepcion)Session["recepcionEditar"];
                txtNombreUnidad.Text = unidad.nombre;
                txtNombreUnidad.Attributes.Add("oninput", "validarTexto(this)");               
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
            divNombreUnidadIncorrecto.Style.Add("display","none");
          
            txtNombreUnidad.CssClass = "form-control";   
      
            #region validacion nombre unidad
            String nombreUnidad = txtNombreUnidad.Text;

            if(nombreUnidad.Trim()=="")
            {
                txtNombreUnidad.CssClass = "form-control alert-danger";
                divNombreUnidadIncorrecto.Style.Add("display", "block");

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
        /// Efecto: Verifica que los datos de una Unidad esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Unidades
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Entidades.Recepcion unidad = (Entidades.Recepcion)Session["recepcionEditar"];
                unidad.nombre = txtNombreUnidad.Text;               
               
                unidadServicios.actualizarUnidad(unidad, (String)Session["nombreCompleto"]);

                String url = Page.ResolveUrl("~/Recepcion/AdministrarRecepcion.aspx");
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: redirreciona a la pagina adminitrar unidades
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Recepcion/AdministrarRecepcion.aspx");
            Response.Redirect(url);
        }
        #endregion
    }
}