namespace _RoBotland.Models
{
    public class UserDetails
    {
        public UserDetails()
        {
        }

        public UserDetails(Guid id, string firstName, string lastName, string email, string phoneNumber, string homeAddress, ICollection<Order> orders, User user)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            HomeAddress = homeAddress;
            Orders = orders;
            User = user;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAddress {  get; set; }
        public ICollection<Order> Orders { get; set; } //PL: informacje o historii zamówień po email
        public User User { get; set; }
      
    }
}
