using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
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
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string ProfileImg_Resources;
        private readonly string ProfileImg_Image;
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
        }
        #endregion

        #region Custom Methods 
        [HttpPost]
        [Route("GetUserList")]
        public ResponseModel GetUserList( )
        {
            List<User> objUserList = new List<User>();
           
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                MasterCaller _newMasterBrand = new MasterCaller();

                objUserList = _newMasterBrand.GetUserList(new UserServices(_connectioSting), authenticate.TenantId,authenticate.UserMasterID);

                StatusCode =
                objUserList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objUserList;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }
        /// <summary>
        /// AddUserPersonalDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("AddUserPersonalDetail")]
        public ResponseModel AddUserPersonalDetail([FromBody] UserModel userModel )
        {         
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();
                userModel.CreatedBy = authenticate.UserMasterID;
                userModel.TenantID = authenticate.TenantId;
                 int Result = userCaller.AddUserPersonaldetail(new UserServices(_connectioSting), userModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }
        /// <summary>
        /// AddUserProfileDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("AddUserProfileDetail")]
        public ResponseModel AddUserProfileDetail(int DesignationID, int ReportTo, int UserID,int IsStoreUser=1)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.AddUserProfiledetail(new UserServices(_connectioSting), DesignationID, ReportTo, authenticate.UserMasterID, authenticate.TenantId, UserID, IsStoreUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// AddUserProfileDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("Mapcategory")]
        public ResponseModel Mapcategory([FromBody] CustomUserModel customUserModel)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();
                customUserModel.CreatedBy = authenticate.UserMasterID;
                customUserModel.TenantID = authenticate.TenantId;
                int Result = userCaller.Mappedcategory(new UserServices(_connectioSting), customUserModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// EditUserDetails
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("EditUserDetails")]
        public ResponseModel EditUserDetails([FromBody]CustomEditUserModel customEditUserModel)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();
                customEditUserModel.TenantID = authenticate.TenantId;
                customEditUserModel.CreatedBy = authenticate.UserMasterID;
                int Result = userCaller.EditUserDetail(new UserServices(_connectioSting), customEditUserModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("DeleteUser")]
        public ResponseModel DeleteUser(int userID,int IsStoreUser=1)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();

                int Result = userCaller.DeleteUser(new UserServices(_connectioSting), userID,authenticate.TenantId, authenticate.UserMasterID, IsStoreUser);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordInUse : (int)EnumMaster.StatusCode.RecordDeletedSuccess;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
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

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();

                objUserList = userCaller.UserList(new UserServices(_connectioSting), authenticate.TenantId, IsStoreUser);

                StatusCode =
                objUserList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objUserList;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
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

            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();

                objUser = userCaller.GetuserDetailsById(new UserServices(_connectioSting), UserID,authenticate.TenantId, IsStoreUser);

                StatusCode =
                objUser == null ? 
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objUser;

            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
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
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false; bool successfilesaved = false;
            int count = 0;
            UserCaller userCaller = new UserCaller();
            SettingsCaller fileU = new SettingsCaller();
            ResponseModel _objResponseModel = new ResponseModel();
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

                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                #region FilePath
                BulkUploadFilesPath = rootPath + "\\" + "BulkUpload\\UploadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor);
                DownloadFilePath = rootPath + "\\" + "BulkUpload\\DownloadFiles" + "\\" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)UserFor);

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
                            FileStream docFile = new FileStream(BulkUploadFilesPath + "\\" + filesName[i], FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();

                        }
                    }
                }



                #endregion

                DataSetCSV = CommonService.csvToDataSet(BulkUploadFilesPath + "\\" + filesName[0]);
                //DataSetCSV = CommonService.csvToDataSet("D:\\TP\\UserBulk.csv");
                CSVlist = userCaller.UserBulkUpload(new UserServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, UserFor, DataSetCSV);


                #region Create Error and Succes files and  Insert in FileUploadLog

                if (!string.IsNullOrEmpty(CSVlist[0]))
                {
                    if (CSVlist[0].ToLower().Contains("username"))
                    {
                        successfilename = "UserSuccessFile.csv";
                        successfilepath = DownloadFilePath + "\\User\\ Success" + "\\" + successfilename;
                        successfilesaved = CommonService.SaveFile(successfilepath, CSVlist[0]);

                    }
                    else
                    {
                        successfilename = "MappedCategorySuccessFile.csv";
                        successfilepath = DownloadFilePath + "\\MappedCategory\\ Success" + "\\" + successfilename;
                        successfilesaved = CommonService.SaveFile(successfilepath, CSVlist[0]);

                    }
                }
                if (!string.IsNullOrEmpty(CSVlist[1]))
                {
                    if (CSVlist[1].ToLower().Contains("username"))
                    {
                        errorfilename = "UserErrorFile.csv";
                        errorfilepath = DownloadFilePath + "\\User\\ Error" + "\\" + errorfilename;
                        errorfilesaved = CommonService.SaveFile(errorfilepath, CSVlist[0]);
                    }
                    else
                    {
                        errorfilename = "MappedCategoryErrorFile.csv";
                        errorfilepath = DownloadFilePath + "\\MappedCategory\\ Error" + "\\" + errorfilename;
                        errorfilesaved = CommonService.SaveFile(errorfilepath, CSVlist[0]);

                    }

                }


                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(DownloadFilePath + "\\User\\Error" + "\\" + "UserErrorFile.csv", CSVlist[1]);

                count = fileU.CreateFileUploadLog(new FileUploadService(_connectioSting), authenticate.TenantId, filesName[0], successfilesaved,
                                   errorfilename, successfilename, authenticate.UserMasterID, "User", errorfilepath, successfilepath, UserFor);
                #endregion

                StatusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = count;

            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;


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
            UpdateUserProfiledetailsModel UpdateUserProfiledetailsModel = new UpdateUserProfiledetailsModel();

            var Keys = Request.Form;
            UpdateUserProfiledetailsModel = JsonConvert.DeserializeObject<UpdateUserProfiledetailsModel>(Keys["UpdateUserProfiledetailsModel"]);
            var file = Request.Form.Files;
            
            var folderName = Path.Combine(ProfileImg_Resources, ProfileImg_Image);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            try
            {
                if (file.Count > 0)
                {

                    var fileName = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                    var fileName_Id = fileName.Replace(".", UpdateUserProfiledetailsModel.UserId + ".") + "";
                    var fullPath = Path.Combine(pathToSave, fileName_Id);
                    var dbPath = Path.Combine(folderName, fileName_Id);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file[0].CopyTo(stream);
                    }
                    UpdateUserProfiledetailsModel.ProfilePicture = fileName_Id;

                }
            }
            catch (Exception ex) { }
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();
                int Result = userCaller.UpdateUserProfileDetail(new UserServices(_connectioSting), UpdateUserProfiledetailsModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                string url = configuration.GetValue<string>("APIURL") + ProfileImg_Resources+"/" +ProfileImg_Image;
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                UserCaller userCaller = new UserCaller();
                objUserList = userCaller.GetUserProfileDetails(new UserServices(_connectioSting), authenticate.UserMasterID, url);
                StatusCode =
               objUserList.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objUserList;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }

        /// <summary>
        /// EditUserPersonalDetail
        /// </summary>
        /// <param name="UserModel"></param>
        [HttpPost]
        [Route("EditUserPersonalDetail")]
        public ResponseModel EditUserPersonalDetail([FromBody] UserModel userModel)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

                UserCaller userCaller = new UserCaller();
                userModel.CreatedBy = authenticate.UserMasterID;
                userModel.TenantID = authenticate.TenantId;
                int Result = userCaller.EditUserPersonaldetail(new UserServices(_connectioSting), userModel);

                StatusCode =
               Result == 0 ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        [HttpPost]
        [Route("SendMailforchangepassword")]
        public ResponseModel SendMailforchangepassword (int userID, int IsStoreUser = 1)
        {
            CustomChangePassword customChangePassword = new CustomChangePassword();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

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
                    string body = "Dear User, Please find the below details.  <br/><br/>" + "Your User Name  : " + customChangePassword.EmailID + "<br/>" + "Your Password : " + decriptedPassword + "<br/><br/>" + "Click on Below link to change the Password <br/>" + url + "?Id:" + encryptedEmailId;
                    bool isUpdate = _securityCaller.sendMailForChangePassword(new SecurityService(_connectioSting), sMTPDetails, customChangePassword.EmailID,body,authenticate.TenantId);
                    if (isUpdate)
                    {
                        _objResponseModel.Status = true;
                        _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                        _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                        _objResponseModel.ResponseData = "Mail sent successfully";
                    }
                    else
                    {
                        _objResponseModel.Status = false;
                        _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                        _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                        _objResponseModel.ResponseData = "Mail sent failure";
                    }
                }

                else
                {
                    _objResponseModel.Status = false;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.RecordNotFound);
                    _objResponseModel.ResponseData = "Sorry User does not exist or active";
                }
                /* StatusCode =
                isUpdate !=true ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                 statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                 _objResponseModel.Status = true;
                 _objResponseModel.StatusCode = StatusCode;
                 _objResponseModel.Message = statusMessage;
                 _objResponseModel.ResponseData = "Email Sent";*/


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("ChangePassword")]
        public ResponseModel ChangePassword([FromBody] CustomChangePassword customChangePassword, int IsStoreUser=1)
        {
            
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
               // CustomChangePassword CustomChangePassword = new CustomChangePassword();
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
               // UserCaller userCaller = new UserCaller();
               // CustomChangePassword = userCaller.SendMailforchangepassword(new UserServices(_connectioSting), customChangePassword.UserID, authenticate.TenantId, IsStoreUser);

                securityCaller _securityCaller = new securityCaller();
                CommonService commonService = new CommonService();
                customChangePassword.Password = SecurityService.Encrypt(customChangePassword.Password);
                customChangePassword.NewPassword = SecurityService.Encrypt(customChangePassword.NewPassword);
                customChangePassword.EmailID = SecurityService.DecryptStringAES(customChangePassword.EmailID);
                bool Result = _securityCaller.ChangePassword(new SecurityService(_connectioSting), customChangePassword, authenticate.TenantId, authenticate.UserMasterID);

                StatusCode =
               Result == false ?
                      (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = Result;


            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }

            return _objResponseModel;
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
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
                
                string result = userCaller.validateUserExist(new UserServices(_connectioSting), UserEmailID, UserMobile,authenticate.TenantId);
                StatusCode =
               string.IsNullOrEmpty(result) ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = result;
            }
            catch (Exception ex)
            {
                StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = null;
            }
            return _objResponseModel;
        }


        #endregion
    }
}