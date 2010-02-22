using FubuDinner.Web.Actions.Accounts;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;

namespace FubuDinner.Web.Infrastructure.Behaviors
{
    public class MustBeAuthenticatedBehavior : BasicBehavior
    {
        private readonly ISecurityContext _securityContext;
        private readonly IOutputWriter _writer;
        private readonly IUrlRegistry _urls;
        private readonly IFubuRequest _request;

        public MustBeAuthenticatedBehavior(ISecurityContext securityContext, IOutputWriter writer, IUrlRegistry urls, IFubuRequest request) 
            : base(PartialBehavior.Ignored)
        {
            _securityContext = securityContext;
            _writer = writer;
            _urls = urls;
            _request = request;
        }

        protected override DoNext performInvoke()
        {
            if( ! _securityContext.IsAuthenticated() )
            {
                var model = _request.Get<ReturnUrlModel>();
                var url = _urls.UrlFor(new LogOnModel { ReturnUrl = model.RawUrl });
                _writer.RedirectToUrl(url);
                return DoNext.Stop;
            }

            return DoNext.Continue;
        }
    }

    public class ReturnUrlModel
    {
        public string RawUrl { get; set; }
    }
}