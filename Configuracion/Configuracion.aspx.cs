using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Servicios;

namespace SIBO.Configuracion
{
    public partial class Configuracion : System.Web.UI.Page
    {
        #region Variables
        private Archivo archivo = new Archivo();
        private BaseDatos baseDatos = new BaseDatos();
        private ConexionServicios conexionServicios = new ConexionServicios();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!IsPostBack)
            {
                baseDatos = archivo.leerArchivo();

                /*Base de datos BI*/
                txtNombreServidorSIBO.Text = baseDatos.servidorSIBO;
                txtNombreBdSIBO.Text = baseDatos.baseSIBO;
                txtNombreUsuarioBdSIBO.Text = baseDatos.usuarioSIBO;
                txtContrasenaBdSIBO.Text = baseDatos.contrasenaSIBO;

                /*Base de datos Login*/
                txtNombreServidorLogin.Text = baseDatos.servidorLogin;
                txtNombreBdLogin.Text = baseDatos.baseLogin;
                txtNombreUsuarioBdLogin.Text = baseDatos.usuarioLogin;
                txtContrasenaBdLogin.Text = baseDatos.contrasenaLogin;
            }
        }
        #endregion

        /*
         * Deiby Picado
         * 10/11/16
         * Evento que actualiza los datos para conexión de base de datos almacenados en el archivo de texto
         */
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            baseDatos = archivo.leerArchivo();

            /*Base de datos SIBO*/
            baseDatos.servidorSIBO = txtNombreServidorSIBO.Text;
            baseDatos.baseSIBO = txtNombreBdSIBO.Text;
            baseDatos.usuarioSIBO = txtNombreUsuarioBdSIBO.Text;
            if (txtContrasenaBdSIBO.Text.Trim() != "")
            {
                baseDatos.contrasenaSIBO = txtContrasenaBdSIBO.Text;
            }

            /*Base de datos Login*/
            baseDatos.baseLogin = txtNombreBdLogin.Text;
            baseDatos.usuarioLogin = txtNombreUsuarioBdLogin.Text;
            if (txtContrasenaBdLogin.Text.Trim() != "")
            {
                baseDatos.contrasenaLogin = txtContrasenaBdLogin.Text;
            }
            baseDatos.servidorLogin = txtNombreServidorLogin.Text;

            archivo.guardarArchivo(baseDatos);

            //(this.Master as SiteMaster).Mensaje("Se han actualizado los datos", "Información");
        }
    }
}