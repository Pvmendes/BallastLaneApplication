
namespace BallastLaneApplication.Core.Entities
{
    public class UserEntitie
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
    }
}
