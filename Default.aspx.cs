using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIBO
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 2, 13 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
        }
    }
}