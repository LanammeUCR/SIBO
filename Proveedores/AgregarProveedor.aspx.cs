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
    public partial class AgregarProveedor : System.Web.UI.Page
    {
        #region variables globales
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

                txbCedulaProveedor.Attributes.Add("oninput", "validarTexto(this)");
                txbNombreProveedor.Attributes.Add("oninput", "validarTexto(this)");             
                
                cargaTipoCedula();
            }
        }
        #region logica

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Agrega al dropdown list los tipos de cedulas admitidas
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        private void cargaTipoCedula()
        {
            ListItem personaFisica = new ListItem("Cédula persona física", "1");
            ListItem personaJuridica = new ListItem("Cédula persona jurídica", "2");          
            // Lo agrega a la colección de Items del DropDownList
            ddlTipoCedula.Items.Add(personaFisica);
            ddlTipoCedula.Items.Add(personaJuridica);           

        }
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
            divCedulaProveedorIncorrecto.Style.Add("display", "none");
            divNombreProveedorIncorrecto.Style.Add("display", "none");
           
            txbCedulaProveedor.CssClass = "form-control";
            txbNombreProveedor.CssClass = "form-control";
          

            #region validacion cedula Proveedor
            String cedulaProveedor = txbCedulaProveedor.Text;

            if (cedulaProveedor.Trim() == "")
            {
                txbCedulaProveedor.CssClass = "form-control alert-danger";
                divCedulaProveedorIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion nombre Proveedor
            String nombreProveedor = txbNombreProveedor.Text;

            if (nombreProveedor.Trim() == "")
            {
                txbNombreProveedor.CssClass = "form-control alert-danger";
                divNombreProveedorIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
           
            return validados;
        }
        #endregion

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/04/2018
        /// Efecto: Verifica que los datos de una Proveedor esten completos y los guarda en la base de datos
        /// redirecciona a la pantalla de Administrar Proveedores
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Proveedor proveedor = new Entidades.Proveedor();
                proveedor.cedula = txbCedulaProveedor.Text;
                if (!proveedorServicios.existeProveedor(proveedor.cedula))
                {

                    proveedor.tipoCedula = Convert.ToInt32(ddlTipoCedula.SelectedItem.Value);
                    proveedor.nombre = txbNombreProveedor.Text;
                    proveedor.telefono = txbTelefonoProveedor.Text;
                    proveedor.correo = txbCorreoProveedor.Text;

                    proveedorServicios.insertarProveedor(proveedor);

                    String url = Page.ResolveUrl("~/Proveedores/AdministrarProveedores.aspx");
                    Response.Redirect(url);
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("El numero de cédula del proveedor ya se encuentra registrado. Por favor veriquelo!", "¡Alerta!");
                }
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Redirecciona a la pagina de administrar proveedores
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Proveedores/AdministrarProveedores.aspx");
            Response.Redirect(url);
        }
    }
}