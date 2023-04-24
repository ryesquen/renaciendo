using BancoAPI.Application.Interfaces;

namespace BancoAPI.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUTC => DateTime.Now;
    }
}
