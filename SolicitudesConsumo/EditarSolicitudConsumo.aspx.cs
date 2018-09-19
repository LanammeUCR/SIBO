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
    public partial class EditarSolicitudConsumo : System.Web.UI.Page
    {
        #region variables globales
        private ArticuloServicios articuloServicio = new ArticuloServicios();
        private FuncionarioServicios funcionarioServicio = new FuncionarioServicios();
        private SolicitudesConsumoServicios solicitudesConsumoServicio = new SolicitudesConsumoServicios();
        private DetalleBodegaServicios detalleBodegaServicios = new DetalleBodegaServicios();
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
                Session["ordenConsumo"] = null;
                Session["articuloConsumo"] = null;
                Session["detalleArticulo"] = null;
                Session["listaArticulos"] = null;
                Session["listaArticulosSolicitud"] = null;
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarSolicitud();", true);
                cargardatos();
            }
        }
        #endregion

        #region logica
        /// <summary>
        /// 
        /// </summary>
        public void cargardatos()
        {

            List<Entidades.Articulo> listaArticulos = articuloServicio.getArticulos();

            int idConsumo = Convert.ToInt32(Session["idOrdenConsumo"].ToString());
            Consumo ordenConsumo = solicitudesConsumoServicio.getOrdenConsumo(idConsumo);
            Session["ordenConsumo"] = ordenConsumo;

            DetalleConsumo detalleConsumo = new DetalleConsumo();
            Session["detalleArticulo"] = detalleConsumo;

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
                        if (articuloConsumo.articulo.cantidadTotal + articuloConsumo.cantidadConsumo >= cantidad)
                        {
                            articuloConsumo.cantidadConsumo = cantidad;
                            Session["detalleArticulo"] = articuloConsumo;
                        }
                        else
                        {
                            (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado no puede ser mayor a la cantidad disponible", "¡Alerta!");
                            txbCantidad.Text = "";
                        }
                    }
                    else if (txbCantidad.ID == "txbCantidadEntregar")
                    {
                        if (articuloConsumo.cantidadPendiente >= cantidad)
                        {
                            articuloConsumo.cantidadEntregar = cantidad;
                            Session["detalleArticulo"] = articuloConsumo;
                        }
                        else
                        {
                            (this.Master as SiteMaster).Mensaje("La cantidad del articulo solicitado no puede ser mayor a la cantidad pendiente de entrega.", "¡Alerta!");
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
            if (txbCantidad.ID == "txbCantidadUbicar")
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
            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            if (!listaSolicitud.Exists(x => x.articulo.idArticulo == articuloConsumo.articulo.idArticulo))
            {
                listaSolicitud.Add(articuloConsumo);
                listaArticulos.Remove(articuloConsumo.articulo);
                Session["listaArticulosSolicitud"] = listaSolicitud;
                Session["listaArticulos"] = listaArticulos;

                rpArticulos.DataSource = listaArticulos;
                rpArticulos.DataBind();

                rpSolicitud.DataSource = listaSolicitud;
                rpSolicitud.DataBind();

                txbCantidad.Text = "";
            }
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalArticulos();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: asigna a la cantidad de entrega la cantidad solicitada por el usuario
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void chkEntregaTotal_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkParcial = (CheckBox)((CheckBox)sender).Parent.FindControl("chkEntregaParcial");
            if (chkParcial.Checked)
            {
                chkParcial.Checked = false;
            }
            //if (((CheckBox)sender).Checked)
            //{
            LinkButton lbID = (LinkButton)((CheckBox)sender).Parent.FindControl("btnEditar");
            int idArticulo = Convert.ToInt32((lbID.CommandArgument).ToString());
            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];

            int indexArticulo = listaSolicitud.FindIndex(x => x.articulo.idArticulo == idArticulo);
            DetalleConsumo detalleConsumo = listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo);
            if (!detalleConsumo.entregado)
            {
                if (detalleConsumo.cantidadEntregada == 0)
                {
                    listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).cantidadEntregar = detalleConsumo.cantidadConsumo;
                    listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).entregado = true;
                }
                else
                {
                    if (detalleConsumo.cantidadPendiente != 0)
                    {
                        listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).cantidadEntregar = detalleConsumo.cantidadPendiente;
                        listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).entregado = true;
                    }
                    else
                    {
                        (this.Master as SiteMaster).Mensaje("La cantidad solicitada del articulo se ha entregado completamente, no hay saldo pendiente.", "¡Alerta!");
                    }
                }
                listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).cantidadPendiente = 0;
                listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).entregado = true;

                Session["listaArticulosSolicitud"] = listaSolicitud;
                rpSolicitud.DataSource = listaSolicitud;
                rpSolicitud.DataBind();

                Entidades.Articulo articulo = new Entidades.Articulo();
                articulo.idArticulo =idArticulo;
                articulo.detalleUbicacionBodega = detalleBodegaServicios.getDetalleBodegas(articulo);
                Session["articuloConsumo"] = articulo;

                rpUbicaciones.DataSource = articulo.detalleUbicacionBodega;
                rpUbicaciones.DataBind();

                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
            }
            else
            {
                ((CheckBox)sender).Checked = false;
                (this.Master as SiteMaster).Mensaje("El articulo ya se encuentra entregado a su totalidad.", "¡Alerta!");
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: levanta el modal de entrega parcial
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void chkEntregaParcial_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkTotal = (CheckBox)((CheckBox)sender).Parent.FindControl("chkEntregaTotal");
            if (chkTotal.Checked)
            {
                chkTotal.Checked = false;
            }
            if (((CheckBox)sender).Checked)
            {
                LinkButton lbID = (LinkButton)((CheckBox)sender).Parent.FindControl("btnEditar");
                int idArticulo = Convert.ToInt32((lbID.CommandArgument).ToString());
                List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
                Entidades.Articulo articulo = new Entidades.Articulo();
                articulo.idArticulo = idArticulo;
                articulo.detalleUbicacionBodega = detalleBodegaServicios.getDetalleBodegas(articulo);
             

                DetalleConsumo detalleConsumo = listaSolicitud.Find(x => x.articulo.idArticulo == idArticulo);
                if (!detalleConsumo.entregado)
                {
                    Session["detalleArticulo"] = detalleConsumo;
                    Session["articuloConsumo"] = articulo;

                    txbCantidadSolicitadaParcial.Text = "" + detalleConsumo.cantidadPendiente;
                    rpUbicacionesParcial.DataSource = articulo.detalleUbicacionBodega;
                    rpUbicacionesParcial.DataBind();
                    ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalCantidad();", true);
                }
                else
                {
                    ((CheckBox)sender).Checked = false;
                    (this.Master as SiteMaster).Mensaje("El articulo ya se encuentra entregado a su totalidad.", "¡Alerta!");
                }
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: cambiar el valor del check para que sea exclusivo con chkmodificar
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void chkRechazar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkModificar.Checked)
            {
                chkModificar.Checked = false;
            }
            txbCantidadSolicitada.ReadOnly = true;
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalAdministrador();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: cambiar el valor del check para que sea exclusivo con chkrechazar
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void chkModificar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRechazar.Checked)
            {
                chkRechazar.Checked = false;
            }
            txbCantidadSolicitada.ReadOnly = false;
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalAdministrador();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: guarda la informacion y acciones realizadas en el modal de administrar
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAceptarAdministrar_Click(object sender, EventArgs e)
        {
            DetalleConsumo detalleConsumo = (DetalleConsumo)Session["detalleArticulo"];
            Consumo ordenConsumo = (Consumo)Session["ordenConsumo"];
            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];           

            if (chkRechazar.Checked)
            {
                if (txbComentario.Text != "")
                {                                     
                    listaSolicitud.RemoveAll(x => x.articulo.idArticulo == detalleConsumo.articulo.idArticulo);
                    ordenConsumo.comentarios.Add(new ComentarioSolicitud(txbComentario.Text));
                    Session["ordenConsumo"] = ordenConsumo;
                    rpSolicitud.DataSource = listaSolicitud;
                    rpSolicitud.DataBind();
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("Debe de agregar un comentario a la solicitud", "¡Alerta!");
                }
            }
            else if (chkModificar.Checked)
            {
                if (txbComentario.Text != "")
                {
                    listaSolicitud.Find(x => x.articulo.idArticulo == detalleConsumo.articulo.idArticulo).cantidadConsumo = detalleConsumo.cantidadConsumo;
                    ordenConsumo.comentarios.Add(new ComentarioSolicitud(txbComentario.Text));
                    Session["ordenConsumo"] = ordenConsumo;
                    rpSolicitud.DataSource = listaSolicitud;
                    rpSolicitud.DataBind();
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("Debe de agregar un comentario a la solicitud", "¡Alerta!");
                }
            }
            else
            {
                (this.Master as SiteMaster).Mensaje("Debe seleccionar una acción para editar la solicitud!", "¡Alerta!");
            }
           
            // ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalAdministrador();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 07/30/2018
        /// Efecto: recorre la solicitud y verifica si todos los elementos se encuentran entregados para finalizar la solicitud como entregada
        /// Requiere:Lista de detallesConsumo
        /// Modifica:
        /// Devuelve: true
        /// </summary>
        /// <param name ="listaSolicitud"></param>
        /// <returns></returns>
        private int isSolicitudEntregada(List<DetalleConsumo> listaSolicitud)
        {
            int cantidadEntregado = 0;

            foreach (DetalleConsumo item in listaSolicitud)
            {
                if (item.entregado)
                {
                    cantidadEntregado ++;
                }
            }

            return cantidadEntregado;
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 07/30/2018
        /// Efecto: recorre la solicitud y verifica si algún elementos se en entrega parcial
        /// Requiere:Lista de detallesConsumo
        /// Modifica:
        /// Devuelve: true
        /// </summary>
        /// <param name ="listaSolicitud"></param>
        /// <returns></returns>
        private int isSolicitudEntregaParcial(List<DetalleConsumo> listaSolicitud)
        {
            int cantidadEntregado = 0;

            foreach (DetalleConsumo item in listaSolicitud)
            {
                if (item.entregaParcial)
                {
                    cantidadEntregado++;
                }
            }

            return cantidadEntregado;
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Seleciona de la tabla el articulo a editar 
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            DetalleConsumo detalleConsumo = listaSolicitud.Find(x => x.articulo.idArticulo == idArticulo);
            lbNombreArticulo.Text = detalleConsumo.articulo.nombreArticulo;
            lbCantidad.Text = detalleConsumo.articulo.cantidadTotal.ToString();
            txbCantidadSolicitada.Text = detalleConsumo.cantidadConsumo.ToString();
            Session["detalleArticulo"] = detalleConsumo;

            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalAdministrador();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 15/05/2018
        /// Efecto: Agrega los cambios de la entrega parcial
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnAceptarEntrega_Click(object sender, EventArgs e)
        {
            DetalleConsumo detalleConsumo = (DetalleConsumo)Session["detalleArticulo"];
            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];

            int indexArticulo = listaSolicitud.FindIndex(x => x.articulo.idArticulo == detalleConsumo.articulo.idArticulo);
            if (!detalleConsumo.entregado)
            {
                listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).cantidadEntregar = +detalleConsumo.cantidadEntregar;
                detalleConsumo.cantidadPendiente = detalleConsumo.cantidadPendiente - detalleConsumo.cantidadEntregar;
                listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).cantidadPendiente = detalleConsumo.cantidadPendiente;

                if (detalleConsumo.cantidadPendiente == 0)
                {
                   listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).entregado = true;
                }else
                    listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).entregaParcial = true;
            
                Session["listaArticulosSolicitud"] = listaSolicitud;
                rpSolicitud.DataSource = listaSolicitud;
                rpSolicitud.DataBind();
            }
            else
            {
                (this.Master as SiteMaster).Mensaje("La cantidad solicitada del articulo ya se encuentra entregada a su totalidad.", "¡Alerta!");
            }
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
            Consumo ordenConsumo = (Consumo)Session["ordenConsumo"];          
            ordenConsumo.persona.nombre = Session["nombreCompleto"].ToString();
            ordenConsumo.detalleConsumo = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];

            for (int i = 0; i < ordenConsumo.detalleConsumo.Count; i++ ) 
            {
                ordenConsumo.detalleConsumo.ElementAt<DetalleConsumo>(i).cantidadEntregada =
                        ordenConsumo.detalleConsumo.ElementAt<DetalleConsumo>(i).cantidadEntregar;
                ordenConsumo.detalleConsumo.ElementAt<DetalleConsumo>(i).cantidadEntregar = 0;
            }
  
            if (chkRechazar.Checked)
            {
                //rechazado = true, para que pueda ser editable!!
                ordenConsumo.rechazado = true;
                solicitudesConsumoServicio.actualizarSolicitudConsumo(ordenConsumo, (String)Session["nombreCompleto"]);
                String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesConsumo.aspx");
                Response.Redirect(url);
            }
            else if (ordenConsumo.detalleConsumo.Count > 0)
            {
                if (ordenConsumo.detalleConsumo.Count == isSolicitudEntregada(ordenConsumo.detalleConsumo))
                {
                    ordenConsumo.entregado = true;
                } else if (isSolicitudEntregaParcial(ordenConsumo.detalleConsumo) > 0)
                {
                    ordenConsumo.entregaParcial = true;
                }
             
                solicitudesConsumoServicio.actualizarSolicitudConsumo(ordenConsumo, (String)Session["nombreCompleto"]);
                String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesConsumo.aspx");
                Response.Redirect(url);
            }
            else if(ordenConsumo.detalleConsumo.Count == 0)
            {
                (this.Master as SiteMaster).Mensaje("Debe de agregar al menos un articulo para realizar la solicitud", "¡Alerta!");
            }

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
            String url = Page.ResolveUrl("~/SolicitudesConsumo/AdministrarSolicitudesConsumo.aspx");
            Response.Redirect(url);
        }
        #endregion

        /// <summary>
        /// Fabián Quirós Masís
        /// 5/09/2018
        /// Efecto: Carga los datos necesario y levanta el modal de ubicaciones para que se seleccione de donde se van a entregar los articulos
        /// Requiere:-
        /// Modifica:-
        /// Devuelve:-
        /// </summary>
        /// <returns>-</returns>
        protected void btnSeleccionarUbicacion_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            String seleccionado = ((LinkButton)(sender)).CommandArgument.ToString();
            String[] ubicacion = seleccionado.Split(',');
            DetalleBodega detalleUbicacion = new DetalleBodega();
            detalleUbicacion.bodega.idBodega = Convert.ToInt32(ubicacion[0]);
            detalleUbicacion.estante = ubicacion[1];
            detalleUbicacion.piso = ubicacion[2];
            detalleUbicacion.cantidadArticulo = Convert.ToInt32(ubicacion[3]);
            Entidades.Articulo articulo = (Entidades.Articulo)Session["articuloConsumo"];

            List<DetalleConsumo> listaSolicitud = (List<DetalleConsumo>)Session["listaArticulosSolicitud"];
            int indexArticulo = listaSolicitud.FindIndex(x => x.articulo.idArticulo == articulo.idArticulo);

            DetalleConsumo detalleConsumo = listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo);
            if (boton.ID == "btnSeleccionarUbicacionTotal")
            {
                if (detalleConsumo.cantidadPendiente <= detalleUbicacion.cantidadArticulo)
                {
                    listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).articulo.detalleUbicacionBodega.Clear();
                    listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).articulo.detalleUbicacionBodega.Add(detalleUbicacion);
                    Session["listaArticulosSolicitud"] = listaSolicitud;
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("La cantidad disponible en la ubicacion seleccionada no es suficiente para realizar la entrega del articulo", "¡Alerta!");

                    ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
                }
            } else
            {
                int cantidad = Convert.ToInt32(txbCantidadEntregar.Text);
                if ( cantidad <= detalleUbicacion.cantidadArticulo)
                {
                    listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).articulo.detalleUbicacionBodega.Clear();
                    listaSolicitud.ElementAt<DetalleConsumo>(indexArticulo).articulo.detalleUbicacionBodega.Add(detalleUbicacion);
                    Session["listaArticulosSolicitud"] = listaSolicitud;
                    ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalCantidad();", true);
                }
                else
                {
                    (this.Master as SiteMaster).Mensaje("La cantidad disponible en la ubicacion seleccionada no es suficiente para realizar la entrega del articulo", "¡Alerta!");
                    ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalCantidad();", true);
                }                        
            }
        }
    }

}