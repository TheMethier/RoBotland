namespace _RoBotland.Models
{
    public class UserDetails
    {
        public UserDetails()
        {
            Username = "user";
            Password = "user1";
            Email = "user@user.pl";
            PhoneNumber = "123 456 789";
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Order> Orders { get; set; }
        public User User { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is UserDetails details &&
                   Id.Equals(details.Id) &&
                   Username == details.Username &&
                   Password == details.Password &&
                   Email == details.Email &&
                   PhoneNumber == details.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username, Password, Email, PhoneNumber);
        }
    }
}
