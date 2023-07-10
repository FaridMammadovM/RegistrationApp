using AddressBook.Domain.Dtos.Order.Room;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.RoomGetById
{
    public sealed class RoomOrderGetByIdQueryHandler : IRequestHandler<RoomOrderGetByIdQuery, GetByIdRetunRoomDto>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<RoomOrderGetByIdQuery> _validator;
        private readonly IMapper _mapper;


        public RoomOrderGetByIdQueryHandler(IOrderRepository orderRepository, IValidator<RoomOrderGetByIdQuery> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<GetByIdRetunRoomDto> Handle(RoomOrderGetByIdQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            GetByIdRetunRoomDto result = new();
            var res = orderRepository.GetByRoomId(request.Id).Result;
            if (res != null)
            {
                GetByIdRetunRoomDto roomDto = _mapper.Map<GetByIdRetunRoomDto>(res);
                result = roomDto;
            }
            return await Task.FromResult(result);

        }
    }
}
