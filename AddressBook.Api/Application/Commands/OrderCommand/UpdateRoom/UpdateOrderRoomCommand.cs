using MediatR;

namespace AddressBook.Api.Application.Commands.OrderCommand.UpdateRoom
{
    public class UpdateOrderRoomCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int UpdateBy { get; set; }
        public string? InsertUser { get; set; }
        public string? MailFrom { get; set; }
        public string? Password { get; set; }
        public string MeetName { get; set; }
        public int ParticipantCount { get; set; }

        public DateTime? Time { get; set; }

        public TimeSpan? StartHours { get; set; }

        public TimeSpan? EndHours { get; set; }
        public int RoomId { get; set; }
        public string? Note { get; set; }
    }
}