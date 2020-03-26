using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }

        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public NotificationService(IDistributedCache cache, TicketDBContext db)
        {
            Db = db;
            Cache = cache;
        }
        #endregion

        #region Custom Methods

        public NotificationModel GetNotification(int TenantID, int UserID)
        {
            List<TicketNotificationModel> ticketNotification = new List<TicketNotificationModel>();
            NotificationModel objNotiLst = new NotificationModel();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                 conn = Db.Connection;
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetNotifications", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantID", TenantID);
                cmd1.Parameters.AddWithValue("@_userID", UserID);


                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ticketNotification = ds.Tables[0].AsEnumerable().Select(r => new TicketNotificationModel()
                        {
                            TicketCount = Convert.ToInt32(r.Field<object>("TicketCount")),
                            NotificationMessage = r.Field<object>("ActionName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("ActionName")),
                            TicketIDs = r.Field<object>("TicketIDs") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("TicketIDs")),

                        }).ToList();

                        if(ticketNotification.Count > 0)
                        {
                            objNotiLst.NotiCount = ticketNotification.Select(x => x.TicketCount).Sum();
                            objNotiLst.TicketNotification = ticketNotification; 
                        }
                        

                    }

                }
            }
            catch (Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                
            }

            return objNotiLst;
        }


        public int ReadNotification(int TenantID, int UserID, int TicketID)
        {
            int updatecount = 0;
            try
            {
                conn = Db.Connection;
                MySqlCommand cmd = new MySqlCommand("SP_UpdateOnReadNotifications", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", TenantID);
                cmd.Parameters.AddWithValue("@_userID", UserID);
                cmd.Parameters.AddWithValue("@_ticketId", TicketID);

                cmd.CommandType = CommandType.StoredProcedure;
                updatecount = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                string message = Convert.ToString(ex.InnerException);
                throw ex;
            }

            return updatecount;
        }


  
        #endregion
    }
}
