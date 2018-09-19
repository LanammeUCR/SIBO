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
    public partial class SolicitudArticulo : System.Web.UI.Page
    {
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private FuncionarioServicios funcionarioServicio = new FuncionarioServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private SolicitudesConsumoServicios solicitudesConsumoServicio = new SolicitudesConsumoServicios();
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
                Session["articuloSeleccionado"] = null;
                Session["detalleArticulo"] = null;
                Session["listaArticulos"] = null;
                Session["listaArticulosSolicitud"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarSolcititud();", true);
                cargardatos();
            }            
        }  
        #endregion
        #region logica
        public void cargardatos() {
            String userName = Session["login"].ToString();
          
            List<Entidades.Articulo> listaArticulos = articuloServicio.getArticulos();
            Session["listaArticulos"] = listaArticulos;

            List<DetalleConsumo> listaArticulosSolicitud = new List<DetalleConsumo>();
            Session["listaArticulosSolicitud"] = listaArticulosSolicitud;

            rpSolicitud.DataSource = listaArticulosSolicitud;
            rpSolicitud.DataBind();

            ddlUnidad.DataSource = unidadServicios.getUnidades();
            ddlUnidad.DataTextField = "nombre";
            ddlUnidad.DataValueField = "idRecepcion";
            ddlUnidad.DataBind();          
        }

        public Boolean validarCampos()
        {
            Boolean validados = true;
            divDescripcionIncorrecto.Style.Add("display", "none");

            txbDescripcion.CssClass = "form-control";


            #region validacion nombre Articulo
            String descripcion = txbDescripcion.Text;

            if (descripcion.Trim() == "")
            {
                txbDescripcion.CssClass = "form-control alert-danger";
                divDescripcionIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            return validados;
        }
        #endregion
        #region eventos

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Agrega a la tabla el articulo seleccionado del listbox
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];
            rpArticulos.DataSource = listaArticulos; 
            rpArticulos.DataBind();
            // ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarArticulos();", true);
            txbArticuloSeleccionado.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
            
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Agrega un articulo seleccionado a la solicitud 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void ChckBxSeleccionado(object sender, EventArgs e)
        {

            LinkButton chkArticulo = (LinkButton)sender;
            HiddenField hfID = (HiddenField)((LinkButton)sender).Parent.FindControl("hfIdArticulo");
            //chkArticulo.Text= "<span class=\"btn glyphicon glyphicon-check\">";
            int idArticulo = Convert.ToInt32(hfID.Value);
            RepeaterItemCollection items =  rpArticulos.Items;
            
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];
          
            Entidades.Articulo articulo = listaArticulos.Find(x => x.idArticulo == idArticulo);

            txbArticuloSeleccionado.Text = articulo.nombreArticulo;
            Session["articuloSeleccionado"] = articulo;
          
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Verifica que la cantidad ingresada no sea negativa o nula
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void txbCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txbCantidad = (TextBox)sender;
            if (!String.IsNullOrWhiteSpace( txbCantidad.Text ))
            {
                int cantidad = Convert.ToInt32(txbCantidad.Text);
                if (cantidad > 0)
                {
                    //HiddenField hfID = (HiddenField)((TextBox)sender).Parent.FindControl("hfIdArticulo");

                    //int idArticulo = Convert.ToInt32(hfID.Value);
                    Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloSeleccionado"];

                    if (articulo != null)
                    {                       
                        List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

                        //int cantidadTotal = listaSolicitud.Find(x => x.articulo.idArticulo == articulo.idArticulo).articulo.cantidadTotal;

                        if (articulo.cantidadTotal >= cantidad)
                        {
                            DetalleConsumo detalleArticulo = new DetalleConsumo();
                            detalleArticulo.articulo = articulo;
                            detalleArticulo.cantidadConsumo = cantidad;
                            detalleArticulo.cantidadPendiente = cantidad;
                            listaArticulos.Remove(articulo);
                            Session["detalleArticulo"] = detalleArticulo;
                            Session["listaArticulos"] = listaArticulos;                           
                        }
                        else
                        {
                            (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado no puede ser mayor a la cantidad disponible", "¡Alerta!");
                            txbCantidad.Text = "";
                        }
                    }
                    else
                    {
                        (this.Master as SiteMaster).Mensaje("Debe seleccionar un articulo primero", "¡Alerta!");
                        txbCantidad.Text = "";
                    }
                }
                else 
                {
                    (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado tiene que ser mayor a 1", "¡Alerta!");
                    txbCantidad.Text = "";
                }
            }
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: na de la tabla el articulo seleccionado 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregarArticuloLista(object sender, EventArgs e)
        {
            DetalleConsumo articuloConsumo = (DetalleConsumo)Session["detalleArticulo"];
            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            if (!listaSolicitud.Exists(x => x.articulo.idArticulo == articuloConsumo.articulo.idArticulo))
            {
                articuloConsumo.cantidadEntregada = articuloConsumo.cantidadEntregar = 0; listaSolicitud.Add(articuloConsumo);
                listaArticulos.Remove(articuloConsumo.articulo);
                Session["listaArticulosSolicitud"] = listaSolicitud;
                Session["listaArticulos"] = listaArticulos;    

                rpArticulos.DataSource = listaArticulos;
                rpArticulos.DataBind();

                rpSolicitud.DataSource = listaSolicitud;
                rpSolicitud.DataBind();

                txbCantidad.Text = "";
            }
            txbArticuloSeleccionado.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: na de la tabla el articulo seleccionado 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnnarArticulo_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            Entidades.Articulo articulo = listaSolicitud.Find(x => x.articulo.idArticulo == idArticulo).articulo;

            listaArticulos.Add(articulo);
            listaSolicitud.RemoveAll(x => x.articulo.idArticulo == idArticulo);

            rpSolicitud.DataSource = listaSolicitud;
            rpSolicitud.DataBind();            
        }

      
        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Envía la solicitud a la base de datos para ser ingresada a los registros
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEnviarSolicitud(object sender, EventArgs e)
        {
            Consumo ordenConsumo = new Consumo();
            ordenConsumo.persona.nombre = Session["nombreCompleto"].ToString();
            ordenConsumo.unidadSolicitante.idRecepcion = Convert.ToInt32(ddlUnidad.SelectedValue);
            ordenConsumo.detalleConsumo = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            ordenConsumo.rechazado = false;
            if (validarCampos())
            {
                ordenConsumo.decripcion = txbDescripcion.Text;
                if (ordenConsumo.detalleConsumo.Count > 0)
                {
                    solicitudesConsumoServicio.insertarConsumoArticulo(ordenConsumo);
                    String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesConsumo.aspx");
                    Response.Redirect(url);
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("Debe de agregar al menos un articulo para realizar la solicitud", "¡Alerta!");
                }
            }
            else
            {
                (this.Master as SiteMaster).Mensaje("Debe de agregar un comentario a la solicitud", "¡Alerta!");
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: regresa a la pantalla para administrar solicitudes
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar(object sender, EventArgs e)
        {

            String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesConsumo.aspx");
            Response.Redirect(url);
        }
        #endregion

    }
}