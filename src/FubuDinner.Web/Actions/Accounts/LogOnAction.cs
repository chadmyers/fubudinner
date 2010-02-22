using System;
using System.Linq;
using FubuDinner.Web.Actions.Home;
using FubuDinner.Web.Infrastructure;
using FubuDinner.Web.Infrastructure.Validation;
using FubuDinner.Web.Model;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Accounts
{
    public class LogOnAction
    {
        private readonly IValidator _validator;
        private readonly IRepository _repository;
        private readonly IAuthenticationContext _authContext;
        private readonly IFubuRequest _request;

        public LogOnAction(IValidator validator, 
            IRepository repository, 
            IAuthenticationContext authContext,
            IFubuRequest request)
        {
            _validator = validator;
            _repository = repository;
            _authContext = authContext;
            _request = request;
        }

        public LogOnModel Query(LogOnModel input)
        {
            return input;
        }

        public FubuContinuation Command(LogOnModelForm input)
        {
            var validation = _validator.Validate(input);

            var user = findUser(input.Username, input.Password);

            if( user == null )
            {
                validation.RegisterFailure("Username", "The username or password provided is incorrect.");
            }

            if( validation.IsValid() )
            {
                var destination = input.ReturnUrl.IsNotEmpty() ? (object) input.ReturnUrl : new HomeModel();

                _authContext.ThisUserHasBeenAuthenticated(user.Username, input.RememberUser);
                return FubuContinuation.RedirectTo(destination);
            }

            return FubuContinuation.TransferTo(new LogOnModel
                                                   {
                                                       Errors = validation,
                                                       Username = input.Username,
                                                       rememberMe = input.RememberUser,
                                                       ReturnUrl = input.ReturnUrl
                                                   });
        }

        private Nerd findUser(string username, string password)
        {
            return _repository.Find<Nerd>(n =>
                n.Username == username && n.Password == password)
                .FirstOrDefault();
        }
    }

    public class LogOnModelForm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberUser { get { return rememberMe == "on"; } }
        public string rememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool rememberMe { get; set; }
        
        [QueryString]
        public string ReturnUrl { get; set; }

        public Notification Errors { get; set; }

        public bool Equals(LogOnModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Username, Username) 
                && Equals(other.Password, Password) 
                && other.rememberMe.Equals(rememberMe) 
                && Equals(other.ReturnUrl, ReturnUrl)
                && other.Errors.IsValid().Equals(Errors.IsValid());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (LogOnModel)) return false;
            return Equals((LogOnModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Username != null ? Username.GetHashCode() : 0);
                result = (result*397) ^ (Password != null ? Password.GetHashCode() : 0);
                result = (result*397) ^ rememberMe.GetHashCode();
                result = (result*397) ^ (ReturnUrl != null ? ReturnUrl.GetHashCode() : 0);
                return result;
            }
        }
    }

    public class LogOn : FubuPage<LogOnModel>
    {
    }
}