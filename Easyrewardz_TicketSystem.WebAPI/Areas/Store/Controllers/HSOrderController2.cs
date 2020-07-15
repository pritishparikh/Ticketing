using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{

    public partial class HSOrderController : ControllerBase
    {
        /// <summary>
        /// GetOrdersDetails
        /// </summary>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrdersDetails")]
        public ResponseModel GetOrdersDetails(OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails orderResponseDetails = new OrderResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails = storecampaigncaller.GetOrdersDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, ordersDataRequest);
                statusCode =
                   orderResponseDetails.TotalCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetShoppingBagDetails
        /// </summary>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShoppingBagDetails")]
        public ResponseModel GetShoppingBagDetails(OrdersDataRequest ordersDataRequest)
        {
            ShoppingBagResponseDetails shoppingBagResponseDetails = new ShoppingBagResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shoppingBagResponseDetails = storecampaigncaller.GetShoppingBagDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, ordersDataRequest);
                statusCode =
                   shoppingBagResponseDetails.TotalShoppingBag.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shoppingBagResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShoppingBagDeliveryType")]
        public ResponseModel GetShoppingBagDeliveryType(int pageID)
        {
            List<ShoppingBagDeliveryFilter> shoppingBagDeliveryFilter = new List<ShoppingBagDeliveryFilter>();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shoppingBagDeliveryFilter = storecampaigncaller.GetShoppingBagDeliveryType(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, pageID);
                statusCode =
                   shoppingBagDeliveryFilter.Count.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shoppingBagDeliveryFilter;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetShipmentDetails")]
        public ResponseModel GetShipmentDetails(OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails orderResponseDetails = new OrderResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails = storecampaigncaller.GetShipmentDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, ordersDataRequest);
                statusCode =
                   orderResponseDetails.TotalCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetOrderTabSettingDetails
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderTabSettingDetails")]
        public ResponseModel GetOrderTabSettingDetails()
        {
            OrderTabSetting orderResponseDetails = new OrderTabSetting();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails = storecampaigncaller.GetOrderTabSettingDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   orderResponseDetails.Exists.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = orderResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// SetOrderHasBeenReturn
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetOrderHasBeenReturn")]
        public ResponseModel SetOrderHasBeenReturn(int orderID)
        {
            int UpdateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = storecampaigncaller.SetOrderHasBeenReturn(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderID);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="shippingTemplateReques"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderShippingTemplate")]
        public ResponseModel GetOrderShippingTemplate(ShippingTemplateRequest shippingTemplateReques)
        {
            ShippingTemplateDetails shippingTemplateDetails = new ShippingTemplateDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shippingTemplateDetails = storecampaigncaller.GetOrderShippingTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, shippingTemplateReques);
                statusCode =
                   shippingTemplateDetails.ShippingTemplateList.Count < 1 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shippingTemplateDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// InsertUpdateOrderShippingTemplate
        /// </summary>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertUpdateOrderShippingTemplate")]
        public ResponseModel InsertUpdateOrderShippingTemplate(AddEditShippingTemplate addEditShippingTemplate)
        {
            int result = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                result = storecampaigncaller.InsertUpdateOrderShippingTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, addEditShippingTemplate);
                statusCode =
                   result.Equals(-1) ?
                           (int)EnumMaster.StatusCode.RecordAlreadyExists :
                           result.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound :
                           (int)EnumMaster.StatusCode.Success;

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
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderShippingTemplateName")]
        public ResponseModel GetOrderShippingTemplateName()
        {
            ShippingTemplateDetails shippingTemplateDetails = new ShippingTemplateDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shippingTemplateDetails = storecampaigncaller.GetOrderShippingTemplateName(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   shippingTemplateDetails.ShippingTemplateList.Count < 1 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = shippingTemplateDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// SetOrderHasBeenSelfPickUp
        /// </summary>
        /// <param name="orderSelfPickUp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetOrderHasBeenSelfPickUp")]
        public ResponseModel SetOrderHasBeenSelfPickUp([FromBody]OrderSelfPickUp orderSelfPickUp)
        {
            int UpdateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                UpdateCount = storecampaigncaller.SetOrderHasBeenSelfPickUp(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, orderSelfPickUp);
                statusCode =
                   UpdateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = UpdateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetCourierPartnerFilter
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCourierPartnerFilter")]
        public ResponseModel GetCourierPartnerFilter(int pageID)
        {
            List<string> CourierPartnerFilter = new List<string>();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                CourierPartnerFilter = storecampaigncaller.GetCourierPartnerFilter(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, pageID);
                statusCode =
                   CourierPartnerFilter.Count.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = CourierPartnerFilter;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// BulkUploadOrderTemplate
        /// </summary>
        /// <param name="UserFor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadOrderTemplate")]
        public ResponseModel BulkUploadOrderTemplate(int OrderTemplate = 5)
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false; bool successfilesaved = false;
            int count = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
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
                else
                {
                    objResponseModel.Status = false;
                    objResponseModel.StatusCode = StatusCode;
                    objResponseModel.Message = "Please upload File";
                    objResponseModel.ResponseData = 0;
                }
                var Keys = Request.Form;


                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                filesName = finalAttchment.Split(",");

                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate));


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
                CSVlist = storecampaigncaller.BulkUploadOrderTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, OrderTemplate, DataSetCSV);


                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "Store_OrderTemplateFile_" + timeStamp + ".csv";
                string ErrorFileName = "Store_OrderTemplateFile_" + timeStamp + ".csv";


                if (!string.IsNullOrEmpty(CSVlist[0]))
                {
                    if (!CSVlist[0].ToLower().Contains("username"))
                        SuccessFileName = "Store_OrderTemplateSuccessFile_" + timeStamp + ".csv";

                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);
                }
                if (!string.IsNullOrEmpty(CSVlist[1]))
                {
                    if (!CSVlist[1].ToLower().Contains("username"))
                        ErrorFileName = "Store_OrderTemplateErrorFile" + timeStamp + ".csv";

                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);
                }

                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate) + "/Error/" + ErrorFileName : string.Empty;

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectionString), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Store_OrderTemplate", SuccessFileUrl, ErrorFileUrl, OrderTemplate);
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
    }
}