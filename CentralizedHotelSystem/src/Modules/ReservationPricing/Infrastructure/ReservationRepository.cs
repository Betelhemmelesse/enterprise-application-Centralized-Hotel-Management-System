using Modules.ReservationPricing.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Modules.ReservationPricing.Infrastructure
{
    public interface IReservationRepository
    {
        Task AddAsync(RoomReservation reservation);
    }

    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDbContext _db;
        public ReservationRepository(ReservationDbContext db) => _db = db;

        public async Task AddAsync(RoomReservation reservation)
        {
            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();
        }
    }
}
