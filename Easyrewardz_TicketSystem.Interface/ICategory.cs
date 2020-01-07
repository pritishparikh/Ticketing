using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Category
    /// </summary>
    public interface ICategory
    {
        List<Category> GetCategoryList(int TenantID);

        int AddCategory(Category category);

        List<Category> CategoryList(int TenantId);

        int DeleteCategory(int CategoryID, int TenantId);

        int UpdateCategory(Category category);
    }
}
