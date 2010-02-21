namespace FubuDinner.Web.Infrastructure.Validation
{
    public interface IValidator
    {
        Notification Validate(object target);
        ValidationFailure[] ValidateByField(object target, string propertyName);
    }
}