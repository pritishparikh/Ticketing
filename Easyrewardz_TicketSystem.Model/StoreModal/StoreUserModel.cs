using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreUserModel
    {
        public StoreUserPersonalDetails PersonalDetails { get; set; }
        public StoreUserProfileDetails ProfileDetails { get; set; }
    }

    public class StoreUserPersonalDetails
    {
       
        public int UserID { get; set; }
        public int TenantID { get; set; }
       public bool IsStoreUser { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }

    public class StoreUserProfileDetails
    {

        
        public int DepartmentId { get; set; }
        public List<StoreUserMappedFunctions> MappedFunctions { get; set; } //comma seperated
        public int DesignationID { get; set; }
        public int ReporteeID { get; set; }
        public int ReportToID { get; set; }


        public int UserID { get; set; }
        public bool IsStoreUser { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }

    public class StoreUserMappedFunctions
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
    }

    #region Claim Category mapping

    public class StoreClaimCategoryModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int BrandID { get; set; }
        public bool IsActive { get; set; }
    }


    public class StoreClaimSubCategoryModel
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryID { get; set; }
      
        public bool IsActive { get; set; }
    }

    public class StoreClaimIssueTypeModel
    {
        public int IssueTypeID { get; set; }
        public string IssueTypeName { get; set; }
        public int SubCategoryID { get; set; }

        public bool IsActive { get; set; }
    }


    #endregion
}
