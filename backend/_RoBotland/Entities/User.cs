using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class User
    {
        public User()
        {
            UserDetails = new UserDetails();
        }

        public User(Guid id, UserDetails userDetails)
        {
            Id = id;
            UserDetails = userDetails;
        }

        public Guid Id { get; set; }
        public UserDetails UserDetails { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

        //PL: w przyszłości będzie tutaj jeszcze lista uprawnień i tokenów 

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Id.Equals(user.Id) &&
                   EqualityComparer<UserDetails>.Default.Equals(UserDetails, user.UserDetails);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserDetails);
        }



        //without authentication and authorization
    }
}
