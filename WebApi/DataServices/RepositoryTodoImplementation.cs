using Repository;
using Service;
using Shared;

namespace WebApi.DataServices
{
    public abstract class RepositoryTodoImplementation : Repository<Todo, DataService>
    {
        protected RepositoryTodoImplementation(DataService context) : base(context)
        {
        }
    }
}
