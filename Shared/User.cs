using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class User
    {
        public int Id { get; set; }

        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
