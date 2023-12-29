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

        [Required, MinLength(5)]
        public string FirstName { get; set; }
        [Required, MinLength(5)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string ZipCode { get; set; }

    }
}
