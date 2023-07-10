namespace AddressBook.Domain.Dtos.Order.Room
{
    public sealed class OrderRoomConfirmDto
    {
        public int Id { get; set; }
        public string? RejectionReason { get; set; }
        public int Status { get; set; }
    }
}
