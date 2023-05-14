using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class AssocMngReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            if (!string.IsNullOrWhiteSpace(name.Text) &&
               (!string.IsNullOrWhiteSpace(username.Text)) && (!string.IsNullOrWhiteSpace(password.Text))
               && name.Text.Length < 21 && username.Text.Length < 21 && password.Text.Length < 21 )
            {

                string mngName = name.Text;
                string user = username.Text;
                string pass = password.Text;
                Boolean flag = false;

                conn.Open();
                SqlCommand users = new SqlCommand("select * from SystemUser", conn);
                SqlDataReader usersInfo = users.ExecuteReader();
                while (usersInfo.Read())
                {
                    string user2 = usersInfo.GetString(0);

                    if (user == user2)
                    {
                        Response.Write("username already taken");
                        flag = true;
                    }
                }
                conn.Close();

                if (!flag)
                {
                    SqlCommand addMng = new SqlCommand("addAssociationManager", conn);
                    addMng.CommandType = CommandType.StoredProcedure;
                    addMng.Parameters.Add(new SqlParameter("@name", mngName));
                    addMng.Parameters.Add(new SqlParameter("@username", user));
                    addMng.Parameters.Add(new SqlParameter("@password", pass));

                    conn.Open();
                    addMng.ExecuteNonQuery();
                    conn.Close();

                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Write("please enter valid values and make sure length is less than 20");

            }
        }
    }
}