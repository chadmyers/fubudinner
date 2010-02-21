using System;
using System.Collections.Generic;
using System.Text;

namespace FubuDinner.Web.Infrastructure.Validation
{
    public class Notification
    {
        public static readonly string INVALID_FORMAT = "Invalid Format";
        public static readonly string MUST_BE_GREATER_OR_EQUAL_TO_ZERO = "Must be greater or equal to zero";
        public static readonly string MUST_BE_GREATER_THAN_ZERO = "Must be a positive number";
        public static readonly string REQUIRED_FIELD = "Required Field";
        public static readonly string SYSTEM_FAILURE = "System Failure";

        public static Notification Valid()
        {
            return new Notification();
        }

        public static Notification Invalid()
        {
            var returnValue = new Notification();
            returnValue.RegisterFailure("something", "something else");

            return returnValue;
        }

        private readonly Dictionary<string, BagOfFail> _bags = new Dictionary<string, BagOfFail>();
        private readonly Dictionary<string, Notification> _children = new Dictionary<string, Notification>();
        private readonly List<ValidationFailure> _list = new List<ValidationFailure>();

        public ValidationFailure[] AllFailures
        {
            get
            {
                _list.Sort();
                return _list.ToArray();
            }
        }

        public void Include(Notification peer)
        {
            _list.AddRange(peer.AllFailures);
        }

        public bool IsValid()
        {
            foreach (var pair in _children)
            {
                if (!pair.Value.IsValid())
                {
                    return false;
                }
            }

            return _list.Count == 0;
        }

        public ValidationFailure RegisterFailure(string fieldName, string message)
        {
            var failure = new ValidationFailure(fieldName, message);

            if (!_list.Contains(failure))
            {
                _list.Add(failure);

                FailuresFor(failure.FieldName).Add(failure);
            }

            return failure;
        }

        public ValidationFailure[] GetFailures(string fieldName)
        {
            return _list.FindAll(m => m.FieldName == fieldName).ToArray();
        }

        public void AddChild(string propertyName, Notification notification)
        {
            if (_children.ContainsKey(propertyName))
            {
                _children[propertyName] = notification;
            }
            else
            {
                _children.Add(propertyName, notification);
            }
        }

        public Notification GetChild(string propertyName)
        {
            if (_children.ContainsKey(propertyName))
            {
                return _children[propertyName];
            }

            return Valid();
        }


        public bool HasFailure(string fieldName, string messageText)
        {
            var message = new ValidationFailure(fieldName, messageText);
            return _list.Contains(message);
        }

        public void AliasField(string fieldName, string alias)
        {
            var substitution = string.Format("[{0}]", fieldName);
            foreach (var message in _list)
            {
                message.Substitute(substitution, alias);
            }
        }

        public bool IsValid(string fieldName)
        {
            foreach (var pair in _children)
            {
                if (!pair.Value.IsValid(fieldName))
                {
                    return false;
                }
            }

            return GetFailures(fieldName).Length == 0;
        }



        public void AssertValid()
        {
            if (!IsValid())
            {
                var sb = new StringBuilder();
                sb.AppendLine("Validation Failures");

                addFailures(sb);

                throw new ApplicationException(sb.ToString());
            }
        }

        private void addFailures(StringBuilder sb)
        {
            foreach (var failure in _list)
            {
                sb.AppendLine(failure.ToString());
            }

            foreach (var pair in _children)
            {
                sb.AppendLine("Properties from " + pair.Key);
                pair.Value.addFailures(sb);
            }
        }

        public BagOfFail FailuresFor(string fieldName)
        {
            if (!_bags.ContainsKey(fieldName))
            {
                var bag = new BagOfFail(fieldName);
                _bags.Add(fieldName, bag);
            }

            return _bags[fieldName];
        }

        public void ForEachField(Action<BagOfFail> action)
        {
            foreach (var bag in _bags.Values)
            {
                action(bag);
            }
        }

        public Notification Flatten()
        {
            var list = new List<ValidationFailure>();
            gather(list);

            var returnValue = new Notification();
            returnValue._list.AddRange(list);

            return returnValue;
        }

        private void gather(List<ValidationFailure> list)
        {
            list.AddRange(_list);
            foreach (var pair in _children)
            {
                pair.Value.gather(list);
            }
        }
    }
}