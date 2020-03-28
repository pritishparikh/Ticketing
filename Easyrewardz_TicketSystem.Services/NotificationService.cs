using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{ 
    public class NotificationService : INotification
    {

        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public NotificationService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        #region Custom Methods

        public NotificationModel GetNotification(int tenantID, int userID)
        {
            List<TicketNotificationModel> ticketNoti = new List<TicketNotificationModel>();
            NotificationModel objNotiLst = new NotificationModel();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetNotifications", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd1.Parameters.AddWithValue("@_userID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ticketNoti = ds.Tables[0].AsEnumerable().Select(r => new TicketNotificationModel()
                        {
                            TicketCount = Convert.ToInt32(r.Field<object>("TicketCount")),
                            NotificationMessage = r.Field<object>("ActionName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ActionName")),
                            TicketIDs = r.Field<object>("TicketIDs") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TicketIDs")),
                            IsFollowUp = r.Field<object>("IsFollowUp") == System.DBNull.Value ? 0 : Convert.ToInt32(r.Field<object>("IsFollowUp"))

                        }).ToList();

                        if(ticketNoti.Count > 0)
                        {
                            objNotiLst.NotiCount = ticketNoti.Where(x => x.TicketCount > 0).Select(x=> x.TicketCount).Sum();
                            objNotiLst.TicketNotification = ticketNoti.Where(x => x.TicketCount > 0).ToList() ; 
                        }
                        

                    }

                }
            }
            catch (Exception )
            {
                
                throw;
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                conn.Close();
            }

            return objNotiLst;
        }


        public int ReadNotification(int tenantID, int userID, int ticketID, int IsFollowUp)
        {
            int updateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateOnReadNotifications", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_userID", userID);
                cmd.Parameters.AddWithValue("@_ticketId", ticketID);
                cmd.Parameters.AddWithValue("@_IsFollowUp", IsFollowUp);
                cmd.CommandType = CommandType.StoredProcedure;
                updateCount = cmd.ExecuteNonQuery();
            }
            catch(Exception)
            {
                
                throw;
            }

            return updateCount;
        }


  
        #endregion
    }
}
