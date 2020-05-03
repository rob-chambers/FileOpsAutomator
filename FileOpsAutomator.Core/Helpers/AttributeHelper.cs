using System.ComponentModel;

namespace FileOpsAutomator.Core.Helpers
{
    public static class AttributeHelper
    {
        public static string GetDescription(object instance)
        {
            var type = instance.GetType();
            var attribute = type.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
            var description = (DescriptionAttribute)attribute;
            return description.Description;
        }

        public static string GetDisplayName(object instance)
        {
            var type = instance.GetType();
            var attribute = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0];
            var displayName = (DisplayNameAttribute)attribute;
            return displayName.DisplayName;
        }
    }
}
