using AddressBook.Domain.Dtos.Room;
using AddressBook.Infrastructure.Repositories.RoomRepository;
using AutoMapper;
using MediatR;

namespace AddressBook.Api.Application.Queries.RoomQuery
{
    public sealed class GetAllRoomQueryHandler : IRequestHandler<GetAllRoomQuery, List<GetAllRoomDto>>
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper _mapper;


        public GetAllRoomQueryHandler(IRoomRepository roomRepository, IMapper mapper)
        {
            this.roomRepository = roomRepository;
            _mapper = mapper;

        }
        public async Task<List<GetAllRoomDto>> Handle(GetAllRoomQuery request, CancellationToken cancellationToken)
        {

            var result = roomRepository.GetAll().Result;
            List<GetAllRoomDto> returnDtos = new List<GetAllRoomDto>();

            foreach (var item in result)
            {
                GetAllRoomDto model = _mapper.Map<GetAllRoomDto>(item);
                returnDtos.Add(model);
            }
            return await Task.FromResult(returnDtos);

        }
    }
}