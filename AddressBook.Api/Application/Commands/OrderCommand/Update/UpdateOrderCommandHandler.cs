using AddressBook.Domain.Models;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Commands.OrderCommand.Update
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, long?>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<UpdateOrderCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IValidator<UpdateOrderCommand> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<long?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
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
            long? id = await orderRepository.Update(model);
            return id;


        }
    }
}
