using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO.SolicitudesConsumo
{
    public partial class EditarSolicitudConsumoFuncionario : System.Web.UI.Page
    {
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private SolicitudesConsumoServicios solicitudesConsumoServicio = new SolicitudesConsumoServicios();
        #endregion
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 13 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {
                Session["listaArticulos"] = null;
                Session["listaArticulosSolicitud"] = null;
                Session["ordenConsumo"] = null;
                Session["articuloConsumo"] = null;
                Session["detalleArticulo"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarSolicitud();", true);
                cargardatos();
            }
        }

        public void cargardatos()
        {
            String userName = Session["login"].ToString();

            List<Entidades.Articulo> listaArticulos = articuloServicio.getArticulos();            

            int idConsumo = Convert.ToInt32(Session["idOrdenConsumo"].ToString());
            Consumo ordenConsumo = solicitudesConsumoServicio.getOrdenConsumo(idConsumo);
            Session["ordenConsumo"] = ordenConsumo;

            DetalleConsumo detalleConsumo = new DetalleConsumo();
            Session["detalleArticulo"] = detalleConsumo;

            Session["ordenConsumo"] = ordenConsumo;
            List<DetalleConsumo> listaArticulosSolicitud = solicitudesConsumoServicio.getDetalleOrdenesConsumo(idConsumo);
            Session["listaArticulosSolicitud"] = listaArticulosSolicitud;


            foreach (DetalleConsumo detalleTemp in listaArticulosSolicitud)
            {
                listaArticulos.RemoveAll(x => x.idArticulo == detalleTemp.articulo.idArticulo);
            }

            Session["listaArticulos"] = listaArticulos;

            lbIDConsumo.Text = idConsumo.ToString();
            lblUnidadNombre.Text = ordenConsumo.unidadSolicitante.nombre;

            rpSolicitud.DataSource = listaArticulosSolicitud;
            rpSolicitud.DataBind();
        }

        #endregion

        #region eventos

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: selecciona un articulo de la solicitud de consumo
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void ChckBxSeleccionado(object sender, EventArgs e)
        {

            LinkButton chkArticulo = (LinkButton)sender;
            HiddenField hfID = (HiddenField)((LinkButton)sender).Parent.FindControl("hfIdArticulo");

            int idArticulo = Convert.ToInt32(hfID.Value);

            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            Entidades.Articulo articulo = listaArticulos.Find(x => x.idArticulo == idArticulo);

            Session["articuloConsumo"] = articulo;

            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Verifica que los text box de cantidad no les asignen datos negativos o mayores a las existencias del articulo
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void txbCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txbCantidad = (TextBox)sender;
            if (txbCantidad.Text != "")
            {
                int cantidad = Convert.ToInt32(txbCantidad.Text);
                if (cantidad > 0)
                {
                    Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloConsumo"];
                    DetalleConsumo articuloConsumo = (DetalleConsumo)Session["detalleArticulo"];
                    if (txbCantidad.ID == "txbCantidad")
                    {
                        if (articulo != null)
                        {
                            if (articulo.cantidadTotal >= cantidad)
                            {
                                articuloConsumo = new DetalleConsumo();
                                articuloConsumo.articulo = articulo;
                                articuloConsumo.cantidadConsumo = cantidad;
                                Session["detalleArticulo"] = articuloConsumo;
                                txbCantidad.Text = "";
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
                    else if (txbCantidad.ID == "txbCantidadSolicitada")
                    {
                        if (articuloConsumo.articulo.cantidadTotal >= cantidad)
                        {
                            articuloConsumo.cantidadConsumo = cantidad;
                            Session["detalleArticulo"] = articuloConsumo;
                            txbCantidad.Text = "";
                        }
                        else
                        {
                            (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado no puede ser mayor a la cantidad disponible", "¡Alerta!");
                            txbCantidad.Text = "";
                        }
                    }
                    else if (txbCantidad.ID == "cantidadArticulo")
                    {
                        LinkButton lbID = (LinkButton)((TextBox)sender).Parent.FindControl("btnnar");
                        int idArticulo = Convert.ToInt32((lbID.CommandArgument).ToString());
                        List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];

                        int index = listaSolicitud.FindIndex(x => x.articulo.idArticulo == idArticulo);
                        articuloConsumo = listaSolicitud.ElementAt<DetalleConsumo>(index);
                        if (articuloConsumo.articulo.cantidadTotal >= cantidad)
                        {
                            listaSolicitud.ElementAt<DetalleConsumo>(index).cantidadConsumo = cantidad;
                            Session["listaArticulosSolicitud"] = listaSolicitud;
                        }
                        else
                        {
                            (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado no puede ser mayor a la cantidad disponible", "¡Alerta!");
                            txbCantidad.Text = "";
                        }
                    }
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado tiene que ser mayor a 1", "¡Alerta!");
                    txbCantidad.Text = "";
                }
            }
            if (txbCantidad.ID == "txbCantidadSolicitada")
            {
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalAdministrador();", true);
            }
            else if (txbCantidad.ID == "txbCantidad")
            {
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
            }
            else if (txbCantidad.ID == "txbCantidadEntregar")
            {
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalCantidad();", true);
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Agrega articulos nuevos a la solicitud ya realizada 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAgregarArticuloLista(object sender, EventArgs e)
        {
            DetalleConsumo articuloConsumo = (DetalleConsumo)Session["detalleArticulo"];
            List<DetalleConsumo> listaArticulosSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            if (!listaArticulosSolicitud.Exists(x => x.articulo.idArticulo == articuloConsumo.articulo.idArticulo))
            {
                listaArticulosSolicitud.Add(articuloConsumo);
                listaArticulos.Remove(articuloConsumo.articulo);
                Session["listaArticulosSolicitud"] = listaArticulosSolicitud;
                Session["listaArticulos"] = listaArticulos;

                rpArticulos.DataSource = listaArticulos;
                rpArticulos.DataBind();

                rpSolicitud.DataSource = listaArticulosSolicitud;
                rpSolicitud.DataBind();

                txbCantidad.Text = "";
            }
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

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
           
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: na de la tabla el articulo seleccionado del listbox
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnnarArticulo_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<DetalleConsumo> listaArticulosSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];

            listaArticulosSolicitud.RemoveAll(x => x.articulo.idArticulo == idArticulo);
            rpSolicitud.DataSource = listaArticulosSolicitud;
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
            ordenConsumo.idConsumo = Convert.ToInt32(Session["idOrdenConsumo"].ToString());
            ordenConsumo.persona.nombre = Session["nombreCompleto"].ToString();
            ordenConsumo.detalleConsumo = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            // rechazado = false para que no se pueda editar
            ordenConsumo.rechazado = false;            
            solicitudesConsumoServicio.actualizarSolicitudConsumo(ordenConsumo, (String)Session["nombreCompleto"]);

            String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesFuncionario.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Redirecciona a la pagina admistrar solicitudes
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesFuncionario.aspx");
            Response.Redirect(url);
        }


        #endregion
    }

}