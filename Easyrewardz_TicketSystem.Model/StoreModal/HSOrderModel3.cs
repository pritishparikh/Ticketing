namespace Easyrewardz_TicketSystem.Model
{
    public class PaymentCommentModel
    {
        public int TenantID { get; set; }
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public string PODPaymentComent { get; set; }
    }

    public class DownloadReportRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Option { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }

    }
    public class DownloadReportResponse
    {
        public string OrderID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CartAmount { get; set; }
        public string Partner { get; set; }
        public string PaymentStaus { get; set; }
    }
}
