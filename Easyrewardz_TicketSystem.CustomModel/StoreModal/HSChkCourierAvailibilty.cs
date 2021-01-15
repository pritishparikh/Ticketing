using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class HSChkCourierAvailibilty
    {
        /// <summary>
        /// Pickup postcode
        /// </summary>
        public int Pickup_postcode { get; set; }

        /// <summary>
        /// Delivery postcode
        /// </summary>
        public int Delivery_postcode { get; set; }

        /// <summary>
        /// Cod
        /// </summary>
        public int Cod { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// pickup_lat
        /// </summary>
        public double pickup_lat { get; set; }
        /// <summary>
        /// pickup_lng
        /// </summary>
        public double pickup_lng { get; set; }
        /// <summary>
        /// drop_lat
        /// </summary>
        public double drop_lat { get; set; }
        /// <summary>
        /// drop_lng
        /// </summary>
        public double drop_lng { get; set; }
        /// <summary>
        /// partnerList
        /// </summary>
        public List<PartnerList> partnerList { get; set; }
    }

    public class PartnerList
    {
        /// <summary>
        /// partnerID
        /// </summary>
        public int partnerID { get; set; }
        /// <summary>
        /// partnerName
        /// </summary>
        public string partnerName { get; set; }
        /// <summary>
        /// priority
        /// </summary>
        public int priority { get; set; }
    }

    public class ResponseCourierAvailibilty
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Available
        /// </summary>
        public string Available  { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// data
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// Responsedata
        /// </summary>
        public Courierdata Responsedata { get; set; }

        /// <summary>
        /// pickup_lat
        /// </summary>
        public double pickup_lat { get; set; }
        /// <summary>
        /// pickup_lng
        /// </summary>
        public double pickup_lng { get; set; }
        /// <summary>
        /// drop_lat
        /// </summary>
        public double drop_lat { get; set; }
        /// <summary>
        /// drop_lng
        /// </summary>
        public double drop_lng { get; set; }
    }

    public class deliveryPartner
    {
        /// <summary>
        /// partnerID
        /// </summary>
        public int partnerID { get; set; }
        /// <summary>
        /// partnerName
        /// </summary>
        public string partnerName { get; set; }
        /// <summary>
        /// priority
        /// </summary>
        public int priority { get; set; }
    }

    public class GeocodeByAddress
    {
        public string address { get; set; }
    }

    public class Courierdata
    {
        /// <summary>
        /// isAvailable
        /// </summary>
        public bool isAvailable { get; set; }
        /// <summary>
        /// deliveryPartner
        /// </summary>
        public deliveryPartner deliveryPartner { get; set; }


        /// <summary>
        /// houseNumber
        /// </summary>
        public string houseNumber { get; set; }
        /// <summary>
        /// houseName
        /// </summary>
        public string houseName { get; set; }
        /// <summary>
        /// poi
        /// </summary>
        public string poi { get; set; }
        /// <summary>
        /// street
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// subSubLocality
        /// </summary>
        public string subSubLocality { get; set; }
        /// <summary>
        /// subLocality
        /// </summary>
        public string subLocality { get; set; }
        /// <summary>
        /// locality
        /// </summary>
        public string locality { get; set; }
        /// <summary>
        /// village
        /// </summary>
        public string village { get; set; }
        /// <summary>
        /// subDistrict
        /// </summary>
        public string subDistrict { get; set; }
        /// <summary>
        /// district
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// state
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// pincode
        /// </summary>
        public int pincode { get; set; }
        /// <summary>
        /// formattedAddress
        /// </summary>
        public string formattedAddress { get; set; }
        /// <summary>
        /// eLoc
        /// </summary>
        public string eLoc { get; set; }
        /// <summary>
        /// latitude
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// longitude
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// geocodeLevel
        /// </summary>
        public string geocodeLevel { get; set; }
        /// <summary>
        /// confidenceScore
        /// </summary>
        public string confidenceScore { get; set; }
    }

    public class GeocodeByAddressResponse
    {
        /// <summary>
        /// statusCode
        /// </summary>
        public string statusCode { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public Courierdata data { get; set; }
    }

    public class OrderIds
    {
        /// <summary>
        /// ids
        /// </summary>
        public List<int> ids { get; set; }
    }

    public class CancelOrderDetails
    {
        /// <summary>
        /// orderId
        /// </summary>
        public OrderIds orderId { get; set; }
        /// <summary>
        /// task_id
        /// </summary>
        public string task_id { get; set; }
        /// <summary>
        /// cancellation_reason
        /// </summary>
        public string cancellation_reason { get; set; }
        /// <summary>
        /// partner
        /// </summary>
        public deliveryPartner partner { get; set; }
    }
    
    public class OrderCancelResponse
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }

    public class OrderCancelRequest
    {
        public int tenantID { get; set; }
        public int userID { get; set; }
        public int orderID { get; set; }
        public string cancellationReason { get; set; }
        public string clientAPIUrl { get; set; }
        public string phygitalClientAPIURL { get; set; }
    }
}
