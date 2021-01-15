using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

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
        public async Task<ResponseModel> GetOrdersDetails(OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails orderResponseDetails = new OrderResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
               // string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails = await storecampaigncaller.GetOrdersDetails(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> GetShoppingBagDetails(OrdersDataRequest ordersDataRequest)
        {
            ShoppingBagResponseDetails shoppingBagResponseDetails = new ShoppingBagResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
               // string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shoppingBagResponseDetails = await storecampaigncaller.GetShoppingBagDetails(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> GetShoppingBagDeliveryType(int pageID)
        {
            List<ShoppingBagDeliveryFilter> shoppingBagDeliveryFilter = new List<ShoppingBagDeliveryFilter>();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shoppingBagDeliveryFilter =await storecampaigncaller.GetShoppingBagDeliveryType(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> GetShipmentDetails(OrdersDataRequest ordersDataRequest)
        {
            OrderResponseDetails orderResponseDetails = new OrderResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails = await storecampaigncaller.GetShipmentDetails(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> GetOrderTabSettingDetails()
        {
            OrderTabSetting orderResponseDetails = new OrderTabSetting();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                orderResponseDetails =await storecampaigncaller.GetOrderTabSettingDetails(new HSOrderService(_connectionString),
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
        public async Task<ResponseModel> SetOrderHasBeenReturn(int orderID)
        {
            int updateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                updateCount = await storecampaigncaller.SetOrderHasBeenReturn(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, orderID);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updateCount;
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
        public async Task<ResponseModel> GetOrderShippingTemplate(ShippingTemplateRequest shippingTemplateReques)
        {
            ShippingTemplateDetails shippingTemplateDetails = new ShippingTemplateDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shippingTemplateDetails =await storecampaigncaller.GetOrderShippingTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, shippingTemplateReques);
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
        public async Task<ResponseModel> InsertUpdateOrderShippingTemplate(AddEditShippingTemplate addEditShippingTemplate)
        {
            int result = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                result =await storecampaigncaller.InsertUpdateOrderShippingTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, addEditShippingTemplate);
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
        public async Task<ResponseModel> GetOrderShippingTemplateName()
        {
            ShippingTemplateDetails shippingTemplateDetails = new ShippingTemplateDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                shippingTemplateDetails = await storecampaigncaller.GetOrderShippingTemplateName(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID);
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
        public async Task<ResponseModel> SetOrderHasBeenSelfPickUp([FromBody]OrderSelfPickUp orderSelfPickUp)
        {
            int updateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                updateCount = await storecampaigncaller.SetOrderHasBeenSelfPickUp(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, orderSelfPickUp);
                statusCode =
                   updateCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updateCount;
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
        public async Task<ResponseModel> GetCourierPartnerFilter(int pageID)
        {
            List<string> courierPartnerFilter = new List<string>();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                courierPartnerFilter = await storecampaigncaller.GetCourierPartnerFilter(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, pageID);
                statusCode =
                   courierPartnerFilter.Count.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = courierPartnerFilter;
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
        public async Task<ResponseModel> BulkUploadOrderTemplate(int OrderTemplate = 5)
        {
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false; bool successfilesaved = false;
            int count = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            StoreFileUploadCaller fileU = new StoreFileUploadCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
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
                    objResponseModel.StatusCode = statusCode;
                    objResponseModel.Message = "Please upload File";
                    objResponseModel.ResponseData = 0;

                    return objResponseModel;
                }
                var Keys = Request.Form;


               // string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                #region FilePath
                string Folderpath = Directory.GetCurrentDirectory();
                filesName = finalAttchment.Split(",");

                BulkUploadFilesPath = Path.Combine(Folderpath, _BulkUpload, _UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate));
                DownloadFilePath = Path.Combine(Folderpath, _BulkUpload, _DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate));


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
                CSVlist = await storecampaigncaller.BulkUploadOrderTemplate(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, OrderTemplate, DataSetCSV);


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
                  _rootPath + _BulkUpload + "/" + _DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    _rootPath + _BulkUpload + "/" + _DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)OrderTemplate) + "/Error/" + ErrorFileName : string.Empty;

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectionString), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Store_OrderTemplate", SuccessFileUrl, ErrorFileUrl, OrderTemplate);
                #endregion

                statusCode = count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        /// GetOrderCountry
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderCountry")]
        public async Task<ResponseModel> GetOrderCountry()
        {
            List<OrderCountry> listOrderCountries = new List<OrderCountry>();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
               // string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                // authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                listOrderCountries =await storecampaigncaller.GetOrderCountry(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID);
                statusCode =
                   listOrderCountries.Count.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = listOrderCountries;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// InsertModifyOrderCountry
        /// </summary>
        /// <param name="modifyOrderCountryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertModifyOrderCountry")]
        public async Task<ResponseModel> InsertModifyOrderCountry(ModifyOrderCountryRequest modifyOrderCountryRequest)
        {
            int updateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                updateCount = await storecampaigncaller.InsertModifyOrderCountry(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, modifyOrderCountryRequest);
                statusCode =
                   updateCount.Equals(0) ?
                    modifyOrderCountryRequest.IsDelete ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.RecordAlreadyExists:
                         (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetCustAddressDetails
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustAddressDetails")]
        public async Task<ResponseModel> GetCustAddressDetails(int orderId)
        {
            CustAddressDetails custAddressDetails = new CustAddressDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                custAddressDetails =await storecampaigncaller.GetCustAddressDetails(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, orderId);
                statusCode = (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = custAddressDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetPrintLabelDetails
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPrintLabelDetails")]
        public async Task<ResponseModel> GetPrintLabelDetails(int orderId)
        {
            PrintLabelDetails printLabelDetails = new PrintLabelDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                //string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];
                //authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                printLabelDetails = await storecampaigncaller.GetPrintLabelDetails(new HSOrderService(_connectionString), authenticate.TenantId, authenticate.UserMasterID, orderId);
                statusCode = (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = printLabelDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// GetSelfPickUpOrdersDetails
        /// </summary>
        /// <param name="selfPickupOrdersDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSelfPickUpOrdersDetails")]
        public async Task<ResponseModel> GetSelfPickUpOrdersDetails(SelfPickupOrdersDataRequest selfPickupOrdersDataRequest)
        {
            SelfPickupOrderResponseDetails selfPickupOrderResponseDetails = new SelfPickupOrderResponseDetails();
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];

                selfPickupOrderResponseDetails = await storecampaigncaller.GetSelfPickUpOrdersDetails(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, selfPickupOrdersDataRequest);
                statusCode =
                   selfPickupOrderResponseDetails.TotalCount.Equals(0) ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = selfPickupOrderResponseDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// UpdateShoppingAvailableQty
        /// </summary>
        /// <param name="requestShoppingAvailableQty"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateShoppingAvailableQty")]
        public async Task<ResponseModel> UpdateShoppingAvailableQty(RequestShoppingAvailableQty requestShoppingAvailableQty)
        {
            int updateCount = 0;
            HSOrderCaller storecampaigncaller = new HSOrderCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                Authenticate authenticate = new Authenticate();
                authenticate = (Authenticate)HttpContext.Items["Authenticate"];

                WebBotContentRequest webBotcontentRequest = new WebBotContentRequest
                {
                    TenantID = authenticate.TenantId,
                    ProgramCode = authenticate.ProgramCode,
                    UserID = authenticate.UserMasterID,
                    MakeBellActiveUrl = _ClientAPIUrl + _makeBellActive,
                    ClientAPIUrl = _ClientAPIUrl + _sendCampaign,
                    WeBBotGenerationLink = _WebBotUrl + _getWebBotLink,
                    MaxWebBotHSMURL = _MaxHSMURL + _MaxWeBotHSM,
                   // CustomerName = shoppingAvailableQty.CustomerName,
                   // MobileNo = shoppingAvailableQty.CustomerMob.Replace(" ", "").Replace("+", ""),
                    OptionID = 6,
                   // ShopingBagNo = shoppingAvailableQty.ShoppingID
                };

                WebBotHSMSetting webBotHSMSetting = new WebBotHSMSetting();
                webBotHSMSetting = _hsmProgramcode.Find(x => x.Programcode.Equals(authenticate.ProgramCode));
                if (webBotHSMSetting != null)
                {
                    webBotcontentRequest.webBotHSMSetting = new WebBotHSMSetting();
                    if (!string.IsNullOrEmpty(webBotHSMSetting.Programcode))
                    {
                        webBotcontentRequest.MaxWebBotHSMURL = webBotcontentRequest.MaxWebBotHSMURL + "?bot=" + webBotHSMSetting.bot;
                        webBotcontentRequest.webBotHSMSetting = webBotHSMSetting;
                        MaxWebBotHSMRequest maxreq = new MaxWebBotHSMRequest();
                        Hsm hsmReq = new Hsm()
                        {
                            @namespace = webBotHSMSetting.@namespace,
                            language = new Language() { policy = _policy, code = _code }
                        };
                        Body body = new Body()
                        {
                            // from = _from,
                            ttl = Convert.ToInt32(_ttl),
                            type = _type,
                            hsm = hsmReq
                        };
                        maxreq.body = body;

                        webBotcontentRequest.MaxHSMRequest = maxreq;
                    }
                }


                updateCount = await storecampaigncaller.UpdateShoppingAvailableQty(new HSOrderService(_connectionString),
                    authenticate.TenantId, authenticate.UserMasterID, requestShoppingAvailableQty.shoppingAvailableQty, webBotcontentRequest);
                statusCode =
                   updateCount < 0 ?
                           (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = updateCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

    }
}