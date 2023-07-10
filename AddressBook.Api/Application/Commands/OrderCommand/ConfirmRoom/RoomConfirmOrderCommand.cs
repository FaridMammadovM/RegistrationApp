using MediatR;

namespace AddressBook.Api.Application.Commands.OrderCommand.ConfirmRoom
{
    public class RoomConfirmOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? RejectReason { get; set; }
        public int Status { get; set; }
        public int UpdateBy { get; set; }
        public string? MailFrom { get; set; }
        public string? Password { get; set; }
    }
}
