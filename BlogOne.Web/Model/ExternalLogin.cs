using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogOne.Web.Model
{
    public class ExternalLogin : ModelBase
    {
        public string DefaultUserName { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        public ExternalLogin() { }

        public ExternalLogin(ExternalLoginInfo fromInfo)
        {
            DefaultUserName = fromInfo.DefaultUserName;
            LoginProvider = fromInfo.Login.LoginProvider;
            ProviderKey = fromInfo.Login.ProviderKey;
        }

        public static implicit operator ExternalLoginInfo(ExternalLogin login)
        {
            return new ExternalLoginInfo
            {
                DefaultUserName = login.DefaultUserName,
                Login = new Microsoft.AspNet.Identity.UserLoginInfo(login.LoginProvider, login.ProviderKey)
            };
        }

        public static implicit operator ExternalLogin(ExternalLoginInfo loginInfo)
        {
            return new ExternalLogin(loginInfo);
        }
    }
}