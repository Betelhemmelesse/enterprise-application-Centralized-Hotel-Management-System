using Modules.ReservationPricing.Domain.Events;

namespace Modules.CustomerManagement.Consumer
{
    public class CustomerActivityConsumer
    {
        public void ConsumeReservation(RoomReserved @event)
        {
            Console.WriteLine($"Customer Management: Customer {@event.CustomerName} made a reservation (ID: {@event.ReservationId})");
        }
    }
}
