namespace _RoBotland.Models
{
    public class UserDetails
    {

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
