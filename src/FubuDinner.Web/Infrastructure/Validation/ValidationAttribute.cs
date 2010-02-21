using System;
using System.Reflection;

namespace FubuDinner.Web.Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidationAttribute : Attribute
    {
        private PropertyInfo _property;

        public PropertyInfo Property
        {
            get { return _property; }
            set { _property = value; }
        }

        public string PropertyName
        {
            get { return _property.Name; }
        }

        public void Validate(object target, Notification notification)
        {
            var rawValue = _property.GetValue(target, null);
            validate(target, rawValue, notification);
        }

        protected void logMessage(Notification notification, string message)
        {
            notification.RegisterFailure(Property.Name, message);
        }

        protected abstract void validate(object target, object rawValue, Notification notification);
    }
}