using System.ComponentModel.DataAnnotations;

namespace StatementApplication.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var memberInfo = enumType.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? ((DisplayAttribute)attributes[0]).Name : value.ToString();
        }
    }
}
