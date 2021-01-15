using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public class StoreDepartmentService : IStoreDepartment
    {

        #region variable
        public static string Xpath = "//NewDataSet//Table1";
        #endregion

        #region Cunstructor
        MySqlConnection conn = new MySqlConnection();
        public StoreDepartmentService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        #endregion


        /// <summary>
        /// Get DepartMent List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<List<StoreDepartmentModel>> GetStoreDepartmentList(int tenantID, int UserID)
        {
            DataTable schemaTable = new DataTable();
            MySqlCommand cmd = null;
            List<StoreDepartmentModel> departmentMasters = new List<StoreDepartmentModel>();
            try
            {

                if (conn != null && conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            using (conn)
            {
                cmd = new MySqlCommand("SP_GetDepartmentList", conn);
                 cmd.Parameters.AddWithValue("@Tenant_Id", tenantID);
                 cmd.Parameters.AddWithValue("@User_Id", UserID);
                  cmd.Connection = conn;


                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        schemaTable.Load(reader);
                        if (schemaTable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in schemaTable.Rows)
                            {
                                    StoreDepartmentModel obj = new StoreDepartmentModel()
                                {
                                    DepartmentID = dr["DepartmentID"] == DBNull.Value ? 0 :  Convert.ToInt32(dr["DepartmentID"]),
                                    DepartmentName = dr["DepartmentName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                                    IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"])
                                };

                                    departmentMasters.Add(obj);
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

            return departmentMasters;
           
        }

        /// <summary>
        /// Get Department By Search
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public List<StoreDepartmentModel> GetStoreDepartmentBySearch(int tenantID, string departmentName)
        {
            DataSet ds = new DataSet();
            List<StoreDepartmentModel> departmentMastersList = new List<StoreDepartmentModel>();

            try
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreDepartmentBySearch", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tenant_Id", tenantID);
                cmd.Parameters.AddWithValue("@_DepartmentName", departmentName);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreDepartmentModel department = new StoreDepartmentModel();
                        {
                            department.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"]);
                            department.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);

                            department.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        }
                        departmentMastersList.Add(department);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return departmentMastersList;
        }


        /// <summary>
        /// Get Function By DepartmentID
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<StoreFunctionModel> GetStoreFunctionByDepartment(int departmentID, int tenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreFunctionModel> FunctionMasters = new List<StoreFunctionModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getFunctionByDepartmentId", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Department_ID", departmentID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreFunctionModel function = new StoreFunctionModel();
                        function.FunctionID = Convert.ToInt32(ds.Tables[0].Rows[i]["FunctionID"]);
                        function.FuncationName = Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]);
                        FunctionMasters.Add(function);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return FunctionMasters;
        }


        /// <summary>
        /// Get Function By multiple DepartmentIDs
        /// </summary>
        /// <param name="DepartmentIds"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<StoreFunctionModel> GetStoreFunctionbyMultiDepartment(string DepartmentIds, int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreFunctionModel> funcationMasters = new List<StoreFunctionModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getFunctionByMultipleDepartmentId", conn); 
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Department_ID", string.IsNullOrEmpty(DepartmentIds) ? "": DepartmentIds.TrimEnd(','));
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreFunctionModel function = new StoreFunctionModel();
                        function.FunctionID = Convert.ToInt32(ds.Tables[0].Rows[i]["FunctionID"]);
                        function.FuncationName = Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]);
                        funcationMasters.Add(function);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return funcationMasters;
        }


        /// <summary>
        /// Search function by department ID and Name
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="SearchText"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<StoreFunctionModel> SearchStoreFunctionByDepartment(int departmentID, string SearchText,int tenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreFunctionModel> funcationMasters = new List<StoreFunctionModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_SearchFunctionByDepartmentId", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Department_ID", departmentID);
                cmd1.Parameters.AddWithValue("@FuncText", string.IsNullOrEmpty(SearchText) ? "" : SearchText.ToLower());
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreFunctionModel function = new StoreFunctionModel();
                        function.FunctionID = Convert.ToInt32(ds.Tables[0].Rows[i]["FunctionID"]);
                        function.FuncationName = Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]);
                        function.DepartmentID = departmentID;
                        function.TenantID = tenantID;
                        funcationMasters.Add(function);
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

                if(ds!=null)
                {
                    ds.Dispose();
                }
            }
            return funcationMasters;
        }


        /// <summary>
        /// Add Department
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="tenantID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public int AddStoreDepartment(string departmentName, int tenantID, int createdBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddDepartment", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Department_Name", departmentName);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@Created_By", createdBy);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteScalar());

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

            return success;
        }

        /// <summary>
        /// Add function
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="functionName"></param>
        /// <param name="tenantID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public int AddStorefunction(int departmentID, string functionName, int tenantID, int createdBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddFunction", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Department_ID", departmentID);
                cmd.Parameters.AddWithValue("@Function_Name", functionName);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@Created_By", createdBy);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteScalar());

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

            return success;
        }

        /// <summary>
        /// Delete department Brand Mapping
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="DepartmentBrandMappingID"></param>
        /// <returns></returns>
        public int DeleteDepartmentMapping(int tenantID, int DepartmentBrandMappingID)
        {

            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteDepartmentBrandMapping", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_tenantID", tenantID);
                cmd.Parameters.AddWithValue("@_DeptBrandMappingID", DepartmentBrandMappingID);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception )
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

            return success;
        }

        /// <summary>
        /// Update Department Mapping
        /// </summary>
        /// <param name="updateDepartmentModel"></param>
        /// <returns></returns>
        public int UpdateDepartmentMapping(CreateStoreDepartmentModel updateDepartmentModel)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateDepartmentMapping", conn);
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@_DepartmentBrandID", updateDepartmentModel.DepartmentBrandID);
                cmd.Parameters.AddWithValue("@_BrandID", Convert.ToInt32(updateDepartmentModel.BrandID)); 
                cmd.Parameters.AddWithValue("@_StoreID", Convert.ToInt32(updateDepartmentModel.StoreID)); 
                cmd.Parameters.AddWithValue("@_DepartmentID", updateDepartmentModel.DepartmentID);
                cmd.Parameters.AddWithValue("@_FunctionID", updateDepartmentModel.FunctionID);
                cmd.Parameters.AddWithValue("@_Status", Convert.ToInt16(updateDepartmentModel.Status));
                cmd.Parameters.AddWithValue("@_TenantID", updateDepartmentModel.TenantID);
                cmd.Parameters.AddWithValue("@_CreatedBy", updateDepartmentModel.CreatedBy);


                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return result;
        }

        /// <summary>
        /// Get list of Stores bY brandIDs
        /// </summary>
        /// <param name="BrandIDs"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<StoreCodeModel> getStoreByBrandID(string BrandIDs, int tenantID)
        {
            List<StoreCodeModel> storeMaster = new List<StoreCodeModel>();
            DataSet ds = new DataSet();


            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreDetailsByBrandID", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tenantID", tenantID);
                cmd.Parameters.AddWithValue("@BrandIds", string.IsNullOrEmpty(BrandIDs) ? "" : BrandIDs.TrimEnd(','));
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {

                    StoreCodeModel store = new StoreCodeModel();

                    storeMaster = ds.Tables[0].AsEnumerable().Select(r => new StoreCodeModel()
                    {
                        StoreID = Convert.ToInt32(r.Field<object>("StoreID")),
                        BrandID = Convert.ToInt32(r.Field<object>("BrandID")),
                        StoreName = r.Field<object>("StoreName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("StoreName")),
                        StoreCode = r.Field<object>("StoreCode") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("StoreCode"))

                    }).ToList();


                }
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return storeMaster;
        }

        /// <summary>
        /// Create Department
        /// </summary>
        /// <param name="createDepartmentModel"></param>
        /// <returns></returns>
        public int CreateDepartment(CreateStoreDepartmentModel createDepartmentModel)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_CreateDepartment", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Brand_ID", string.IsNullOrEmpty(createDepartmentModel.BrandID)? "" : createDepartmentModel.BrandID.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_StoreID", string.IsNullOrEmpty(createDepartmentModel.StoreID) ? "" : createDepartmentModel.StoreID.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_DepartmentID", createDepartmentModel.DepartmentID);
                cmd.Parameters.AddWithValue("@_FunctionID", createDepartmentModel.FunctionID);
                cmd.Parameters.AddWithValue("@_Status", Convert.ToInt16(createDepartmentModel.Status));
                cmd.Parameters.AddWithValue("@_TenantID", createDepartmentModel.TenantID);
                cmd.Parameters.AddWithValue("@_UserID", createDepartmentModel.CreatedBy);


                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (Exception )
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

            return result;
        }

        /// <summary>
        /// Get Department Mapping Listing
        /// <param name="TenantID"></param>
        /// </summary>
        /// <returns></returns>
        public List<DepartmentListingModel> GetBrandDepartmentMappingList(int TenantID)
        {
            List<DepartmentListingModel> objDeptLst = new List<DepartmentListingModel>();

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetDepartmentMappingList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_tenantId", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objDeptLst = ds.Tables[0].AsEnumerable().Select(r => new DepartmentListingModel()
                        {
                            DepartmentBrandMappingID = Convert.ToInt32(r.Field<object>("DepartmentBrandMappingID")),
                            BrandID = Convert.ToInt32(r.Field<object>("BrandID")),
                            BrandName = r.Field<object>("BrandName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("BrandName")),

                            StoreID = Convert.ToInt32(r.Field<object>("StoreID")),
                            StoreCode = r.Field<object>("StoreCode") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("StoreCode")),

                            DepartmentID = Convert.ToInt32(r.Field<object>("DepartmentID")),
                            DepartmentName = r.Field<object>("DepartmentName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("DepartmentName")),

                            FunctionID = Convert.ToInt32(r.Field<object>("FunctionID")),
                            FunctionName = r.Field<object>("FuncationName") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("FuncationName")),

                            Status = r.Field<object>("IsActive") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("IsActive")),
                            CreatedBy = r.Field<object>("CreatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedBy")),
                            CreatedDate = r.Field<object>("CreatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("CreatedDate")),
                            ModifiedBy = r.Field<object>("UpdatedBy") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedBy")),
                            ModifiedDate = r.Field<object>("UpdatedDate") == System.DBNull.Value ? string.Empty : Convert.ToString(r.Field<object>("UpdatedDate")),
                        }).ToList();
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ds != null) ds.Dispose(); conn.Close();
            }
            return objDeptLst;

        }

        /// <summary>
        /// Bulk Upload Department
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public List<string> DepartmentBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
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
                        MySqlCommand cmd = new MySqlCommand("SP_BulkUploadDepartmentMaster", conn);

                        cmd.Parameters.AddWithValue("@_xml_content", xmlDoc.InnerXml);
                        cmd.Parameters.AddWithValue("@_node", Xpath);
                        cmd.Parameters.AddWithValue("@_createdBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@_tenantID", TenantID);

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
    }
}
