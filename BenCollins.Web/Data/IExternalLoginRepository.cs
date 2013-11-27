using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenCollins.Web.Data
{
    public interface IExternalLoginRepository : IRepository<ExternalLoginInfo>
    {
    }
}
