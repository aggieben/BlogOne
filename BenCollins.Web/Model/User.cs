using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenCollins.Web.Model
{
    public class User : IUser
    {
        private readonly string _id;

        public string Id
        {
            get { return _id; }
        }

        public string UserName { get; set; }

        public User(string id, string username)
        {
            _id = id;
            UserName = username;
        }
    }
}