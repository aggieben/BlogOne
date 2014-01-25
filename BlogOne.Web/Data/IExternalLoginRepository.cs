using BlogOne.Web.Model;

namespace BlogOne.Web.Data
{
    public interface IExternalLoginRepository : IRepository<ExternalLogin>
    {
        ExternalLogin FindByProviderAndKey(string provider, string key);
    }
}
