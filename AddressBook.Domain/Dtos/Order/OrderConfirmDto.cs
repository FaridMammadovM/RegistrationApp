namespace AddressBook.Domain.Dtos.Order
{
    public sealed class OrderConfirmDto
    {
        public int Id { get; set; }

        public int? CarId { get; set; }

        public int? DriverId { get; set; }

        public string? RejectionReason { get; set; }
        public int Status { get; set; }

    }
}
