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
    public partial class StdMngReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void StdMng_Reg_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection connt = new SqlConnection(connStr);
            if (!string.IsNullOrWhiteSpace(Mng_name.Text) &&
                (!string.IsNullOrWhiteSpace(Mng_user.Text)) && (!string.IsNullOrWhiteSpace(Mng_pass.Text))
                && (!string.IsNullOrWhiteSpace(St_name.Text)) && Mng_name.Text.Length < 21 && Mng_user.Text.Length < 21 && Mng_pass.Text.Length < 21 && St_name.Text.Length < 21)
            {


                string std_name = St_name.Text;
                SqlCommand checkStdProc = new SqlCommand("validateStadium", connt);
                checkStdProc.CommandType = CommandType.StoredProcedure;
                checkStdProc.Parameters.Add(new SqlParameter("@name", std_name));
                SqlParameter res = checkStdProc.Parameters.Add("@res", SqlDbType.Int);
                res.Direction = ParameterDirection.Output;

                string mng_user = Mng_user.Text;
                SqlCommand checkUserProc = new SqlCommand("checkUsername", connt);
                checkUserProc.CommandType = CommandType.StoredProcedure;
                checkUserProc.Parameters.Add(new SqlParameter("@name", mng_user));
                SqlParameter res1 = checkUserProc.Parameters.Add("@res1", SqlDbType.Int);
                res1.Direction = ParameterDirection.Output;

                string mng_name = Mng_name.Text;
                string mng_pass = Mng_pass.Text;


                SqlCommand addMngProc = new SqlCommand("addStadiumManager", connt);
                addMngProc.CommandType = CommandType.StoredProcedure;
                addMngProc.Parameters.Add(new SqlParameter("@name", mng_name));
                addMngProc.Parameters.Add(new SqlParameter("@stadiumName",std_name));
                addMngProc.Parameters.Add(new SqlParameter("@username", mng_user));
                addMngProc.Parameters.Add(new SqlParameter("@password", mng_pass));


                connt.Open();
                checkUserProc.ExecuteNonQuery();
                checkStdProc.ExecuteNonQuery();
                if (res1.Value.Equals(1))
                {
                    Response.Write("username already taken");
                }
                else
                {
                    if (res.Value.Equals(0))
                    {
                        Response.Write("Stadium does not exist");
                    }
                    else
                    {
                        addMngProc.ExecuteNonQuery();
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