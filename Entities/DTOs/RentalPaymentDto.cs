using Entities.Concrete;

namespace Entities.DTOs
{
    public class RentalPaymentDto
    {
        public Rental Rental { get; set; }
        public Payment Payment { get; set; }
    }
}
