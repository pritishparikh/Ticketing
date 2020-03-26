using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class MasterServices : IMasterInterface
    {
        #region
        MySqlConnection conn = new MySqlConnection();
        public MasterServices(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion


        /// <summary>
        /// Get Channel Of Purchase List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<ChannelOfPurchase> GetChannelOfPurchaseList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<ChannelOfPurchase> objChannel = new List<ChannelOfPurchase>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetChannelOfPurchaseList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ChannelOfPurchase channel = new ChannelOfPurchase();
                        channel.ChannelOfPurchaseID = Convert.ToInt32(ds.Tables[0].Rows[i]["ChannelOfPurchaseID"]);
                        channel.TenantID = Convert.ToInt32(ds.Tables[0].Rows[i]["TenantID"]);
                        channel.NameOfChannel = Convert.ToString(ds.Tables[0].Rows[i]["NameOfChannel"]);
                        channel.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        objChannel.Add(channel);
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
            return objChannel;
        }

        /// <summary>
        /// Get DepartMent List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<DepartmentMaster> GetDepartmentList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<DepartmentMaster> departmentMasters = new List<DepartmentMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetDepartmentList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DepartmentMaster department = new DepartmentMaster();
                        department.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"]);
                        department.DepartmentName = Convert.ToString(ds.Tables[0].Rows[i]["DepartmentName"]);
                        department.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        departmentMasters.Add(department);
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
            return departmentMasters;
        }
        /// <summary>
        /// Get Function By DepartmentID
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <returns></returns>
        public List<FuncationMaster> GetFunctionByDepartment(int DepartmentID, int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<FuncationMaster> funcationMasters = new List<FuncationMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getFunctionByDepartmentId", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Department_ID", DepartmentID);
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FuncationMaster function = new FuncationMaster();
                        function.FunctionID = Convert.ToInt32(ds.Tables[0].Rows[i]["FunctionID"]);
                        function.FuncationName = Convert.ToString(ds.Tables[0].Rows[i]["FuncationName"]);
                        funcationMasters.Add(function);
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
            return funcationMasters;
        }

        /// <summary>
        /// Get Payment List
        /// </summary>
        /// <returns></returns>
        public List<PaymentMode> GetPaymentMode()
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<PaymentMode> paymentModes = new List<PaymentMode>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getPaymentModeMaster", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PaymentMode paymentMode = new PaymentMode();
                        paymentMode.PaymentModeID = Convert.ToInt32(ds.Tables[0].Rows[i]["PaymentModeID"]);
                        paymentMode.PaymentModename = Convert.ToString(ds.Tables[0].Rows[i]["PaymentModename"]);
                        paymentModes.Add(paymentMode);
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

            return paymentModes;
        }

        /// <summary>
        /// Ticket Source Master
        /// </summary>
        /// <returns></returns>
        public List<TicketSourceMaster> GetTicketSources()
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<TicketSourceMaster> ticketSourceMasters = new List<TicketSourceMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getTicketSourceMaster", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TicketSourceMaster ticketSource = new TicketSourceMaster();
                        ticketSource.TicketSourceId = Convert.ToInt32(ds.Tables[0].Rows[i]["TicketSourceId"]);
                        ticketSource.TicketSourceName = Convert.ToString(ds.Tables[0].Rows[i]["TicketSourceName"]);
                        ticketSourceMasters.Add(ticketSource);
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

            return ticketSourceMasters;
        }

        /// <summary>
        /// Get State List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<StateMaster> GetStateList()
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StateMaster> stateMaster  = new List<StateMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStateList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StateMaster state = new StateMaster();
                        state.StateID = Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"]);
                        state.CountryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CountryID"]);
                        state.StateName = Convert.ToString(ds.Tables[0].Rows[i]["StateName"]);
                        state.StateCode = Convert.ToInt32(ds.Tables[0].Rows[i]["StateCode"]);
                        stateMaster.Add(state);
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
            return stateMaster;
        }

        #region SMTP Information 

        public SMTPDetails GetSMTPDetails(int TenantID)
        {
            DataSet ds = new DataSet();
            SMTPDetails sMTPDetails = new SMTPDetails();

            try
            {
                MySqlCommand cmd = new MySqlCommand();

                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_getSMTPDetails", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    sMTPDetails.EnableSsl = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnabledSSL"]);
                    sMTPDetails.SMTPPort = Convert.ToString(ds.Tables[0].Rows[0]["SMTPPort"]);
                    sMTPDetails.FromEmailId = Convert.ToString(ds.Tables[0].Rows[0]["EmailUserID"]);
                    sMTPDetails.IsBodyHtml = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsBodyHtml"]);
                    sMTPDetails.Password = Convert.ToString(ds.Tables[0].Rows[0]["EmailPassword"]);
                    sMTPDetails.SMTPHost = Convert.ToString(ds.Tables[0].Rows[0]["SMTPHost"]);
                    sMTPDetails.SMTPServer = Convert.ToString(ds.Tables[0].Rows[0]["SMTPHost"]);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return sMTPDetails;
        }


        #endregion

        /// <summary>
        /// Get City List
        /// </summary>
        /// <returns></returns>
        public List<CityMaster> GetCitylist(int StateId)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CityMaster> cityMaster = new List<CityMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetCityList", conn);
                cmd1.Parameters.AddWithValue("@State_Id", StateId);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CityMaster city = new CityMaster();
                        city.StateID = Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"]);
                        city.CityID = Convert.ToInt32(ds.Tables[0].Rows[i]["CityID"]);
                        city.CityName = Convert.ToString(ds.Tables[0].Rows[i]["CityName"]);
                        city.CityCode = Convert.ToInt32(ds.Tables[0].Rows[i]["CityCode"]);
                        cityMaster.Add(city);
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
            return cityMaster;
        }

        /// <summary>
        /// Get Region List
        /// </summary>
        /// <returns></returns>
        public List<RegionMaster> GetRegionList()
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<RegionMaster> regionMaster = new List<RegionMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetRegionList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        RegionMaster region = new RegionMaster();
                        region.RegionID = Convert.ToInt32(ds.Tables[0].Rows[i]["RegionID"]);
                        region.PinCodeID = Convert.ToInt32(ds.Tables[0].Rows[i]["PinCodeID"]);
                        region.RegionName = Convert.ToString(ds.Tables[0].Rows[i]["RegionName"]);
                        regionMaster.Add(region);
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
            return regionMaster;
        }

        /// <summary>
        /// Get StoreType List
        /// </summary>
        /// <returns></returns>
        public List<StoreTypeMaster> GetStoreTypeList()
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<StoreTypeMaster> storeTypeMaster = new List<StoreTypeMaster>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetStoreTypeList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreTypeMaster storeType = new StoreTypeMaster();
                        storeType.StoreTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["StoreTypeID"]);
                        storeType.StoreTypeName = Convert.ToString(ds.Tables[0].Rows[i]["StoreTypeName"]);
                        storeTypeMaster.Add(storeType);
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
            return storeTypeMaster;
        }

        /// <summary>
        /// Add Department
        /// </summary>
        /// <returns></returns>
        public int AddDepartment(string DepartmentName, int TenantID, int CreatedBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddDepartment", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Department_Name", DepartmentName);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return success;
        }

        /// <summary>
        ///Add function
        /// </summary>
        /// <returns></returns>
        public int Addfunction(int DepartmentID, string FunctionName, int TenantID, int CreatedBy)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddFunction", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Department_ID", DepartmentID);
                cmd.Parameters.AddWithValue("@Function_Name", FunctionName);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                cmd.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return success;
        }

        public List<StoreTypeMaster> GetStoreNameWithsStoreCode(int TenantID)
        {
            DataSet ds = new DataSet();
            List<StoreTypeMaster> storeMaster = new List<StoreTypeMaster>();

            try
            {

                conn.Open();
                
                MySqlCommand cmd = new MySqlCommand("SP_GetStoreCodewithStoreName", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoreTypeMaster storeType = new StoreTypeMaster();
                        storeType.StoreTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["StoreID"]);
                        storeType.StoreTypeName = Convert.ToString(ds.Tables[0].Rows[i]["StoreNameWithCode"]);
                        storeMaster.Add(storeType);
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

        /// <summary>
        /// Get Language List
        /// </summary>
        /// <returns></returns>
        public List<LanguageModel> GetLanguageList(int TenantID)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<LanguageModel> languageModels = new List<LanguageModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetLanguageList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        LanguageModel languageModel = new LanguageModel();
                        languageModel.LanguageID = Convert.ToInt32(ds.Tables[0].Rows[i]["LanguageID"]);
                        languageModel.LanguageName = Convert.ToString(ds.Tables[0].Rows[i]["LanguageName"]);  
                        //languageModel.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        //brand.CreatedByName = Convert.ToString(ds.Tables[0].Rows[i]["dd"]);

                        languageModels.Add(languageModel);
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

            return languageModels;
        }

        /// <summary>
        /// Get CountryStateCity
        /// </summary>
        /// <returns></returns>
        public List<CommonModel> GetCountryStateCityList(int TenantID,string Pincode)
        {

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CommonModel> commonModels = new List<CommonModel>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                CommonModel commonModel = new CommonModel();
                MySqlCommand cmd1 = new MySqlCommand("SP_GetCountryStateCity", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@_PinCode", Pincode);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        commonModel.CityID = Convert.ToInt32(ds.Tables[0].Rows[i]["CityID"]);
                        commonModel.CityName = Convert.ToString(ds.Tables[0].Rows[i]["CityName"]);
                        commonModel.StateID = Convert.ToInt32(ds.Tables[1].Rows[i]["StateID"]);
                        commonModel.StateName = Convert.ToString(ds.Tables[1].Rows[i]["StateName"]);
                        commonModel.CountryID = Convert.ToInt32(ds.Tables[2].Rows[i]["CountryID"]);
                        commonModel.CountryName = Convert.ToString(ds.Tables[2].Rows[i]["CountryName"]);

                        commonModels.Add(commonModel);
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

            return commonModels;
        }

        /// <summary>
        /// Create Department
        /// </summary>
        /// <returns></returns>
        public int CreateDepartment(CreateDepartmentModel createDepartmentModel)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_CreateDepartment", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@BrandID", createDepartmentModel.BrandID);
                cmd.Parameters.AddWithValue("@StoreID", createDepartmentModel.StoreID);
                cmd.Parameters.AddWithValue("@DepartmentID", createDepartmentModel.DepartmentID);
                cmd.Parameters.AddWithValue("@FunctionID", createDepartmentModel.FunctionID);
                cmd.Parameters.AddWithValue("@Status", createDepartmentModel.Status);
                cmd.Parameters.AddWithValue("@TenantID", createDepartmentModel.TenantID);
                cmd.Parameters.AddWithValue("@UserID", createDepartmentModel.CreatedBy);


                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());

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

            return result;
        }
        /// <summary>
        /// GetLogedInEmail
        /// </summary>
        /// <returns></returns>
        public CustomGetEmailID GetLogedInEmail(int UserID, int TenantID)
        {
            DataSet ds = new DataSet();
            CustomGetEmailID customGetEmailID = new CustomGetEmailID(); 
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Sp_GetLogInEmailID", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@User_ID", UserID);
                cmd.Parameters.AddWithValue("@Tenant_ID", TenantID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        customGetEmailID.EmailID = ds.Tables[0].Rows[i]["EmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                        customGetEmailID.RoleID = ds.Tables[0].Rows[i]["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["RoleID"]);
                        customGetEmailID.RoleName = ds.Tables[0].Rows[i]["RoleName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["RoleName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                string messages = Convert.ToString(ex.InnerException);
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return customGetEmailID;
        }
    }
    }
