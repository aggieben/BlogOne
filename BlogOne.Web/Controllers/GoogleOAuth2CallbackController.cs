using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlogOne.Web.Integration;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Auth.OAuth2.Mvc.Controllers;
using Google.Apis.Auth.OAuth2.Responses;

namespace BlogOne.Web.Controllers
{
    public class GoogleOAuth2CallbackController : AuthCallbackController
    {
        protected override FlowMetadata FlowData
        {
            get { return new AppFlowMetadata(); }
        }

        [Route("login/oauth/google")]
        public override Task<ActionResult> IndexAsync(AuthorizationCodeResponseUrl authorizationCode, CancellationToken taskCancellationToken)
        {
            return base.IndexAsync(authorizationCode, taskCancellationToken);
        }
    }
}