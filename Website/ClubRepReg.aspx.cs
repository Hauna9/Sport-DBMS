using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.Configuration;

namespace M3
{
    public partial class ClubRepReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Club_Rep_Reg_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection connt = new SqlConnection(connStr);
            if (!string.IsNullOrWhiteSpace(CR_name.Text)&&
                (!string.IsNullOrWhiteSpace(CR_username.Text)) &&(!string.IsNullOrWhiteSpace(CR_pass.Text))
                &&(!string.IsNullOrWhiteSpace(CR_club.Text)) && CR_name.Text.Length<21 && CR_username.Text.Length < 21 && CR_pass.Text.Length < 21 && CR_club.Text.Length < 21)
            {
                

                string c_name = CR_club.Text;
                SqlCommand checkClubProc = new SqlCommand("validateAddDelClub", connt);
                checkClubProc.CommandType = CommandType.StoredProcedure;
                checkClubProc.Parameters.Add(new SqlParameter("@name", c_name));
                SqlParameter res = checkClubProc.Parameters.Add("@res", SqlDbType.Int);
                res.Direction = ParameterDirection.Output;

                string cr_user = CR_username.Text;
                SqlCommand checkUserProc = new SqlCommand("checkUsername", connt);
                checkUserProc.CommandType = CommandType.StoredProcedure;
                checkUserProc.Parameters.Add(new SqlParameter("@name", cr_user));
                SqlParameter res1 = checkUserProc.Parameters.Add("@res1", SqlDbType.Int);
                res1.Direction = ParameterDirection.Output;

                string cr_name = CR_name.Text;
                string cr_pass=CR_pass.Text;

                
                SqlCommand addRepReq = new SqlCommand("addRepresentative", connt);
                addRepReq.CommandType = CommandType.StoredProcedure;
                addRepReq.Parameters.Add(new SqlParameter("@name", cr_name));
                addRepReq.Parameters.Add(new SqlParameter("@club_name", c_name));
                addRepReq.Parameters.Add(new SqlParameter("@username", cr_user));
                addRepReq.Parameters.Add(new SqlParameter("@password", cr_pass));


                connt.Open();
                checkUserProc.ExecuteNonQuery();
                checkClubProc.ExecuteNonQuery();
                if (res1.Value.Equals(1))
                {
                    Response.Write("username already taken");
                }
                else
                {
                    if (res.Value.Equals(0)) 
                    {
                        Response.Write("club does not exist");
                    }
                    else
                    {
                        addRepReq.ExecuteNonQuery();
                        Response.Redirect("login.aspx");
                    }
                }
                connt.Close();
            }
            else
            {
                Response.Write("please enter valid values and make sure length is less than 20");
            }
        }
    }
}