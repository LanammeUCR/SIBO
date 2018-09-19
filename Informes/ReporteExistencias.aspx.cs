using System;
using System.Web.UI;

namespace SIBO.Informes
{
    public partial class ReporteExistencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ReportViewer1.ServerReport.Refresh();
            if (!Page.IsPostBack)
            {
               
            }  
        }
    }
}