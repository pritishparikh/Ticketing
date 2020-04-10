using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreUserDetailsModel
    {
        public int BrandID { get; set; }
        public int StoreID { get; set; }
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public int DesignationID { get; set; }
        public int ReporteeID { get; set; }
        public int DepartmentID { get; set; }
        public string FunctionIDs { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BrandIds { get; set; }
        public string CategoryIds { get; set; }
        public string SubCategoryIds { get; set; }
        public string IssuetypeIds { get; set; }
        public int IsStoreUser { get; set; }
       
        public bool IsClaimApprove { get; set; }
        public int CRMRoleID { get; set; }

    }

    public class StoreUserListing
    {
        public int UserID { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }


        public int StoreID { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }

       
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }

        public string FunctionIDs { get; set; }
        public string MappedFunctions { get; set; }


        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public string BrandIDs { get; set; }

        public string MappedBrand { get; set; }


        public int CategoryCount { get; set; }
        public string CategoryIDs { get; set; }
        public string MappedCategory { get; set; }


        public int SubCategoryCount { get; set; }
        public string SubCategoryIDs { get; set; }
        public string MappedSubCategory { get; set; }

        public int IssueTypeCount { get; set; }
        public string IssueTypeIDs { get; set; }
        public string MappedIssuetype { get; set; }

        public int DesignationID { get; set; }
        public string DesignationName { get; set; }

        public int ReporteeID { get; set; }
        public string ReporteeName { get; set; }
        public int ReporteeDesignationID { get; set; }
        public string ReporteeDesignation { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public string isActive { get; set; }
        public bool isClaimApprover { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

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




    #region Profile Mapping

    //public class StoreUserProfileDetails
    //{


    //    public int DepartmentId { get; set; }
    //    public string DepartmentName { get; set; }
    //    public List<StoreUserMappedFunctions> MappedFunctions { get; set; } //comma seperated
    //    public int DesignationID { get; set; }
    //    public string DesignationName { get; set; }

    //    public int ReporteeID { get; set; }
    //    public string ReporteeName { get; set; }
    //    public int ReporteeDesignationID { get; set; }
    //    public string ReporteeDesignation { get; set; }

    //}

    //public class StoreUserMappedFunctions
    //{
    //    public int FunctionID { get; set; }
    //    public string FunctionName { get; set; }
    //}

    public class StoreUserDepartmentList
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int BrandID { get; set; }
        public int StoreID { get; set; }
        public bool IsActive { get; set; }
    }

    #endregion

    #region Claim Category mapping

    //public class StoreUserClaimCategoryDetails
    //{

    //    public List<StoreClaimBrandModel> MappedBrand { get; set; }
    //    public List<StoreClaimCategoryModel> MappedCategory { get; set; }
    //    public List<StoreClaimSubCategoryModel> MappedSubCategory { get; set; }
    //    public List<StoreClaimIssueTypeModel> MappedIssueType { get; set; }

    //    public bool isClaimApprover { get; set; }
    //    public int CRMRoleID { get; set; }
    //    public string RoleName { get; set; }
    //    public bool isActive { get; set; }


    //}

    public class StoreClaimBrandModel
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public bool IsActive { get; set; }
    }


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


    public class StoreClaimCategory 
    {
        public string BrandIDs { get; set; }
        public string CategoryIds { get; set; }
        public string SubCategoryIds { get; set; }
        public string IssuetypeIds { get; set; }
        public bool isClaimApprover { get; set; }
        public int CRMRoleID { get; set; }
        public bool isActive { get; set; }
        public int CreatedBy { get; set; }
        public bool IsStoreUser { get; set; }
        public int UserID { get; set; }
        public int TenantID { get; set; }
    }

    #endregion

    public class StoreUser
    {

        /// <summary>
        /// User Id 
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Reportee Id 
        /// </summary>
        public int ReporteeID { get; set; }
        public string RoleName { get; set; }
        public int RoleID { get; set; }
    }
}
