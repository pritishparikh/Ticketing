﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public partial class AppointmentController : ControllerBase
    {
        /// <summary>
        /// GetStoreDetailsByStoreCode
        /// </summary>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreDetailsByStoreCode")]
        public ResponseModel GetStoreDetailsByStoreCode(string storeCode)
        {
            StoreDetails storeDetails = new StoreDetails();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                storeDetails = newAppointment.GetStoreDetailsByStoreCode(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, authenticate.ProgramCode, storeCode);

                statusCode =
                string.IsNullOrEmpty(storeDetails.StoreName) ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = storeDetails;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get Store Operational Days
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreOperationalDays")]
        public ResponseModel GetStoreOperationalDays()
        {
            List<StoreOperationalDays> Operationaldays = new List<StoreOperationalDays>();
           ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                Operationaldays = newAppointment.GetStoreOperationalDays(new AppointmentServices(_connectioSting),
                    authenticate.TenantId, authenticate.ProgramCode,authenticate.UserMasterID);

                statusCode = Operationaldays.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = Operationaldays;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Get Slot Templates
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSlotTemplates")]
        public ResponseModel GetSlotTemplates()
        {
            List<SlotTemplateModel> TemplateList = new List<SlotTemplateModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                TemplateList = newAppointment.GetSlotTemplates(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.ProgramCode);

                statusCode = TemplateList.Count> 0  ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = TemplateList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        ///Get Generated Slots
        /// </summary>
        /// <param name="CreateStoreSlotTemplate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetGeneratedSlots")]
        public ResponseModel GetGeneratedSlots([FromBody] CreateStoreSlotTemplate Template)
        {
            List<TemplateBasedSlots> SlotsList = new List<TemplateBasedSlots>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                Template.TenantId = authenticate.TenantId;
                Template.ProgramCode = authenticate.ProgramCode;
                Template.UserID = authenticate.UserMasterID;


                AppointmentCaller newAppointment = new AppointmentCaller();

                SlotsList = newAppointment.GetGeneratedSlots(new AppointmentServices(_connectioSting), Template);

                statusCode = SlotsList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = SlotsList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        ///Create Slot Template
        /// </summary>
        /// <param name="CreateStoreSlotTemplate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSlotTemplate")]
        public ResponseModel CreateSlotTemplate([FromBody] CreateStoreSlotTemplate Template)
        {
            
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            int Result = 0; 
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                Template.TenantId = authenticate.TenantId;
                Template.ProgramCode = authenticate.ProgramCode;
                Template.UserID = authenticate.UserMasterID;


                AppointmentCaller newAppointment = new AppointmentCaller();

                Result = newAppointment.CreateSlotTemplate(new AppointmentServices(_connectioSting), Template);

                statusCode = Result > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
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
        ///GetS lots By TemplateID
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="SlotTemplateID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSlotsByTemplateID")]
        public ResponseModel GetSlotsByTemplateID(int SlotTemplateID)
        {
            List<TemplateBasedSlots> SlotsList = new List<TemplateBasedSlots>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                AppointmentCaller newAppointment = new AppointmentCaller();

                SlotsList = newAppointment.GetSlotsByTemplateID(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.ProgramCode
                    ,authenticate.UserMasterID,SlotTemplateID);

                statusCode = SlotsList.Count > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = SlotsList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        ///Get Appointment Count On SlotID
        /// </summary>
        /// <param name="SlotSettingID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAppointmentCountOnSlotID")]
        public ResponseModel GetAppointmentCountOnSlotID(int SlotSettingID)
        {
            int AppointmentCount = 0;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                AppointmentCaller newAppointment = new AppointmentCaller();

                AppointmentCount = newAppointment.GetAppointmentCountOnSlotID(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.ProgramCode, SlotSettingID);
                  

                statusCode = AppointmentCount > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = AppointmentCount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Get Slot Templates
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSlotFilterDetails")]
        public async Task<ResponseModel> GetSlotFilterDetails()
        {
            SlotFilterModel SlotFilter = null;
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                SlotFilter = await newAppointment.GetSlotFilterDetails(new AppointmentServices(_connectioSting),authenticate.TenantId, authenticate.ProgramCode);

                statusCode = SlotFilter!=null ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = SlotFilter;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

    }
}