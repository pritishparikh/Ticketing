using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class RequestCouriersPartnerAndAWBCode
    {
        public int pickup_postcode { get; set; }
        public int delivery_postcode { get; set; }
        public int weight { get; set; }
        public OrderDetails orderDetails { get; set; }
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
    }

    public class ResponseCouriersPartnerAndAWBCode
    {
        public string statusCode { get; set; }
        public Data data { get; set; }
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
}
