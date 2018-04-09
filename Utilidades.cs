﻿using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
namespace SIBO
{
    internal static class Utilidades
    {
        public static Dictionary<int, string> roles { get; set; }
        public static List<System.Web.UI.WebControls.HyperLink> aplicaciones { get; set; }
        public static string path = "\\\\issac\\AppFiles\\CTL\\";
        public static string logs_path = path + "logs";


        public static void MensajeBox(string mensaje, string tipo, ClientScriptManager ClientScript, System.Web.UI.Page pag)
        { //Tipo de mensaje puede ser alert,confirm o popup
            string message = mensaje;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append(tipo);
            sb.Append("('");
            sb.Append(message);
            sb.Append("');");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(pag.GetType(), "alert", sb.ToString());
        }

        public static void ErrorBitacora(string e, string m)
        {
            Directory.SetCurrentDirectory(logs_path);
            FileStream archivo = new FileStream("Error.log", FileMode.Open, FileAccess.Write);
            archivo.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(archivo);
            sw.WriteLine("");
            sw.WriteLine("**************************");
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine("**************************");
            sw.WriteLine("(######)   " + m + "   (######)");
            sw.WriteLine(e);
            sw.Close();
            archivo.Close();
        }

        public static void SetLogDirectory()
        {
            if (Directory.Exists(logs_path))
            {
                Directory.SetCurrentDirectory(logs_path);
            }
            else
            {
                Directory.CreateDirectory(logs_path);
                Directory.SetCurrentDirectory(logs_path);
            }
            FileStream archivo = new FileStream("Error.log", FileMode.OpenOrCreate);
            archivo.Close();

        }

        public static bool IsPDF(string txt)
        {
            return txt.Substring(txt.Length - 4, 4).Equals(".pdf") || txt.Substring(txt.Length - 4, 4).Equals(".PDF");

        }

        public static int SaveFile(HttpPostedFile file, int year, String nombreArchivo, String carpeta)
        {
            // Path del directorio donde vamos a guardar el archivo
            String pathToCheck = path + year;

            //Verificamos si existe el directorio, sino existe se crea
            if (!Directory.Exists(pathToCheck))
            {
                Directory.CreateDirectory(pathToCheck);
            }

            // Path de la carpeta dentro del directorio en donde se va a guardar el archivo
            pathToCheck = pathToCheck + "\\" + carpeta;

            // Verificamos si ya existe la carpeta dentro del directorio, si no existe entonces se crea
            if (!Directory.Exists(pathToCheck))
            {
                Directory.CreateDirectory(pathToCheck);
            }

            // Crear la ruta y el nombre del archivo para comprobar si hay duplicados.
            pathToCheck = pathToCheck + "\\" + nombreArchivo;

            // Compruebe si ya existe un archivo con el
            // mismo nombre que el archivo que desea cargar .       
            if ((System.IO.File.Exists(pathToCheck)))
            {
                return 1; //El archivo existe
            }
            else
            {
                // Llame al método SaveAs para guardar el archivo
                // guardado en el directorio especificado.
                try
                {
                    file.SaveAs(pathToCheck);
                }
                catch { }
                return 0;
            }
        }

        // Retorna 1 si logro sobreescribir, 0 si no habia archivo, por lo que no sobreecribio, solo creo un nuevo archivo.
        public static int OverWriteFile(System.Web.UI.WebControls.FileUpload FileUpload1, int year, string TextConsecutivo, string nuevoTextConsecutivo)
        {
            // Path del directorio donde vamos a guardar el archivo
            string pathToCheck = path + " " + year;

            //Verificamos si existe el directorio, sino existe se crea
            if (!Directory.Exists(pathToCheck))
            {
                Directory.CreateDirectory(pathToCheck);
            }

            // Crear la ruta y el nombre del archivo para comprobar si hay duplicados.
            string olFile = pathToCheck + "\\" + TextConsecutivo + ".PDF";
            string newFile = pathToCheck + "\\" + nuevoTextConsecutivo + ".PDF";

            // Compruebe si ya existe un archivo con el
            // mismo nombre que el archivo que desea cargar .       
            if ((System.IO.File.Exists(olFile)))
            {
                System.IO.File.Move(olFile, newFile);
                return 1; //El archivo existe
            }
            else
            {
                // Llame al método SaveAs para guardar el archivo.
                FileUpload1.SaveAs(newFile);
                return 0;
            }
        }

