
namespace ProjectApp.Model.Entities.User
{
    public class InternalUser : UserProfile
    {
        public InternalUser()
        {
        }

        public override int UserType { get => 1; set => UserProfileId = value; }
    }
}
