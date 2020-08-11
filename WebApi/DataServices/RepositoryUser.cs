using Service;
using Shared;
using System.Linq;

namespace WebApi.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryUser : RepositoryUserImplementation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepositoryUser(DataService context) : base(context)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override User GetFirst(string email, string password)
        {
            return Context.Set<User>()
                    .Where(w => w.Email == email && w.Password == password)
                    .FirstOrDefault();
        }
    }
}
