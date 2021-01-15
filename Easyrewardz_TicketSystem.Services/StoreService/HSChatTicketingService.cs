using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Services
{
    public class HSChatTicketingService : IHSChatTicketing
    {
        MySqlConnection conn = new MySqlConnection();
        ChatbotBellHttpClientService APICall = null;

        #region Constructor
        public HSChatTicketingService(string _connectionString, ChatbotBellHttpClientService _APICall = null)
        {
            conn.ConnectionString = _connectionString;
            APICall = _APICall;
        }
        #endregion

        /// <summary>
        /// Add Chat Ticket Notes
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="tenantID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public int AddChatTicketNotes(int ticketID, string comment, int userID, int tenantID, string programCode)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_HSAddChatTicketNotes", conn)
                {
                    Connection = conn
                };
                cmd1.Parameters.AddWithValue("@ticket_ID", ticketID);
                cmd1.Parameters.AddWithValue("@_Comments", comment);
                cmd1.Parameters.AddWithValue("@User_ID", userID);
                cmd1.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@program_Code", programCode);
                cmd1.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd1.ExecuteNonQuery());

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
        ///  Create Chat Ticket
        /// </summary>
        /// <param name="createChatTickets"></param>
        /// <returns></returns>
        public int CreateChatTicket(CreateChatTickets createChatTickets)
        {
            int TicketID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_HSCreateChatTicket", conn)
                {
                    Connection = conn
                };
                cmd1.Parameters.AddWithValue("@Tenant_ID", createChatTickets.TenantID);
                cmd1.Parameters.AddWithValue("@User_ID", createChatTickets.CreatedBy);
                cmd1.Parameters.AddWithValue("@_Category", string.IsNullOrEmpty(createChatTickets.Category)? "" : createChatTickets.Category);
                cmd1.Parameters.AddWithValue("@_SubCategory", string.IsNullOrEmpty(createChatTickets.SubCategory) ? "" : createChatTickets.SubCategory);
                cmd1.Parameters.AddWithValue("@_IssueType", string.IsNullOrEmpty(createChatTickets.IssueType) ? "" : createChatTickets.IssueType);
                cmd1.Parameters.AddWithValue("@Customer_ID", createChatTickets.CustomerID);
                cmd1.Parameters.AddWithValue("@Mobile_Number", string.IsNullOrEmpty(createChatTickets.CustomerMobileNumber) ? "" : createChatTickets.CustomerMobileNumber);
                cmd1.Parameters.AddWithValue("@_Brand", createChatTickets.Brand);
                cmd1.Parameters.AddWithValue("@_Priority", string.IsNullOrEmpty(createChatTickets.Priority) ?"" : createChatTickets.Priority);
                cmd1.Parameters.AddWithValue("@Ticket_Title", string.IsNullOrEmpty(createChatTickets.TicketTitle) ? "" : createChatTickets.TicketTitle);
                cmd1.Parameters.AddWithValue("@Ticket_Description", string.IsNullOrEmpty(createChatTickets.TicketDescription) ? "" : createChatTickets.TicketDescription);
                cmd1.Parameters.AddWithValue("@Store_Code", string.IsNullOrEmpty(createChatTickets.StoreCode) ? "" : createChatTickets.StoreCode);
                cmd1.CommandType = CommandType.StoredProcedure;
                TicketID  = Convert.ToInt32(cmd1.ExecuteScalar());
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

            return TicketID;
        }

        /// <summary>
        /// Get CategoryList
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public List<Category> GetCategoryList(int tenantID, int userID, string programCode)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<Category> categoryList = new List<Category>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSGetChatCategory", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_Id", tenantID);
                cmd1.Parameters.AddWithValue("@user_ID", userID);
                cmd1.Parameters.AddWithValue("@program_Code", programCode);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Category category = new Category();
                        category.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        category.CategoryName = Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        categoryList.Add(category);
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
            return categoryList;
        }

        /// <summary>
        /// Get Chat Ticket history
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        public List<CustomTicketHistory> GetChatTickethistory(int ticketID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomTicketHistory> ListTicketHistory = new List<CustomTicketHistory>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSGetHistoryOfChatTicket", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Ticket_Id", ticketID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomTicketHistory TicketHistory = new CustomTicketHistory();
                        TicketHistory.Name = ds.Tables[0].Rows[i]["Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        TicketHistory.Action = ds.Tables[0].Rows[i]["Action"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Action"]);
                        TicketHistory.DateandTime = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        ListTicketHistory.Add(TicketHistory);
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
            return ListTicketHistory;
        }

        /// <summary>
        /// Get Chat Ticket Notes
        /// </summary>
        /// <param name="ticketID"></param>
        public List<ChatTicketNotes> GetChatticketNotes(int ticketID)
        {
            DataSet ds = new DataSet();
            List<ChatTicketNotes> lstChatTicketNotes = new List<ChatTicketNotes>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_HSGetChatNotesByTicketID", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@ticket_ID", ticketID);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ChatTicketNotes chatTicketNotes = new ChatTicketNotes();
                        chatTicketNotes.Name = ds.Tables[0].Rows[i]["Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        chatTicketNotes.Comment = ds.Tables[0].Rows[i]["Comment"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Comment"]);
                        chatTicketNotes.CommentDate= ds.Tables[0].Rows[i]["CommentDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CommentDate"]);
                        lstChatTicketNotes.Add(chatTicketNotes);
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
            return lstChatTicketNotes;
        }

        /// <summary>
        /// Get Chat Tickets By ID
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public async Task<GetChatTicketsByID> GetChatTicketsByID(int ticketID, int tenantID, int userMasterID, string programCode,string ClientAPIURL)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            GetChatTicketsByID customGetChatTickets = new GetChatTicketsByID();
            bool IsTimeOutBell = false;
            string CustomerLastMsgtime = string.Empty;
            try
            {
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }

            using (conn)
            {
                MySqlCommand cmd = new MySqlCommand("SP_HSGetChatTicketsByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("ticket_ID", ticketID);
                cmd.Parameters.AddWithValue("Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("UserMaster_ID", userMasterID);
                cmd.Parameters.AddWithValue("program_Code", programCode);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                        dt1.Load(reader);
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            customGetChatTickets.TicketID = dr["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TicketID"]);
                            customGetChatTickets.TicketStatus = dr["StatusID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StatusID"]);
                            customGetChatTickets.TicketTitle = dr["TicketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketTitle"]);
                            customGetChatTickets.TicketDescription = dr["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketDescription"]);
                            customGetChatTickets.CategoryID = dr["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CategoryID"]);
                            customGetChatTickets.Category = dr["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CategoryName"]);
                            customGetChatTickets.SubCategoryID = dr["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SubCategoryID"]);
                            customGetChatTickets.SubCategory = dr["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategoryName"]);
                            customGetChatTickets.IssueTypeID = dr["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IssueTypeID"]);
                            customGetChatTickets.IssueType = dr["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IssueTypeName"]);
                            customGetChatTickets.AssignTo = dr["Assignee"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Assignee"]);
                            customGetChatTickets.Priority = dr["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PriortyName"]);
                            customGetChatTickets.CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]);
                            customGetChatTickets.CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]);
                            customGetChatTickets.CustomerMobileNumber = dr["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerMobileNumber"]);
                            customGetChatTickets.Brand = dr["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Brand"]);
                            customGetChatTickets.CreatedDate = dr["CreatedAgo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedAgo"]);
                            customGetChatTickets.ChatID = dr["ChatID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ChatID"]);
                            customGetChatTickets.ChatEndDateTime = dr["ChatEndDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ChatEndDateTime"]);
                            customGetChatTickets.IsIconDisplay = dr["IsIconDisplay"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsIconDisplay"]);
                            customGetChatTickets.IsChatAllreadyActive = dr["IsChatAllreadyActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsChatAllreadyActive"]);
                            customGetChatTickets.ReInitiateChatDateTime = dr["ReInitiateChatDateTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ReInitiateChatDateTime"]);
                            IsTimeOutBell = dr["IsTimeOutBell"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsTimeOutBell"]);

                            if (dt1 != null)
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    customGetChatTickets.ChatTicketNote = dt1.AsEnumerable().Select(x => new ChatTicketNotes()
                                    {
                                        Name = x.Field<object>("Name") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Name")),
                                        Comment = x.Field<object>("Comment") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Comment")),
                                        CommentDate = x.Field<object>("CommentDate") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CommentDate"))
                                    }).ToList();
                                }
                            }
                        }

                        if (!IsTimeOutBell)
                        {
                                // call client api for getting customer last message date and time

                                //NameValueCollection Params = new NameValueCollection();
                                Dictionary<string, string> Params = new Dictionary<string, string>();
                                Params.Add("Mobilenumber", customGetChatTickets.CustomerMobileNumber.Length.Equals(10) ?
                                                                "91" + customGetChatTickets.CustomerMobileNumber : customGetChatTickets.CustomerMobileNumber);
                                Params.Add("ProgramCode", programCode);


                                try
                                {
                                    // CustomerLastMsgtime = CommonService.SendParamsApiRequest(ClientAPIURL + "api/ChatbotBell/GetCustomerLastUpdatedTime", Params);

                                    CustomerLastMsgtime = await APICall.SendApiRequestParams(ClientAPIURL , Params);
                                }
                                catch (Exception) { }


                            if (!string.IsNullOrEmpty(CustomerLastMsgtime))
                            {

                              CustomerLastMsgtime = JsonConvert.DeserializeObject<string>(CustomerLastMsgtime);
                              customGetChatTickets.ChatEndDateTime = CustomerLastMsgtime;
                               customGetChatTickets.IsIconDisplay = Convert.ToDateTime(CustomerLastMsgtime) > DateTime.Now.AddHours(-24);
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dt1 != null)
                {
                    dt1.Dispose();
                }
            }
            return customGetChatTickets;
        }

        /// <summary>
        /// Get IssueType List
        /// </summary>
        ///  <param name="tenantID"></param>
        /// <param name="subCategoryID"></param>
        /// <returns></returns>
        public List<IssueType> GetIssueTypeList(int tenantID, int subCategoryID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<IssueType> objIssueType = new List<IssueType>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSChatIssueTypeBySubCaategoryID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd1.Parameters.AddWithValue("@SubCategory_ID", subCategoryID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IssueType issueType = new IssueType();
                        issueType.IssueTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        issueType.IssueTypeName = Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        issueType.SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        objIssueType.Add(issueType);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return objIssueType;
        }

        /// <summary>
        /// Get SubCategoryBy Category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public List<SubCategory> GetSubCategoryByCategoryID(int categoryID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<SubCategory> objSubCategory = new List<SubCategory>();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_HSGetChatSubCategoryByCategoryID", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Category_ID", categoryID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SubCategory SubCat = new SubCategory();
                        SubCat.SubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        SubCat.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        SubCat.SubCategoryName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        objSubCategory.Add(SubCat);
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
            return objSubCategory;
        }

        /// <summary>
        /// Get Chat Tickets
        /// </summary>
        /// <param name="statusID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public List<CustomGetChatTickets> GetTicketsOnLoad(int statusID, int tenantID, int userMasterID, string programCode)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomGetChatTickets> lstGetChatTickets = new List<CustomGetChatTickets>();
            List<string> countList = new List<string>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSGetChatTickets", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("Status_ID", statusID);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("UserMaster_ID", userMasterID);
                sqlcmd.Parameters.AddWithValue("program_Code", programCode);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomGetChatTickets customGetChatTickets = new CustomGetChatTickets
                        {
                            TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]),
                            TicketStatus = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"])),
                            TicketTitle = ds.Tables[0].Rows[i]["TicketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]),
                            Category = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                            SubCategory = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                            IssueType = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
                            AssignTo = ds.Tables[0].Rows[i]["Assignee"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignee"]),
                            Priority  = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
                            CreatedOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]),
                            CreatedDate= ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            UpdatedBy= ds.Tables[0].Rows[i]["ModifyBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyBy"]),
                            UpdatedDate= ds.Tables[0].Rows[i]["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]),
                        };

                        lstGetChatTickets.Add(customGetChatTickets);
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
            return lstGetChatTickets;
        }

        /// <summary>
        /// Get tickets On View Search click
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public List<CustomGetChatTickets> GetTicketsOnSearch(ChatTicketSearch searchModel)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomGetChatTickets> lstGetChatTickets = new List<CustomGetChatTickets>();
            List<string> countList = new List<string>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSSearchChatTicket", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.CategoryId);
                sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.SubCategoryId);
                sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.IssueTypeId);
                sqlcmd.Parameters.AddWithValue("TicketStatus_ID", searchModel.TicketStatusID);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("User_ID", searchModel.UserID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomGetChatTickets customGetChatTickets = new CustomGetChatTickets
                        {
                            TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]),
                            TicketStatus = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"])),
                            TicketTitle = ds.Tables[0].Rows[i]["TicketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]),
                            TicketDescription= ds.Tables[0].Rows[i]["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketDescription"]),
                            Category = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                            SubCategory = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                            IssueType = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
                            AssignTo = ds.Tables[0].Rows[i]["Assignee"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignee"]),
                            Priority = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
                            CreatedOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]),
                            CreatedDate = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            UpdatedBy = ds.Tables[0].Rows[i]["ModifyBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyBy"]),
                            UpdatedDate = ds.Tables[0].Rows[i]["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]),
                        };

                        lstGetChatTickets.Add(customGetChatTickets);
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
            return lstGetChatTickets;
        }

        /// <summary>
        /// Update Chat Ticket Status
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="statusID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int SubmitChatTicket(int ticketID,int statusID, int userID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_HSUpdateChatTicketStatus", conn)
                {
                    Connection = conn
                };
                cmd1.Parameters.AddWithValue("@ticket_ID", ticketID);
                cmd1.Parameters.AddWithValue("@status_ID", statusID);
                cmd1.Parameters.AddWithValue("@User_ID", userID);
                cmd1.CommandType = CommandType.StoredProcedure;
                success = Convert.ToInt32(cmd1.ExecuteNonQuery());
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
        /// Get Chat Ticket Status Count
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public List<TicketStatusModel> TicketStatusCount(int tenantID, int userID, string programCode)
        {
            List<TicketStatusModel> ticketCount = new List<TicketStatusModel>();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();

            try

            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSGetChatTicketCount", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("tenantID", tenantID);
                sqlcmd.Parameters.AddWithValue("user_ID", userID);
                sqlcmd.Parameters.AddWithValue("program_Code", programCode);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                        ticketCount = ds.Tables[0].AsEnumerable().Select(r => new TicketStatusModel()
                        {
                            ticketStatus = Convert.ToString(r.Field<object>("TicketStatus")),
                            ticketCount = Convert.ToInt32(r.Field<object>("TicketStatusCount"))

                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                conn.Close();
            }

            return ticketCount;
        }


        public List<CustomGetChatTickets> GetTicketsByCustomerOnLoad(int statusID, int tenantID, int userMasterID, string programCode)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomGetChatTickets> lstGetChatTickets = new List<CustomGetChatTickets>();
            List<string> countList = new List<string>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSGetChatTicketsByCustomer", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("Status_ID", statusID);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", tenantID);
                sqlcmd.Parameters.AddWithValue("UserMaster_ID", userMasterID);
                sqlcmd.Parameters.AddWithValue("program_Code", programCode);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomGetChatTickets customGetChatTickets = new CustomGetChatTickets
                        {
                            TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]),
                            TicketStatus = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"])),
                            TicketTitle = ds.Tables[0].Rows[i]["TicketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]),
                            Category = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                            SubCategory = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                            IssueType = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
                            AssignTo = ds.Tables[0].Rows[i]["Assignee"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignee"]),
                            Priority = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
                            CreatedOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]),
                            CreatedDate = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            UpdatedBy = ds.Tables[0].Rows[i]["ModifyBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyBy"]),
                            UpdatedDate = ds.Tables[0].Rows[i]["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]),
                        };

                        lstGetChatTickets.Add(customGetChatTickets);
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
            return lstGetChatTickets;
        }


        public List<CustomGetChatTickets> GetTicketsByCustomerOnSearch(ChatTicketSearch searchModel)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            List<CustomGetChatTickets> lstGetChatTickets = new List<CustomGetChatTickets>();
            List<string> countList = new List<string>();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSSearchChatTicketByCustomer", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.CategoryId);
                sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.SubCategoryId);
                sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.IssueTypeId);
                sqlcmd.Parameters.AddWithValue("TicketStatus_ID", searchModel.TicketStatusID);
                sqlcmd.Parameters.AddWithValue("Mobile_No", searchModel.MobileNumber);
                sqlcmd.Parameters.AddWithValue("Chat_Last_Date", searchModel.ChatLastDate);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("User_ID", searchModel.UserID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomGetChatTickets customGetChatTickets = new CustomGetChatTickets
                        {
                            TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]),
                            TicketStatus = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"])),
                            TicketTitle = ds.Tables[0].Rows[i]["TicketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]),
                            TicketDescription = ds.Tables[0].Rows[i]["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketDescription"]),
                            Category = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                            SubCategory = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                            IssueType = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]),
                            AssignTo = ds.Tables[0].Rows[i]["Assignee"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignee"]),
                            Priority = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]),
                            CreatedOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedBy"]),
                            CreatedDate = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]),
                            UpdatedBy = ds.Tables[0].Rows[i]["ModifyBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifyBy"]),
                            UpdatedDate = ds.Tables[0].Rows[i]["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedDate"]),
                        };

                        lstGetChatTickets.Add(customGetChatTickets);
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
            return lstGetChatTickets;
        }
    }
}
