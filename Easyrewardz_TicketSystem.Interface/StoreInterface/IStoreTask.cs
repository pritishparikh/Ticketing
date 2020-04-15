using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreTask
    {
        int AddTaskDetails(TaskMaster taskMaster, int TenantID, int UserID);
        List<CustomStoreTaskDetails> GetTaskList(int tabFor, int tenantID, int userID);
        StoreTaskMaster GetStoreTaskByID(int TaskID, int TenantID, int UserID);
        int AddStoreTaskComment(StoreTaskComment TaskComment, int TenantID, int UserID);
        List<TaskCommentModel> GetCommentOnTask(int TaskID, int TenantID, int UserID);
        List<CustomTaskHistory> GetTaskHistory(int TaskID, int TenantID, int UserID);
        int SubmitTask(StoreTaskMaster taskMaster, int UserID, int TenantId);
        List<CustomStoreUserList> GetUserList(int TenantID, int TaskID);
        int AssignTask(string TaskID, int TenantID, int UserID, int AgentID);
        List<CustomStoreTaskDetails> GetStoreTaskByTicket(int tenantID, int userID);
        StoreTaskWithTicket GetStoreTicketingTaskByTaskID(int TaskID, int TenantID, int UserID);

        List<StoreCampaign> GetStoreCampaignCustomer(int TenantID, int UserID);
        CampaignStatusResponse GetCampaignStatusResponse(int TenantID, int UserID);
        int UpdateCampaignStatusResponse(StoreCampaignCustomerRequest objRequest, int TenantID, int UserID);
        int CloseCampaign(int CampaignTypeID, int IsClosed, int TenantID, int UserID);
    }
}
