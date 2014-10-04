using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using System.Web.Configuration;

namespace BlogOne.Web.Integration
{
    public class AppFlowMetadata : FlowMetadata
    {
        private readonly GoogleAuthorizationCodeFlow _flow;

        public AppFlowMetadata(bool forceApproval = false)
        {
            var settings = WebConfigurationManager.OpenWebConfiguration("~").AppSettings.Settings;
            _flow = new AppGoogleAuthorizationCodeFlow(new AppGoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = settings[AppSettingsKeys.GoogleClientId].Value,
                    ClientSecret = settings[AppSettingsKeys.GoogleClientSecret].Value
                },
                /* I wanted to use a defined constant somewhere for this, but couldn't find it and didn't want to pull in more nupkegs just for that. */
                Scopes = new[] { DriveService.Scope.DriveAppdata, "email" },
                DataStore = new AppSettingsDataStore(),
                ForceApproval = forceApproval
            });
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return _flow; }
        }

        public override string GetUserId(System.Web.Mvc.Controller controller)
        {
            return "admin";
        }

        public override string AuthCallback
        {
            get { return "/login/oauth/google"; }
        }
    }
}