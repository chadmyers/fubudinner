using FubuDinner.Web.Infrastructure.Validation;

namespace FubuDinner.Web.Model
{
    public class Nerd : Entity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}