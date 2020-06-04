using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Easyrewardz_TicketSystem.Services
{
    public class HSChatTicketingService : IHSChatTicketing
    {
        MySqlConnection conn = new MySqlConnection();

        public HSChatTicketingService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

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

        public GetChatTicketsByID GetChatTicketsByID(int ticketID, int tenantID, int userMasterID, string programCode)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            GetChatTicketsByID customGetChatTickets= new GetChatTicketsByID() ;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand sqlcmd = new MySqlCommand("SP_HSGetChatTicketsByID", conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("ticket_ID", ticketID);
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

                        customGetChatTickets.TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]);
                        customGetChatTickets.TicketStatus = ds.Tables[0].Rows[i]["StatusID"] == DBNull.Value ? 0 :Convert.ToInt32(ds.Tables[0].Rows[i]["StatusID"]);
                        customGetChatTickets.TicketTitle = ds.Tables[0].Rows[i]["TicketTitle"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketTitle"]);
                        customGetChatTickets.TicketDescription = ds.Tables[0].Rows[i]["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TicketDescription"]);
                        customGetChatTickets.CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        customGetChatTickets.Category = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        customGetChatTickets.SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        customGetChatTickets.SubCategory = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        customGetChatTickets.IssueTypeID = ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        customGetChatTickets.IssueType = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        customGetChatTickets.AssignTo = ds.Tables[0].Rows[i]["Assignee"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignee"]);
                        customGetChatTickets.Priority = ds.Tables[0].Rows[i]["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PriortyName"]);
                        customGetChatTickets.CustomerMobileNumber = ds.Tables[0].Rows[i]["CustomerMobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerMobileNumber"]);
                        customGetChatTickets.Brand = ds.Tables[0].Rows[i]["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand"]);
                        customGetChatTickets.CreatedDate = ds.Tables[0].Rows[i]["CreatedAgo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedAgo"]);
                        customGetChatTickets.ChatTicketNote = ds.Tables[1].AsEnumerable().Select(x => new ChatTicketNotes()
                        {
                            Name = x.Field<object>("Name") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Name")),
                            Comment = x.Field<object>("Comment") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Comment")),
                            CommentDate = x.Field<object>("CommentDate") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CommentDate"))
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
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return customGetChatTickets;
        }

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
            }
            return objIssueType;
        }

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
            }
            return lstGetChatTickets;
        }

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
            }
            return lstGetChatTickets;
        }

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
    }
}
