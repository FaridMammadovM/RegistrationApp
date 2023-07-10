using AddressBook.Domain.Models;
using AddressBook.Domain.Models.Department;
using AddressBook.Infrastructure.Helpers;
using DapperExtension;

namespace AddressBook.Infrastructure.Repositories.DepartmentRepository
{
    public sealed class DepartmentRepository : IDepartmentRepository
    {
        private string _conString;
        public DepartmentRepository(string conString)
        {
            _conString = conString;
        }

        public async Task<long?> InsertDepartment(Department model)
        {
            try
            {
                long result = 0;
                using (var con = DbHelper.GetConn(_conString))
                {
                    con.Open();
                    model.InsertDate = DateTime.Now.ToUniversalTime();
                    result = con.Insert(model);
                    con.Close();

                }
                return await Task.FromResult(result);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> UpdateDepartment(Department model)
        {
            int res = 0;
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Department temp = con.GetById<Department>(model.Id);
                if (temp != null)
                {
                    model.UpdateDate = DateTime.Now.ToUniversalTime();
                    res = con.Update(model);
                }
                con.Close();
                return await Task.FromResult(res);
            }
        }

        public async Task<Department> GetById(long id)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Department model = con.GetById<Department>(id);
                con.Close();
                return await Task.FromResult(model);
            }
        }

        public async Task<List<Department>> GetAll()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                var x = con.GetAll<Department>();
                con.Close();
                return await Task.FromResult(x.ToList());
            }
        }

        public async Task<int> Delete(long id)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                var departments = con.Delete<Department>(id);
                con.Close();
                return await Task.FromResult(1);
            }

        }
        public async Task<List<Position>> GetAllPosition()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                var res = con.GetAll<Position>().OrderBy(e => e.Name).ToList();
                con.Close();

                return await Task.FromResult(res.ToList());
            }
        }

        public async Task<List<Car>> GetAllCar()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                var res = con.GetAll<Car>().OrderBy(X => X.Id);
                con.Close();
                return await Task.FromResult(res.ToList());
            }
        }
    }
}
