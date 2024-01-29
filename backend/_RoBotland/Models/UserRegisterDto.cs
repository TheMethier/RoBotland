using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class UserRegisterDto
    {
        [Required, MinLength(5)]
        public string Username { get; set; }
        [Required, MinLength(5) ]
        public string Password { get; set; }
        [Required, MinLength(5)]
        public string FirstName { get; set; }
        [Required, MinLength(5)]
        public string LastName { get; set; }
        [Required, MinLength(5)]
        [EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(5)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required, MinLength(5)]
        public string ZipCode { get; set; }

    }
}
