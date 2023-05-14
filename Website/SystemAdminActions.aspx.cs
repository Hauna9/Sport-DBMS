using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace M3
{
    public partial class SystemAdminActions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_C_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            if (!string.IsNullOrWhiteSpace(C_name_add.Text) && (!string.IsNullOrWhiteSpace(C_loc_add.Text))
                && C_name_add.Text.Length<21 && C_loc_add.Text.Length<21)
            {

                string C_name = C_name_add.Text;
                string C_loc = C_loc_add.Text;
                SqlCommand addClubProc = new SqlCommand("addClub", conn);
                addClubProc.CommandType = CommandType.StoredProcedure;
                addClubProc.Parameters.Add(new SqlParameter("@name", C_name));
                addClubProc.Parameters.Add(new SqlParameter("@location", C_loc));

                SqlCommand checkAddProc = new SqlCommand("validateAddDelClub", conn);
                checkAddProc.CommandType = CommandType.StoredProcedure;
                checkAddProc.Parameters.Add(new SqlParameter("@name", C_name));
                SqlParameter res = checkAddProc.Parameters.Add("@res", SqlDbType.Int);
                res.Direction = ParameterDirection.Output;

                conn.Open();
                checkAddProc.ExecuteNonQuery();
                if (res.Value.Equals(1))
                    Response.Write("Club already exists");
                else
                {
                    addClubProc.ExecuteNonQuery();
                    Response.Write("Club Added Successfully");
                }
                conn.Close();

            }
            else
                Response.Write("please enter a value and make sure its less than 20 characters");
        }

        protected void Del_C_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            if (!string.IsNullOrWhiteSpace(C_name_del.Text))
            {


                string C_name = C_name_del.Text;
                SqlCommand delClubProc = new SqlCommand("deleteClub", conn);
                delClubProc.CommandType = CommandType.StoredProcedure;
                delClubProc.Parameters.Add(new SqlParameter("@name", C_name));


                SqlCommand checkDelProc = new SqlCommand("validateAddDelClub", conn);
                checkDelProc.CommandType = CommandType.StoredProcedure;
                checkDelProc.Parameters.Add(new SqlParameter("@name", C_name));
                SqlParameter res = checkDelProc.Parameters.Add("@res", SqlDbType.Int);
                res.Direction = ParameterDirection.Output;

                conn.Open();
                checkDelProc.ExecuteNonQuery();
                if (res.Value.Equals(0))
                    Response.Write("Club does not exist");
                else
                {
                    delClubProc.ExecuteNonQuery();
                    Response.Write("Club deleted Successfully");
                }
                conn.Close();

                
            }
            else
                Response.Write("please enter a value");

        }

        protected void Add_St_Click(object sender, EventArgs e)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


            if (!string.IsNullOrWhiteSpace(St_name_add.Text)&&(!string.IsNullOrWhiteSpace(St_loc_add.Text)) &&(!string.IsNullOrWhiteSpace(St_cap_add.Text)
                && St_name_add.Text.Length < 21 && St_loc_add.Text.Length < 21))
        {
                try
                {
                    string St_name = St_name_add.Text;
                    string St_loc = St_loc_add.Text;
                    int St_cap = Int16.Parse(St_cap_add.Text);
            // might have a try and catch in case not a correct format
            SqlCommand addStadProc = new SqlCommand("addStadium", conn);
                    addStadProc.CommandType = CommandType.StoredProcedure;
                    addStadProc.Parameters.Add(new SqlParameter("@name", St_name));
                    addStadProc.Parameters.Add(new SqlParameter("@location", St_loc));
                    addStadProc.Parameters.Add(new SqlParameter("@capacity", St_cap));

                    SqlCommand checkAddStd = new SqlCommand("validateStadium", conn);
                    checkAddStd.CommandType = CommandType.StoredProcedure;
                    checkAddStd.Parameters.Add(new SqlParameter("@name", St_name));
                    SqlParameter res = checkAddStd.Parameters.Add("@res", SqlDbType.Int);
                    res.Direction = ParameterDirection.Output;


                    conn.Open();
                    checkAddStd.ExecuteNonQuery();
                    if (res.Value.Equals(1))
                    {
                        Response.Write("Stadium Already Exists");
                    }
                    else
                    {
                        addStadProc.ExecuteNonQuery();
                        Response.Write("Stadium Added Successfully");
                    }
                    conn.Close();
                }
                catch(Exception ex)
                {
                    Response.Write("inavalid");
                }
        }
            else
            {
                Response.Write("please enter a value and make sure its length is less than 20 characters");
            }



        }

        protected void Del_St_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            if (!string.IsNullOrWhiteSpace(St_name_del.Text))
            {
                string St_name = St_name_del.Text;
                SqlCommand delStadProc = new SqlCommand("deleteStadium", conn);
                delStadProc.CommandType = CommandType.StoredProcedure;
                delStadProc.Parameters.Add(new SqlParameter("@name", St_name));


                SqlCommand checkDelStd = new SqlCommand("validateStadium", conn);
                checkDelStd.CommandType = CommandType.StoredProcedure;
                checkDelStd.Parameters.Add(new SqlParameter("@name", St_name));
                SqlParameter res = checkDelStd.Parameters.Add("@res", SqlDbType.Int);
                res.Direction = ParameterDirection.Output;






                conn.Open();
                checkDelStd.ExecuteNonQuery();
                if (res.Value.Equals(0))
                {
                    Response.Write("Stadium does not exist");
                }
                else
                {
                    delStadProc.ExecuteNonQuery();
                    Response.Write("Stadium Deleted Successfully");
                }
                conn.Close();
            }
            else
                Response.Write("Please enter a value");
            


        }

        protected void Block_fan_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            if (!string.IsNullOrWhiteSpace(fan_id_blk.Text))

            {
                string nat_ID = fan_id_blk.Text;
                SqlCommand blockFanProc = new SqlCommand("blockFan", conn);
                blockFanProc.CommandType = CommandType.StoredProcedure;
                blockFanProc.Parameters.Add(new SqlParameter("@national_ID", nat_ID));


                SqlCommand validatefanProc = new SqlCommand("validatefan", conn);
                validatefanProc.CommandType = CommandType.StoredProcedure;
                validatefanProc.Parameters.Add(new SqlParameter("@nat_id", nat_ID));
                SqlParameter res = validatefanProc.Parameters.Add("@res", SqlDbType.Int);
                res.Direction = ParameterDirection.Output;

                conn.Open();
                validatefanProc.ExecuteNonQuery();

                if (res.Value.Equals(10))
                {
                    Response.Write("Fan Already blocked");
                }
                else
                {
                    if (res.Value.Equals(11))

                    {
                        blockFanProc.ExecuteNonQuery();
                        Response.Write("Fan blocked Successfully");
                    }
                    else
                    {
                        Response.Write("Fan does not exist in database");


                    }
                }
                
                conn.Close();

            }
            else
            {
                Response.Write("please enter a value");
            }
        }

        protected void Unblock_fan_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            if (!string.IsNullOrWhiteSpace(fan_id_unblk.Text))

            {
                string nat_ID = fan_id_unblk.Text;
                SqlCommand unblockFanProc = new SqlCommand("unblockFan", conn);
                unblockFanProc.CommandType = CommandType.StoredProcedure;
                unblockFanProc.Parameters.Add(new SqlParameter("@national_ID", nat_ID));


                SqlCommand validatefanProc = new SqlCommand("validatefan", conn);
                validatefanProc.CommandType = CommandType.StoredProcedure;
                validatefanProc.Parameters.Add(new SqlParameter("@nat_id", nat_ID));
                SqlParameter res = validatefanProc.Parameters.Add("@res", SqlDbType.Int);
                res.Direction = ParameterDirection.Output;

                conn.Open();

                validatefanProc.ExecuteNonQuery();

                if (res.Value.Equals(11))
                {
                    Response.Write("Fan Already unblocked");
                }
                else
                {
                    if (res.Value.Equals(10))

                    {
                        unblockFanProc.ExecuteNonQuery();
                        Response.Write("Fan unblocked Successfully");
                    }
                    else
                    {
                        Response.Write("Fan does not exist in database");


                    }
                }
                conn.Close();

            }
            else
            {
                Response.Write("please enter a value");
            }
        }
    }
}