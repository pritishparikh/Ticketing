using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        /// <returns></returns>
        public List<StoreDepartmentModel> GetStoreDepartmentList(int tenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreDepartmentModel> departmentMasters = new List<StoreDepartmentModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetDepartmentList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreDepartmentModel department = new StoreDepartmentModel();
                        department.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"]);
                        department.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);
                        department.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        departmentMasters.Add(department);
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
            return departmentMasters;
        }
        /// <summary>
        /// Get Function By DepartmentID
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <returns></returns>
        public List<StoreFunctionModel> GetStoreFunctionByDepartment(int departmentID, int tenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreFunctionModel> funcationMasters = new List<StoreFunctionModel>();

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
            }
            return funcationMasters;
        }

        /// <summary>
        /// Add Department
        /// </summary>
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
        ///Add function
        /// </summary>
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
        /// <param name="TenantID"></param>
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
                cmd.Parameters.AddWithValue("@Store_ID", tenantID);
                cmd.Parameters.AddWithValue("@tenant_ID", DepartmentBrandMappingID);
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
        /// Update Department Mapping
        /// </summary>
        /// <returns></returns>
        public int UpdateDepartmentMapping(int TenantID, int DepartmentBrandID, int BrandID, int StoreID, int DepartmentID, int FunctionID, bool Status, int CreatedBy)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_UpdateDepartmentMapping", conn);
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@BrandID", DepartmentBrandID);
                cmd.Parameters.AddWithValue("@BrandID", BrandID);
                cmd.Parameters.AddWithValue("@StoreID", StoreID);
                cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                cmd.Parameters.AddWithValue("@FunctionID", FunctionID);
                cmd.Parameters.AddWithValue("@Status", Convert.ToInt16(Status));
                cmd.Parameters.AddWithValue("@TenantID", TenantID);
                cmd.Parameters.AddWithValue("@UserID", CreatedBy);


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
        /// <param name="TicketId">Id of the Ticket</param>
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
            catch (Exception ex)
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
        /// <returns></returns>
        public int CreateDepartment(CreateStoreDepartmentModel createDepartmentModel)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_CreateDepartment", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@_BrandID", string.IsNullOrEmpty(createDepartmentModel.BrandID)? "" : createDepartmentModel.BrandID.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_StoreID", string.IsNullOrEmpty(createDepartmentModel.StoreID) ? "" : createDepartmentModel.StoreID.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_DepartmentID", createDepartmentModel.DepartmentID);
                cmd.Parameters.AddWithValue("@_FunctionID", createDepartmentModel.FunctionID);
                cmd.Parameters.AddWithValue("@_Status", Convert.ToInt16(createDepartmentModel.Status));
                cmd.Parameters.AddWithValue("@_TenantID", createDepartmentModel.TenantID);
                cmd.Parameters.AddWithValue("@_UserID", createDepartmentModel.CreatedBy);


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
        /// Get DepartmentMapping Listing
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
    }
}
