using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;


namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreTaskCaller
    {
        #region Variable
        public IStoreTask _TaskRepository;
        #endregion
        
        public int AddTask(IStoreTask task, TaskMaster taskMaster, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AddTaskDetails(taskMaster, TenantID, UserID);
        }
        public List<CustomStoreTaskDetails> GettaskList(IStoreTask task, int tabFor,int tenantID,int userID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskList(tabFor, tenantID, userID);
        }
        public StoreTaskMaster GetStoreTaskByID(IStoreTask task, int TaskID, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetStoreTaskByID(TaskID, TenantID, UserID);
        }
        public int AddStoreTaskComment(IStoreTask task, StoreTaskComment TaskComment, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AddStoreTaskComment(TaskComment, TenantID, UserID);
        }
        public List<TaskCommentModel> GetCommentOnTask(IStoreTask task, int TaskID, int taskFor, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetCommentOnTask(TaskID, taskFor, TenantID, UserID);
        }
        public List<CustomTaskHistory> GetTaskHistory(IStoreTask task, int TaskID, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskHistory(TaskID, TenantID, UserID);
        }
        public int SubmitTask(IStoreTask task, StoreTaskMaster taskMaster, int UserID, int TenantId)
        {
            _TaskRepository = task;
            return _TaskRepository.SubmitTask(taskMaster, UserID, TenantId);
        }
        public List<CustomStoreUserList> UserList(IStoreTask task, int TenantID, int TaskID, int TaskFor)
        {
            _TaskRepository = task;
            return _TaskRepository.GetUserList(TenantID, TaskID, TaskFor);
        }
        public int AssignTask(IStoreTask task, AssignTaskModel assignTaskModel, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AssignTask(assignTaskModel, TenantID, UserID);

        }
        public List<CustomStoreTaskDetails> GetStoreTaskByTicket(IStoreTask task, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetStoreTaskByTicket(TenantID, UserID);

        }
        public StoreTaskWithTicket GetStoreTicketingTaskByTaskID(IStoreTask task, int TaskID, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetStoreTicketingTaskByTaskID(TaskID, TenantID, UserID);

        }
        public List<CustomUserAssigned> GetAssignedTo(IStoreTask task, int Function_ID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetAssignedTo(Function_ID);

        }
        public List<StoreTaskProcressBar> GetStoreTaskProcressBar(IStoreTask task, int TaskId, int TaskBy)
        {
            _TaskRepository = task;
            return _TaskRepository.GetStoreTaskProcressBar(TaskId, TaskBy);

        }
        public int SubmitTaskByTicket(IStoreTask task, StoreTaskMaster taskMaster, int UserID, int TenantId)
        {
            _TaskRepository = task;
            return _TaskRepository.SubmitTaskByTicket(taskMaster, UserID, TenantId);

        }
        public int AssignTaskByTicket(IStoreTask task, AssignTaskModel assignTaskModel, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.AssignTaskByTicket(assignTaskModel, TenantID, UserID);

        }

        public List<StoreCampaign> GetStoreCampaignCustomer(IStoreTask task, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetStoreCampaignCustomer(TenantID, UserID);
        }
        public CampaignStatusResponse GetCampaignStatusResponse(IStoreTask task, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.GetCampaignStatusResponse(TenantID, UserID);
        }
        public int UpdateCampaignStatusResponse(IStoreTask task, StoreCampaignCustomerRequest objRequest, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.UpdateCampaignStatusResponse(objRequest, TenantID, UserID);
        }
        public int CloseCampaign(IStoreTask task, int CampaignTypeID, int IsClosed, int TenantID, int UserID)
        {
            _TaskRepository = task;
            return _TaskRepository.CloseCampaign(CampaignTypeID, IsClosed, TenantID, UserID);
        }

        public List<TaskFilterRaisedBymeResponseModel> GetRaisedbyfiterData(IStoreTask task, TaskFilterRaisedBymeModel taskMaster)
        {
            _TaskRepository = task;
            return _TaskRepository.GetRaisedbyfiterData(taskMaster);
        }

        public List<TaskFilterAssignBymeResponseModel> GetAssigenBYfiterData(IStoreTask task, TaskFilterAssignBymeModel taskMaster)
        {
            _TaskRepository = task;
            return _TaskRepository.GetAssignBYfiterData(taskMaster);
        }


        public List<TaskFilterTicketByResponseModel> GetTaskTicketData(IStoreTask task, TaskFilterTicketByModel taskMaster)
        {
            _TaskRepository = task;
            return _TaskRepository.GetTaskTicketData(taskMaster);
        }
    }
}
