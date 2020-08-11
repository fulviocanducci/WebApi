using Repository;
using Service;
using Shared;

namespace WebApi.DataServices
{
    public abstract class RepositoryTodoImplementation : Repository<Todo, DataService>
    {
        public RepositoryTodoImplementation(DataService context) : base(context)
        {
        }
    }
}
