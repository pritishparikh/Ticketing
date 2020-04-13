using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

            return updateCount;
        }
        #endregion

    }
}
