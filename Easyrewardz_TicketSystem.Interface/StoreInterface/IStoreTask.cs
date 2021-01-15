using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreTask
    {
        /// <summary>
        /// Add Task Details
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddTaskDetails(TaskMaster taskMaster, int TenantID, int UserID);

        /// <summary>
        /// Get Task List
        /// </summary>
        /// <param name="tabFor"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<CustomStoreTaskDetails> GetTaskList(int tabFor, int tenantID, int userID);

        /// <summary>
        /// Get Store Task By ID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        StoreTaskMaster GetStoreTaskByID(int TaskID, int TenantID, int UserID);

        /// <summary>
        /// Add Store Task Comment
        /// </summary>
        /// <param name="TaskComment"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddStoreTaskComment(StoreTaskComment TaskComment, int TenantID, int UserID);

        /// <summary>
        /// Get Comment On Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="taskFor"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<TaskCommentModel> GetCommentOnTask(int TaskID, int taskFor, int TenantID, int UserID);

        /// <summary>
        /// Get Task History
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<CustomTaskHistory> GetTaskHistory(int TaskID, int TenantID, int UserID);

        /// <summary>
        /// Submit Task
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <param name="UserID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int SubmitTask(StoreTaskMaster taskMaster, int UserID, int TenantId);

        /// <summary>
        /// Get User List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="TaskID"></param>
        /// <param name="TaskFor"></param>
        /// <returns></returns>
        List<CustomStoreUserList> GetUserList(int TenantID, int TaskID, int TaskFor);

        /// <summary>
        /// Assign Task
        /// </summary>
        /// <param name="assignTaskModel"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AssignTask(AssignTaskModel assignTaskModel, int TenantID, int UserID);

        /// <summary>
        /// Get Store Task By Ticket
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<CustomStoreTaskDetails> GetStoreTaskByTicket(int tenantID, int userID);

        /// <summary>
        /// Get Store Ticketing Task By Task ID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        StoreTaskWithTicket GetStoreTicketingTaskByTaskID(int TaskID, int TenantID, int UserID);

        /// <summary>
        /// Get Assigned To
        /// </summary>
        /// <param name="Function_ID"></param>
        /// <returns></returns>
        List<CustomUserAssigned> GetAssignedTo(int Function_ID);

        /// <summary>
        /// Get Store Task Procress Bar
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="TaskBy"></param>
        /// <returns></returns>
        List<StoreTaskProcressBar> GetStoreTaskProcressBar(int TaskId, int TaskBy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskMaster"></param>
        /// <param name="UserID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int SubmitTaskByTicket(StoreTaskMaster taskMaster, int UserID, int TenantId);

        /// <summary>
        /// Assign Task By Ticket
        /// </summary>
        /// <param name="assignTaskModel"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AssignTaskByTicket(AssignTaskModel assignTaskModel, int TenantID, int UserID);

        /// <summary>
        /// Get Store Campaign Customer
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<StoreCampaign> GetStoreCampaignCustomer(int TenantID, int UserID);



        /// <summary>
        /// Get Store Campaign Customer New 
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<List<StoreCampaign>> GetStoreCampaignCustomerNew(int TenantID, int UserID);

        /// <summary>
        /// Get Campaign Status Response
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        CampaignStatusResponse GetCampaignStatusResponse(int TenantID, int UserID);

        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int UpdateCampaignStatusResponse(StoreCampaignCustomerRequest objRequest, int TenantID, int UserID);


        /// <summary>
        /// Update Campaign Status Response
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<int> UpdateCampaignStatusResponseNew(StoreCampaignCustomerRequest objRequest, int TenantID, int UserID);


        /// <summary>
        /// Add Task Campaign Comment
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="campaignCustomerID"></param>
        /// <param name="Comment"></param>
        /// <returns></returns>
        Task<int> AddStoreTaskCampaignComment(int TenantID, int UserID, int campaignCustomerID, string Comment);


        /// <summary>
        /// Get Task Campaign Comments
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="campaignCustomerID"></param>
        /// <returns></returns>
        Task<List<StoreTaskCampaignComments>> GetStoreTaskCampaignComment(int TenantID, int campaignCustomerID);

        /// <summary>
        /// Close Campaign
        /// </summary>
        /// <param name="CampaignTypeID"></param>
        /// <param name="IsClosed"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int CloseCampaign(int CampaignTypeID, int IsClosed, int TenantID, int UserID);

        /// <summary>
        /// Get Raised by fiter Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<TaskFilterRaisedBymeResponseModel> GetRaisedbyfiterData(TaskFilterRaisedBymeModel model);

        /// <summary>
        /// Get Assign By fiter Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<TaskFilterAssignBymeResponseModel> GetAssignBYfiterData(TaskFilterAssignBymeModel model);

        /// <summary>
        /// Get Task Ticket Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<TaskFilterTicketByResponseModel> GetTaskTicketData(TaskFilterTicketByModel model);

        /// <summary>
        /// Get Store Campaign Customer By Status
        /// </summary>
        /// <param name="statusID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<StoreCampaign> GetStoreCampaignCustomerByStatus(string statusID ,int TenantID, int UserID);

        /// <summary>
        /// Filter Store Campaign Customer 
        /// </summary>
        /// <param name="statusID"></param>
        /// <param name="ResponseID"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<List<StoreCampaign>> FilterStoreCampaignCustomer(int CampaignTypeID,string statusID, string ResponseID,string FromDate, string ToDate, int TenantID, int UserID);

        /// <summary>
        /// Get Campaign Response By Status
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        Task<List<CampaignResponse>> GetCampaignResponseByStatus(string statusID);

        /// <summary>
        /// Get Store Customer Popup Details List
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="programCode"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="ClientAPIURL"></param>
        /// <returns></returns>
        StoresCampaignDetailResponse GetStoreCustomerpopupDetailsList(string mobileNumber, string programCode, int TenantID, int UserID, string ClientAPIURL);
    }
}
