using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    /// <summary>
    /// User controller to manage User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
 [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class UserController : ControllerBase
    {
        #region  Variable Declaration
        private IConfiguration Configuration;
        private readonly string rootPath;
        private readonly IDistributedCache Cache;
        internal static TicketDBContext Db { get; set; }
        private readonly string ProfileImg_Resources;
        private readonly string ProfileImg_Image;
        #endregion

        #region Constructor
        /// <summary>
        /// User Controller
        /// </summary>
        /// <param name="_iConfig"></param>
        public UserController(IConfiguration _iConfig, IDistributedCache cache, TicketDBContext db)
        {
            Configuration = _iConfig;
            rootPath = Configuration.GetValue<string>("APIURL");
            ProfileImg_Resources = Configuration.GetValue<string>("ProfileImg_Resources");
            ProfileImg_Image = Configuration.GetValue<string>("ProfileImg_Image");
            Cache = cache;
            Db = db;
        }
        #endregion

        #region Custom Methods 
        /// <summary>
        /// Get user list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserList")]
        public ResponseModel GetUserList( )
        {
            List<User> objUserList = new List<User>();
           
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                MasterCaller newMasterBrand = new MasterCaller();

                objUserList = newMasterBrand.GetUserList(new UserServices(Cache, Db), authenticate.TenantId,authenticate.UserMasterID);

                statusCode =
                objUserList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// AddUserPersonalDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("AddUserPersonalDetail")]
        public ResponseModel AddUserPersonalDetail([FromBody] UserModel userModel )
        {         
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                userModel.CreatedBy = authenticate.UserMasterID;
                userModel.TenantID = authenticate.TenantId;
                 int result = userCaller.AddUserPersonaldetail(new UserServices(Cache, Db), userModel);

                statusCode =
               result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// AddUserProfileDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("AddUserProfileDetail")]
        public ResponseModel AddUserProfileDetail(int DesignationID, int ReportTo, int UserID,int IsStoreUser=1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.AddUserProfiledetail(new UserServices(Cache, Db), DesignationID, ReportTo, authenticate.UserMasterID, authenticate.TenantId, UserID, IsStoreUser);

                statusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// AddUserProfileDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("Mapcategory")]
        public ResponseModel Mapcategory([FromBody] CustomUserModel customUserModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                customUserModel.CreatedBy = authenticate.UserMasterID;
                customUserModel.TenantID = authenticate.TenantId;
                int Result = userCaller.Mappedcategory(new UserServices(Cache, Db), customUserModel);

                statusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// EditUserDetails
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("EditUserDetails")]
        public ResponseModel EditUserDetails([FromBody]CustomEditUserModel customEditUserModel)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                customEditUserModel.TenantID = authenticate.TenantId;
                customEditUserModel.CreatedBy = authenticate.UserMasterID;
                int Result = userCaller.EditUserDetail(new UserServices(Cache, Db), customEditUserModel);

                statusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("DeleteUser")]
        public ResponseModel DeleteUser(int userID,int IsStoreUser=1)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                int Result = userCaller.DeleteUser(new UserServices(Cache, Db), userID,authenticate.TenantId, authenticate.UserMasterID, IsStoreUser);

                statusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get User List Data
        /// </summary>
        /// <param name=""></param>
        [HttpGet]
        [Route("GetUserListData")]
        public ResponseModel GetUserListData(int IsStoreUser=1)
        {
            List<CustomUserList> objUserList = new List<CustomUserList>();

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                objUserList = userCaller.UserList(new UserServices(Cache, Db), authenticate.TenantId, IsStoreUser);

                statusCode =
                objUserList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get User Details By Id
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("GetUserDetailsById")]
        public ResponseModel GetUserDetailsById(int UserID,int IsStoreUser=1)
        {
            CustomUserList objUser  = new CustomUserList();

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                objUser = userCaller.GetuserDetailsById(new UserServices(Cache, Db), UserID,authenticate.TenantId, IsStoreUser);

                statusCode =
                objUser == null ? 
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUser;

            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Bulk Upload User
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadUser")]
        public ResponseModel BulkUploadUser(int UserFor)
        {
            string downloadFilePath = string.Empty;
            string bulkUploadFilesPath = string.Empty;
            bool errorFileSaved = false; bool successfilesaved = false;
            int count = 0;
            UserCaller userCaller = new UserCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = ""; string fileName = ""; string finalAttchment = "";
            string timeStamp = DateTime.Now.ToString("ddmmyyyyhhssfff");
            DataSet dataSetCSV = new DataSet();
            string[] filesName = null;
            List<string> CSVlist = new List<string>();
            string successFileName = string.Empty, errorFileName = string.Empty; string errorFilePath = string.Empty; string successFilePath = string.Empty;


            try
            {
                var files = Request.Form.Files;

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                #region FilePath
                bulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor);
                downloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor);

                #endregion

                #region Read from Form

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", timeStamp + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }

                //var exePath = Path.GetDirectoryName(System.Reflection
                //     .Assembly.GetExecutingAssembly().CodeBase);
                //Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                //var appRoot = appPathMatcher.Match(exePath).Value;
                //string Folderpath = appRoot + "\\" + "UploadFiles"+"\\"+ CommonFunction.GetEnumDescription((EnumMaster.FIleUpload)HierarchyFor);



                if (files.Count > 0)
                {
                    filesName = finalAttchment.Split(",");
                    for (int i = 0; i < files.Count; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(bulkUploadFilesPath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();

                        }
                    }
                }



                #endregion

                dataSetCSV = CommonService.csvToDataSet(bulkUploadFilesPath + "\\" + filesName[0]);
                //dataSetCSV = CommonService.csvToDataSet("D:\\TP\\UserBulk.csv");
                CSVlist = userCaller.UserBulkUpload(new UserServices(Cache, Db), authenticate.TenantId, authenticate.UserMasterID, UserFor, dataSetCSV);


                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                {
                    if (CSVlist[0].ToLower().Contains("username"))
                    {
                        successFileName = "UserSuccessFile.csv";
                        successFilePath = downloadFilePath + "\\User\\ Success" + "\\" + successFileName;
                        successfilesaved = CommonService.SaveFile(successFilePath, CSVlist[0]);

                    }
                    else
                    {
                        successFileName = "MappedCategorySuccessFile.csv";
                        successFilePath = downloadFilePath + "\\MappedCategory\\ Success" + "\\" + successFileName;
                        successfilesaved = CommonService.SaveFile(successFilePath, CSVlist[0]);

                    }
                }
                if (!string.IsNullOrEmpty(CSVlist[1]))
                {
                    if (CSVlist[1].ToLower().Contains("username"))
                    {
                        errorFileName = "UserErrorFile.csv";
                        errorFilePath = downloadFilePath + "\\User\\ Error" + "\\" + errorFileName;
                        errorFileSaved = CommonService.SaveFile(errorFilePath, CSVlist[0]);
                    }
                    else
                    {
                        errorFileName = "MappedCategoryErrorFile.csv";
                        errorFilePath = downloadFilePath + "\\MappedCategory\\ Error" + "\\" + errorFileName;
                        errorFileSaved = CommonService.SaveFile(errorFilePath, CSVlist[0]);

                    }

                }


                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorFileSaved = CommonService.SaveFile(downloadFilePath + "\\User\\Error" + "\\" + "UserErrorFile.csv", CSVlist[1]);

                count = fileU.CreateFileUploadLog(new FileUploadService(Cache, Db), authenticate.TenantId, filesName[0], successfilesaved,
                                   errorFileName, successFileName, authenticate.UserMasterID, "User", errorFilePath, successFilePath, UserFor);
                #endregion

                statusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;


        }

        /// <summary>
        /// UpdateUserProfiledetails
        /// </summary>
        /// <param name="UpdateUserProfiledetailsModel"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("UpdateUserProfileDetails")]
        public ResponseModel UpdateUserProfileDetails(IFormFile File)
        {
            UpdateUserProfiledetailsModel updateUserProfiledetailsModel = new UpdateUserProfiledetailsModel();

            var keys = Request.Form;
            updateUserProfiledetailsModel = JsonConvert.DeserializeObject<UpdateUserProfiledetailsModel>(keys["UpdateUserProfiledetailsModel"]);
            var file = Request.Form.Files;
            
            var folderName = Path.Combine(ProfileImg_Resources, ProfileImg_Image);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            try
            {
                if (file.Count > 0)
                {

                    var fileName = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                    var fileName_Id = fileName.Replace(".", updateUserProfiledetailsModel.UserId + ".") + "";
                    var fullPath = Path.Combine(pathToSave, fileName_Id);
                    var dbPath = Path.Combine(folderName, fileName_Id);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file[0].CopyTo(stream);
                    }
                    updateUserProfiledetailsModel.ProfilePicture = fileName_Id;

                }
            }
            catch (Exception ex) { }
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.UpdateUserProfileDetail(new UserServices(Cache, Db), updateUserProfiledetailsModel);

                statusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
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
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                string url = Configuration.GetValue<string>("APIURL") + ProfileImg_Resources+"/" +ProfileImg_Image;
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                UserCaller userCaller = new UserCaller();
                objUserList = userCaller.GetUserProfileDetails(new UserServices(Cache, Db), authenticate.UserMasterID, url);
                statusCode =
               objUserList.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objUserList;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
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
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                userModel.CreatedBy = authenticate.UserMasterID;
                userModel.TenantID = authenticate.TenantId;
                int Result = userCaller.EditUserPersonaldetail(new UserServices(Cache, Db), userModel);

                statusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Send mail for change password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IsStoreUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendMailforchangepassword")]
        public ResponseModel SendMailforchangepassword (int userID, int IsStoreUser = 1)
        {
            CustomChangePassword customChangePassword = new CustomChangePassword();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();

                customChangePassword = userCaller.SendMailforchangepassword(new UserServices(Cache, Db), userID, authenticate.TenantId, IsStoreUser);
                if(customChangePassword.UserID >0 && customChangePassword.Password!=null && customChangePassword.EmailID !=null)
                {
                    MasterCaller masterCaller = new MasterCaller();
                    SMTPDetails sMTPDetails = masterCaller.GetSMTPDetails(new MasterServices(Cache, Db), authenticate.TenantId);
                    securityCaller securityCaller = new securityCaller();
                    CommonService commonService = new CommonService();
                    string encryptedEmailId = SecurityService.Encrypt(customChangePassword.EmailID);

                    string decriptedPassword = SecurityService.DecryptStringAES(customChangePassword.Password);
                    string url = Configuration.GetValue<string>("websiteURL") + "/ChangePassword";
                    string body = "Dear User, Please find the below details.  <br/><br/>" + "Your User Name  : " + customChangePassword.EmailID + "<br/>" + "Your Password : " + decriptedPassword + "<br/><br/>" + "Click on Below link to change the Password <br/>" + url + "?Id:" + encryptedEmailId;
                    bool isUpdate = securityCaller.sendMailForChangePassword(new SecurityService(Cache,Db), sMTPDetails, customChangePassword.EmailID,body,authenticate.TenantId);
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
                /* StatusCode =
                isUpdate !=true ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                 statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                 objResponseModel.Status = true;
                 objResponseModel.StatusCode = StatusCode;
                 objResponseModel.Message = statusMessage;
                 objResponseModel.ResponseData = "Email Sent";*/


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("ChangePassword")]
        public ResponseModel ChangePassword([FromBody] CustomChangePassword customChangePassword, int IsStoreUser=1)
        {
            
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
               // CustomChangePassword CustomChangePassword = new CustomChangePassword();
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
               // UserCaller userCaller = new UserCaller();
               // CustomChangePassword = userCaller.SendMailforchangepassword(new UserServices(_connectioSting), customChangePassword.UserID, authenticate.TenantId, IsStoreUser);

                securityCaller securityCaller = new securityCaller();
                CommonService commonService = new CommonService();
                customChangePassword.Password = SecurityService.Encrypt(customChangePassword.Password);
                customChangePassword.NewPassword = SecurityService.Encrypt(customChangePassword.NewPassword);
                customChangePassword.EmailID = SecurityService.DecryptStringAES(customChangePassword.EmailID);
                bool Result = securityCaller.ChangePassword(new SecurityService(Cache,Db), customChangePassword, authenticate.TenantId, authenticate.UserMasterID);

                statusCode =
               Result == false ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }

            return objResponseModel;
        }

        /// <summary>
        /// validateUserExist
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("validateUserExist")]
        public ResponseModel validateUserExist(string UserEmailID, string UserMobile)
        {
            UserCaller userCaller = new UserCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
                
                string result = userCaller.validateUserExist(new UserServices(Cache, Db), UserEmailID, UserMobile,authenticate.TenantId);
                statusCode =
               string.IsNullOrEmpty(result) ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = result;
            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = null;
            }
            return objResponseModel;
        }

        /// <summary>
        /// DeleteUserProfile
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("DeleteUserProfile")]
        public ResponseModel DeleteUserProfile(int IsStoreUser = 1)
        {
            ResponseModel responseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.DeleteProfilePicture(new UserServices(Cache, Db), authenticate.TenantId, authenticate.UserMasterID, IsStoreUser);

                statusCode =
                Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                responseModel.Status = true;
                responseModel.StatusCode = statusCode;
                responseModel.Message = statusMessage;
                responseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                statusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                responseModel.Status = true;
                responseModel.StatusCode = statusCode;
                responseModel.Message = statusMessage;
                responseModel.ResponseData = null;
            }

            return responseModel;
        }
        #endregion
    }
}