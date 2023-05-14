using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace M3
{
    public partial class StdMng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ViewStadiumInfo_Click(object sender, EventArgs e)
        {
            

            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);   
            try
            { 

             
                SqlCommand stadiumManagerInfo = new SqlCommand("SELECT * from dbo.StadiumManagerInfo(@SM_Username)", conn);
                stadiumManagerInfo.Parameters.Add(new SqlParameter("@SM_Username", Session["username"])); 

                /*if (Session["username"]==null)
                    Response.Write(" username null?");*/

                SqlDataAdapter da = new SqlDataAdapter(stadiumManagerInfo);
                DataTable dt = new DataTable();

                da.Fill(dt);
                gridViewStadiumInfo.DataSource = dt;
                gridViewStadiumInfo.DataBind();


            }
            catch
            {
                Response.Write("Error in viewing info about stadium");
            }


        }

        protected void ViewAllRequests_Click(object sender, EventArgs e)
        {


            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);   // verify all input , check for success and redirect
            try
            {

                SqlCommand allRequestsForStadiumManagerInfo = new SqlCommand("SELECT * from dbo.allRequestsForStadiumManager(@managerUser)", conn);
                allRequestsForStadiumManagerInfo.Parameters.Add(new SqlParameter("@managerUser", Session["username"]));
                /*if (Session["username"]==null)
                    Response.Write(" username null?");*/
                SqlDataAdapter da = new SqlDataAdapter(allRequestsForStadiumManagerInfo);
                DataTable dt = new DataTable();

                da.Fill(dt);
                gridViewAllRequests.DataSource = dt;
                gridViewAllRequests.DataBind();


            }
            catch
            {
                Response.Write("Error in viewing all requests of stadium manager info about stadium ig");
            }




        }



        protected void acceptRequest_Click(object sender, EventArgs e)
        {


            try {


                string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                String host = hostAccept.Text;
                String guest = guestAccept.Text;
                DateTime start = DateTime.Parse(startAccept.Text);

                SqlCommand acceptRequest = new SqlCommand("acceptRequestUnhandled", conn);
                acceptRequest.CommandType = CommandType.StoredProcedure;
                acceptRequest.Parameters.Add(new SqlParameter("@managerUser", Session["username"])); 
                acceptRequest.Parameters.Add(new SqlParameter("@hostName", host));
                acceptRequest.Parameters.Add(new SqlParameter("@guestName", guest));
                acceptRequest.Parameters.Add(new SqlParameter("@startTime", start));



                if (!DateTime.TryParse(startAccept.Text, out start) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(guest) || Session["username"] == null)
                {
                    Response.Write("Invalid input for Accept request");

                }
                else
                {
                    conn.Open();
                    SqlCommand match = new SqlCommand("select * from allMatches", conn);
                    SqlDataReader matchesInfo = match.ExecuteReader();
                    bool flag1 = false;

                    while (matchesInfo.Read())
                    {
                       
                        if (host == matchesInfo.GetString(0) && guest == matchesInfo.GetString(1))
                        {
                            String test = start + "";
                            if (start == matchesInfo.GetSqlDateTime(2))
                                flag1 = true;
                        }

                    }
                    matchesInfo.Close(); // we close the other readers in other funtions 

                    if (flag1)
                    {
            

                        try
                        {
                            acceptRequest.ExecuteNonQuery();

                        }
                        catch (Exception error)
                        {
                            Response.Write("problem with exec query of accept request procedure" + error.Message);
                        }



                    }
                    else
                    {
                        Response.Write("Match/Request does not exist");
                    }

                    conn.Close();




                }


            }
            catch { Response.Write("Please input valid date for accept request "); }

        }

        
    
    protected void rejectRequest_Click(object sender, EventArgs e)
    {
        //CREATE FUNCTION allPendingRequestsForStadiumManager
        // (@managerUser VARCHAR(20))
        //   RETURNS @query TABLE(
        //


        try
        {


            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String host = hostAccept.Text;
            String guest = guestAccept.Text;
            DateTime start = DateTime.Parse(startAccept.Text);

            SqlCommand rejectRequest = new SqlCommand("rejectRequestUnhandled", conn);
                rejectRequest.CommandType = CommandType.StoredProcedure;
                rejectRequest.Parameters.Add(new SqlParameter("@managerUser", Session["username"])); // Session["user"]
                rejectRequest.Parameters.Add(new SqlParameter("@hostName", host));
                rejectRequest.Parameters.Add(new SqlParameter("@guestName", guest));
                rejectRequest.Parameters.Add(new SqlParameter("@startTime", start));



            if (!DateTime.TryParse(startAccept.Text, out start) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(guest) || Session["username"] == null)
            {
                Response.Write("Invalid input for Reject request");

            }
            else
            {
                conn.Open();
                SqlCommand match = new SqlCommand("select * from allMatches", conn);
                SqlDataReader matchesInfo = match.ExecuteReader();
                bool flag1 = false;

                while (matchesInfo.Read())
                {
                    //string hostClub2 = clubsInfo.GetString(0);
                    //string guestClub2 = clubsInfo.GetString(1);
                    if (host == matchesInfo.GetString(0) && guest == matchesInfo.GetString(1))
                    {
                        String test = start + "";
                        // DateTime startTimeString = DateTime.Parse(matchesInfo.GetString(2));
                        if (start == matchesInfo.GetSqlDateTime(2))
                            flag1 = true;
                    }

                }
                matchesInfo.Close(); // we close the other readers in other funtions 

                if (flag1)
                {
                    try
                    {
                            rejectRequest.ExecuteNonQuery();

                    }
                    catch (Exception error)
                    {
                        Response.Write("problem with exec query of reject request procedure" + error.Message);
                    }



                }
                else
                {
                    Response.Write("Match/Request does not exist");
                }

                conn.Close();




            }


        }
        catch { Response.Write("Please input valid date for accept request "); }

    }


}    
             

        /*protected void gridViewPendingRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            String host  = gridViewPendingRequests.SelectedRow.Cells[0].Text;
            String guest = gridViewPendingRequests.SelectedRow.Cells[1].Text;
            DateTime start = DateTime.Parse(gridViewPendingRequests.SelectedRow.Cells[2].Text);
            Response.Write(host + "   " + guest + "   " + start);
        }*/
    
    
}