using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    /// <summary>
    /// Common function 
    /// </summary>
    public static class CommonFunction
    {

        /// <summary>
        /// Get description of the Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

    }
}
