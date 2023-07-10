using AddressBook.Domain.Dtos.Employee;
using AddressBook.Domain.Models.Employee;

namespace AddressBook.Infrastructure.Repositories.EmployeRepository
{
    public interface IEmployeeRepository
    {
        Task<int?> InsertEmployee(Employee model);

        Task<int> UpdateEmployee(Employee model);

        Task<Employee> GetById(int id);
        Task<int> Delete(int Id, string deleteUser);

        Task<List<EmployeeGetAllReturnDto>> GetAllFilter(EmployeeGetAllDto request);
        Task<List<Employee>> GetAll();
        Task<List<Employee>> Driver();
        Task<Employee> GetLogin(LoginDto data);
    }
}
