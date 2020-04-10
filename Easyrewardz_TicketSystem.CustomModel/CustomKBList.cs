using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomKBList
    {
        public List<KBisApproved> Approved { get; set; }

        public List<KBisNotApproved> NotApproved { get; set; }

        public List<SimilarTicket> SimilarTickets { get; set; }

    }

    public class KBisApproved
    {
        public int KBID { get; set; }
        /// <summary>
        /// KB CODE
        /// </summary>
        public string KBCODE { get; set; }


        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        public int CategoryID { get; set; }

        public int SubCategoryID { get; set; }

        public int IssueTypeID { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// IssueType Name
        /// </summary>
        public string IssueTypeName { get; set; }

        public string IsApproveStatus { get; set; }

    }
    public class KBisNotApproved
    {
        public int? KBID { get; set; }
        /// <summary>
        /// KB CODE
        /// </summary>
        public string KBCODE { get; set; }


        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }




        public int CategoryID { get; set; }

        public int SubCategoryID { get; set; }

        public int IssueTypeID { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// IssueType Name
        /// </summary>
        public string IssueTypeName { get; set; }

        public string IsApproveStatus { get; set; }

    }

    public class SimilarTicket
    {
        public int? KBID { get; set; }
        /// <summary>
        /// KB CODE
        /// </summary>
        public string KBCODE { get; set; }


        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }




        public int CategoryID { get; set; }

        public int SubCategoryID { get; set; }

        public int IssueTypeID { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// IssueType Name
        /// </summary>
        public string IssueTypeName { get; set; }

        public string IsApproveStatus { get; set; }

        public int? TicketID { get; set; }
    }
}
