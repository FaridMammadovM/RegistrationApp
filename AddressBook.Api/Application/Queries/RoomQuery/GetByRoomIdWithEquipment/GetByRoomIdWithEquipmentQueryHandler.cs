using AddressBook.Domain.Dtos.Room;
using AddressBook.Infrastructure.Repositories.RoomRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.RoomQuery.GetByRoomIdWithEquipment
{
    public sealed class GetByRoomIdWithEquipmentQueryHandler : IRequestHandler<GetByRoomIdWithEquipmentQuery, GetByIdRoomWithEpiuqmentDto>
    {
        private readonly IRoomRepository roomRepository;
        private readonly IValidator<GetByRoomIdWithEquipmentQuery> _validator;
        private readonly IMapper _mapper;


        public GetByRoomIdWithEquipmentQueryHandler(IRoomRepository roomRepository, IValidator<GetByRoomIdWithEquipmentQuery> validator, IMapper mapper)
        {
            this.roomRepository = roomRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<GetByIdRoomWithEpiuqmentDto> Handle(GetByRoomIdWithEquipmentQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var res = roomRepository.GetById(request.Id).Result;
            if (res != null)
            {
                return res;
            }
            return null;

        }
    }
}
