using System;

namespace FubuDinner.Web.Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidatedChildAttribute : ValidationAttribute
    {
        protected override void validate(object target, object rawValue, Notification notification)
        {
            var childNotification = Validator.ValidateObject(rawValue);
            notification.AddChild(PropertyName, childNotification);
        }
    }


}