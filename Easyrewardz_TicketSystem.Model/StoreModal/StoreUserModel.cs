using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreUserDetailsModel
    {
        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Designation ID
        /// </summary>
        public int DesignationID { get; set; }

        /// <summary>
        /// Reportee ID
        /// </summary>
        public int ReporteeID { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Function IDs
        /// </summary>
        public string FunctionIDs { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mobile No
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Email ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Brand Ids
        /// </summary>
        public string BrandIds { get; set; }

        /// <summary>
        /// Category Ids
        /// </summary>
        public string CategoryIds { get; set; }

        /// <summary>
        /// Sub Category Ids
        /// </summary>
        public string SubCategoryIds { get; set; }

        /// <summary>
        /// Issue type Ids
        /// </summary>
        public string IssuetypeIds { get; set; }

        /// <summary>
        /// Is Store User
        /// </summary>
        public int IsStoreUser { get; set; }

        /// <summary>
        /// Is Claim Approve
        /// </summary>
        public bool IsClaimApprove { get; set; }

        /// <summary>
        /// CRM Role ID
        /// </summary>
        public int CRMRoleID { get; set; }

    }

    public class StoreUserListing
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Mobile No
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Email ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Function IDs
        /// </summary>
        public string FunctionIDs { get; set; }

        /// <summary>
        /// Mapped Functions
        /// </summary>
        public string MappedFunctions { get; set; }

        /// <summary>
        /// Role ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Brand IDs
        /// </summary>
        public string BrandIDs { get; set; }

        /// <summary>
        /// Mapped Brand
        /// </summary>
        public string MappedBrand { get; set; }

        /// <summary>
        /// Category Count
        /// </summary>
        public int CategoryCount { get; set; }

        /// <summary>
        /// Category IDs
        /// </summary>
        public string CategoryIDs { get; set; }

        /// <summary>
        /// Mapped Category
        /// </summary>
        public string MappedCategory { get; set; }

        /// <summary>
        /// Sub Category Count
        /// </summary>
        public int SubCategoryCount { get; set; }

        /// <summary>
        /// Sub Category IDs
        /// </summary>
        public string SubCategoryIDs { get; set; }

        /// <summary>
        /// Mapped Sub Category
        /// </summary>
        public string MappedSubCategory { get; set; }

        /// <summary>
        /// Issue Type Count
        /// </summary>
        public int IssueTypeCount { get; set; }

        /// <summary>
        /// Issue Type IDs
        /// </summary>
        public string IssueTypeIDs { get; set; }

        /// <summary>
        /// Mapped Issue type
        /// </summary>
        public string MappedIssuetype { get; set; }

        /// <summary>
        /// Designation ID
        /// </summary>
        public int DesignationID { get; set; }

        /// <summary>
        /// Designation Name
        /// </summary>
        public string DesignationName { get; set; }

        /// <summary>
        /// Reportee ID
        /// </summary>
        public int ReporteeID { get; set; }

        /// <summary>
        /// Reportee Name
        /// </summary>
        public string ReporteeName { get; set; }

        /// <summary>
        /// Reportee Designation ID
        /// </summary>
        public int ReporteeDesignationID { get; set; }

        /// <summary>
        /// Reportee Designation
        /// </summary>
        public string ReporteeDesignation { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public string isActive { get; set; }

        /// <summary>
        /// Is Claim Approver
        /// </summary>
        public bool isClaimApprover { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        public string UpdatedDate { get; set; }

    }


    public class StoreUserPersonalDetails
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Is Store User
        /// </summary>
        public bool IsStoreUser { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Mobile No
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Email ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }
    }


    public class StoreUserDepartmentList
    {
        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }
    }

    #region Claim Category mapping

    public class StoreClaimBrandModel
    {
        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }
    }


    public class StoreClaimCategoryModel
    {
        /// <summary>
        /// Category ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }
    }


    public class StoreClaimSubCategoryModel
    {
        /// <summary>
        /// Sub Category ID
        /// </summary>
        public int SubCategoryID { get; set; }

        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// Category ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }
    }

    public class StoreClaimIssueTypeModel
    {
        /// <summary>
        /// Issue Type ID
        /// </summary>
        public int IssueTypeID { get; set; }

        /// <summary>
        /// Issue Type Name
        /// </summary>
        public string IssueTypeName { get; set; }

        /// <summary>
        /// Sub Category ID
        /// </summary>
        public int SubCategoryID { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }
    }


    public class StoreClaimCategory 
    {
        /// <summary>
        /// Brand IDs
        /// </summary>
        public string BrandIDs { get; set; }

        /// <summary>
        /// Category Ids
        /// </summary>
        public string CategoryIds { get; set; }

        /// <summary>
        /// Sub Category Ids
        /// </summary>
        public string SubCategoryIds { get; set; }

        /// <summary>
        /// Issue type Ids
        /// </summary>
        public string IssuetypeIds { get; set; }

        /// <summary>
        /// Is Claim Approver
        /// </summary>
        public bool isClaimApprover { get; set; }

        /// <summary>
        /// CRM Role ID
        /// </summary>
        public int CRMRoleID { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public bool isActive { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Is Store User
        /// </summary>
        public bool IsStoreUser { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
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

        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Role ID
        /// </summary>
        public int RoleID { get; set; }
    }
}
