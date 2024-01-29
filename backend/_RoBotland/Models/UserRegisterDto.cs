using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class UserRegisterDto
    {
        [Required(AllowEmptyStrings =false), MinLength(5)]
        public string Username { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!"), MinLength(5) ]
        public string Password { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!"), MinLength(5)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!"), MinLength(5)]
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "To pole jest wymagane!"), MinLength(5)]
        public string Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "To pole jest wymagane!"), MinLength(5)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string Street { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string City { get; set;}
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string HouseNumber { get; set; }
        [RegularExpression("^[0-9]{2}-[0-9]{3}$",
       ErrorMessage = "Zipcode is not valid!")]
        [Required(ErrorMessage = "To pole jest wymagane!"), MinLength(5)]
      
        public string ZipCode { get; set; }

    }
}
