using System.Reflection;

namespace FubuDinner.Web.Infrastructure.Validation
{
    public class MaximumStringLengthAttribute : ValidationAttribute
    {
        private readonly int _length;

        public MaximumStringLengthAttribute(int length)
        {
            _length = length;
        }

        public int Length { get { return _length;  } }

        protected override void validate(object target, object rawValue, Notification notification)
        {
            if (rawValue == null)
            {
                return;
            }

            if (rawValue.ToString().Length <= _length) return;
            var message = string.Format("[{0}] cannot be longer than {1} characters", PropertyName, _length);
            logMessage(notification, message);
        }

        public static int GetLength(PropertyInfo property)
        {
            if (property.PropertyType != typeof(string))
            {
                return 0;
            }

            var attribute =
                GetCustomAttribute(property, typeof(MaximumStringLengthAttribute)) as MaximumStringLengthAttribute;

            return attribute == null ? 100 : attribute._length;
        }
    }
}