using AddressBook.Domain.Dtos.Employee;
using AddressBook.Domain.Models.Employee;
using AddressBook.Infrastructure.Helpers;
using DapperExtension;

namespace AddressBook.Infrastructure.Repositories.EmployeRepository
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        private string _conString;
        public EmployeeRepository(string conString)
        {
            _conString = conString;
        }
        public async Task<int?> InsertEmployee(Employee model)
        {
            try
            {
                int result = 0;
                using (var con = DbHelper.GetConn(_conString))
                {
                    con.Open();

                    string check = "\"AB_ORG\".checkorderno";

                    var bazaCheck = con
                     .GetFirstOrDefaultPostgreFunctionData<bool>($@"{check}(
                       @p_orderno)",

                 new
                 {
                     p_orderno = model.OrderNo
                 });

                    if (!bazaCheck)
                    {
                        result = 1;
                        con.Close();
                        return await Task.FromResult(result);
                    }


                    string checkUsername = "\"AB_ORG\".checkusername";

                    var bazaCheckUsername = con
                     .GetFirstOrDefaultPostgreFunctionData<bool>($@"{checkUsername}(
                       @username)",

                 new
                 {
                     username = model.UserName

                 });

                    if (bazaCheckUsername)
                    {
                        result = 2;
                        con.Close();
                        return await Task.FromResult(result);
                    }

                    string update = "\"AB_ORG\".updateorderno";

                    var bazaUpdate = con
                     .GetAllPostgreTableValuedFunctionData<string>($@"{update}(
                       @p_orderno)",

                 new
                 {
                     p_orderno = model.OrderNo,


                 });
                    model.InsertDate = DateTime.Now.ToUniversalTime();
                    result = con.Insert(model);
                    con.Close();

                }
                result = 3;
                return await Task.FromResult(result);
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> UpdateEmployee(Employee model)
        {
            try
            {
                int res = 0;
                using (var con = DbHelper.GetConn(_conString))
                {
                    con.Open();
                    string check = "\"AB_ORG\".checkorderno";

                    var bazaCheck = con
                     .GetFirstOrDefaultPostgreFunctionData<bool>($@"{check}(
                       @p_orderno)",

                 new
                 {
                     p_orderno = model.OrderNo,


                 });

                    if (!bazaCheck)
                    {
                        res = 1;

                        con.Close();
                        return await Task.FromResult(res);
                    }


                    Employee temp = con.GetById<Employee>(model.Id);
                    if (temp != null)
                    {
                        if (model.OrderNo < temp.OrderNo)
                        {
                            string update = "\"AB_ORG\".kyupdateorderno";

                            var updateCheck = con
                             .GetFirstOrDefaultPostgreFunctionData<string>($@"{update}(
                       @y_orderno, @k_orderno)",

                         new
                         {
                             y_orderno = model.OrderNo,
                             k_orderno = temp.OrderNo
                         });

                            temp.OrderNo = model.OrderNo;

                        }
                        else if (model.OrderNo > temp.OrderNo)
                        {
                            string update = "\"AB_ORG\".kyupdateorderno2";

                            var updateCheck = con
                             .GetAllPostgreTableValuedFunctionData<string>($@"{update}(
                       @y_orderno, @k_orderno)",

                         new
                         {
                             y_orderno = model.OrderNo,
                             k_orderno = temp.OrderNo
                         });

                            temp.OrderNo = model.OrderNo;
                        }
                        temp.Firstname = model.Firstname;
                        temp.Surname = model.Surname;
                        temp.Patronymic = model.Patronymic;
                        temp.PositionId = model.PositionId;
                        temp.DepartmentId = model.DepartmentId;
                        temp.InternalTelephone = model.InternalTelephone;
                        temp.Email = model.Email;
                        temp.RoomNumber = model.RoomNumber;
                        temp.MobilPhone = model.MobilPhone;
                        temp.Password = model.Password;
                        temp.UserRole = model.UserRole;
                        temp.UserName = model.UserName;
                        temp.Gender = model.Gender;
                        temp.Floor = model.Floor;
                        temp.UpdateUser = model.UpdateUser;
                        temp.UpdateDate = DateTime.Now.ToUniversalTime();
                        res = con.Update(temp);
                    }
                    res = 3;
                    con.Close();
                    return await Task.FromResult(res);
                }

            }
            catch (Exception)
            {

                return 0;
            }

        }

        public async Task<Employee> GetById(int id)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Employee model = con.GetById<Employee>(id);
                con.Close();
                return await Task.FromResult(model);
            }
        }

        public async Task<int> Delete(int id, string deleteUser)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                int res = 0;

                con.Open();
                Employee model = con.GetById<Employee>(id);

                if (model != null)
                {

                    string update = "\"AB_ORG\".minusupdateorderno";

                    var bazaUpdate = con
                     .GetAllPostgreTableValuedFunctionData<string>($@"{update}(
                       @p_orderno)",

                 new
                 {
                     p_orderno = model.OrderNo,


                 });

                    model.UpdateDate = DateTime.Now.ToUniversalTime();
                    model.UpdateUser = deleteUser;
                    model.IsDeleted = true;
                    res = con.Update(model);
                }
                con.Close();
                return await Task.FromResult(res);
            }

        }


        public async Task<List<EmployeeGetAllReturnDto>> GetAllFilter(EmployeeGetAllDto request)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();


                string baza = "\"AB_ORG\".get_employee_by_params_filter_last";
                var res = con
                         .GetAllPostgreTableValuedFunctionData<EmployeeGetAllReturnDto>($@"{baza}(
                       @p_key_world,  @firstname, @surname, @patronymic, @position_id, @floor, @room_number, @structure_id)",

                     new
                     {
                         p_key_world = request.KeyWord,
                         firstname = request.Firstname,
                         surname = request.Surname,
                         patronymic = request.Patronymic,
                         position_id = request.PositionId,
                         floor = request.Floor,
                         room_number = request.RoomNumber,
                         structure_id = request.DepartmentId
                     });

                con.Close();

                return await Task.FromResult(res.ToList());
            }
        }

        public async Task<Employee> GetLogin(LoginDto data)
        {
            Employee model = new Employee();
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                var a = con.GetByWhere<Employee>("WHERE \"user_name\"=@Username AND \"password\"=@Password", new { Username = data.Username, Password = data.Password });
                if (a.Count() != 0)
                {
                    model = a.First();
                }

                con.Close();

                return await Task.FromResult(model);
            }
        }



        public async Task<List<Employee>> GetAll()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                var res = con.GetAll<Employee>().OrderBy(e => e.Firstname).ToList();

                con.Close();
                return await Task.FromResult(res.ToList());
            }
        }

        public async Task<List<Employee>> Driver()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                string baza = "\"AB_ORG\".get_driver";
                var res = con.GetAllPostgreTableValuedFunctionData<Employee>($@"{baza}()").OrderBy(x => x.Firstname);

                con.Close();
                return await Task.FromResult(res.ToList());
            }
        }
    }
}
