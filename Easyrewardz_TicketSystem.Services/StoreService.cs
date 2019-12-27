using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreService : IStore
    {
        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public StoreService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion
        public List<StoreMaster> getStoreDetailByStorecodenPincode(string Storename, string Storecode, int Pincode)
        {
            List<StoreMaster> storeMaster = new List<StoreMaster>();
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("", conn);
                cmd1.Parameters.AddWithValue("@objEmailId", Storename);
                cmd1.Parameters.AddWithValue("@objCustomerPhoneNumber", Storecode);
                cmd1.Parameters.AddWithValue("@objCustomerPhoneNumber", Pincode);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreMaster store = new StoreMaster();
                        store.StoreCode = Convert.ToString(ds.Tables[0].Rows[i]["ToString"]);
                        store.StoreName = Convert.ToString(ds.Tables[0].Rows[i]["StoreName"]);
                        store.PincodeID = Convert.ToInt32(ds.Tables[0].Rows[i]["PincodeID"]);


                        storeMaster.Add(store);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return storeMaster;
        }

    }
}
