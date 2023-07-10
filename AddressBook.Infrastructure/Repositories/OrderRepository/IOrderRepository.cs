using AddressBook.Domain.Dtos.Employee;
using AddressBook.Domain.Dtos.Order;
using AddressBook.Domain.Dtos.Order.Room;
using AddressBook.Domain.Models;
using AddressBook.Domain.Models.Room;

namespace AddressBook.Infrastructure.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<EmailDto?> Insert(Order model);

        Task<EmailDto?> InsertRoom(RoomRegistrationModel model);

        Task<List<Order>> GetAll();
        Task<Order> GetById(long id);

        Task<RoomRegistrationModel> GetByRoomId(int id);


        Task<List<OrderGetAllReturnDto>> GetAllFilter(OrderGetAllDto request);

        Task<List<GetAllByFilterRoomDto>> GetAllRoomFilter(GetAllRequestByFilterRoomDto request);

        Task<int> Update(Order model);
        Task<int> UpdateRoom(RoomRegistrationModel model);

        Task<EmailDto> Confirm(Order model);
        Task<EmailDto> ConfirmRoom(RoomRegistrationModel model);


        Task<List<UsernameDto>> GetAdminFind();
        Task<List<UsernameDto>> GetCarAdminFind();

    }
}
