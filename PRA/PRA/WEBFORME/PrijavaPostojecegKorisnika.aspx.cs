using PRA.DBQuizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PRA.WEBFORME
{
    public partial class PrijavaPostoječegKorisnika : System.Web.UI.Page
    {
        private DBQuizardEntities _context = new DBQuizardEntities();
        public UserAcc user = new UserAcc();
        public string email;
        public string password;
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnPrijavi_Click(object sender, EventArgs e)
        {
             

            email = txtEmail.Text;
            password = txtLozinka.Text;

            Session["userEmail"] = email;
            var userUBazi = _context.UserAcc.SingleOrDefault(u => u.Email == email);

            if (email == userUBazi.Email && password == userUBazi.Pass)
            {
                Response.Redirect("https://localhost:44305/Home/PrijavljeniKorisnik");
                errorSpan.Text = "";
            }
            else
            {
                errorSpan.Text = "Unijeli ste krive podatke";
            }


            
        }
    }
}