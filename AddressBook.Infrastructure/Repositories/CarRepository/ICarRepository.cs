using AddressBook.Domain.Dtos.Car;
using AddressBook.Domain.Models;

namespace AddressBook.Infrastructure.Repositories.CarRepository
{
    public interface ICarRepository
    {
        Task<int?> InsertCar(Car model);
        Task<int> UpdateCar(Car model);
        Task<Car> GetById(int id);
        Task<List<GetAllCarDataDto>> GetAll();
        Task<List<CarDto>> GetAllCar();
        Task<int> Delete(int Id, string insertUser);
    }
}
