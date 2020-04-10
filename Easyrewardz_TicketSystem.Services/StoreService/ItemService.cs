using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public class ItemService : IItem
    {

        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

        /// <summary>
        /// Get Brand list for drop down 
        /// </summary>
        /// <param name="EncptToken"></param>
        /// <returns></returns>


        MySqlConnection conn = new MySqlConnection();

        public ItemService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        /// <summary>
        /// Bulk Upload Item
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public List<string> ItemBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            XmlDocument xmlDoc = new XmlDocument();
            DataSet Bulkds = new DataSet();
            List<string> csvLst = new List<string>();
            string SuccessFile = string.Empty; string ErrorFile = string.Empty;
            try
            {
                if (DataSetCSV != null && DataSetCSV.Tables.Count > 0)
                {
                    if (DataSetCSV.Tables[0] != null && DataSetCSV.Tables[0].Rows.Count > 0)
                    {

                        xmlDoc.LoadXml(DataSetCSV.GetXml());
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SP_ItemBulkUpload", conn);

                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                        cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);

                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter da = new MySqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
                        da.Fill(Bulkds);

                        if (Bulkds != null && Bulkds.Tables[0] != null && Bulkds.Tables[1] != null)
                        {

                            //for success file
                            SuccessFile = Bulkds.Tables[0].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[0]) : string.Empty;
                            csvLst.Add(SuccessFile);

                            //for error file
                            ErrorFile = Bulkds.Tables[1].Rows.Count > 0 ? CommonService.DataTableToCsv(Bulkds.Tables[1]) : string.Empty;
                            csvLst.Add(ErrorFile);


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
                if (DataSetCSV != null)
                {
                    DataSetCSV.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return csvLst;
        }

        /// <summary>
        /// Claim Category List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<ItemModel> GetItemList(int TenantId)
        {
            DataSet ds = new DataSet();
            List<ItemModel> listItemMapping = new List<ItemModel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetItemList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_Id", TenantId);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ItemModel ItemCategory = new ItemModel
                        {
                            ItemID = ds.Tables[0].Rows[i]["ItemID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ItemID"]),
                            BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]),
                            BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]),
                            ItemCode = ds.Tables[0].Rows[i]["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]),
                            ItemName = ds.Tables[0].Rows[i]["ItemName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]),
                            DepartmentName = ds.Tables[0].Rows[i]["DepartmentName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]),
                            ItemCategory = ds.Tables[0].Rows[i]["ItemCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemCategory"]),
                            ItemSubCategory = ds.Tables[0].Rows[i]["ItemSubCategory"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemSubCategory"]),
                            ItemGroup = ds.Tables[0].Rows[i]["ItemGroup"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ItemGroup"])
                        };
                        listItemMapping.Add(ItemCategory);
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
            }
            return listItemMapping;
        }

    }
}
