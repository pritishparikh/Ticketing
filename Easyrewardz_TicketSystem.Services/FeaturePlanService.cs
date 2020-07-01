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
    public class FeaturePlanService : IFeaturePlan   
    {
        #region
        MySqlConnection conn = new MySqlConnection();
        public FeaturePlanService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        /// <summary>
        /// GetFeaturePlanList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public FeaturePlanModel GetFeaturePlanList(int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();
            FeaturePlanModel featurePlanModel = new FeaturePlanModel();

            try
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlCommand cmd1 = new MySqlCommand("SP_GetFeatureList", conn);
                cmd1.CommandType = CommandType.StoredProcedure;

                da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0) //plans
                    {
                        featurePlanModel.Plans = ds.Tables[0].AsEnumerable().Select(r => new PlanModel()
                        {

                            PlanID = Convert.ToInt32(r.Field<object>("PlanID")),
                            PlanName = Convert.ToString(r.Field<object>("PlanName")),
                            MonthlyAmount = Convert.ToDecimal(r.Field<object>("MonthlyAmount")),
                            YearlyAmount = Convert.ToDecimal(r.Field<object>("YearlyAmount")),

                        }).ToList();
                    }

                    if (ds.Tables[1].Rows.Count > 0) //Features 
                    {
                        featurePlanModel.Features = ds.Tables[1].AsEnumerable().Select(r => new FeatureModel()
                        {
                            FeatureName = Convert.ToString(r.Field<object>("FeatureName")),
                            FeatureID = Convert.ToInt32(r.Field<object>("FeatureID")),
                            Tooltip = Convert.ToString(r.Field<object>("Tooltip"))

                        }).ToList();
                    }

                    if (ds.Tables[2].Rows.Count > 0) //planCheckeds 
                    {
                        featurePlanModel.PlanCheckeds = ds.Tables[2].AsEnumerable().Select(r => new PlanCheckedModel()
                        {
                            FeatureID = Convert.ToInt32(r.Field<object>("FeatureID")),
                            PlanName = Convert.ToString(r.Field<object>("PlanName")),
                            PlanID = Convert.ToInt32(r.Field<object>("PlanID")),
                            Checked = Convert.ToString(r.Field<object>("checked")),


                        }).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return featurePlanModel;
        }

        /// <summary>
        /// Add Feature
        /// </summary>
        /// <param name="objFeatures"></param>
        /// <returns></returns>
        public string AddFeature(FeaturesModel objFeatures)
        {
            int result = 0;
            string message = string.Empty;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddFeature", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Feature_ID", objFeatures.FeatureID);
                cmd.Parameters.AddWithValue("@Feature_Name", objFeatures.FeatureName);
                cmd.Parameters.AddWithValue("@Monthly_Price", objFeatures.MonthlyPrice);
                cmd.Parameters.AddWithValue("@Yearly_Price", objFeatures.YearlyPrice);
                cmd.Parameters.AddWithValue("@_Tooltip", objFeatures.Tooltip);
                cmd.Parameters.AddWithValue("@IsActive", objFeatures.IsActive);
                cmd.Parameters.AddWithValue("@User_ID", objFeatures.UserID);
                cmd.Parameters.AddWithValue("@Message","");
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());
                message = Convert.ToString(cmd.Parameters["@Message"].Value);
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

            return message;
        }

        /// <summary>
        /// Delete Feature
        /// </summary>
        /// <param name="objFeatures"></param>
        /// <returns></returns>
        public int DeleteFeature(int UserID,int FeatureID)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_DeleteFeature", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Feature_ID", FeatureID);               
                cmd.Parameters.AddWithValue("@User_ID", UserID);
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

        #region AddPlan

        public int AddPlan(PlanModel plan)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_AddPlan", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Tenant_ID", TenantId);
                cmd.Parameters.AddWithValue("@UserID", plan.CreatedBy);
                cmd.Parameters.AddWithValue("@Planname", plan.PlanName);
                cmd.Parameters.AddWithValue("@MonthlyPlanPrice", plan.MonthlyAmount);
                cmd.Parameters.AddWithValue("@YearlyPlanPrice", plan.YearlyAmount);
                cmd.Parameters.AddWithValue("@TotalUsers", plan.TotalUsers);
                cmd.Parameters.AddWithValue("@IsPublish", plan.IsPublished);
                cmd.Parameters.AddWithValue("@IsMostPopular", plan.IsMostPopular);
                cmd.Parameters.AddWithValue("@FeatureID", plan.FeatureID);
                cmd.Parameters.AddWithValue("@AddOnsID", plan.AddOnsID);


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
        #endregion

        #region Get_PlanOnEdit
        public List<PlanModel> GetPlanOnEdit(int TenantID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<PlanModel> planModels = new List<PlanModel>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_GetPlanOnEdit", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@Tenant_Id", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PlanModel model = new PlanModel();
                        model.PlanName = Convert.ToString(ds.Tables[0].Rows[i]["PlanName"]);
                        model.MonthlyAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["PlanMonthlyAmount"]);
                        model.YearlyAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["PlanYearlyAmount"]);
                        model.TotalUsers = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalNumberOfUser"]);
                        model.IsPublished = ds.Tables[0].Rows[i]["IsPublish"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IsPublish"]);
                        model.IsMostPopular = ds.Tables[0].Rows[i]["IsMostPopular"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IsMostPopular"]);

                        planModels.Add(model);
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
            return planModels;

        }
        #endregion
    }
}
