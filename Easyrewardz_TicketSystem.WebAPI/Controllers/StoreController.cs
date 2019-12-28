using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        #endregion

        #region Cunstructor
        public StoreController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Search Store details
        /// </summary>
        /// <param name="Storename"></param>
        /// <param name="Storecode"></param>
        /// <param name="Pincode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("searchStoreDetail")]
        public ResponseModel searchStoreDetail(string Storename, string Storecode, int Pincode)
        {
            List<StoreMaster> objstoreList = new List<StoreMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                StoreCaller _newStore = new StoreCaller();

                objstoreList = _newStore.getStoreDetailbyNameAndPincode(new StoreService(_connectioSting), Storename, Storecode, Pincode);
                StatusCode =
                objstoreList.Count == 0 ?
                     (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = objstoreList;
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
        /// Search Store
        /// </summary>
        /// <param name="store"></param>
        /// <param name="searchText"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getStores")]
        public ResponseModel getStores(string searchText, int tenantID)
        {
            List<StoreMaster> storeMasters = new List<StoreMaster>();
            ResponseModel _objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                StoreCaller _newMasterBrand = new StoreCaller();

                storeMasters = _newMasterBrand.getStores(new StoreService(_connectioSting), searchText, tenantID);

                StatusCode =
                storeMasters.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);

                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = StatusCode;
                _objResponseModel.Message = statusMessage;
                _objResponseModel.ResponseData = storeMasters;

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