        public static bool existeArchivo(string nombre_archivo, int year)
        {
            bool existe = false;

            string pathToCheck = path + year;

            if (Directory.Exists(pathToCheck))
            {
                pathToCheck = pathToCheck + "\\" + nombre_archivo + ".PDF";

                if ((System.IO.File.Exists(pathToCheck)))
                {
                    existe = true;
                }
            }

            return existe;
        }

        public static List<string> buscarEnLista(string prefixText, List<string> li)
        {
            List<string> result = new List<string>();
            foreach (string s in li)
            {
                if (s.Contains(prefixText) || s.Contains(prefixText.ToUpper()) || s.Contains(prefixText.ToLower()))
                {
                    result.Add(s);
                }
            }

            return result;

        }

        /* Lucia Sandoval
     * 26/10/15
     * se escoge el menu segun el rol del usuario, si el usuario no tiene ningun permiso en esta aplicacion lo redirecciona al login
     */
        public static void escogerMenu(Page page, int[] rolesPermitidos)
        {
            int rol = page.Session["rol"] == null ? 0 : Int32.Parse(page.Session["rol"].ToString());//Int32.Parse(page.Session["rol"].ToString());
            if (page.Session["nombreCompleto"] == null)
            {
                page.Session.RemoveAll();
                page.Session.Abandon();
                page.Session.Clear();
                String url = page.ResolveUrl("~/login.aspx");
                page.Response.Redirect(url);
            }
            else if (rol == 2)
            {
                if (rolesPermitidos.Contains(rol))
                {
                    page.Master.FindControl("MenuAdministrador").Visible = true;
                }
                else
                {
                    page.Session.RemoveAll();
                    page.Session.Abandon();
                    page.Session.Clear();
                    String url = page.ResolveUrl("~/login.aspx");
                    page.Response.Redirect(url);
                }
            }
            else
            {
                page.Session.RemoveAll();
                page.Session.Abandon();
                page.Session.Clear();
                String url = page.ResolveUrl("~/login.aspx");
                page.Response.Redirect(url);
            }
        }

        /*Leonardo Carrion
         15/07/2016
         Metodo que devuelve un vector con los permisos de Ver, Nuevo, Editar y Eliminar respectivamente
         segun el nombre de la pagina que ingrese el usuario*/
        public static Boolean[] permisosPorPagina(Page page, String nombrePagina)
        {
            Boolean[] permisos = { true, true, true, true };

            ////esta lista tiene los permisos del usuario, se llena cuando se loguea.
            //List<Usuario_Rol_Pagina_Permiso> listaPermisos = (List<Usuario_Rol_Pagina_Permiso>)page.Session["listaPermisosPagina"];

            ////se recorre la lista con los permisos por pagina y se guarda cuales permisos tiene el usario para esa pantalla especifica
            //foreach (Usuario_Rol_Pagina_Permiso usuarioRolPaginaPerniso in listaPermisos)
            //{
            //    if (usuarioRolPaginaPerniso.pagina.nombrePagina.Equals(nombrePagina))
            //    {
            //        if (usuarioRolPaginaPerniso.permiso.nombrePermiso.Equals("Ver"))
            //        {
            //            permisos[0] = true;
            //        }
            //        else
            //        {
            //            if (usuarioRolPaginaPerniso.permiso.nombrePermiso.Equals("Nuevo"))
            //            {
            //                permisos[1] = true;
            //            }
            //            else
            //            {
            //                if (usuarioRolPaginaPerniso.permiso.nombrePermiso.Equals("Editar"))
            //                {
            //                    permisos[2] = true;
            //                }
            //                else
            //                {
            //                    if (usuarioRolPaginaPerniso.permiso.nombrePermiso.Equals("Eliminar"))
            //                    {
            //                        permisos[3] = true;
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}

            return permisos;

        }

