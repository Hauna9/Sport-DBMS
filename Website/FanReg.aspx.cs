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
    public partial class FanReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void f_reg_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection connt = new SqlConnection(connStr);
            try
            {
                DateTime bdate = DateTime.Parse(f_bd.Text);

                if (!string.IsNullOrWhiteSpace(f_name.Text) &&
               (!string.IsNullOrWhiteSpace(f_user.Text)) && (!string.IsNullOrWhiteSpace(f_pass.Text))
               && (!string.IsNullOrWhiteSpace(f_id.Text)) && (!string.IsNullOrWhiteSpace(f_num.Text)) && (!string.IsNullOrWhiteSpace(f_bd.Text)) && (!string.IsNullOrWhiteSpace(f_address.Text))
               && f_name.Text.Length<21 && f_user.Text.Length < 21 && f_pass.Text.Length < 21 && f_id.Text.Length < 21 && f_address.Text.Length < 21)
                {
                    string name = f_name.Text;
                    string pass = f_pass.Text;
                    string id = f_id.Text;
                    long num =Int64.Parse(f_num.Text);
                    string address = f_address.Text;
                    string user = f_user.Text;

                    SqlCommand checkUserProc = new SqlCommand("checkUsername", connt);
                    checkUserProc.CommandType = CommandType.StoredProcedure;
                    checkUserProc.Parameters.Add(new SqlParameter("@name", user));
                    SqlParameter res1 = checkUserProc.Parameters.Add("@res1", SqlDbType.Int);
                    res1.Direction = ParameterDirection.Output;

                   
                    SqlCommand validateFanProc = new SqlCommand("validateFanReg", connt);
                    validateFanProc.CommandType = CommandType.StoredProcedure;
                    validateFanProc.Parameters.Add(new SqlParameter("@nat_id", id));
                    validateFanProc.Parameters.Add(new SqlParameter("@phone", num));
                    validateFanProc.Parameters.Add(new SqlParameter("@b_date", bdate));
                    SqlParameter res2 = validateFanProc.Parameters.Add("@res", SqlDbType.Int);
                    res2.Direction = ParameterDirection.Output;

                    SqlCommand addFanProc = new SqlCommand("addFan", connt);
                    addFanProc.CommandType = CommandType.StoredProcedure;
                    addFanProc.Parameters.Add(new SqlParameter("@name", name));
                    addFanProc.Parameters.Add(new SqlParameter("@username", user));
                    addFanProc.Parameters.Add(new SqlParameter("@password", pass));
                    addFanProc.Parameters.Add(new SqlParameter("@nat_id", id));
                    addFanProc.Parameters.Add(new SqlParameter("@b_date", bdate));
                    addFanProc.Parameters.Add(new SqlParameter("@address", address));
                    addFanProc.Parameters.Add(new SqlParameter("@phone", num));


                    connt.Open();
                    checkUserProc.ExecuteNonQuery();
                    validateFanProc.ExecuteNonQuery();
                    if (res1.Value.Equals(1))
                    {
                        Response.Write("username already taken");
                    }
                    else if (res2.Value.Equals(3)) 
                    {
                        Response.Write("phone already taken");
                    }
                    else if (res2.Value.Equals(2)) {
                        Response.Write("national id already taken");
                    }
                    else if (res2.Value.Equals(1))
                    {
                        Response.Write("invalide date of birth");
                    }
                    else
                    {
                        addFanProc.ExecuteNonQuery() ;
                        Response.Redirect("login.aspx");
                    }

                    connt.Close();
                }
                else
                {
                    Response.Write("please enter valid values and make sure length is less than 20");
                }
            }
            catch
            {
                Response.Write("Enter valid data");
            }
        }
    }
}