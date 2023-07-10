using AddressBook.Domain.Dtos.Order.Room;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.RoomGetAllByFilter
{
    public sealed class GetAllByFilterRoomOrderQueryHandler : IRequestHandler<GetAllByFilterRoomOrderQuery, List<GetAllByFilterRoomDto>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper _mapper;


        public GetAllByFilterRoomOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _mapper = mapper;

        }
        public async Task<List<GetAllByFilterRoomDto>> Handle(GetAllByFilterRoomOrderQuery request, CancellationToken cancellationToken)
        {
            GetAllRequestByFilterRoomDto requestdto = new GetAllRequestByFilterRoomDto();

            requestdto = _mapper.Map<GetAllRequestByFilterRoomDto>(request);

            var result = orderRepository.GetAllRoomFilter(requestdto);
            return await Task.FromResult(result.Result);

        }
    }
}