        /*
         * Jonathan Fonseca Vallejos
         * 30/may/2016
         * Efecto: envía un correo con la información indicada en el parámetro 
         * Requiere: la información necesaria para enviar el correo
         * Modifica: -
         * Devuelve: -
         */
        public static Boolean enviarCorreo(Dictionary<String, String> informacionCorreo)
        //public static Boolean enviarCorreo(Dictionary<String, String> informacionCorreo)
        {
            // Código obtenido en http://oscarsotorrio.com/post/2011/01/22/Envio-de-correo-en-NET-con-CSharp.aspx

            /*-------------------------MENSAJE DE CORREO ----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            // Obtenemos las direcciones de correo de los destinatarios desde el diccionario informaciónCorreo
            String destinatarios = informacionCorreo["destinatarios"];
            String[] listaDestinatarios = destinatarios.Split(';');

            foreach (String destinatario in listaDestinatarios)
            {
                try
                {
                    //Direccion de correo electronico a la que queremos enviar el mensaje
                    if (destinatario.Trim() != "")
                        mmsg.To.Add(destinatario); //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario
                }
                catch { }
            }

            // Asunto
            mmsg.Subject = informacionCorreo["asunto"];
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            String conCopia = informacionCorreo["conCopia"];
            String[] listaConCopia = conCopia.Split(';');
            if (conCopia.Trim() != "")
            {
                //Direccion de correo electronico que queremos que reciba una copia del mensaje
                //envia una copia del correo
                //mmsg.Bcc.Add(conCopia);

                //adjunta como copia a "X"
                foreach (String conCopiaA in listaConCopia)
                {
                    try
                    {
                        if (conCopiaA.Trim() != "")
                        {
                            mmsg.CC.Add(conCopiaA);
                        }
                    }
                    catch { }
                }
            }

            String conCopiaOculta = informacionCorreo["conCopiaOculta"];
            String[] listaConCopiaOculta = conCopiaOculta.Split(';');
            if (conCopiaOculta.Trim() != "")
            {
                foreach (String conCopiaOcultaA in listaConCopiaOculta)
                {
                    try
                    {
                        //Direccion de correo electronico que queremos que reciba una copia del mensaje oculto
                        //envia una copia del correo
                        if (conCopiaOcultaA.Trim() != "")
                        {
                            mmsg.Bcc.Add(conCopiaOcultaA);
                        }
                    }
                    catch { }
                }
            }

            //Cuerpo del Mensaje
            mmsg.Body = informacionCorreo["cuerpo"];
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;

            //Correo electronico desde la que enviamos el mensaje
            String remitente = informacionCorreo["remitente"].Split(';')[0];
            mmsg.From = new System.Net.Mail.MailAddress(remitente);

            // Prioridad del mensaje
            mmsg.Priority = MailPriority.High;

            // Si queremos enviar un archivo adjunto
            // mmsg.Attachments.Add(new Attachment(@"G:\LANAMME\Viaticos locales\24022014_Reporte viaticos.pdf"));

            String rutasArchivos = informacionCorreo["archivos"];
            String[] listaRutasArchivos = rutasArchivos.Split(';');

            foreach (String rutaArchivo in listaRutasArchivos)
            {
                //Direccion de correo electronico a la que queremos enviar el mensaje
                if (rutaArchivo.Trim() != "")
                    mmsg.Attachments.Add(new Attachment(rutaArchivo));
            }

            /*---------------------- FIN MENSAJE DE CORREO --------------------*/

            /*-------------------------CLIENTE DE CORREO ----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials = new System.Net.NetworkCredential("consejotecnico2016@gmail.com", "lanamme2016");
            //cliente.Credentials = new System.Net.NetworkCredential("laboratorios.lanamme@ucr.ac.cr", "14bs.l4n4mm3");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            cliente.Port = 587;
            cliente.EnableSsl = true;

            cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";
            //cliente.Host = "smtp.ucr.ac.cr"; //Para UCR "smtp.ucr.ac.cr";

            /*----------------------FIN CLIENTE DE CORREO -------------------*/

            /*-------------------------ENVIO DE CORREO ----------------------*/

            Boolean enviado = true;

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
                mmsg.Dispose();
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                enviado = false;
            }

            /*---------------------- FIN ENVIO DE CORREO --------------------*/

            return enviado;
        }

    }

}