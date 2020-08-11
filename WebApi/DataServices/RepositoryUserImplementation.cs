using Repository;
using Service;
using Shared;

namespace WebApi.DataServices
{
    public abstract class RepositoryUserImplementation : Repository<User, DataService>
    {
        public RepositoryUserImplementation(DataService context) : base(context)
        {
        }

        public abstract User GetFirst(string email, string password);
    }
}
