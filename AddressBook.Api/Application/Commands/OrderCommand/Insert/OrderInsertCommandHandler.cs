using AddressBook.Domain.Models;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using System.Net.Mail;

namespace AddressBook.Api.Application.Commands.OrderCommand.Insert
{
    public class OrderInsertCommandHandler : IRequestHandler<OrderInsertCommand, long?>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<OrderInsertCommand> _validator;
        private readonly IMapper _mapper;

        public OrderInsertCommandHandler(IOrderRepository orderRepository, IValidator<OrderInsertCommand> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<long?> Handle(OrderInsertCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);


            if (request.DepartureTimeDay == null)
            {
                var now = DateTime.Now.AddHours(4);
                var selectedTime = (TimeSpan)request.DepartureTimeHours;
                if (now.TimeOfDay < new TimeSpan(15, 0, 0))
                {
                    request.DepartureTimeDay = DateTime.UtcNow;
                    request.ReturnTimeDay = DateTime.UtcNow;
                }
                else if (selectedTime > TimeSpan.Parse("00:00") && selectedTime < TimeSpan.Parse("09:59:59"))
                {
                    request.DepartureTimeDay = DateTime.UtcNow.AddDays(1);
                    request.ReturnTimeDay = DateTime.UtcNow.AddDays(1);
                }
                else
                {
                    request.DepartureTimeDay = DateTime.UtcNow;
                    request.ReturnTimeDay = DateTime.UtcNow;
                }
            }

            Order model = _mapper.Map<Order>(request);
            var email = await orderRepository.Insert(model);
            string time = request.DepartureTimeDay?.ToString("dd.MM.yyyy") + " " + request.DepartureTimeHours?.ToString(@"hh\:mm");

            long id = 1;
            Task.Run(() => SendEmail(email.User, time, request.MailFrom, request.Password));
            return id;

        }

        public async Task SendEmail(string user, string time, string mailFrom, string password)
        {

            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            mail.From = new MailAddress(mailFrom);
            var result = await orderRepository.GetAdminFind();

            if (result.Any())
            {
                foreach (var admin in result)
                {
                    mail.To.Add(admin.username);
                }
            }
            mail.Subject = "Maşın rezervasiyası";
            mail.Body = $"Hörmətli admin,\n" +
                $"{user} {time} üçün sifariş əlavə etdi.";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(mailFrom, password);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mail);

        }
    }
}
