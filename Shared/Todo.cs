using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class Todo
    {
        
        public int Id { get; set; }

        [Required()]
        public string Description { get; set; }

        public bool Done { get; set; } = false;
    }
}
