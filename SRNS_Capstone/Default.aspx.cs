using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers;
using SQLiteDataHelpers.Objects;

namespace SRNS_Capstone
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var aClass = new SQLiteDataHelpers.DBConnector();

            //txtPassword.Text = aClass.InsertUser(new User()).ToString();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if( validateUser(txtUsername.Text, txtPassword.Text))
            {
                //Store Session Values
            }
            
            Response.Redirect("~/Home.aspx");
        }

        protected bool validateUser(string username, string password)
        {
            bool valid = false;



            return valid;
        }
    }
}