using BancoAPI.Domain.Common;

namespace BancoAPI.Domain.Entities
{
    public class Client : AuditableBaseEntity
    {
        private int _edad;
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int Edad
        {
            get
            {
                if (this._edad <= 0)
                {
                    this._edad = new DateTime(DateTime.Now.Subtract(this.Birthdate).Ticks).Year - 1;
                }
                return this._edad;
            }
        }
    }
}