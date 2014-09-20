using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;

namespace BlogOne.Web.Integration
{
    public class AppGoogleAuthorizationCodeFlow : GoogleAuthorizationCodeFlow
    {
        public readonly bool _forceApprovalPrompt;
        public AppGoogleAuthorizationCodeFlow(Initializer initializer) : base(initializer)
        {
            _forceApprovalPrompt = initializer.ForceApproval;
        }

        public override AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri)
        {
            var codeRequest = base.CreateAuthorizationCodeRequest(redirectUri) as GoogleAuthorizationCodeRequestUrl;
            codeRequest.ApprovalPrompt = _forceApprovalPrompt ? "force" : null;
            return codeRequest;
        }

        public new class Initializer : GoogleAuthorizationCodeFlow.Initializer
        {
            public bool ForceApproval { get; set; }
        }
    }
}