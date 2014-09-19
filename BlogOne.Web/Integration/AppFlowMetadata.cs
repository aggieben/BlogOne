using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using System;
using System.Web.Configuration;

namespace BlogOne.Web.Integration
{
    public class AppFlowMetadata : FlowMetadata
    {
        private readonly GoogleAuthorizationCodeFlow _flow;

        public AppFlowMetadata()
        {
            var settings = WebConfigurationManager.OpenWebConfiguration("~").AppSettings.Settings;
            _flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = settings[AppSettingsKeys.GoogleClientId].Value,
                    ClientSecret = settings[AppSettingsKeys.GoogleClientSecret].Value
                },
                Scopes = new[] { DriveService.Scope.DriveAppdata },
                DataStore = new AppSettingsDataStore()
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
    }
}