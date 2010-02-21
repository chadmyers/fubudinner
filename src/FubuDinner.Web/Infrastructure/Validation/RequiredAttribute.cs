using System;
using System.Reflection;

namespace FubuDinner.Web.Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : ValidationAttribute
    {
        protected override void validate(object target, object rawValue, Notification notification)
        {
            if (rawValue == null || (rawValue.ToString().Trim() == string.Empty) )
            {
                logMessage(notification, message());
            }
        }

        protected virtual string message()
        {
            return Notification.REQUIRED_FIELD;
        }

        public static bool IsRequired(PropertyInfo property)
        {
            var attribute = GetCustomAttribute(property, typeof(RequiredAttribute)) as RequiredAttribute;
            return attribute != null && attribute.GetType().Equals(typeof(RequiredAttribute));
        }
    }
}