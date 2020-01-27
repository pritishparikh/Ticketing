﻿using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IKnowledge
    {


        List<KnowlegeBaseMaster> SearchByCategory(int type_ID, int Category_ID, int SubCategory_ID, int TenantId);

        int AddKB(KnowlegeBaseMaster knowlegeBaseMaster);

        int UpdateKB(KnowlegeBaseMaster knowlegeBaseMaster);

        int DeleteKB(int KBID, int TenantId);

        int RejectApproveKB(KnowlegeBaseMaster knowlegeBaseMaster);

        CustomKBList SearchKB( int Category_ID, int SubCategory_ID, int type_ID, int TenantId);

        CustomKBList KBList(int TenantId);
    }
}
