﻿using BellatorTabernae.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Routing;

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
            if (Session["SiteMsg"] != null)
            {
                Panel MsgPanel = (Panel)Master.FindControl("MsgPanel");
                MsgPanel.Visible = true;
                Literal SiteMsg = (Literal)Master.FindControl("SiteMsg");
                SiteMsg.Text = Session["SiteMsg"].ToString();
                Session["SiteMsg"] = null;
            }

            if (Context.User.Identity.IsAuthenticated)
            {
                NewUserPanel.Visible = false;
                LoginPanel.Visible = false;
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    int userID = Service.CheckLogin(Username.Text, Password.Text);
                    FormsAuthentication.SetAuthCookie(userID.ToString(), true);
                    Response.RedirectToRoute("Character");
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

        protected void NewUser_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("CreateUser");
        }
    }
}