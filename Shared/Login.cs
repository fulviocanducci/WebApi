using System.ComponentModel.DataAnnotations;

namespace Shared
{
    /// <summary>
    /// Login class
    /// </summary>
    public class Login
    {
        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
