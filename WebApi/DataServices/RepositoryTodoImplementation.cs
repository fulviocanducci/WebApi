using Repository;
using Service;
using Shared;

namespace WebApi.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RepositoryTodoImplementation : Repository<Todo, DataService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepositoryTodoImplementation(DataService context) : base(context)
        {
        }
    }
}
