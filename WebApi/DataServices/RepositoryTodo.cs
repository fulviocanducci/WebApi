using Service;

namespace WebApi.DataServices
{
    public class RepositoryTodo : RepositoryTodoImplementation
    {
        public RepositoryTodo(DataService context) : base(context)
        {
        }
    }
}
