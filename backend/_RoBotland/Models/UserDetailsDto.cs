using _RoBotland.Enums;
using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class UserDetailsDto: OrderOptionsDto
    {
        public UserDetailsDto()
        {
        }

        public UserDetailsDto(string firstName, string lastName, string email, string phoneNumber, string street, string city, string houseNumber, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Street = street;
            City = city;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string Street { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string City { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]

        public string ZipCode { get; set; }

    }
}
