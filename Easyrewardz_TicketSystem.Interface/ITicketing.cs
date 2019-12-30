﻿using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Ticketing
    /// </summary>
    public interface ITicketing
    {     
        //IEnumerable<TicketingDetails> getTcikets();

        List<TicketingDetails> GetTicketList(string TikcketTitle);

        int addTicket(TicketingDetails ticketingDetails);

        List<CustomDraftDetails> GetDraft(int UserID);

       List <CustomSearchTicketAgent> SearchAgent(string FirstName,string LastName,string Email,int DesignationID);      
    }
}
