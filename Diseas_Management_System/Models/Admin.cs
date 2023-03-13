namespace Diseas_Management_System.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    }
}
