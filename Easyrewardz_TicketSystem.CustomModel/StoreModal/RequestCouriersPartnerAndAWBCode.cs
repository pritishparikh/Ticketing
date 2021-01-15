using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class RequestCouriersPartnerAndAWBCode
    {
        public int pickup_postcode { get; set; }
        public int delivery_postcode { get; set; }
        public double weight { get; set; }
        public OrderDetails orderDetails { get; set; }

        public deliveryPartner deliveryPartner { get; set; }
        public List<string> package_content { get; set; }

        public double pickup_lat { get; set; }
        public double pickup_lng { get; set; }
        public double drop_lat { get; set; }
        public double drop_lng { get; set; }

        public StoreAddressOrder StoreAddress { get; set; }
        public Insurancedetails Insurancedetails { get; set; }
    }
    public class OrderDetails
    {
        public string order_id { get; set; }
        public string order_date { get; set; }
        public string pickup_location { get; set; }
        public string channel_id { get; set; }
        public string billing_customer_name { get; set; }
        public string billing_last_name { get; set; }
        public string billing_address { get; set; }
        public string billing_address_2 { get; set; }
        public string billing_city { get; set; }
        public string billing_pincode { get; set; }
        public string billing_state { get; set; }
        public string billing_country { get; set; }
        public string billing_email { get; set; }
        public string billing_phone { get; set; }
        public string billing_alternate_phone { get; set; }
        public bool shipping_is_billing { get; set; }
        public string shipping_customer_name { get; set; }
        public string shipping_last_name { get; set; }
        public string shipping_address { get; set; }
        public string shipping_address_2 { get; set; }
        public string shipping_city { get; set; }
        public string shipping_pincode { get; set; }
        public string shipping_country { get; set; }
        public string shipping_state { get; set; }
        public string shipping_email { get; set; }
        public string shipping_phone { get; set; }
        public List<Orderitems> order_items { get; set; }

        public string payment_method { get; set; }
        public int shipping_charges { get; set; }
        public int giftwrap_charges { get; set; }
        public int transaction_charges { get; set; }
        public int total_discount { get; set; }
        public int sub_total { get; set; }
        public int length { get; set; }
        public int breadth { get; set; }
        public int height { get; set; }
        public double weight { get; set; }

        public double droplatitude { get; set; }
        public double droplongitude { get; set; }
        public double pickuplatitude { get; set; }
        public double pickuplongitude { get; set; }
        public string store_code { get; set; }
        public string collection_mode { get; set; }

    }
    public class Orderitems
    {
        public string name { get; set; }
        public string sku { get; set; }
        public int units { get; set; }
        public string selling_price { get; set; }
        public int discount { get; set; }
        public int tax { get; set; }
        public int hsn { get; set; }
        public string RequestPayload { get; set; }
    }

    public class ResponseCouriersPartnerAndAWBCode
    {
        public string statusCode { get; set; }
        public string Message { get; set; }
        public Data data { get; set; }

        public deliveryPartner deliveryPartner { get; set; }
    }
    public class Data
    {
        public string awb_code { get; set; }
        public string order_id { get; set; }
        public string shipment_id { get; set; } = "0";
        public string courier_company_id { get; set; }
        public string courier_name { get; set; }
        public string rate { get; set; }
        public string is_custom_rate { get; set; }
        public string cod_multiplier { get; set; }
        public string cod_charges { get; set; }
        public string freight_charge { get; set; }
        public string rto_charges { get; set; }
        public string min_weight { get; set; }
        public string etd_hours { get; set; }
        public string etd { get; set; }
        public string estimated_delivery_days { get; set; }
        public string etd_minutes { get; set; }
        public string etd_Dropminutes { get; set; }
        public string etd_Drop_DateTime { get; set; }
        public string etd_Pickup_DateTime { get; set; }
    }

    public class RequestGeneratePickup
    {
        public List<int> shipmentId { get; set; }
    }
    //public class Shipment
    //{
    //    public int shipmentIds { get; set; }
    //}
    public class ResponseGeneratePickup
    {
        public int pickupStatus { get; set; }
        public Response response { get; set; }
    }
    public class Response
    {  
        public string pickupTokenNumber { get; set; }
        public string pickupScheduledDate { get; set; }
        public int status { get; set; }
        public PickupGeneratedDate pickupGeneratedDate { get; set; }
        public string data  { get; set; }  
    }
    public class PickupGeneratedDate
    {
        public string date { get; set; }
        public int timezoneType { get; set; }
        public string timezone { get; set; }
    }

    public class ResponseGenerateManifest
    {
        public int status { get; set; }
        public string manifestUrl { get; set; }
        public string message { get; set; }
        public int status_code { get; set; }   
    }

    public class StoreAddressOrder
    {
        public string storeName { get; set; }
        public string storeCode { get; set; } = string.Empty;
        public string storeAddress { get; set; } = string.Empty;
        public string storeLat { get; set; } = string.Empty;
        public string storeLong { get; set; } = string.Empty;
        public string storeContactNo { get; set; } = string.Empty;
    }

    public class Insurancedetails
    {
        public string Applied { get; set; }
        public string Fee { get; set; }
        public string Product_description { get; set; }
        public string Product_price { get; set; }
    }
}
