using PRA.DBQuizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PRA.WEBFORME
{
    public partial class PrijavaPostojecegKorisnika : System.Web.UI.Page
    {
        private DBQuizardEntities _context = new DBQuizardEntities();
        public UserAcc user = new UserAcc();
        public string email;
        public string password;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["accout"] != null)
            {
                Response.Cookies["account"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        protected void btnPrijavi_Click(object sender, EventArgs e)
        {
             

            email = txtEmail.Text;
            password = txtLozinka.Text;

            Session["userEmail"] = email;
            var userUBazi = _context.UserAcc.SingleOrDefault(u => u.Email == email);

            if (email == userUBazi.Email && password == userUBazi.Pass)
            {
                HttpCookie kuki = new HttpCookie("account");
                kuki["ID"] = _context.UserAcc.FirstOrDefault(x => x.Email == email).IDUserAcc.ToString();
                kuki.Expires = DateTime.Now.AddSeconds(300); //povecati trajanje kuki-a
                Response.Cookies.Add(kuki);
                Response.Redirect("https://localhost:44305/Home/Index");
                errorSpan.Text = "";
            }
            else
            {
                errorSpan.Text = "Unijeli ste krive podatke";
            }


            
        }
    }
}