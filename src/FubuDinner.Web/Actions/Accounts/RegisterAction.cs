using System;
using FubuDinner.Web.Infrastructure;
using FubuDinner.Web.Infrastructure.Validation;
using FubuDinner.Web.Model;
using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Accounts
{
    public class RegisterAction
    {
        private readonly AccountSettings _settings;
        private readonly IValidator _validator;
        private readonly IRepository _repository;

        public RegisterAction(AccountSettings settings, IValidator validator, IRepository repository)
        {
            _settings = settings;
            _validator = validator;
            _repository = repository;
        }

        public RegisterModel Query(RegisterModel input)
        {
            return new RegisterModel {PasswordLength = _settings.MinPasswordLength};
        }

        public RegisterModel Command(RegisterForm input)
        {
            var errors = _validator.Validate(input);

            //TODO: need unique username validation

            if( input.Nerd.Password != input.ConfirmPassword )
            {
                errors.RegisterFailure("ConfirmPassword", "Confirm password must match password exactly.");
            }
            
            if( errors.IsValid())
            {
                _repository.Save(input.Nerd);
            }

            return new RegisterModel { Errors = errors, Nerd = input.Nerd, ConfirmPassword = input.ConfirmPassword };
        }
    }

    public class RegisterForm : RegisterModel
    {
    }

    public class RegisterModel
    {
        public int PasswordLength { get; set; }

        public Notification Errors { get; set; }

        [Required, ValidatedChild]
        public Nerd Nerd { get; set; } 

        [Required]
        public string ConfirmPassword { get; set; }
    }

    public class Register : FubuPage<RegisterModel>
    {
    }
}