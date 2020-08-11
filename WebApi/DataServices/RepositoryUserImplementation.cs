using Repository;
using Service;
using Shared;

namespace WebApi.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RepositoryUserImplementation : Repository<User, DataService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepositoryUserImplementation(DataService context) : base(context)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public abstract User GetFirst(string email, string password);
    }
}
