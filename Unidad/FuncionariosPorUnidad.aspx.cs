using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.Unidad
{
    public partial class RecepcionistasPorUnidad : System.Web.UI.Page
    {

        #region estructuras

        /*
         * Leonardo Carrion
         * 11/11/2015
         * estructura de los usuarios que se obtienen del active directory
         */
        public struct UsuarioActiveDirectory
        {
            public string usuario { get; set; }
            public string nombreCompleto { get; set; }
        }
        #endregion

        #region Variables Globales
        UnidadServicios unidadServicio = new UnidadServicios();
        FuncionarioServicios funcionarioServicios = new FuncionarioServicios();
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
            Boolean[] permisos = Utilidades.permisosPorPagina(Page, "AdministrarClientes");


            if (!permisos[2])
            {
                String url = Page.ResolveUrl("~/Default.aspx");
                Response.Redirect(url);
            }
                            
            if (!IsPostBack)
            {                               
                string dominName = string.Empty;
                foreach (string key in System.Configuration.ConfigurationManager.AppSettings.Keys)
                {
                    dominName = key.Contains("DirectoryDomain") ? System.Configuration.ConfigurationManager.AppSettings[key] : dominName;
                }

                //DirectoryContext dc = new DirectoryContext(DirectoryContextType.Domain, Environment.UserDomainName);
                DirectoryContext dc = new DirectoryContext(DirectoryContextType.Domain, dominName);
                Domain domain = Domain.GetDomain(dc);
                DirectoryEntry de = domain.GetDirectoryEntry();

                Entidades.Unidad unidad = (Entidades.Unidad)Session["unidadAsociar"];
                lblNombreUnidad.Text += " "+unidad.nombre;

                Session["listaFuncionariosAsociadosTemp"] = null;
                Session["listaFuncionariosDesasociarTemp"] = null;

                llenarUsuariosActiveDirectory(de);
                cargarListaFuncionarios(unidad);               
            }
        }

       
        private void llenarUsuariosActiveDirectory(DirectoryEntry de)
        {
            List<UsuarioActiveDirectory> listaUsuariosActiveDirectory = new List<UsuarioActiveDirectory>();
            DirectorySearcher deSearch = new DirectorySearcher(de);

            deSearch.Filter = "(&(objectClass=user)(objectCategory=person))";
            SearchResultCollection results = deSearch.FindAll();

            foreach (SearchResult srUser in results)
            {
                DirectoryEntry deUser = srUser.GetDirectoryEntry();

                int flag = (int)deUser.Properties["userAccountControl"].Value;

                bool mActivo = false;
                if (!Convert.ToBoolean(flag & 0x0002)) mActivo = true;

                if (mActivo)
                {
                    UsuarioActiveDirectory usuarioActiveDirectory = new UsuarioActiveDirectory();

                    usuarioActiveDirectory.nombreCompleto = deUser.Properties["Name"].Value != null ? deUser.Properties["Name"].Value.ToString() : "";

                    usuarioActiveDirectory.usuario = deUser.Properties["sAMAccountName"].Value.ToString();

                    listaUsuariosActiveDirectory.Add(usuarioActiveDirectory);
                }
            }

            Session["listaUsuariosActiveDirectory"] = listaUsuariosActiveDirectory;

            foreach (UsuarioActiveDirectory usuarioAcitveDirectory in listaUsuariosActiveDirectory)
            {
                Funcionario usuario = new Funcionario();
                usuario.nombre = usuarioAcitveDirectory.nombreCompleto;
                usuario.usuario = usuarioAcitveDirectory.usuario;
                usuario.mostrar = true;

                funcionarioServicios.insertarFuncionario(usuario);
            }

        }

        protected void cargarListaFuncionarios(Entidades.Unidad unidad)
        {           
            List<Funcionario> listaFuncionariosAsociados = new List<Funcionario>();
            List<Funcionario> listaFuncionariosDesasociarTemp = new List<Funcionario>();

            listaFuncionariosAsociados = funcionarioServicios.getFuncionariosUnidad(unidad);

            Session["listaFuncionariosAsociadosTemp"] = listaFuncionariosAsociados;
            Session["listaFuncionariosDesasociarTemp"] = listaFuncionariosDesasociarTemp;

            LbFuncionarios.DataSource = funcionarioServicios.getFuncionariosFueraUnidad(unidad);
            LbFuncionarios.DataTextField = "nombre";
            LbFuncionarios.DataValueField = "idFuncionario";
            LbFuncionarios.DataBind();

            LbFuncionariosAsociados.DataSource = listaFuncionariosAsociados;
            LbFuncionariosAsociados.DataTextField = "nombre";
            LbFuncionariosAsociados.DataValueField = "idFuncionario";
            LbFuncionariosAsociados.DataBind();
        }

        #region Eventos
        /// <summary>
        /// Fabián Quirós Masís
        /// 23/04/2018
        /// Efecto: Metodo se actica cuando se quiere asociar un funcionario a una unidad 
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>

        protected void btnPasarDerecha_Click(object sender, EventArgs e)
        {
            ListItemCollection listaFuncionariosAsociados = LbFuncionariosAsociados.Items;
            ListItemCollection listaFuncionarios = LbFuncionarios.Items;
            ListItemCollection listaEliminados = new ListItemCollection();

            List<Funcionario> listaFuncionariosAsociadosTemp = (List<Funcionario>) Session["listaFuncionariosAsociadosTemp"];
            List<Funcionario> listaFuncionariosDesasociarTemp = (List<Funcionario>) Session["listaFuncionariosDesasociarTemp"];

            foreach (ListItem item in listaFuncionariosAsociados)
            {
                if (item.Selected)
                {
                    Funcionario funcionario = funcionarioServicios.getFuncionarioPorId(Convert.ToInt32(item.Value));                    

                    listaFuncionariosDesasociarTemp.Add(funcionario);                   
                    if (listaFuncionariosAsociadosTemp.Exists(x => x.idFuncionario == funcionario.idFuncionario))
                    {
                       
                        listaFuncionariosAsociadosTemp.RemoveAt(listaFuncionariosAsociadosTemp.FindIndex(x => x.idFuncionario == funcionario.idFuncionario));
                    }

                    listaFuncionarios.Add(item);
                    listaEliminados.Add(item);
                }
            }

            foreach (ListItem item in listaEliminados)
            {
                listaFuncionariosAsociados.Remove(item);

            }

            Session["listaFuncionariosAsociadosTemp"] = listaFuncionariosAsociadosTemp;
            Session["listaFuncionariosDesasociarTemp"] = listaFuncionariosDesasociarTemp;
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 23/04/2018
        /// Efecto: Metodo se actica cuando se quiere eliminar un funcionario de una unidad 
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>

        protected void btnPasarIzquierda_Click(object sender, EventArgs e)
        {
            ListItemCollection listaFuncionario = LbFuncionarios.Items;
            ListItemCollection listaFuncionariosAsociados = LbFuncionariosAsociados.Items;
            ListItemCollection listaEliminados = new ListItemCollection();

            List<Funcionario> listaFuncionariosAsociadosTemp = (List<Funcionario>)Session["listaFuncionariosAsociadosTemp"];
            List<Funcionario> listaFuncionariosDesasociarTemp = (List<Funcionario>)Session["listaFuncionariosDesasociarTemp"];

            foreach (ListItem item in listaFuncionario)
            {
                if (item.Selected)
                {
                    Funcionario funcionario = funcionarioServicios.getFuncionarioPorId(Convert.ToInt32(item.Value));

                    if (listaFuncionariosDesasociarTemp.Exists(x => x.idFuncionario == funcionario.idFuncionario))
                    {
                      listaFuncionariosDesasociarTemp.RemoveAt(listaFuncionariosDesasociarTemp.FindIndex(x => x.idFuncionario == funcionario.idFuncionario));
                    }
                   
                    listaFuncionariosAsociadosTemp.Add(funcionario);
                    listaFuncionariosAsociados.Add(item);
                    listaEliminados.Add(item);
                }
            }

             foreach (ListItem item in listaEliminados)
            {
                listaFuncionario.Remove(item);
            }

             Session["listaFuncionariosAsociadosTemp"] = listaFuncionariosAsociadosTemp;
             Session["listaFuncionariosDesasociarTemp"] = listaFuncionariosDesasociarTemp;
                  
        }                
        

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
            Response.Redirect(url);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<Funcionario> listaFuncionariosAsociadosTemp = (List<Funcionario>)Session["listaFuncionariosAsociadosTemp"];
            List<Funcionario> listaFuncionariosDesasociarTemp = (List<Funcionario>)Session["listaFuncionariosDesasociarTemp"];
            Entidades.Unidad unidad = (Entidades.Unidad)Session["unidadAsociar"];
            String usuario =(String)Session["nombreCompleto"];
            funcionarioServicios.eliminarFuncionariosUnidad(listaFuncionariosDesasociarTemp, unidad, usuario);
            funcionarioServicios.asociarFuncionariosUnidad(listaFuncionariosAsociadosTemp, unidad, usuario);

            String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
            Response.Redirect(url);
        }
        #endregion

        protected void txtBuscarFuncionarios_TextChanged(object sender, EventArgs e)
        {
            ListItemCollection listaFuncionarios = LbFuncionarios.Items;
            ListItemCollection listaFiltrada = new ListItemCollection();
            String busqueda = txtBuscarFuncionarios.Text;

            foreach (ListItem item in listaFuncionarios){
                String nombreFuncionario = item.Text;
                if (nombreFuncionario.ToLower().Contains(busqueda.ToLower()))
                {
                    listaFiltrada.Add(item);
                }            
            }

            listaFuncionarios = listaFiltrada;
            
           
        }

    }
}