namespace FubuDinner.Web.Infrastructure.Validation
{
    public interface IValidated
    {
        void Validate(Notification notification);
    }
}