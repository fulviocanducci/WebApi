using System.ComponentModel.DataAnnotations;

namespace Shared
{
    /// <summary>
    /// Todo class
    /// </summary>
    public class Todo
    {
        
        public int Id { get; set; }

        [Required()]
        public string Description { get; set; }

        public bool Done { get; set; } = false;
    }
}
