using AddressBook.Domain.Models.Room;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using System.Net.Mail;

namespace AddressBook.Api.Application.Commands.OrderCommand.InsertRoom
{
    public class InsertOrderRoomCommandHandler : IRequestHandler<InsertOrderRoomCommand, int>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<InsertOrderRoomCommand> _validator;
        private readonly IMapper _mapper;

        public InsertOrderRoomCommandHandler(IOrderRepository orderRepository, IValidator<InsertOrderRoomCommand> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<int> Handle(InsertOrderRoomCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            RoomRegistrationModel model = _mapper.Map<RoomRegistrationModel>(request);
            var email = await orderRepository.InsertRoom(model);
            if (email == null)
            {
                return 0;
            }
            string time = request.Time?.ToString("dd.MM.yyyy") + " " + request.StartHours?.ToString(@"hh\:mm");

            int id = 1;
            Task.Run(() => CarSendEmail(email.User, time, request.MailFrom, request.Password));
            return id;

        }


        public async Task CarSendEmail(string user, string time, string mailFrom, string password)
        {

            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            mail.From = new MailAddress(mailFrom);
            var result = await orderRepository.GetCarAdminFind();

            if (result.Any())
            {
                foreach (var admin in result)
                {
                    mail.To.Add(admin.username);
                }
            }
            mail.Subject = "Otaq rezervasiyası";
            mail.Body = $"Hörmətli admin,\n" +
                $"{user} {time} üçün sifariş əlavə etdi.";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(mailFrom, password);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mail);

        }
    }
}
