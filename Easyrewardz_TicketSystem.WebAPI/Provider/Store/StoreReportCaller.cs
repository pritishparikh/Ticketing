﻿using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreReportCaller
    {
        #region Variable declaration

        private IStoreReport _Reports;

        #endregion

        public int StoreReportSearch(IStoreReport Report, StoreReportModel searchModel, List<StoreUserListing> StoreUserList, List<StoreUserListing> StoreReportUserList)
        {
            _Reports = Report;
            return _Reports.GetStoreReportSearch(searchModel, StoreUserList, StoreReportUserList);
        }

        public string DownloadStoreReportSearch(IStoreReport Report, int ReportID, int UserID, int TenantID, List<StoreUserListing> StoreUserList)
        {
            _Reports = Report;
            return _Reports.DownloadStoreReportSearch(ReportID, UserID, TenantID, StoreUserList);
        }


        public bool CheckIfReportNameExists(IStoreReport Report, int ReportID, string ReportName, int ScheuleID, int TenantID)
        {
            _Reports = Report;
            return _Reports.CheckIfReportNameExists(ReportID, ReportName, ScheuleID, TenantID);
        }

        public int ScheduleStoreReport(IStoreReport Report, ScheduleMaster scheduleMaster, int TenantID, int UserID)
        {
            _Reports = Report;
            return _Reports.ScheduleStoreReport(scheduleMaster, TenantID, UserID);

        }

        public List<ReportModel> GetStoreReportList(IStoreReport Report, int tenantID)
        {
            _Reports = Report;
            return _Reports.StoreReportList(tenantID);
        }


        public int DeleteStoreReport(IStoreReport Report, int tenantID, int ReportID)
        {
            _Reports = Report;
            return _Reports.DeleteStoreReport(tenantID, ReportID);

        }

        public int InsertStoreReport(IStoreReport Report, StoreReportRequest ReportMaster)
        {
            _Reports = Report;
            return _Reports.SaveStoreReport(ReportMaster);
        }


        public List<CampaignScriptName> GetCampaignNames(IStoreReport Report)
        {
            _Reports = Report;
            return _Reports.GetCampaignNames();
        }

    }
}
