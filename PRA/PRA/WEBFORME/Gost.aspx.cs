using PRA.DBQuizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PRA.WEBFORME
{
    public partial class Gost : System.Web.UI.Page
    {
        private DBQuizardEntities _context = new DBQuizardEntities();
        public NewGuest user = new NewGuest();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnPrijavi_Click(object sender, EventArgs e)
        {
            user.Nickname = txtNadimak.Text;
            _context.NewGuest.Add(user);
            _context.SaveChanges();
            Response.Redirect("https://localhost:44305/Home/Index");

        }
    }
}