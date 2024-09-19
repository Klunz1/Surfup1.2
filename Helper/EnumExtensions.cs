using Microsoft.AspNetCore.Mvc.Rendering;

namespace SurfsupEmil.Helper
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                       .Cast<TEnum>()
                       .Select(e => new SelectListItem
                       {
                           Value = e.ToString(),
                           Text = e.ToString()
                       });
        }
    }
}
