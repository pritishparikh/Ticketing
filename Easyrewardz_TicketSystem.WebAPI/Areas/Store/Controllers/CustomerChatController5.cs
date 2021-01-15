using System;
using System.IO;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    
    public partial class CustomerChatController : ControllerBase
    {
        #region Custom Methods
        /// <summary>
        /// Save Re-Initiate Chat 
        /// </summary>
        /// <param name="customerChatMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveReInitiateChat")]
        public async Task<ResponseModel> saveReInitiateChat([FromBody]  ReinitiateChatModel customerChatMaster )
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
                customerChatMaster.TenantID = authenticate.TenantId;
                customerChatMaster.ProgramCode = authenticate.ProgramCode;


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                customerChatMaster.CreatedBy = authenticate.UserMasterID;
                result = await customerChatCaller.SaveReInitiateChat(new CustomerChatService(_connectionString, _chatbotBellHttpClientService), customerChatMaster, _ClientAPIUrl ,_sendTextMessage,_makeBellActive);

                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
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
        ///Get Mobile Notifications Details
        /// </summary>
        /// <param name="Pageno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMobileNotificationsDetails")]
        public async Task<ResponseModel> GetMobileNotificationsDetails(int Pageno=1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            MobileNotificationModel Notifications = new MobileNotificationModel();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                CustomerChatCaller customerChatCaller = new CustomerChatCaller();

                Notifications = await customerChatCaller.GetMobileNotificationsDetails(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode,authenticate.UserMasterID, Pageno);

                statusCode = Notifications.ChatNotification.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Notifications;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        ///update Notifications Details
        /// </summary>
        /// <param name="IndexID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateMobileNotification")]
        public async Task<ResponseModel> UpdateMobileNotification(string IndexID)
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

                result = await customerChatCaller.UpdateMobileNotification(new CustomerChatService(_connectionString), authenticate.TenantId,
                    authenticate.ProgramCode, authenticate.UserMasterID, IndexID);

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
        /// Send Attachment On Chat
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("SendAttachmentOnChat")]
        public async Task<ResponseModel> SendAttachmentOnChat()
        {
            ResponseModel objResponseModel = new ResponseModel();
            SendAttachmentResponse AttachmentResponse = null;
            
            int result = 0;
            int statusCode = 0;
            string statusMessage = "";

            int ChatID = 0;
            string ClientAPIResponse = string.Empty;
            string FileName= string.Empty;
            string FileExtension = string.Empty;
            string CustomerMobileNo = string.Empty;
            string AttachmentMsg = string.Empty;
            string ChatSource = string.Empty;
            string Base64Data = string.Empty;

            try
            {
                CustomerChatCaller customerChatCaller = new CustomerChatCaller();
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));



                var files = Request.Form.Files;

                CustomerMobileNo = Convert.ToString(Request.Form["CustomerMobileNo"]);
                AttachmentMsg = Convert.ToString(Request.Form["AttachmentMessage"]);
                ChatSource = Convert.ToString(Request.Form["ChatSource"]);
                ChatID= Convert.ToInt32(Request.Form["ChatID"]);


                if (files.Count > 0)
                {

                    #region attachment File Read 


                    for (int i = 0; i < files.Count; i++)
                    {
                        SendAttachmentModel attachmentModel = new SendAttachmentModel();
                        bool SaveMessage = false;

                        FileName= Path.GetFileNameWithoutExtension(files[i].FileName);
                        FileExtension = Path.GetExtension(files[i].FileName).Replace(".", "");
                        using (MemoryStream ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            Byte[] fileBytes = ms.ToArray();
                            Base64Data = Convert.ToBase64String(fileBytes);
                        }



                        if (!string.IsNullOrEmpty(Base64Data))
                        {
                            attachmentModel.programCode = authenticate.ProgramCode;//"erreportingdemo";
                            attachmentModel.mobileNumber = CustomerMobileNo;
                            attachmentModel.textMessage = AttachmentMsg;
                            attachmentModel.mediaName = FileName;
                            attachmentModel.fileExtention = FileExtension;
                            attachmentModel.mediaType = FileExtension.Equals("jpg") || FileExtension.Equals("jpeg") ? "Image" : "Document";
                            attachmentModel.additionalDetails = "Chat Attachment: " + FileName;
                            attachmentModel.mediaBinaryData = Base64Data;
                            attachmentModel.source = ChatSource;

                            string JsonRequest = JsonConvert.SerializeObject(attachmentModel);

                            ClientAPIResponse = await _chatbotBellHttpClientService.SendApiRequest(_ClientAPIUrl + _sendAttachment, JsonRequest);

                            if (!string.IsNullOrEmpty(ClientAPIResponse))
                            {
                                AttachmentResponse = JsonConvert.DeserializeObject<SendAttachmentResponse>(ClientAPIResponse);

                                if(AttachmentResponse!=null)
                                {
                                    SaveMessage = !string.IsNullOrEmpty(AttachmentResponse.mediaURL);
                                }
                            }

                        }

                        #endregion


                        #region Save File URL in DB  

                        if (SaveMessage )
                        {
                            if(ChatSource.Equals("cb"))
                            {
                                CustomerChatModel saveAttachment = new CustomerChatModel()
                                {
                                    ChatID = ChatID,
                                    Message = AttachmentMsg,
                                    Attachment = AttachmentResponse.mediaURL,
                                    ByCustomer = false,
                                    ChatStatus = 1,
                                    StoreManagerId = authenticate.UserMasterID,
                                    CreatedBy = authenticate.UserMasterID
                                };

                                result = result + customerChatCaller.SaveChatMessages(new CustomerChatService(_connectionString), saveAttachment);
                            }
                            else
                            {
                                result = 1;
                            }
                           
                        }

                        #endregion

                    }

                }


                statusCode = result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = !string.IsNullOrEmpty(AttachmentResponse.mediaURL) && result > 0 ? AttachmentResponse.mediaURL : string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        #endregion
    }
}