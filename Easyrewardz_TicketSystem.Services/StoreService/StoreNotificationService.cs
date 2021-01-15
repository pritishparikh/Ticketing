using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreNotificationService: IStoreNotification
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public StoreNotificationService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// Get Notification
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ListStoreNotificationModels GetNotification(int tenantID, int userID)
        {
           List<StoreNotificationModel> ListstoreNotificationModel = new List<StoreNotificationModel>();

            ListStoreNotificationModels storeNotificationModels = new ListStoreNotificationModels();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreNotifications", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@User_ID", userID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreNotificationModel storeNotificationModel = new StoreNotificationModel();
                        storeNotificationModel.NotificationCount= ds.Tables[0].Rows[i]["NotificationCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["NotificationCount"]);
                        storeNotificationModel.AlertID = ds.Tables[0].Rows[i]["AlertID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AlertID"]);
                        storeNotificationModel.NotificationName= ds.Tables[0].Rows[i]["NotificationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NotificationName"]);
                        int alertID = Convert.ToInt32(ds.Tables[0].Rows[i]["AlertID"]);
                        storeNotificationModel.CustomTaskNotificationModels = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("AlertID")).
                       Equals(alertID)).Select(x => new CustomTaskNotificationModel()
                       {
                           NotificationID = Convert.ToInt32(x.Field<int>("NotificationID")),
                           AlertID = Convert.ToInt32(x.Field<int>("AlertID")),
                           NotificatonTypeID = Convert.ToInt32(x.Field<int>("NotificatonTypeID")),
                           NotificatonType = Convert.ToInt32(x.Field<int>("NotificatonType")),
                           NotificatonTypeName = x.Field<object>("NotificatonTypeName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("NotificatonTypeName"))
                       }).ToList();
                        ListstoreNotificationModel.Add(storeNotificationModel);
                        if (ListstoreNotificationModel.Count > 0)
                        {
                            storeNotificationModels.StoreNotificationModel = ListstoreNotificationModel;
                            storeNotificationModels.NotiCount = ListstoreNotificationModel.Select(x => x.NotificationCount).Sum();
                        }
                    }
                   


                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                conn.Close();
            }

            return storeNotificationModels;
        }

        /// <summary>
        /// Get Notification New
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<ListStoreNotificationModels> GetNotificationNew(int tenantID, int userID)
        {

            DataTable Table1 = new DataTable();
            DataTable Table2 = new DataTable();

            List<StoreNotificationModel> ListstoreNotificationModel = new List<StoreNotificationModel>();
            ListStoreNotificationModels storeNotificationModels = new ListStoreNotificationModels();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                using (conn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_GetStoreNotifications_New", conn))
                    {
                        cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                        cmd.Parameters.AddWithValue("@User_ID", userID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                Table1.Load(reader);
                                Table2.Load(reader);

                                if(Table1.Rows.Count > 0)
                                {
                                    foreach(DataRow dr in Table1.Rows)
                                    {
                                        StoreNotificationModel storeNotificationModel = new StoreNotificationModel();

                                        storeNotificationModel.NotificationCount = dr["NotificationCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NotificationCount"]);
                                        storeNotificationModel.AlertID = dr["AlertID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AlertID"]);
                                        storeNotificationModel.NotificationName = dr["NotificationName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["NotificationName"]);

                                        int alertID = Convert.ToInt32(dr["AlertID"]);

                                        if(Table2.Rows.Count > 0)
                                        {
                                            storeNotificationModel.CustomTaskNotificationModels= Table2.AsEnumerable()
                                               .Where(x => Convert.ToInt32(x.Field<object>("AlertID")).
                                               Equals(alertID)).Select(x => new CustomTaskNotificationModel()
                                               {
                                                   NotificationID = Convert.ToInt32(x.Field<object>("NotificationID")),
                                                   AlertID = Convert.ToInt32(x.Field<object>("AlertID")),
                                                   NotificatonTypeID = Convert.ToInt32(x.Field<object>("NotificatonTypeID")),
                                                   NotificatonType = Convert.ToInt32(x.Field<object>("NotificatonType")),
                                                   NotificatonTypeName = x.Field<object>("NotificatonTypeName") == DBNull.Value ? string.Empty :
                                                   Convert.ToString(x.Field<object>("NotificatonTypeName"))
                                               }).ToList();
                                        }

                                        ListstoreNotificationModel.Add(storeNotificationModel);
                                        if (ListstoreNotificationModel.Count > 0)
                                        {
                                            storeNotificationModels.StoreNotificationModel = ListstoreNotificationModel;
                                            storeNotificationModels.NotiCount = ListstoreNotificationModel.Select(x => x.NotificationCount).Sum();
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (Table1 != null)
                {
                    Table1.Dispose();
                }
                if (Table2 != null)
                {
                    Table2.Dispose();
                }
            }

            return storeNotificationModels;
        }
        


        /// <summary>
        /// Read Notification
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="NotificatonTypeID"></param>
        /// <param name="NotificatonType"></param>
        /// <returns></returns>
        public int ReadNotification(int tenantID, int userID, int NotificatonTypeID, int NotificatonType)
        {
            int updateCount = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ReadStoreNotifications", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);
                cmd.Parameters.AddWithValue("@NotificatonType_ID", NotificatonTypeID);
                cmd.Parameters.AddWithValue("@Notificaton_Type", NotificatonType);
                cmd.CommandType = CommandType.StoredProcedure;
                updateCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return updateCount;
        }

        /// <summary>
        ///Get Campaign Followup Notifications
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="FollowUpID"></param>
        /// <returns></returns>
        public async Task<List<CampaignFollowUpNotiModel>> GetCampaignFollowupNotifications(int TenantID, int UserID, int FollowUpID)
        {
            DataTable schemaTable = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            List<CampaignFollowUpNotiModel> NotiList = new List<CampaignFollowUpNotiModel>();
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                using (conn)
                {
                    cmd = new MySqlCommand("SP_GetCampaignFollowupNotifications", conn);
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                    cmd.Parameters.AddWithValue("@FollowUp_ID", FollowUpID);
                    cmd.Parameters.AddWithValue("@User_ID", UserID); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            schemaTable.Load(reader);
                            if (schemaTable!=null && schemaTable.Rows.Count > 0)
                            {
                                foreach (DataRow dr in schemaTable.Rows)
                                {
                                    CampaignFollowUpNotiModel obj = new CampaignFollowUpNotiModel()
                                    {
                                        FollowUpID = Convert.ToInt32(dr["FollowUpID"]),
                                        NotificationContent = dr["NotificationContent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["NotificationContent"]),
                                        CallRescheduleDate = dr["CallRescheduleDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CallRescheduleDate"]),

                                    };
                                        NotiList.Add(obj);
                                }
                            }

                            
                        }
                    }
                }



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (schemaTable != null)
                {
                    schemaTable.Dispose();
                }
            }

            return NotiList;
        }

    }
}
