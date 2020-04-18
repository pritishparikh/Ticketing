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
    public class StoreClaimService: IStoreClaim
    {
        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public StoreClaimService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }

        public int AddClaimComment(int ClaimID, string Comment, int UserID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_AddStoreClaimComment", conn)
                {
                    Connection = conn
                };
                cmd1.Parameters.AddWithValue("@Claim_ID", ClaimID);
                cmd1.Parameters.AddWithValue("@_Comments", Comment);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
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

        public int AddClaimCommentByApprovel(int ClaimID, string Comment, int UserID)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_StoreClaimCommentByApprovel", conn)
                {
                    Connection = conn
                };
                cmd1.Parameters.AddWithValue("@Claim_ID", ClaimID);
                cmd1.Parameters.AddWithValue("@_Comments", Comment);
                cmd1.Parameters.AddWithValue("@User_ID", UserID);
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

        public int AssignClaim(int claimID, int assigneeID, int userMasterID, int tenantId)
        {

            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ClaimReAssign", conn)
                {
                    Connection = conn
                };
                cmd.Parameters.AddWithValue("@claim_ID", claimID);
                cmd.Parameters.AddWithValue("@assignee_ID", assigneeID);
                cmd.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                cmd.Parameters.AddWithValue("@tenant_Id", tenantId);
                cmd.CommandType = CommandType.StoredProcedure;
                result = cmd.ExecuteNonQuery();
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

        public int ClaimApprove(int claimID, double finalClaimAsked, bool IsApprove, int userMasterID, int tenantId)
        {
            int success = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SP_IsApproveClaim", conn)
                {
                    Connection = conn
                };
                cmd1.Parameters.AddWithValue("@claim_ID", claimID);
                cmd1.Parameters.AddWithValue("@finalClaim_Asked", finalClaimAsked);
                cmd1.Parameters.AddWithValue("@Is_Approve", IsApprove);
                cmd1.Parameters.AddWithValue("@userMaster_ID", userMasterID);
                cmd1.Parameters.AddWithValue("@tenant_Id", tenantId);
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

        public CustomClaimByID GetClaimByID(int ClaimID, int tenantID, int userID,string url)
        {
            DataSet ds = new DataSet();
            CustomClaimByID customClaimList = new CustomClaimByID();
            //List<CustomClaimList> lstCustomClaim = new List<CustomClaimList>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimByID", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Claim_ID", ClaimID);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);
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
                        customClaimList.ClaimID = ds.Tables[0].Rows[i]["ClaimID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ClaimID"]);
                        customClaimList.Status = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.ClaimStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));
                        customClaimList.AssigneeID = ds.Tables[0].Rows[i]["AssigneeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["AssigneeID"]);
                        customClaimList.AssignTo = ds.Tables[0].Rows[i]["Assignto"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]);
                        customClaimList.BrandID = ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        customClaimList.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        customClaimList.CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        customClaimList.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        customClaimList.SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        customClaimList.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        customClaimList.IssueTypeID = ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        customClaimList.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        customClaimList.CustomerID= ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]);
                        customClaimList.CustomerName= ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        customClaimList.CustomerPhoneNumber = ds.Tables[0].Rows[i]["CustomerPhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerPhoneNumber"]);
                        customClaimList.CustomerAlternateNumber = ds.Tables[0].Rows[i]["AltNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AltNumber"]);
                        customClaimList.EmailID = ds.Tables[0].Rows[i]["CustomerEmailId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailId"]);
                        customClaimList.AlternateEmailID = ds.Tables[0].Rows[i]["AltEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AltEmailID"]);
                        customClaimList.Gender = ds.Tables[0].Rows[i]["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Gender"]);
                        customClaimList.ClaimAskFor= ds.Tables[0].Rows[i]["ClaimPercent"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ClaimPercent"]);
                        customClaimList.TargetClouserDate = ds.Tables[0].Rows[i]["ClosureDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ClosureDate"]);
                        customClaimList.TicketID = ds.Tables[0].Rows[i]["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketID"]);
                        customClaimList.TicketingTaskID = ds.Tables[0].Rows[i]["TicketingTaskID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TicketingTaskID"]);
                        customClaimList.Attachments = ds.Tables[1].AsEnumerable().Select(x => new ClaimAttachment()
                        {
                            ClaimAttachmentId = Convert.ToInt32(x.Field<int>("ClaimAttachmentId")),
                            AttachmentName = x.Field<object>("AttachmentName") == System.DBNull.Value || string.IsNullOrEmpty(Convert.ToString(x.Field<object>("AttachmentName"))) ? string.Empty : url + "/" + Convert.ToString(x.Field<object>("AttachmentName"))
                        }).ToList();

                        customClaimList.CommentByStores = ds.Tables[2].AsEnumerable().Select(x => new CommentByStore()
                        {
                            Name = x.Field<object>("Name") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Name")),
                            Comment = x.Field<object>("Comment") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Comment")),
                            CommentDate = x.Field<object>("CommentDate") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CommentDate"))
                        }).ToList();

                        customClaimList.CommentByApprovels = ds.Tables[3].AsEnumerable().Select(x => new CommentByApprovel()
                        {
                            Name = x.Field<object>("Name") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Name")),
                            Comment = x.Field<object>("Comment") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("Comment")),
                            CommentDate = x.Field<object>("CommentDate") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("CommentDate"))
                        }).ToList();

                    }
                    if (ds != null && ds.Tables[4] != null)
                    {

                        for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                        {
                            {
                                CustomOrderMaster customOrderMaster = new CustomOrderMaster();

                                customOrderMaster.OrderMasterID = Convert.ToInt32(ds.Tables[4].Rows[i]["OrderMasterID"]);
                                customOrderMaster.InvoiceNumber = ds.Tables[4].Rows[i]["InvoiceNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[i]["InvoiceNumber"]);
                                customOrderMaster.InvoiceDate = Convert.ToDateTime(ds.Tables[4].Rows[i]["InvoiceDate"]);
                                customOrderMaster.OrdeItemPrice = ds.Tables[4].Rows[i]["OrderPrice"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[4].Rows[i]["OrderPrice"]);
                                customOrderMaster.OrderPricePaid = ds.Tables[4].Rows[i]["PricePaid"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[4].Rows[i]["PricePaid"]);                            
                                customOrderMaster.DateFormat = customOrderMaster.InvoiceDate.ToString("dd/MMM/yyyy");
                                customOrderMaster.StoreCode = ds.Tables[4].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[i]["StoreCode"]);
                                customOrderMaster.StoreAddress = ds.Tables[4].Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[4].Rows[i]["Address"]);
                                customOrderMaster.Discount = ds.Tables[4].Rows[i]["Discount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[4].Rows[i]["Discount"]);
                                int orderMasterId = Convert.ToInt32(ds.Tables[4].Rows[i]["OrderMasterID"]);
                                customOrderMaster.OrderItems = ds.Tables[5].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("OrderMasterID")).
                                Equals(orderMasterId)).Select(x => new OrderItem()
                                {
                                    OrderItemID = Convert.ToInt32(x.Field<int>("OrderItemID")),
                                    OrderMasterID = Convert.ToInt32(x.Field<int>("OrderMasterID")),
                                    ArticleNumber = x.Field<object>("SKUNumber") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUNumber")),
                                    ArticleName = x.Field<object>("SKUName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUName")),
                                    ItemPrice = x.Field<object>("ItemPrice") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("ItemPrice")),
                                    PricePaid = x.Field<object>("PricePaid") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("PricePaid")),
                                    Discount = x.Field<object>("Discount") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("Discount")),
                                    RequireSize = x.Field<object>("RequireSize") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("RequireSize"))
                                }).ToList();
                                customOrderMaster.ItemCount = customOrderMaster.OrderItems.Count();
                                customOrderMaster.ItemPrice = customOrderMaster.OrderItems.Sum(item => item.ItemPrice);
                                customOrderMaster.PricePaid = customOrderMaster.OrderItems.Sum(item => item.PricePaid);
                                customClaimList.CustomOrderMaster = customOrderMaster;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
            return customClaimList;
        }

        public List<UserComment> GetClaimComment(int ClaimID)
        {
            DataSet ds = new DataSet();
            List<UserComment> lstClaimComment = new List<UserComment>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimCommentByClaimId", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Claim_ID", ClaimID);
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
                        UserComment userComment = new UserComment();
                        userComment.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        userComment.Comment = Convert.ToString(ds.Tables[0].Rows[i]["Comment"]);
                        userComment.datetime = Convert.ToString(ds.Tables[0].Rows[i]["datetime"]);
                        lstClaimComment.Add(userComment);
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
            return lstClaimComment;
        }

        public List<CommentByApprovel> GetClaimCommentForApprovel(int ClaimID)
        {

            DataSet ds = new DataSet();
            List<CommentByApprovel> lstClaimComment = new List<CommentByApprovel>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimCommentByApprovel", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Claim_ID", ClaimID);
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
                        CommentByApprovel userComment = new CommentByApprovel();
                        userComment.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        userComment.Comment = Convert.ToString(ds.Tables[0].Rows[i]["Comment"]);
                        userComment.CommentDate = Convert.ToString(ds.Tables[0].Rows[i]["CommentDate"]);
                        lstClaimComment.Add(userComment);
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
            return lstClaimComment;
        }

        public List<CustomClaimList> GetClaimList(int tabFor, int tenantID, int userID)
        {
            DataSet ds = new DataSet();
            List<CustomClaimList> lstCustomClaim = new List<CustomClaimList>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ListClaimDetails", conn);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tab_For", tabFor);
                cmd.Parameters.AddWithValue("@tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@user_ID", userID);
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
                        CustomClaimList customClaimList = new CustomClaimList();
                        customClaimList.ClaimID = ds.Tables[0].Rows[i]["ClaimID"]== DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ClaimID"]);
                        customClaimList.Status = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString((EnumMaster.ClaimStatus)Convert.ToInt32(ds.Tables[0].Rows[i]["Status"]));
                        customClaimList.AssignTo = ds.Tables[0].Rows[i]["Assignto"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Assignto"]);
                        customClaimList.BrandID= ds.Tables[0].Rows[i]["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"]);
                        customClaimList.BrandName= ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        customClaimList.CategoryID = ds.Tables[0].Rows[i]["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]);
                        customClaimList.CategoryName= ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        customClaimList.SubCategoryID = ds.Tables[0].Rows[i]["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryID"]);
                        customClaimList.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        customClaimList.IssueTypeID= ds.Tables[0].Rows[i]["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IssueTypeID"]);
                        customClaimList.IssueTypeName = ds.Tables[0].Rows[i]["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IssueTypeName"]);
                        customClaimList.RaiseBy = ds.Tables[0].Rows[i]["RaiseBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RaiseBy"]);
                        customClaimList.CreationOn = ds.Tables[0].Rows[i]["CreationOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreationOn"]);
                        lstCustomClaim.Add(customClaimList);
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
            return lstCustomClaim;
        }

        public List<CustomOrderwithCustomerDetails> GetOrderDetailByTicketID(int ticketID, int tenantID)
        {
            DataSet ds = new DataSet();
            List<CustomOrderwithCustomerDetails> objorderMaster = new List<CustomOrderwithCustomerDetails>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetOrderandCustomerDetailByTicketID", conn);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ticket_ID", ticketID);
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomOrderwithCustomerDetails customOrderMaster = new CustomOrderwithCustomerDetails();
                        customOrderMaster.OrderMasterID = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customOrderMaster.InvoiceNumber = ds.Tables[0].Rows[i]["InvoiceNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["InvoiceNumber"]);
                        customOrderMaster.InvoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]);
                        customOrderMaster.OrdeItemPrice = ds.Tables[0].Rows[i]["OrderPrice"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderPrice"]);
                        customOrderMaster.OrderPricePaid = ds.Tables[0].Rows[i]["PricePaid"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PricePaid"]);
                        customOrderMaster.DateFormat = customOrderMaster.InvoiceDate.ToString("dd/MMM/yyyy");
                        customOrderMaster.StoreCode = ds.Tables[0].Rows[i]["StoreCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StoreCode"]);
                        customOrderMaster.StoreAddress = ds.Tables[0].Rows[i]["Address"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                        customOrderMaster.Discount = ds.Tables[0].Rows[i]["Discount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Discount"]);
                        customOrderMaster.CustomerID = ds.Tables[0].Rows[i]["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"]);
                        customOrderMaster.CustomerName = ds.Tables[0].Rows[i]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerName"]);
                        customOrderMaster.CustomerPhoneNumber = ds.Tables[0].Rows[i]["CustomerPhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerPhoneNumber"]);
                        customOrderMaster.CustomerAlternateNumber = ds.Tables[0].Rows[i]["AltNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AltNumber"]);
                        customOrderMaster.EmailID = ds.Tables[0].Rows[i]["CustomerEmailId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CustomerEmailId"]);
                        customOrderMaster.AlternateEmailID = ds.Tables[0].Rows[i]["AltEmailID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AltEmailID"]);
                        customOrderMaster.Gender = ds.Tables[0].Rows[i]["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Gender"]);

                        int orderMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderMasterID"]);
                        customOrderMaster.OrderItems = ds.Tables[1].AsEnumerable().Where(x => Convert.ToInt32(x.Field<int>("OrderMasterID")).
                        Equals(orderMasterId)).Select(x => new OrderItem()
                        {
                            OrderItemID = Convert.ToInt32(x.Field<int>("OrderItemID")),
                            OrderMasterID = Convert.ToInt32(x.Field<int>("OrderMasterID")),
                            ArticleNumber = x.Field<object>("SKUNumber") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUNumber")),
                            ArticleName = x.Field<object>("SKUName") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("SKUName")),
                            ItemPrice = x.Field<object>("ItemPrice") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("ItemPrice")),
                            PricePaid = x.Field<object>("PricePaid") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("PricePaid")),
                            Discount = x.Field<object>("Discount") == DBNull.Value ? 0 : Convert.ToInt32(x.Field<object>("Discount")),
                            RequireSize = x.Field<object>("RequireSize") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("RequireSize")),
                            InvoiceNumber = x.Field<object>("InvoiceNo") == DBNull.Value ? string.Empty : Convert.ToString(x.Field<object>("InvoiceNo")),// customOrderMaster.InvoiceNumber,
                            InvoiceDate = customOrderMaster.InvoiceDate
                        }).ToList();
                        customOrderMaster.ItemCount = customOrderMaster.OrderItems.Count();
                        customOrderMaster.ItemPrice = customOrderMaster.OrderItems.Sum(item => item.ItemPrice);
                        customOrderMaster.PricePaid = customOrderMaster.OrderItems.Sum(item => item.PricePaid);
                        objorderMaster.Add(customOrderMaster);
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
            return objorderMaster;
        }

        public List<CustomStoreUserList> GetUserList(int tenantID, int assignID)
        {
            DataSet ds = new DataSet();
            List<CustomStoreUserList> listUser = new List<CustomStoreUserList>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_GetClaimAssignDropdown", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tenant_ID", tenantID);
                cmd.Parameters.AddWithValue("@assign_ID", assignID);
                cmd.Connection = conn;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomStoreUserList customUserList = new CustomStoreUserList();
                        customUserList.User_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                        customUserList.UserName = ds.Tables[0].Rows[i]["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        listUser.Add(customUserList);
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
            return listUser;
        }

        public int RaiseClaim(StoreClaimMaster storeClaimMaster, string finalAttchment)
        {
            int ClaimID = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertRaiseClaim", conn);
                cmd.Parameters.AddWithValue("@Tenant_ID", storeClaimMaster.TenantID);
                cmd.Parameters.AddWithValue("@Created_By", storeClaimMaster.CreatedBy);
                cmd.Parameters.AddWithValue("@fileName", string.IsNullOrEmpty(finalAttchment) ? "" : finalAttchment);
                cmd.Parameters.AddWithValue("@Brand_ID", storeClaimMaster.BrandID);
                cmd.Parameters.AddWithValue("@Category_ID", storeClaimMaster.CategoryID);
                cmd.Parameters.AddWithValue("@SubCategory_ID", storeClaimMaster.SubCategoryID);
                cmd.Parameters.AddWithValue("@IssueType_ID", storeClaimMaster.IssueTypeID);
                cmd.Parameters.AddWithValue("@Order_IDs", string.IsNullOrEmpty(storeClaimMaster.OrderItemID) ? "" : storeClaimMaster.OrderItemID);
                cmd.Parameters.AddWithValue("@Claim_Percent", storeClaimMaster.ClaimPercent); 
                cmd.Parameters.AddWithValue("@Customer_ID", storeClaimMaster.CustomerID);
                cmd.Parameters.AddWithValue("@Ticket_ID", storeClaimMaster.TicketID);
                cmd.Parameters.AddWithValue("@Ticketing_TaskID", storeClaimMaster.TaskID);
                cmd.CommandType = CommandType.StoredProcedure;
                ClaimID = Convert.ToInt32(cmd.ExecuteScalar());            
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
            return ClaimID;
        }
        #endregion
    }
}
