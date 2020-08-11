using Service;

namespace WebApi.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryTodo : RepositoryTodoImplementation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepositoryTodo(DataService context) : base(context)
        {
        }
    }
}
