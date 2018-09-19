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
    public partial class ArticulosUbicaciones : System.Web.UI.Page
    {
        #region variables globales
        ArticuloServicios articuloServicios = new ArticuloServicios();
        BodegaServicios bodegaServicios = new BodegaServicios();
        DetalleBodegaServicios detalleBodegaServicio = new DetalleBodegaServicios();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //controla los menus q se muestran y las pantallas que se muestras segun el rol que tiene el usuario
            //si no tiene permiso de ver la pagina se redirecciona a login
            int[] rolesPermitidos = { 2 };
            Utilidades.escogerMenu(Page, rolesPermitidos);

            if (!Page.IsPostBack)
            {               
                Session["articulo"] = null;
                Session["listaUbicaciones"] = null;
                Session["listaArticulos"] = null;

                txbEstante.Attributes.Add("onpinput", "validarTexto(this)");
                txbPiso.Attributes.Add("onpinput", "validarTexto(this)");       
                cargarDatosTblArticuloes();
                ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarUbicaciones();", true);
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "keyUp();", true);
            }
            //ScriptManager1.RegisterAsyncPostBackControl(btnAgregar);
            //ScriptManager1.RegisterAsyncPostBackControl(rpArticuloUbicaciones);
        }
        #endregion

        private void cargarDatosTblArticuloes()
        {
            List<Entidades.Articulo> listaArticulos = new List<Entidades.Articulo>();
            listaArticulos = articuloServicios.getArticulos();
            rpUbicacionesArticulos.DataSource = listaArticulos;
            rpUbicacionesArticulos.DataBind();
            List<DetalleBodega> listaUbicaciones = new List<DetalleBodega>();
            Session["listaUbicaciones"] = listaUbicaciones;

            ddlBodegas.DataSource = bodegaServicios.getBodegas();
            ddlBodegas.DataTextField = "nombre";
            ddlBodegas.DataValueField = "idBodega";
            ddlBodegas.DataBind();

            Session["listaArticulos"] = listaArticulos;                    
        }

        private bool validarCampos()
        {
            Boolean validados = true;

            divEstanteIncorrecto.Style.Add("display", "none");
            divPisoIncorrecto.Style.Add("display", "none");

            txbEstante.CssClass = "form-control";
            txbPiso.CssClass = "form-control";

            #region validacion Estante
            String estante = txbEstante.Text;

            if (estante.Trim() == "")
            {
                txbEstante.CssClass = "form-control alert-danger";
                divEstanteIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion

            #region validacion piso
            String piso = txbPiso.Text;

            if (piso.Trim() == "")
            {
                txbPiso.CssClass = "form-control alert-danger";
                divPisoIncorrecto.Style.Add("display", "block");

                validados = false;
            }
            #endregion
            return validados;
        }

        #region eventos
        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: verifica que la cantidad no sea nula o negativa
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void txbCantidad_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txbCantidad.Text))
            {
                int cantidad = Convert.ToInt32(txbCantidad.Text);
                if (cantidad < 0)
                {
                    txbCantidad.Text = "1";
                    (this.Master as SiteMaster).Mensaje("La cantidad del Articulo no puede ser un numero negativo.", "¡Alerta!");
                }
            }
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: redirecciona a la pantalla donde de inicio 
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Articulo/AdministrarArticulos.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Selecciona un articulo para despues administrar las ubicaciones de este
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            List<Entidades.Articulo> listaArticulos = (List<Entidades.Articulo>)Session["listaArticulos"];

            Entidades.Articulo articulo = listaArticulos.Find(x => x.idArticulo == idArticulo);

            Session["articulo"] = articulo;        

            if (articulo != null)
            {
                articulo.detalleUbicacionBodega = detalleBodegaServicio.getDetalleBodegas(articulo);
                Session["listaUbicaciones"] = articulo.detalleUbicacionBodega;
                rpArticuloUbicaciones.DataSource = articulo.detalleUbicacionBodega;
                rpArticuloUbicaciones.DataBind();
                lblNombreArticulo.Text = articulo.nombreArticulo;
               // ClientScript.RegisterStartupScript(GetType(), "activar", "limpiarArticuloUbicaciones();", true);
                ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
            }               
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Agrega una ubicación del articulo según bodega, estante y piso, 
        /// verifica que esta misma ya no se encuetra registrada
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if(validarCampos())
            {
                DetalleBodega ubicacionArticulo = new DetalleBodega();
                ubicacionArticulo.bodega.idBodega = Convert.ToInt32(ddlBodegas.SelectedValue);
                ubicacionArticulo.bodega.nombre = ddlBodegas.SelectedItem.Text;
                ubicacionArticulo.estante = txbEstante.Text;
                ubicacionArticulo.piso = txbPiso.Text;

                Entidades.Articulo articulo= (Entidades.Articulo) Session["articulo"];
                int cantidad = Convert.ToInt32(txbCantidad.Text);

                if (cantidad <= articulo.cantidadUbicable)
                {
                    List<DetalleBodega> listaUbicaciones = (List<DetalleBodega>)Session["listaUbicaciones"];
                    DetalleBodega detalleUbicacion = listaUbicaciones.Find(
                        x => x.bodega.idBodega == ubicacionArticulo.bodega.idBodega 
                            && x.estante == ubicacionArticulo.estante && x.piso == ubicacionArticulo.piso);
                    if (detalleUbicacion != null)
                    {
                        listaUbicaciones.Remove(detalleUbicacion);
                        detalleUbicacion.cantidadArticulo += cantidad;
                        articulo.cantidadUbicable = articulo.cantidadUbicable - cantidad;
                        listaUbicaciones.Add(detalleUbicacion);

                    }
                    else
                    {
                        ubicacionArticulo.cantidadArticulo = cantidad;
                        articulo.cantidadUbicable = articulo.cantidadUbicable - cantidad;
                        listaUbicaciones.Add(ubicacionArticulo);
                    }

                    Session["articulo"] = articulo;
                    Session["listaUbicaciones"] = listaUbicaciones;

                    rpArticuloUbicaciones.DataSource = listaUbicaciones;
                    rpArticuloUbicaciones.DataBind();
                }
                else 
                {
                    txbCantidad.Text = "";
                    (this.Master as SiteMaster).Mensaje("La cantidad a ubicar del articulo no puede ser menor a la cantidad disponible para ubicar!", "¡Alerta!");
                }              
            }
            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: na una ubicación del articulo según el id de la bodega
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnEliminarUbicacion_Click(object sender, EventArgs e)
        {
            int idBodega = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            String estante = ((HiddenField)((LinkButton)sender).Parent.FindControl("hdfEstante")).Value;
            List<DetalleBodega> listaUbicaciones = (List<DetalleBodega>)Session["listaUbicaciones"];
            DetalleBodega detalleUbicacion = listaUbicaciones.Find(x => x.bodega.idBodega == idBodega && x.estante == estante);
            listaUbicaciones.Remove(detalleUbicacion);
            Session["listaUbicaciones"] = listaUbicaciones;
            rpArticuloUbicaciones.DataSource = listaUbicaciones;
            rpArticuloUbicaciones.DataBind();          

            ClientScript.RegisterStartupScript(GetType(), "activar", "levantarModalUbicacion();", true);
        }

        /// <summary>
        /// Fabián Quirós Masís
        /// 09/06/2018
        /// Efecto: Agrega las ubicaciones a la base de datos.
        /// Requiere:
        /// Modifica:
        /// Devuelve:
        /// </summary>
        /// <returns></returns>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            List<DetalleBodega> listaUbicaciones = (List<DetalleBodega>)Session["listaUbicaciones"];
            Entidades.Articulo articulo = (Entidades.Articulo)Session["articulo"];
            articulo.detalleUbicacionBodega = listaUbicaciones;
            
            detalleBodegaServicio.actualizarBodega(listaUbicaciones, articulo.idArticulo, (String)Session["nombreCompleto"]);
            cargarDatosTblArticuloes();
        }


        protected void rpDetalleUbicaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfIdArticulo = e.Item.FindControl("hfIdUbicacionesArticulo") as HiddenField;

                Entidades.Articulo articulo = new Entidades.Articulo();
                articulo.idArticulo = Convert.ToInt32(hfIdArticulo.Value);
                articulo.detalleUbicacionBodega = detalleBodegaServicio.getDetalleBodegas(articulo);

                Repeater rpUbicaciones = e.Item.FindControl("rpUbicaciones") as Repeater;
                rpUbicaciones.DataSource = articulo.detalleUbicacionBodega;
                rpUbicaciones.DataBind();

                LinkButton btnEditar = e.Item.FindControl("btnEditar") as LinkButton;
                LinkButton btnnar = e.Item.FindControl("btnnar") as LinkButton;
                
            }
        }
        #endregion        
    }
}