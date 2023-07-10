using AddressBook.Domain.Dtos.Car;
using AddressBook.Domain.Models;
using AddressBook.Infrastructure.Helpers;
using DapperExtension;

namespace AddressBook.Infrastructure.Repositories.CarRepository
{
    public sealed class CarRepository : ICarRepository
    {
        private string _conString;
        public CarRepository(string conString)
        {
            _conString = conString;
        }

        public async Task<int?> InsertCar(Car model)
        {
            try
            {
                int result = 0;
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

        public async Task<int> UpdateCar(Car model)
        {
            int res = 0;
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Car temp = con.GetById<Car>(model.Id);
                if (temp != null)
                {
                    temp.Brand = model.Brand;
                    temp.Model = model.Model;
                    temp.Number = model.Number;
                    temp.Seat = model.Seat;
                    temp.Kind = model.Kind;
                    temp.Color = model.Color;
                    temp.Year = model.Year;
                    temp.UpdateDate = DateTime.Now.ToUniversalTime();
                    res = con.Update(temp);
                }
                con.Close();
                return await Task.FromResult(res);
            }
        }

        public async Task<Car> GetById(int id)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Car model = con.GetById<Car>(id);
                con.Close();
                return await Task.FromResult(model);
            }
        }

        public async Task<List<GetAllCarDataDto>> GetAll()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                string baza = "\"AB_CAR\".get_allcardata";

                var res = con
                         .GetAllPostgreTableValuedFunctionData<GetAllCarDataDto>($@"{baza}()");
                con.Close();

                return await Task.FromResult(res.ToList());
            }
        }

        public async Task<int> Delete(int id, string insertUser)
        {
            int res = 0;
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Car temp = con.GetById<Car>(id);
                if (temp != null)
                {
                    temp.InsertUser = insertUser;
                    temp.UpdateDate = DateTime.Now.ToUniversalTime();
                    temp.IsDeleted = true;
                    res = con.Update(temp);
                }
                con.Close();
                return await Task.FromResult(res);
            }
        }

        public async Task<List<CarDto>> GetAllCar()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                string baza = "\"AB_CAR\".get_allcarname";

                var res = con
                         .GetAllPostgreTableValuedFunctionData<CarDto>($@"{baza}()");
                con.Close();

                return await Task.FromResult(res.ToList());
            }
        }
    }
}
