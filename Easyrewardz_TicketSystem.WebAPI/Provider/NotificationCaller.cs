﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class NotificationCaller 
    {
        #region Variable declaration

        private INotification _Notification;

        #endregion

        /// <summary>
        /// Get AlertList
        // / </summary>
        public List<NotificationModel> GetNotification(INotification Notification, int TenantID, int UserID )
        {
            _Notification = Notification;
            return _Notification.GetNotification( TenantID,  UserID);

        }

        /// <summary>
        /// Update is Read on Notification Read
        // / </summary>
        public int  GetNotification(INotification Notification, int TenantID, int UserID, string TicketIDs)
        {
            _Notification = Notification;
            return _Notification.ReadNotification(TenantID, UserID, TicketIDs);

        }
    }
}