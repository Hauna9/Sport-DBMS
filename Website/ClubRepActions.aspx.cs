using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class ClubRepActions : System.Web.UI.Page
    {
        string club_name;

        protected void Page_Load(object sender, EventArgs e)
        {
           // string username = Session["username"].ToString();

        }

        protected void Club_info_Click(object sender, EventArgs e)
        {

            string username = Session["username"].ToString();

            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand clubInfoProc = new SqlCommand("clubRepHelp", conn);
            clubInfoProc.CommandType = CommandType.StoredProcedure;
            clubInfoProc.Parameters.Add(new SqlParameter("@username", username));

            SqlParameter c_name = clubInfoProc.Parameters.Add("@c_name", SqlDbType.VarChar, 20);
            SqlParameter c_loc = clubInfoProc.Parameters.Add("@c_loc", SqlDbType.VarChar, 20);

            c_name.Direction = ParameterDirection.Output;
            c_loc.Direction = ParameterDirection.Output;

            conn.Open();
            clubInfoProc.ExecuteNonQuery();
            conn.Close();

            c_info_name.Text=c_name.Value.ToString();
            c_info_loc.Text=c_loc.Value.ToString();   

            club_name= c_name.Value.ToString(); 

        }

        

        protected void View_matches_Click(object sender, EventArgs e)
        {
            string username = Session["username"].ToString();

            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand clubInfoProc = new SqlCommand("clubRepHelp", conn);
            clubInfoProc.CommandType = CommandType.StoredProcedure;
            clubInfoProc.Parameters.Add(new SqlParameter("@username", username));

            SqlParameter c_name = clubInfoProc.Parameters.Add("@c_name", SqlDbType.VarChar, 20);
            SqlParameter c_loc = clubInfoProc.Parameters.Add("@c_loc", SqlDbType.VarChar, 20);

            c_name.Direction = ParameterDirection.Output;
            c_loc.Direction = ParameterDirection.Output;
            conn.Open();
            clubInfoProc.ExecuteNonQuery();
            

            club_name = c_name.Value.ToString();

           
            SqlCommand matchesInfo = new SqlCommand("upcomingMatchesOfClub_proc", conn);
            matchesInfo.CommandType = CommandType.StoredProcedure;
            matchesInfo.Parameters.Add(new SqlParameter("@club_name", club_name));

            SqlDataAdapter da = new SqlDataAdapter(matchesInfo);
            DataTable dt=new DataTable();
            da.Fill(dt);
            GridView1.DataSource= dt;
            GridView1.DataBind();


           // clubInfoProc.ExecuteNonQuery();
            matchesInfo.ExecuteNonQuery();
            conn.Close();




        }

        protected void View_Stadium_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date_txt = DateTime.Parse(Date_st_v.Text);
                string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand viewAvailableStadium = new SqlCommand("viewAvailableStadiumsOn_proc2", conn);
                viewAvailableStadium.CommandType = CommandType.StoredProcedure;

                viewAvailableStadium.Parameters.Add(new SqlParameter("@date", date_txt));

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(viewAvailableStadium);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView2.DataSource = dt;
                GridView2.DataBind();

                viewAvailableStadium.ExecuteNonQuery();

                conn.Close();
            }
            catch
            {
                Response.Write("please enter valid date");
            }
        }

        protected void Send_Request_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(std_name.Text))
                { string username = Session["username"].ToString();
                    string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();

                    SqlConnection conn = new SqlConnection(connStr);

                    SqlCommand clubInfoProc = new SqlCommand("clubRepHelp", conn);
                    clubInfoProc.CommandType = CommandType.StoredProcedure;
                    clubInfoProc.Parameters.Add(new SqlParameter("@username", username));

                    SqlParameter c_name = clubInfoProc.Parameters.Add("@c_name", SqlDbType.VarChar, 20);
                    SqlParameter c_loc = clubInfoProc.Parameters.Add("@c_loc", SqlDbType.VarChar, 20);
                    //create query to check if stadium exist or not if exist send otherwise write not valid.
                    c_name.Direction = ParameterDirection.Output;
                    c_loc.Direction = ParameterDirection.Output;
                    conn.Open();
                    clubInfoProc.ExecuteNonQuery();
                    string club_name = c_name.Value.ToString();
                    string stadium_name = std_name.Text;
                    DateTime start_time = DateTime.Parse(st_time.Text);

                    SqlCommand checkAddStd = new SqlCommand("validateStadium", conn);
                    checkAddStd.CommandType = CommandType.StoredProcedure;
                    checkAddStd.Parameters.Add(new SqlParameter("@name", stadium_name));
                    SqlParameter res = checkAddStd.Parameters.Add("@res", SqlDbType.Int);
                    res.Direction = ParameterDirection.Output;


                    SqlCommand sendReqProc = new SqlCommand("addHostRequest2", conn);
                    sendReqProc.CommandType = CommandType.StoredProcedure;
                    sendReqProc.Parameters.Add(new SqlParameter("@clubName", club_name));
                    sendReqProc.Parameters.Add(new SqlParameter("@stadiumName", stadium_name));
                    sendReqProc.Parameters.Add(new SqlParameter("@startTime", start_time));
                    SqlParameter out1 = sendReqProc.Parameters.Add("@out", SqlDbType.Int);
                    out1.Direction = ParameterDirection.Output;
                    checkAddStd.ExecuteNonQuery();

                    if (res.Value.Equals(0))
                    {
                        Response.Write("Stadium does not exist");
                    }

                    else
                    {
                       
                        sendReqProc.ExecuteNonQuery();
                        if (out1.Value.Equals(1))
                        Response.Write("Stadium is already booked (Request not sent");
                        if(out1.Value.Equals(0))
                        Response.Write("Stadium is already unavailable (Request not sent)");
                        if (out1.Value.Equals(10))
                        Response.Write("Request has been sent successfully"); 

                    }
                    conn.Close();
                }
                else
                {
                    Response.Write("please enter value");
                }
                
            }
            
            catch(Exception error)
            {
                Response.Write(error.Message);
                Response.Write("please enter valid data");
            }
        }
    }
}