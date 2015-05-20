using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BellatorTabernae.Pages.Shared
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                LoggedIn.Visible = true;
                LoggedInAs.Text = String.Concat("Inloggad som: ", GetUsername());
            }
        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session["SiteMsg"] = String.Format("Du är inte längre inloggad som {0}!", GetUsername());
            FormsAuthentication.SignOut();
            Response.RedirectToRoute("Default");
        }

        protected string GetUsername()
        {
            return Service.GetUser(int.Parse(Context.User.Identity.Name)).Username;
        }
    }
}