using System.Collections.Generic;

namespace FubuDinner.Web.Infrastructure.Validation
{
    public class BagOfFail
    {
        private readonly string _fieldName;
        private readonly List<ValidationFailure> _list = new List<ValidationFailure>();

        public BagOfFail(string fieldName)
        {
            _fieldName = fieldName;
        }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public void Add(ValidationFailure failure)
        {
            _list.Add(failure);
        }

        public ValidationFailure[] Failures
        {
            get
            {
                return _list.ToArray();
            }
        }

        public bool Contains(ValidationFailure failure)
        {
            return _list.Contains(failure);
        }
    }
}