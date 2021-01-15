﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class AppointmentModel
    {
        /// <summary>
        /// Appointment Date
        /// </summary>
        public string AppointmentDate { get; set; }

        /// <summary>
        /// Slot Id
        /// </summary>
        public int SlotId { get; set; }

        /// <summary>
        /// Time Slot
        /// </summary>
        public string TimeSlot { get; set; }

        /// <summary>
        /// NO Of People
        /// </summary>
        public int NOofPeople { get; set; }

        /// <summary>
        /// Max Capacity
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// Appointment Customer List
        /// </summary>
        public List<AppointmentCustomer> AppointmentCustomerList { get; set; }

    }

    public class AppointmentCustomer
    {
        /// <summary>
        /// Appointment ID
        /// </summary>
        public int AppointmentID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Number
        /// </summary>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// NO Of People
        /// </summary>
        public int NOofPeople { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
       
    }

    

    public class AppointmentCount
    {
        /// <summary>
        /// AppointmentDate
        /// </summary>
        public string AppointmentDate { get; set; }

        /// <summary>
        /// DayName
        /// </summary>
        public string DayName { get; set; }

        /// <summary>
        /// Appointment Count
        /// </summary>
        public int AptCount { get; set; }
    }

    public class AppointmentDetails
    {
        /// <summary>
        /// Appointment ID
        /// </summary>
        public int AppointmentID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Mobile No
        /// </summary>
        public string CustomerMobileNo { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Store Address
        /// </summary>
        public string StoreAddress { get; set; }

        /// <summary>
        /// No Of People
        /// </summary>
        public string NoOfPeople { get; set; }

        /// <summary>
        /// Store Manager Mobile
        /// </summary>
        public string StoreManagerMobile { get; set; }
    }

    public class AppointmentMessageTag
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// TagName
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// PlaceHolder
        /// </summary>
        public string PlaceHolder { get; set; }
    }

    public class AppointmentSearchRequest
    {
        /// <summary>
        /// Appointment ID
        /// </summary>
        public int AppointmentID { get; set; }

        /// <summary>
        /// Customer Number
        /// </summary>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public string Date { get; set; }

    }
}
