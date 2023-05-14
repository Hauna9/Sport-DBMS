using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void login(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
           
            //in the class
            string user = username.Text;
            string pass = password.Text;
            Session["username"] = user;

           // type.

           SqlCommand loginValidationproc = new SqlCommand("loginvalidation", conn);
            loginValidationproc.CommandType = CommandType.StoredProcedure;
            loginValidationproc.Parameters.Add(new SqlParameter("@username", user));
            loginValidationproc.Parameters.Add(new SqlParameter("@password", pass));

            SqlParameter type = loginValidationproc.Parameters.Add("@type", SqlDbType.VarChar,20);

            type.Direction = ParameterDirection.Output;

            conn.Open();
            loginValidationproc.ExecuteNonQuery();
            conn.Close();


          

            if (type.Value.Equals( "AM"))
            {
                Response.Redirect("AssocMgr.aspx");
            }
            else
            {
                if (type.Value.Equals("CR"))
                {
                    //Response.Write("Club Rep");
               
                   // ClubRepActions Cr_direct= new ClubRepActions();
                    
                     Response.Redirect("ClubRepActions.aspx");
                     //Cr_direct.Pass_user(user.ToString());
                    
                }
                //do not forget to redirect and session
                else
                {
                    if (type.Value.Equals("SM"))
                    {
                        Response.Redirect("StdMng.aspx");//do not forget to redirect
                    }
                    else
                    {
                        if (type.Value.Equals("Fan"))
                        {
                            Response.Redirect("Fan.aspx");
                        }

                        else
                        {
                            if (type.Value.Equals("WP"))
                            {
                                Response.Write(" wrong pass");
                            }
                            else
                            {
                                if (type.Value.Equals("WU"))
                                {
                                    Response.Write(" wrong user");

                                }
                                else
                                {
                                    if (type.Value.Equals("Admin"))
                                    {
                                        Response.Redirect("SystemAdminActions.aspx");
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void registerType(object sender, EventArgs e)
        {
            string type = Type.SelectedItem.ToString();

            if (type == "Sports Association Manager")
            {
                Response.Redirect("AssocMngReg.aspx");
            }
            else if (type == "Club Representative")
            {
                Response.Redirect("ClubRepReg.aspx");
            }
            else if (type == "Stadium Manager")
            {
                Response.Redirect("StdMngReg.aspx");
            }
            else if (type == "Fan")
            {
                Response.Redirect("FanReg.aspx");
            }

        }
    }
}

            