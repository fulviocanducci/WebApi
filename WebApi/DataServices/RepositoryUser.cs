using Service;
using Shared;
using System.Linq;

namespace WebApi.DataServices
{
    public class RepositoryUser : RepositoryUserImplementation
    {
        public RepositoryUser(DataService context) : base(context)
        {
        }

        public override User GetFirst(string email, string password)
        {
            return Context.Set<User>()
                    .Where(w => w.Email == email && w.Password == password)
                    .FirstOrDefault();
        }
    }
}
