using AddressBook.Domain.Models.Room;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Commands.OrderCommand.UpdateRoom
{
    public class UpdateOrderRoomCommandHandler : IRequestHandler<UpdateOrderRoomCommand, int>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<UpdateOrderRoomCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateOrderRoomCommandHandler(IOrderRepository orderRepository, IValidator<UpdateOrderRoomCommand> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateOrderRoomCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            RoomRegistrationModel model = _mapper.Map<RoomRegistrationModel>(request);
            int id = await orderRepository.UpdateRoom(model);
            return id;


        }
    }
}