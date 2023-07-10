using AddressBook.Domain.Dtos.Room;
using AddressBook.Domain.Models.Room;

namespace AddressBook.Infrastructure.Repositories.RoomRepository
{
    public interface IRoomRepository
    {
        Task<List<RoomModel>> GetAll();

        Task<GetByIdRoomWithEpiuqmentDto> GetById(int id);


    }
}
