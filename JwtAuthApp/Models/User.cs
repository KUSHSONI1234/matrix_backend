namespace JwtAuthApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Encrypted string stored
        // public object UserPageAccesses { get; internal set; }
        
    }
}
