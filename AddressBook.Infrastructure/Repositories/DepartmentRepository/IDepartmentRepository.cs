using AddressBook.Domain.Models;
using AddressBook.Domain.Models.Department;

namespace AddressBook.Infrastructure.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<long?> InsertDepartment(Department model);
        Task<int> UpdateDepartment(Department model);
        Task<Department> GetById(long id);
        Task<List<Department>> GetAll();
        Task<List<Position>> GetAllPosition();
        Task<List<Car>> GetAllCar();
        Task<int> Delete(long Id);
    }
}
