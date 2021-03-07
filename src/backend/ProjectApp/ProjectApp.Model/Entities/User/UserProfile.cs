
namespace ProjectApp.Model.Entities.User
{
    public abstract class UserProfile
    {
        public UserProfile()
        {
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public abstract int UserType { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
