using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace TravelAgency.WebApplication
{
    public static class EnumHelper
    {
        public static IEnumerable<SelectListItem> GetEnumDescriptions(Type enumType)
        {
            var selectListItems = new Collection<SelectListItem>();
            foreach (Enum value in Enum.GetValues(enumType))
            {
                var descriptionText = value.GetType()
                                           .GetField(value.ToString())?
                                           .GetCustomAttribute<DescriptionAttribute>()?.Description
                                           ?? value.ToString();

                selectListItems.Add(new SelectListItem { Text = descriptionText, Value = descriptionText });
            }

            return selectListItems;
        }
    }
}
