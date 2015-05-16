using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;

namespace BellatorTabernae.Pages
{
    public partial class CreateUser : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Create_User_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Service.CreateUser(Username.Text, Password.Text, Email.Text);
                    Session["SiteMsg"] = String.Format("Du är nu registrerad som {0} och kan nu logga in!", Username.Text);
                    Response.RedirectToRoute("Default");
                }
                catch (SqlException ex)
                {
                    Page.ModelState.AddModelError(String.Empty, ex.Message);
                }
                catch (ApplicationException ex)
                {
                    Page.ModelState.AddModelError(String.Empty, ex.Message);
                }
                catch
                {
                    Page.ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade.");
                }
            }
        }
    }
}