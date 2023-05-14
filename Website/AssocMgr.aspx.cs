using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class AssocMgr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addMatch_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr); 
            try
            {
                String hostClub = hostClubName.Text;
                String guestClub = guestClubName.Text;
                DateTime start = DateTime.Parse(addStartTime.Text);
                DateTime end = DateTime.Parse(addEndTime.Text);


                SqlCommand addMatch = new SqlCommand("addNewMatchCorrect", conn);
                addMatch.CommandType = CommandType.StoredProcedure;
                addMatch.Parameters.Add(new SqlParameter("@hostClubName", hostClub));
                addMatch.Parameters.Add(new SqlParameter("@guestClubName", guestClub));
                addMatch.Parameters.Add(new SqlParameter("@startTime", start));
                addMatch.Parameters.Add(new SqlParameter("@endTime", end));

               

                if (!DateTime.TryParse(addEndTime.Text, out end) || !DateTime.TryParse(addStartTime.Text, out start) || string.IsNullOrEmpty(hostClub) || string.IsNullOrEmpty(guestClub)|| string.IsNullOrWhiteSpace(hostClub)||string.IsNullOrWhiteSpace(guestClub) )
                {
                   
                    Response.Write("Invalid input for add Match");
            
                }
                else
                {
                    //I will add a parameter to determine success in the addNewMatch query?
                    conn.Open();
                    SqlCommand clubs = new SqlCommand("select * from Club", conn);
                    SqlDataReader clubsInfo = clubs.ExecuteReader();
                    bool flag1 = false;
                    bool flag2 = false;
                    while (clubsInfo.Read())
                    {
                        
                        string test = clubsInfo.GetString(1);
                        //string guestClub2 = clubsInfo.GetString(1);
                        if (test == hostClub)
                        {
                            flag1 = true;
                            
                        }
                        if (guestClub == test)
                        {
                            flag2 = true;
                            
                        }
                    }
                    clubsInfo.Close();

                  
                    if (flag1 && flag2)
                    {

                        addMatch.ExecuteNonQuery();
                        //Response.Write("Match added successfully");
                    }
                    else
                    {
                        Response.Write("Input Club name(s) do not exist within the db in SAM");
                    }

                    conn.Close();




                }
            }
            catch
            {
                Response.Write("Invalid date for add Match");

            }

           



        }
        protected void deleteMatch_Click(object sender, EventArgs e) 
        {
            
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);   
            try
            {
                String hostClub = hostClubNameD.Text;
                String guestClub = guestClubNameD.Text;
                DateTime start = DateTime.Parse(addStartTimeD.Text);
                DateTime end = DateTime.Parse(addEndTimeD.Text);


                SqlCommand deleteMatch = new SqlCommand("deleteMatchWithTime", conn);
                deleteMatch.CommandType = CommandType.StoredProcedure;
                deleteMatch.Parameters.Add(new SqlParameter("@hostClubName", hostClub));
                deleteMatch.Parameters.Add(new SqlParameter("@guestClubName", guestClub));
                deleteMatch.Parameters.Add(new SqlParameter("@startTime", start));
                deleteMatch.Parameters.Add(new SqlParameter("@endTime", end));



                if (!DateTime.TryParse(addEndTimeD.Text, out end)|| !DateTime.TryParse(addStartTimeD.Text, out start) || string.IsNullOrEmpty(hostClub) || string.IsNullOrEmpty(guestClub))
                {
                    conn.Open();
                    Response.Write("Invalid input for delete Match");
                    conn.Close();



                }
                else
                {
                    try
                    {
                        conn.Open();
                        deleteMatch.ExecuteNonQuery();
                        conn.Close();

                    }
                    catch
                    {
                        Response.Write("Match does not exist for delete Match");

                    }


                }
            }
            catch
            {
                Response.Write("Invalid date for Delete Match");

            }

        }

        protected void viewUpcomingMatches_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);   

            
            SqlCommand viewUpcomingMatches = new SqlCommand("select * from upcomingMatches", conn);
            SqlDataAdapter da = new SqlDataAdapter(viewUpcomingMatches);
            DataTable dt = new DataTable();

            da.Fill(dt);
            gridViewUpcomingMatches.DataSource = dt;
            gridViewUpcomingMatches.DataBind();

          
 
        }

        protected void viewPlayedMatches_Click(object sender, EventArgs e)
        {


            
            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);   

            SqlCommand viewPlayedMatches = new SqlCommand("select * from pastMatches", conn);
            SqlDataAdapter da = new SqlDataAdapter(viewPlayedMatches);
            DataTable dt = new DataTable();

            da.Fill(dt);
            gridViewPlayedMatches.DataSource = dt;
            gridViewPlayedMatches.DataBind();
            


        }

        protected void viewClubsNeverPlayed_Click(object sender, EventArgs e)
        {


            string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
            SqlConnection conn = new SqlConnection(connStr);   

            SqlCommand viewClubsNeverPlayedMatches = new SqlCommand("select * from clubsNeverMatched", conn);
            SqlDataAdapter da = new SqlDataAdapter(viewClubsNeverPlayedMatches);
            DataTable dt = new DataTable();

            da.Fill(dt);
            gridViewClubsNeverPlayed.DataSource = dt;
            gridViewClubsNeverPlayed.DataBind();

        }
    }
}