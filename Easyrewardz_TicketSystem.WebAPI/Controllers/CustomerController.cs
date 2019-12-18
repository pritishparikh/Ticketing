using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        [Route("getcustomerdetailsbyid")]
        public string getcustomerdetailsbyid(int CustomerID)
        {
            Result resp = new Result();
            try
            {
                if(CustomerID > 0)
                {
                    Customercaller _customercaller = new Customercaller();
                    CustomerMaster objcu = new CustomerMaster();                   
                    var customer = _customercaller.getCustomerDetailsById(new CustomerService(), CustomerID);
                    string customername = customer[0].CustomerName;
                    string EmailID = customer[0].CustomerEmailId;
                    string AltEmailID = customer[0].AltEmailID;
                    string MobileNo = customer[0].CustomerPhoneNumber;
                    int GenderID = customer[0].GenderID;
                    string AlternateNo = customer[0].AltNumber;
                    resp.IsResponse = true;
                    resp.statusCode = 200;
                    resp.customerFullName = customername;
                    resp.emailid = EmailID;
                    resp.alternateemail = AltEmailID;
                    resp.MobileNumber = MobileNo;
                    resp.Gender = GenderID;
                    resp.AlternateNumber = AlternateNo;
                    resp.ErrorMessage = null;
                }
                else
                {
                    resp.IsResponse = false;
                    resp.statusCode = 1001;
                    resp.Message = "Record Not Found";
                    resp.ErrorMessage = resp.ErrorMessage;
                    return JsonConvert.SerializeObject(resp);
                }

            }
            catch (Exception _ex)
            {

                resp.IsResponse = false;
                resp.statusCode = 500;
                resp.Message = "Request Error";
                resp.ErrorMessage = _ex.InnerException.ToString();
                return JsonConvert.SerializeObject(resp);
            }
            finally
            {
                GC.Collect();
            }
            return "";
        }

        //public string searchCustomer()
        //{
        //    Result resp = new Result();
        //    try
        //    {

        //    }
        //    catch (Exception _ex)
        //    {

        //        resp.IsResponse = false;
        //        resp.statusCode = 500;
        //        resp.Message = "Request Error";
        //        resp.ErrorMessage = _ex.InnerException.ToString();
        //        return JsonConvert.SerializeObject(resp);
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }

        //}






        public class Result
        {
            public int statusCode { get; set; }
            public string Message { get; set; }
            public bool IsResponse { get; set; }
            public string ErrorMessage { get; set; }
            public string customerFullName { get; set; }
            public string emailid { get; set; }
            public string alternateemail { get; set; }
            public string MobileNumber { get; set; }
            public int Gender { get; set; }
            public string AlternateNumber { get; set; }
        }
    }
    
}