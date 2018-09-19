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

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

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

                Entidades.Recepcion unidad = (Entidades.Recepcion)Session["unidadAsociar"];
                lblNombreUnidad.Text += " "+unidad.nombre;

                Session["listaFuncionariosAsociadosTemp"] = null;
                Session["listaFuncionariosDesasociarTemp"] = null;
                Session["listaFunsionariosSinAsociarTemp"] = null;

                llenarUsuariosActiveDirectory(de);
                cargarListaFuncionarios(unidad);               
            }
        }
        #endregion
        #region logica
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

        protected void cargarListaFuncionarios(Entidades.Recepcion unidad)
        {           
            List<Funcionario> listaFuncionariosAsociados = new List<Funcionario>();
            List<Funcionario> listaFuncionariosDesasociarTemp = new List<Funcionario>();
            List<Funcionario> listaFuncionariosSinAsociarTemp = new List<Funcionario>();

            listaFuncionariosAsociados = funcionarioServicios.getFuncionariosUnidad(unidad);
            listaFuncionariosSinAsociarTemp = funcionarioServicios.getFuncionariosFueraUnidad(unidad);

            Session["listaFuncionariosAsociadosTemp"] = listaFuncionariosAsociados;
            Session["listaFuncionariosDesasociarTemp"] = listaFuncionariosDesasociarTemp;
            Session["listaFunsionariosSinAsociarTemp"] = listaFuncionariosSinAsociarTemp;

            LbFuncionarios.DataSource = listaFuncionariosSinAsociarTemp;
            LbFuncionarios.DataTextField = "nombre";
            LbFuncionarios.DataValueField = "idFuncionario";
            LbFuncionarios.DataBind();

            LbFuncionariosAsociados.DataSource = listaFuncionariosAsociados;
            LbFuncionariosAsociados.DataTextField = "nombre";
            LbFuncionariosAsociados.DataValueField = "idFuncionario";
            LbFuncionariosAsociados.DataBind();
        }
        #endregion

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
            ListItemCollection listanados = new ListItemCollection();

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
                    listanados.Add(item);
                }
            }

            foreach (ListItem item in listanados)
            {
                listaFuncionariosAsociados.Remove(item);

            }

            Session["listaFuncionariosAsociadosTemp"] = listaFuncionariosAsociadosTemp;
            Session["listaFuncionariosDesasociarTemp"] = listaFuncionariosDesasociarTemp;

        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 23/04/2018
        /// Efecto: Metodo se actica cuando se quiere nar un funcionario de una unidad 
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>

        protected void btnPasarIzquierda_Click(object sender, EventArgs e)
        {
            ListItemCollection listaFuncionario = LbFuncionarios.Items;
            ListItemCollection listaFuncionariosAsociados = LbFuncionariosAsociados.Items;
            ListItemCollection listanados = new ListItemCollection();

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
                    listanados.Add(item);
                }
            }

             foreach (ListItem item in listanados)
            {
                listaFuncionario.Remove(item);
            }

             Session["listaFuncionariosAsociadosTemp"] = listaFuncionariosAsociadosTemp;
             Session["listaFuncionariosDesasociarTemp"] = listaFuncionariosDesasociarTemp;

                  
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 23/04/2018
        /// Efecto: redirreciona a la pagina adminitrar unidades
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 23/04/2018
        /// Efecto: actualiza los funcionarios asociados a una unidad 
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<Funcionario> listaFuncionariosAsociadosTemp = (List<Funcionario>)Session["listaFuncionariosAsociadosTemp"];
            List<Funcionario> listaFuncionariosDesasociarTemp = (List<Funcionario>)Session["listaFuncionariosDesasociarTemp"];
            Entidades.Recepcion unidad = (Entidades.Recepcion)Session["unidadAsociar"];
            String usuario =(String)Session["nombreCompleto"];
            funcionarioServicios.eliminarFuncionariosUnidad(listaFuncionariosDesasociarTemp, unidad, usuario);
            funcionarioServicios.asociarFuncionariosUnidad(listaFuncionariosAsociadosTemp, unidad, usuario);

            String url = Page.ResolveUrl("~/Unidad/AdministrarUnidades.aspx");
            Response.Redirect(url);
        }
        
        /// <summary>
        /// Fabián Quirós Masís
        /// 23/04/2018
        /// Efecto: Se encarga de llenar el listbox con una lista filtrada por el nombre del funcionario que es ingresado en el textbox 
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void txtBuscarFuncionarios_TextChanged(object sender, EventArgs e)
        {
            List<Funcionario> listaFuncionariosAsociados = (List<Funcionario>)Session["listaFunsionariosSinAsociarTemp"];
            List<Funcionario> listaFiltrados = new List<Funcionario>();

            String busqueda = txtBuscarFuncionarios.Text;
            listaFiltrados = listaFuncionariosAsociados.FindAll(x => x.nombre.ToLower().Contains(busqueda.ToLower()));

            LbFuncionarios.Items.Clear();
            LbFuncionarios.DataSource = listaFiltrados;
            LbFuncionarios.DataTextField = "nombre";
            LbFuncionarios.DataValueField = "idFuncionario";
            LbFuncionarios.DataBind();                           
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 23/04/2018
        /// Efecto: Se encarga de llenar el listbox con una lista filtrada por el nombre del funcionario asociado a una
        /// unidad  
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void txtBuscarFuncionariosAsociados_TextChanged(object sender, EventArgs e)
        {
            List<Funcionario> listaFuncionariosAsociados = (List<Funcionario>)Session["listaFuncionariosAsociadosTemp"];
            List<Funcionario> listaFiltrados = new List<Funcionario>();
           
            String busqueda = txtBuscarFuncionariosAsociados.Text;
            listaFiltrados = listaFuncionariosAsociados.FindAll(x => x.nombre.ToLower().Contains(busqueda.ToLower()));          

            LbFuncionariosAsociados.Items.Clear();
            LbFuncionariosAsociados.DataSource = listaFiltrados;
            LbFuncionariosAsociados.DataTextField = "nombre";
            LbFuncionariosAsociados.DataValueField = "idFuncionario";
            LbFuncionariosAsociados.DataBind();
        }
        #endregion

    }
}