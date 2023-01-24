using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LearnWords.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Получить значение из поля "Name" атрибута "Display"
        /// </summary>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()?
                       .GetMember(enumValue.ToString())?
                       .First()?
                       .GetCustomAttribute<DisplayAttribute>()?
                       .Name
                   ?? enumValue.ToString();
        }
    }
}
