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
    public partial class Fan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewAllMatches_Click(object sender, EventArgs e)
        {
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
                SqlConnection conn = new SqlConnection(connStr);   // verify all input , check for success and redirect
                                                                   //try
                DateTime date1 = DateTime.Parse(viewMatches.Text);
              

                // CHANGE FROM CURRENT TIME STAMP TO ACTUALLY TAKE A VARIABLE
                SqlCommand viewAllMatches = new SqlCommand("SELECT * from dbo.availableMatches(@given_datetime)", conn);
                viewAllMatches.Parameters.Add(new SqlParameter("@given_datetime", date1));
                SqlDataAdapter da = new SqlDataAdapter(viewAllMatches);
                DataTable dt = new DataTable();

                da.Fill(dt);
                gridViewAllMatches.DataSource = dt;
                gridViewAllMatches.DataBind();
            }
            catch
            {
                Response.Write("Please input valid date for view available matches to purchase ticket");
            }
        }
        protected void purchaseTicket_Click(object sender, EventArgs e)
        {
           try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["M3"].ToString();
                
                SqlConnection conn = new SqlConnection(connStr);
                String nationalID = ID.Text;
                String hostClub = hostClubName.Text;
                String guestClub = guestClubName.Text;
                DateTime start = DateTime.Parse(startTime.Text);


                SqlCommand purchase = new SqlCommand("purchaseTicketWithOutput", conn);
                purchase.CommandType = CommandType.StoredProcedure;
                purchase.Parameters.Add(new SqlParameter("@fan_id", nationalID));
                purchase.Parameters.Add(new SqlParameter("@host_name", hostClub));
                purchase.Parameters.Add(new SqlParameter("@guest_name", guestClub));
                purchase.Parameters.Add(new SqlParameter("@start_time", start));

                SqlParameter purchaseFailed = purchase.Parameters.Add("@purchaseFailed", SqlDbType.Int);
                 purchaseFailed.Direction = ParameterDirection.Output;


            if ( !DateTime.TryParse(startTime.Text, out start) || string.IsNullOrEmpty(hostClub) || string.IsNullOrEmpty(guestClub)|| string.IsNullOrEmpty(nationalID))
                {
                    Response.Write("Invalid input for Ticket Purchase");
                  
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
                        if (hostClub == matchesInfo.GetString(0) && guestClub == matchesInfo.GetString(1))
                        {
                            // String test = start + "";
                            // DateTime startTimeString = DateTime.Parse(matchesInfo.GetString(2));
                            if (start == matchesInfo.GetSqlDateTime(2))
                            {
                                flag1 = true;
                                

                            }
                        }
                        
                    }
                matchesInfo.Close(); // we close the other readers in other funtions 

                    if (flag1)
                    {
                        try
                        {
                            purchase.ExecuteNonQuery();     //if purchased, then 0, if blocked 1, if ticket sold out 2
                        if (purchaseFailed.Value.ToString() == "0")
                        {
                            Response.Write("Purchase Success");
                            Page_Load(sender, e);
                        }
                        if (purchaseFailed.Value.ToString() == "1")
                            Response.Write("Purchase Failed: Fan Blocked");
                        if (purchaseFailed.Value.ToString() == "2") // might never come up
                            Response.Write("Purchase Failed: Tickets sold out");  


                    }
                        catch (Exception error)
                        {
                            Response.Write("problem with exec query" + error.Message);
                        }
                        
                           
                        
                    }
                    else
                    {
                        Response.Write("Match does not exist or didnt work idk why double check");
                    }

                    conn.Close();
                    




                }

            }
           catch
            {
                Response.Write("Please input valid date for ticket purchase");
            }

        }

       
    }
}