using BenCollins.Web.Model;

namespace BenCollins.Web.Data
{
    public interface IExternalLoginRepository : IRepository<ExternalLogin>
    {
        ExternalLogin FindByProviderAndKey(string provider, string key);
    }
}
