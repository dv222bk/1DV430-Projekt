using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BellatorTabernae.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    int userID = Service.CheckLogin(Username.Text, Password.Text);
                    Session["User"] = userID;
                    Session["FingerPrint"] = Service.CreateFingerPrint(userID, Request, true);
                }
                catch (SqlException ex)
                {
                    Page.ModelState.AddModelError(String.Empty, ex.Message);
                }
                catch (ValidationException ex)
                {
                    Page.ModelState.AddModelError(String.Empty, ex.Message);
                }
                catch (ArgumentException ex)
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