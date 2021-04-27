
namespace ProjectApp.Model.Entities.User
{
    public class Client : UserProfile
    {
        public Client()
        {
        }

        public override int UserType { get => 2; set => UserProfileId = value; }
    }
}
