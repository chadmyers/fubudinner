using System;

namespace FubuDinner.Web.Infrastructure.Validation
{
    public class ValidationFailure : IComparable, IComparable<ValidationFailure>
    {
        public ValidationFailure()
        {
        }

        public ValidationFailure(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }

        public string FieldName { get; set; }
        public string Message { get; set; }

        public void Substitute(string substitution, string alias)
        {
            Message = Message.Replace(substitution, alias);
        }


        public int CompareTo(object obj)
        {
            var message = (ValidationFailure)obj;
            return CompareTo(message);
        }

        public int CompareTo(ValidationFailure other)
        {
            return FieldName == other.FieldName
                ? Message.CompareTo(other.Message)
                : FieldName.CompareTo(other.FieldName);
        }

        public override string ToString()
        {
            return string.Format("Field {0}:  {1}", FieldName, Message);
        }

        public bool Equals(ValidationFailure other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FieldName, FieldName) && Equals(other.Message, Message);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ValidationFailure)) return false;
            return Equals((ValidationFailure) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FieldName != null ? FieldName.GetHashCode() : 0)*397) ^ (Message != null ? Message.GetHashCode() : 0);
            }
        }
    }
}