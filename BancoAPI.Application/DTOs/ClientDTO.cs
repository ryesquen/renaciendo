namespace BancoAPI.Application.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int Edad { get; set; }
    }
}
