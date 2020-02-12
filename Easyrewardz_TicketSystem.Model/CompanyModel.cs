﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class CompanyModel
    {

        public string CompanyName { get; set; }

        public int CompanyTypeID { get; set; }

        public DateTime CompanyIncorporationDate { get; set; }

        public int NoOfEmployee { get; set; }

        public string CompanayEmailID  { get; set; }

        public string CompanayContactNo { get; set; }

        public string ContactPersonName { get; set; }

        public string ContactPersonNo { get; set; }

        public string CompanayAddress { get; set; }

        public int Pincode { get; set; }

        public int CityID { get; set; }

        public int StateID { get; set; }

        public int CountryID { get; set; }

        public int CreatedBy { get; set; }



    }
}
