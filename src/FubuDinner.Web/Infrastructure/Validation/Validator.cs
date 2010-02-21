using System;
using System.Collections.Generic;
using System.Reflection;

namespace FubuDinner.Web.Infrastructure.Validation
{
    public class Validator : IValidator
    {
        private static readonly Dictionary<Type, List<ValidationAttribute>> _attributeDictionary
            = new Dictionary<Type, List<ValidationAttribute>>();

        private static readonly object _locker = new object();

        public Notification Validate(object target)
        {
            return ValidateObject(target);
        }


        ValidationFailure[] IValidator.ValidateByField(object target, string propertyName)
        {
            return ValidateField(target, propertyName);
        }

        public static List<ValidationAttribute> FindAttributes(Type type)
        {
            var atts = new List<ValidationAttribute>();

            foreach (var property in type.GetProperties())
            {
                var attributes = Attribute.GetCustomAttributes(property, typeof(ValidationAttribute));
                foreach (ValidationAttribute attribute in attributes)
                {
                    attribute.Property = property;
                    atts.Add(attribute);
                }
            }

            return atts;
        }

        public static Notification ValidateObject(object target)
        {
            if (target == null)
            {
                return new Notification();
            }

            var atts = scanType(target.GetType());
            var notification = new Notification();

            if (target is IValidated)
            {
                ((IValidated)target).Validate(notification);
            }

            foreach (ValidationAttribute att in atts)
            {
                att.Validate(target, notification);
            }

            return notification;
        }

        private static List<ValidationAttribute> scanType(Type type)
        {
            if (!_attributeDictionary.ContainsKey(type))
            {
                lock (_locker)
                {
                    if (!_attributeDictionary.ContainsKey(type))
                    {
                        _attributeDictionary.Add(type, FindAttributes(type));
                    }
                }
            }

            return _attributeDictionary[type];
        }

        public static void AssertValid(object target)
        {
            var notification = ValidateObject(target);
            if (!notification.IsValid())
            {
                var message = string.Format("{0} was not valid", target);
                throw new ApplicationException(message);
            }
        }

        public static ValidationFailure[] ValidateField(object target, string propertyName)
        {
            var atts = scanType(target.GetType());
            var list = atts.FindAll(att => att.PropertyName == propertyName);

            var notification = new Notification();
            foreach (var attribute in list)
            {
                attribute.Validate(target, notification);
            }

            if (target is IValidated)
            {
                ((IValidated)target).Validate(notification);
            }

            return notification.GetFailures(propertyName);
        }
    }
}