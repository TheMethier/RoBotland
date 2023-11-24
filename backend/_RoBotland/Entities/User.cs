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
        //PL: w przyszłości będzie tutaj jescze lista uprawnień i tokenów 

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
