using PRA.DBQuizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PRA.WEBFORME
{
    public partial class DodajNepostojecegUsera : System.Web.UI.Page
    {
        private DBQuizardEntities _context = new DBQuizardEntities();
        public UserAcc user = new UserAcc();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            user.Email = txtEmail.Text;
            user.Pass = txtLozinka.Text;
            user.Username = txtNadimak.Text;
            _context.UserAcc.Add(user);
            _context.SaveChanges();
            Response.Redirect("https://localhost:44305/");
        }
    }
}