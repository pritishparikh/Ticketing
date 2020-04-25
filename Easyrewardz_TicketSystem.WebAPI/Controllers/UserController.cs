using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    /// <summary>
    /// User controller to manage User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class UserController : ControllerBase
    {
        #region  Variable Declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string ProfileImg_Resources;
        private readonly string ProfileImg_Image;


        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;
        #endregion

        #region Constructor
        /// <summary>
        /// User Controller
        /// </summary>
        /// <param name="_iConfig"></param>
        public UserController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
            ProfileImg_Resources = configuration.GetValue<string>("ProfileImg_Resources");
            ProfileImg_Image = configuration.GetValue<string>("ProfileImg_Image");

            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");
        }
        #endregion

        #region Custom Methods 
        /// <summary>
        /// Get User List
        /// </summary>
        [HttpPost]
        [Route("GetUserList")]
        public ResponseModel GetUserList( )
        {
            List<User> objUserList = new List<User>();
           
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                objUserList = newMasterBrand.GetUserList(new UserServices(_connectioSting), authenticate.TenantId,authenticate.UserMasterID);

                StatusCode =
                objUserList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Add User Personal Details
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("AddUserPersonalDetail")]
        public ResponseModel AddUserPersonalDetail([FromBody] UserModel userModel )
        {         
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                userModel.CreatedBy = authenticate.UserMasterID;
                userModel.TenantID = authenticate.TenantId;
                 int Result = userCaller.AddUserPersonaldetail(new UserServices(_connectioSting), userModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Add User Profile Detail
        /// </summary>
        /// <param name="DesignationID"></param>
        /// <param name="ReportTo"></param>
        /// <param name="UserID"></param>
        /// <param name="IsStoreUser"></param>
        [HttpPost]
        [Route("AddUserProfileDetail")]
        public ResponseModel AddUserProfileDetail(int DesignationID, int ReportTo, int UserID,int IsStoreUser=1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.AddUserProfiledetail(new UserServices(_connectioSting), DesignationID, ReportTo, authenticate.UserMasterID, authenticate.TenantId, UserID, IsStoreUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Map category
        /// </summary>
        /// <param name="customUserModel"></param>
        [HttpPost]
        [Route("Mapcategory")]
        public ResponseModel Mapcategory([FromBody] CustomUserModel customUserModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                customUserModel.CreatedBy = authenticate.UserMasterID;
                customUserModel.TenantID = authenticate.TenantId;
                int Result = userCaller.Mappedcategory(new UserServices(_connectioSting), customUserModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Edit User Details
        /// </summary>
        /// <param name="customEditUserModel"></param>
        [HttpPost]
        [Route("EditUserDetails")]
        public ResponseModel EditUserDetails([FromBody]CustomEditUserModel customEditUserModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                customEditUserModel.TenantID = authenticate.TenantId;
                customEditUserModel.CreatedBy = authenticate.UserMasterID;
                int Result = userCaller.EditUserDetail(new UserServices(_connectioSting), customEditUserModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IsStoreUser"></param>
        [HttpPost]
        [Route("DeleteUser")]
        public ResponseModel DeleteUser(int userID,int IsStoreUser=1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                int Result = userCaller.DeleteUser(new UserServices(_connectioSting), userID,authenticate.TenantId, authenticate.UserMasterID, IsStoreUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get User List Data
        /// </summary>
        /// <param name="IsStoreUser"></param>
        [HttpGet]
        [Route("GetUserListData")]
        public ResponseModel GetUserListData(int IsStoreUser=1)
        {
            List<CustomUserList> objUserList = new List<CustomUserList>();

            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                objUserList = userCaller.UserList(new UserServices(_connectioSting), authenticate.TenantId, IsStoreUser);

                StatusCode =
                objUserList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get User Details By Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="IsStoreUser"></param>
        [HttpPost]
        [Route("GetUserDetailsById")]
        public ResponseModel GetUserDetailsById(int UserID,int IsStoreUser=1)
        {
            CustomUserList objUser  = new CustomUserList();

            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                objUser = userCaller.GetuserDetailsById(new UserServices(_connectioSting), UserID,authenticate.TenantId, IsStoreUser);

                StatusCode =
                objUser == null ? 
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUser;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Bulk Upload User
        /// </summary>
        /// <param name="UserFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadUser")]
        public ResponseModel BulkUploadUser(int UserFor=1)
        {
            string DownloadFilePath = string.Empty; 
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false; bool successfilesaved = false;
            int count = 0;
            UserCaller userCaller = new UserCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = ""; string fileName = ""; string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet DataSetCSV = new DataSet();
            string[] filesName = null;
            List<string> CSVlist = new List<string>();
            string successfilename = string.Empty, errorfilename = string.Empty; string errorfilepath = string.Empty; string successfilepath = string.Empty; 


            try
            {
                var files = Request.Form.Files;

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }
                var Keys = Request.Form;



                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

               

                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                 filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor)); 


                if (!Directory.Exists(BulkUploadFilesPath))
                {
                    Directory.CreateDirectory(BulkUploadFilesPath);
                }



                if (files.Count > 0)
                {

                    for (int i = 0; i < files.Count; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(Path.Combine(BulkUploadFilesPath, filesName[i]), FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();
                            string s = Convert.ToBase64String(fileBytes);
                            byte[] a = Convert.FromBase64String(s);
                            // act on the Base64 data

                        }
                    }
                }

                #endregion

                DataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
                CSVlist = userCaller.UserBulkUpload(new UserServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, UserFor, DataSetCSV);


                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "UserSuccessFile_" + timeStamp + ".csv";
                string ErrorFileName = "UserErrorFile_" + timeStamp + ".csv";



                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor) + "/Error/" + ErrorFileName : string.Empty;

                if (!string.IsNullOrEmpty(CSVlist[0]))
                {
                    if (!CSVlist[0].ToLower().Contains("username"))
                        SuccessFileName = "MappedCategorySuccessFile_" + timeStamp + ".csv";

                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);
                }
                if (!string.IsNullOrEmpty(CSVlist[1]))
                {
                    if (!CSVlist[1].ToLower().Contains("username"))
                        ErrorFileName = "MappedCategoryErrorFile" + timeStamp + ".csv";

                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);
                }
                   

                count = fileU.CreateFileUploadLog(new FileUploadService(_connectioSting), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "User", SuccessFileUrl, ErrorFileUrl, UserFor);
                #endregion



                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;


        }

        /// <summary>
        /// UpdateUserProfiledetails
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("UpdateUserProfileDetails")]
        public ResponseModel UpdateUserProfileDetails(IFormFile File)
        {
            UpdateUserProfiledetailsModel UpdateUserProfiledetailsModel = new UpdateUserProfiledetailsModel();
            ProfileDetailsmodel profileDetailsmodel = new ProfileDetailsmodel();
            var Keys = Request.Form;
            UpdateUserProfiledetailsModel = JsonConvert.DeserializeObject<UpdateUserProfiledetailsModel>(Keys["UpdateUserProfiledetailsModel"]);
            var file = Request.Form.Files;
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            var folderName = Path.Combine(ProfileImg_Resources, ProfileImg_Image);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            try
            {
                if (file.Count > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                    var fileName_Id = fileName.Replace(".", UpdateUserProfiledetailsModel.UserId + timeStamp + ".") + "";
                    var fullPath = Path.Combine(pathToSave, fileName_Id);
                    var dbPath = Path.Combine(folderName, fileName_Id);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file[0].CopyTo(stream);
                    }
                    UpdateUserProfiledetailsModel.ProfilePicture = fileName_Id;
                    string url = configuration.GetValue<string>("APIURL") + ProfileImg_Resources + "/" + ProfileImg_Image + "/" + fileName_Id;
                    profileDetailsmodel.ProfilePath = url;
                }
            }
            catch (Exception) { }
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.UpdateUserProfileDetail(new UserServices(_connectioSting), UpdateUserProfiledetailsModel);
                
                profileDetailsmodel.Result = Result;
             
                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = profileDetailsmodel;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// GetUserProfileDetails
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetUserProfileDetail")]
        public ResponseModel GetUserProfileDetail()
        {
            List<UpdateUserProfiledetailsModel> objUserList = new List<UpdateUserProfiledetailsModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                string url = configuration.GetValue<string>("APIURL") + ProfileImg_Resources+"/" +ProfileImg_Image;
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                UserCaller userCaller = new UserCaller();
                objUserList = userCaller.GetUserProfileDetails(new UserServices(_connectioSting), authenticate.UserMasterID, url);
                StatusCode =
               objUserList.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// EditUserPersonalDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("EditUserPersonalDetail")]
        public ResponseModel EditUserPersonalDetail([FromBody] UserModel userModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                userModel.CreatedBy = authenticate.UserMasterID;
                userModel.TenantID = authenticate.TenantId;
                int Result = userCaller.EditUserPersonaldetail(new UserServices(_connectioSting), userModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Send Mail for changepassword
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IsStoreUser"></param>
        [HttpPost]
        [Route("SendMailforchangepassword")]
        public ResponseModel SendMailforchangepassword (int userID, int IsStoreUser = 1)
        {
            CustomChangePassword customChangePassword = new CustomChangePassword();
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                customChangePassword = userCaller.SendMailforchangepassword(new UserServices(_connectioSting), userID, authenticate.TenantId, IsStoreUser);
                if(customChangePassword.UserID >0 && customChangePassword.Password!=null && customChangePassword.EmailID !=null)
                {
                    MasterCaller masterCaller = new MasterCaller();
                    SMTPDetails sMTPDetails = masterCaller.GetSMTPDetails(new MasterServices(_connectioSting), authenticate.TenantId);
                    securityCaller _securityCaller = new securityCaller();
                    CommonService commonService = new CommonService();
                    string encryptedEmailId = SecurityService.Encrypt(customChangePassword.EmailID);

                    string decriptedPassword = SecurityService.DecryptStringAES(customChangePassword.Password);
                    string url = configuration.GetValue<string>("websiteURL") + "/ChangePassword";
                    string body = "Dear User, <br/>Please find the below details.  <br/><br/>" + "Your Email ID  : " + customChangePassword.EmailID + "<br/>" + "Your Password : " + decriptedPassword + "<br/><br/>" + "Click on Below link to change the Password <br/>" + url + "?Id:" + encryptedEmailId;
                    bool isUpdate = _securityCaller.sendMailForChangePassword(new SecurityService(_connectioSting), sMTPDetails, customChangePassword.EmailID,body,authenticate.TenantId);
                    if (isUpdate)
                    {
                        objResponseModel.Status = true;
                        objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                        objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                        objResponseModel.ResponseData = "Mail sent successfully";
                    }
                    else
                    {
                        objResponseModel.Status = false;
                        objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                        objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                        objResponseModel.ResponseData = "Mail sent failure";
                    }
                }

                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.RecordNotFound);
                    objResponseModel.ResponseData = "Sorry User does not exist or active";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="customChangePassword"></param>
        /// <param name="IsStoreUser"></param>
        [HttpPost]
        [Route("ChangePassword")]
        public ResponseModel ChangePassword([FromBody] CustomChangePassword customChangePassword, int IsStoreUser=1)
        {
            
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                securityCaller _securityCaller = new securityCaller();
                CommonService commonService = new CommonService();
                
                if(customChangePassword.ChangePasswordType.Equals("mail"))
                {
                    customChangePassword.EmailID = SecurityService.DecryptStringAES(customChangePassword.EmailID);
                }
                customChangePassword.Password = SecurityService.Encrypt(customChangePassword.Password);
               
                
                bool Result = _securityCaller.ChangePassword(new SecurityService(_connectioSting), customChangePassword, authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
               Result == false ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// validateUserExist
        /// </summary>
        /// <param name="UserEmailID"></param>
        /// <param name="UserMobile"></param>
        [HttpPost]
        [Route("validateUserExist")]
        public ResponseModel ValidateUserExist(string UserEmailID, string UserMobile)
        {
            UserCaller userCaller = new UserCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));
                
                string result = userCaller.validateUserExist(new UserServices(_connectioSting), UserEmailID, UserMobile,authenticate.TenantId);
                StatusCode =
               string.IsNullOrEmpty(result) ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
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
        /// Delete User Profile
        /// </summary>
        /// <param name="IsStoreUser"></param>
        [HttpPost]
        [Route("DeleteUserProfile")]
        public ResponseModel DeleteUserProfile(int IsStoreUser = 1)
        {
            ResponseModel responseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.DeleteProfilePicture(new UserServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, IsStoreUser);

                StatusCode =
                Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                responseModel.Status = true;
                responseModel.StatusCode = StatusCode;
                responseModel.Message = statusMessage;
                responseModel.ResponseData = Result;


            }
            catch (Exception)
            {
                throw;
            }

            return responseModel;
        }

        #endregion
    }
}