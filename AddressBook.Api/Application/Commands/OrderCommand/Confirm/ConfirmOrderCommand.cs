using MediatR;

namespace AddressBook.Api.Application.Commands.OrderCommand.Confirm
{
    public class ConfirmOrderCommand : IRequest<long?>
    {
        public int Id { get; set; }

        public int? CarId { get; set; }

        public int? DriverId { get; set; }

        public string? RejectionReason { get; set; }
        public int Status { get; set; }
        public string? UpdateUser { get; set; }
        public string? MailFrom { get; set; }
        public string? Password { get; set; }
    }
}
