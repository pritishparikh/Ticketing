using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class CustomerChatController : ControllerBase
    {
        /// <summary>
        /// update customer chat session
        /// </summary>
        /// <param name="ChatSessionValue"></param>
        /// <param name="ChatSessionDuration"></param>
        /// <param name="ChatDisplayValue"></param>
        /// <param name="ChatDisplayDuration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateChatSession")]
        public ResponseModel UpdateChatSession(int ChatSessionValue, string ChatSessionDuration, int ChatDisplayValue, string ChatDisplayDuration,int ChatCharLimit)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.UpdateChatSession(new CustomerChatService(_connectionString),authenticate.TenantId,authenticate.ProgramCode,
                    ChatSessionValue,  ChatSessionDuration,  ChatDisplayValue, ChatDisplayDuration, ChatCharLimit, authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// get customer chat session
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChatSession")]
        public ResponseModel GetChatSession()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            ChatSessionModel ChatSession = new ChatSessionModel();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                ChatSession = customerChatCaller.GetChatSession(new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode );

                statusCode = ChatSession!=null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ChatSession;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Agent Recent Chat List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAgentRecentChat")]
        public ResponseModel GetAgentRecentChat(int CustomerID)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<AgentRecentChatHistory> RecentChatsList = new List<AgentRecentChatHistory>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                RecentChatsList = customerChatCaller.GetAgentRecentChat(new CustomerChatService(_connectionString),authenticate.TenantId,authenticate.ProgramCode, CustomerID);

                statusCode = RecentChatsList.Count > 0? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = RecentChatsList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get Agent Chat History
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAgentChatHistory")]
        public ResponseModel GetAgentChatHistory()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<AgentCustomerChatHistory> ChatsList = new List<AgentCustomerChatHistory>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                ChatsList = customerChatCaller.GetAgentChatHistory(new CustomerChatService(_connectionString),authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode);

                statusCode = ChatsList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ChatsList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Agent List For Ongoin Chat
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAgentList")]
        public ResponseModel GetAgentList()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<AgentRecentChatHistory> AgentList = new List<AgentRecentChatHistory>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                AgentList = customerChatCaller.GetAgentList(new CustomerChatService(_connectionString),authenticate.TenantId);

                statusCode = AgentList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = AgentList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Get Card Image Upload log
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCardImageUploadlog")]
        public ResponseModel GetCardImageUploadlog(int ListingFor=1) //1=asset Approval listing 2=upload log

        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<ChatCardImageUploadModel> CardImageLog = new List<ChatCardImageUploadModel>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CardImageLog = customerChatCaller.GetCardImageUploadlog(new CustomerChatService(_connectionString), ListingFor, authenticate.TenantId,authenticate.ProgramCode);

                statusCode = CardImageLog.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CardImageLog;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Insert Card Image Upload
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertCardImageUpload")]
        public ResponseModel InsertCardImageUpload()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            string ImageFilePath = string.Empty;
            string ImageUrl= string.Empty;
            List<string> ImageList = new List<string>();
            string ItemID = string.Empty;
            string SearchText = string.Empty;

            try
            {
                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                #region CardImage File Read and Save

                var files = Request.Form.Files;

                ItemID = Convert.ToString(Request.Form["ItemID"]);
                SearchText = Convert.ToString(Request.Form["SearchText"]);


                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        ImageList.Add (files[i].FileName.Replace(".",  "_" +  ItemID +"_"+  DateTime.Now.ToString("ddmmyyyyhhssfff") + ".") );
                    }
                   
                }


                ImageFilePath = Path.Combine(Directory.GetCurrentDirectory(), UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)4), "ChatBotCardImages");
               

                if (!Directory.Exists(ImageFilePath))
                {
                    Directory.CreateDirectory(ImageFilePath);
                }

                if(ImageList.Count > 0)
                {
                  
                        using (var ms = new MemoryStream())
                        {
                            files[0].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(Path.Combine(ImageFilePath, ImageList[0]), FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();
                            string s = Convert.ToBase64String(fileBytes);
                            byte[] a = Convert.FromBase64String(s);
                            // act on the Base64 data

                        }
                    
                }

                // ImageFilePath = Path.Combine(ImageFilePath, ImageList[0]);
                ImageUrl = rootPath + UploadFiles +"/"+ CommonFunction.GetEnumDescription((EnumMaster.FileUpload)4) + "/ChatBotCardImages/" + ImageList[0];

                #endregion


                result = customerChatCaller.InsertCardImageUpload
                    (new CustomerChatService(_connectionString), authenticate.TenantId,authenticate.ProgramCode, _ClientAPIUrl, SearchText, ItemID, ImageUrl, authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Approve Reject Card Image
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ApproveRejectCardImage")]
        public ResponseModel ApproveRejectCardImage(int ID,string ItemID, bool AddToLibrary)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.ApproveRejectCardImage
                    (new CustomerChatService(_connectionString), ID, authenticate.TenantId, authenticate.ProgramCode, ItemID, AddToLibrary, authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Insert New CardItem Configuration
        /// </summary>
        /// <param name="CardItem"></param>
        /// <param name="IsEnabled"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertCardItemConfiguration")]
        public ResponseModel InsertCardItemConfiguration(string CardItem, bool IsEnabled)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.InsertNewCardItemConfiguration
                    (new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode, CardItem, IsEnabled, authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Update CardItem Configuration
        /// </summary>
        /// <param name="EnabledCardItems"></param>
        /// <param name="DisabledCardItems"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCardItemConfiguration")]
        public ResponseModel UpdateCardItemConfiguration(string EnabledCardItems, string DisabledCardItems)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.UpdateCardItemConfiguration(new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode, 
                    EnabledCardItems, DisabledCardItems, authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get Card Configuration List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCardConfiguration")]
        public ResponseModel GetCardConfiguration()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<ChatCardConfigurationModel> CardConfigList = new List<ChatCardConfigurationModel>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CardConfigList = customerChatCaller.GetCardConfiguration(new CustomerChatService(_connectionString),  authenticate.TenantId, authenticate.ProgramCode);

                statusCode = CardConfigList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CardConfigList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Update StoreManager chat status
        /// </summary>
        /// <param name="ChatID"></param>
        /// <param name="ChatStatusID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateStoreManagerChatStatus")]
        public ResponseModel UpdateStoreManagerChatStatus(int ChatID, int ChatStatusID)
        {
            /*
             ******* ChatStatusID:*******
             * Dropped Chat -->1
               Unanswered Chat -->2
               Closed Chat -->3
               Hold Chat -->4
               UnHold Chat -->5
               InActive Chat -->6
 
             * */


            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.UpdateStoreManagerChatStatus(new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode, ChatID, 
                    ChatStatusID,authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        /// Update Card Image Approval
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCardImageApproval")]
        public ResponseModel UpdateCardImageApproval(int ID)
        {
           

            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.UpdateCardImageApproval(new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode, ID,authenticate.UserMasterID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// /Get Card Image Approval List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCardImageApproval")]
        public ResponseModel GetCardImageApproval()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            List<CardImageApprovalModel> CardApprovalist = new List<CardImageApprovalModel>();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                CardApprovalist = customerChatCaller.GetCardImageApprovalList(new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode);

                statusCode = CardApprovalist.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CardApprovalist;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// End Chat Form Customer
        /// </summary>
        /// <param name="ChatID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EndCustomerChat")]
        public ResponseModel EndCustomerChat(int ChatID)
        {


            ResponseModel objResponseModel = new ResponseModel();
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                result = customerChatCaller.EndCustomerChat(new CustomerChatService(_connectionString), authenticate.TenantId, authenticate.ProgramCode,ChatID);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

    }
}