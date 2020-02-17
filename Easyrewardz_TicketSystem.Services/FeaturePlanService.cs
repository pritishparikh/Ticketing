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
    }
}
