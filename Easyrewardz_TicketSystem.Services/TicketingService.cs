using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class TicketingService : ITicketing
    {

        MySqlConnection conn = new MySqlConnection();
        public TicketingService()
        {
            conn.ConnectionString = "Data Source = 13.67.69.216; port = 3306; Initial Catalog = Ticketing; User Id = brainvire; password = Logitech@123";
        }
        public List<TicketingDetails> GetTicketList(string TikcketTitle)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketingDetails> ticketing = new List<TicketingDetails>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getTitleSuggestions", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@TikcketTitle", TikcketTitle);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TicketingDetails ticketingDetails = new TicketingDetails();
                        ticketingDetails.Ticket_title = Convert.ToString(ds.Tables[0].Rows[i]["TikcketTitle"]);
                        ticketing.Add(ticketingDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ticketing;
        }
    }
}
