using AddressBook.Domain.Dtos.Employee;
using AddressBook.Domain.Dtos.Order;
using AddressBook.Domain.Dtos.Order.Room;
using AddressBook.Domain.Models;
using AddressBook.Domain.Models.Room;
using AddressBook.Infrastructure.Helpers;
using DapperExtension;

namespace AddressBook.Infrastructure.Repositories.OrderRepository
{
    public sealed class OrderRepository : IOrderRepository
    {
        private string _conString;
        public OrderRepository(string conString)
        {
            _conString = conString;
        }

        public async Task<EmailDto?> Insert(Order model)
        {
            EmailDto emailDto = new EmailDto();
            try
            {
                long result = 0;
                using (var con = DbHelper.GetConn(_conString))
                {
                    con.Open();
                    model.Status = "Gözləmədə";
                    model.InsertDate = DateTime.Now.ToUniversalTime();

                    result = con.Insert(model);
                    string baza = "\"AB_ORG\".get_fullname";
                    if (model.EmployeeId != null)
                    {
                        var res = con
                         .GetAllPostgreTableValuedFunctionData<EmailBazaDto>($@"{baza}(
                       @p_employee_id)",

                     new
                     {
                         p_employee_id = model.EmployeeId,


                     });
                        foreach (var item in res)
                        {
                            emailDto.User = item.Fullname;
                        }

                    }

                    con.Close();

                }
                return await Task.FromResult(emailDto);
            }
            catch
            {
                return await Task.FromResult(emailDto);
            }
        }

        public async Task<List<Order>> GetAll()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                var x = con.GetAll<Order>();
                con.Close();
                return await Task.FromResult(x.ToList());
            }
        }

        public async Task<Order> GetById(long id)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Order model = con.GetById<Order>(id);
                con.Close();
                return await Task.FromResult(model);
            }
        }

        public async Task<int> Update(Order model)
        {
            int result;


            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                Order order = con.GetById<Order>(model.Id);

                if (order == null)
                {
                    result = 0;
                    con.Close();

                    return result;
                }
                else if (model.Status == null)
                {
                    order.EmployeeId = model.EmployeeId;
                    order.PassengerType = model.PassengerType;
                    order.PassengerCount = model.PassengerCount;
                    order.EmployeeUsingId = model.EmployeeUsingId;
                    order.DepartureType = model.DepartureType;
                    order.DepartureTimeDay = model.DepartureTimeDay;
                    order.ReturnTimeDay = model.ReturnTimeDay;
                    order.DepartureTimeHours = model.DepartureTimeHours;
                    order.ReturnTimeHours = model.ReturnTimeHours;
                    order.Direction = model.Direction;
                    order.Luggage = model.Luggage;
                    order.LuggageSize = model.LuggageSize;
                    order.Address = model.Address;
                    order.Note = model.Note;
                    order.CarId = model.CarId;
                    order.DriverId = model.DriverId;
                    order.UpdateDate = DateTime.UtcNow;

                }

                con.Update(order);

                result = 1;

                con.Close();
            }
            return result;
        }

        public async Task<EmailDto> Confirm(Order model)
        {
            EmailDto emailDto = new EmailDto();

            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                string baza = "\"AB_ORG\".get_username_three";
                string bazatwo = "\"AB_ORG\".get_username_two";
                string bazatwodriver = "\"AB_ORG\".get_username_two_driver";
                Order order = con.GetById<Order>(model.Id);

                if (order == null)
                {
                    con.Close();

                    return emailDto;
                }
                else
                {
                    order.Status = model.Status;
                    order.CarId = model.CarId;
                    order.DriverId = model.DriverId;
                    order.RejectionReason = model.RejectionReason;
                    order.UpdateUser = model.UpdateUser;
                }
                con.Update(order);

                if (order.EmployeeId != null && order.EmployeeUsingId != null && order.EmployeeUsingId != 0 && order.DriverId != null)
                {
                    var res = con
                         .GetAllPostgreTableValuedFunctionData<EmailBazaDto>($@"{baza}(
                       @p_employee_id, @p_employee_using_id, @p_driver_id)",

                     new
                     {
                         p_employee_id = order.EmployeeId,
                         p_employee_using_id = order.EmployeeUsingId,
                         p_driver_id = order.DriverId,

                     });

                    int count = 0;
                    foreach (var item in res)
                    {
                        if (count == 0)
                        {
                            emailDto.Driver = item.Username;
                            emailDto.DriverFullname = item.Fullname;
                        }
                        else if (count == 1)
                        {
                            emailDto.Userusing = item.Username;
                            emailDto.UserusingFullname = item.Fullname;
                        }
                        else if (count == 2)
                        {
                            emailDto.User = item.Username;
                            emailDto.UserFullname = item.Fullname;
                        }
                        count++;
                    }

                }
                else if (order.EmployeeId != null && order.EmployeeUsingId != null && order.EmployeeUsingId != 0 && order.DriverId == null)
                {
                    var res = con
                         .GetAllPostgreTableValuedFunctionData<EmailBazaDto>($@"{bazatwo}(
                       @p_employee_id, @p_employee_using_id)",

                     new
                     {
                         p_employee_id = order.EmployeeId,
                         p_employee_using_id = order.EmployeeUsingId


                     });

                    int count = 1;
                    foreach (var item in res)
                    {

                        if (count == 1)
                        {
                            emailDto.User = item.Username;
                            emailDto.UserFullname = item.Fullname;
                        }
                        else if (count == 2)
                        {
                            emailDto.Userusing = item.Username;
                            emailDto.UserusingFullname = item.Fullname;
                        }
                        count++;
                    }
                }
                else if (order.EmployeeId != null && order.DriverId != null)
                {
                    var res = con
                        .GetAllPostgreTableValuedFunctionData<EmailBazaDto>($@"{bazatwodriver}(
                       @p_employee_id,  @p_driver_id)",

                    new
                    {
                        p_employee_id = order.EmployeeId,
                        p_employee_using_id = order.EmployeeUsingId,
                        p_driver_id = order.DriverId,

                    });

                    int count = 0;
                    foreach (var item in res)
                    {
                        if (count == 0)
                        {
                            emailDto.User = item.Username;
                            emailDto.UserFullname = item.Fullname;
                        }
                        else if (count == 1)
                        {
                            emailDto.Driver = item.Username;
                            emailDto.DriverFullname = item.Fullname;
                        }
                        count++;
                    }
                    emailDto.UserusingFullname = "Qonaq";
                }
                else if (order.EmployeeId != null)
                {
                    string bazafullname = "\"AB_ORG\".get_fullname";


                    var res = con
                     .GetAllPostgreTableValuedFunctionData<EmailBazaDto>($@"{bazafullname}(
                       @p_employee_id)",

                 new
                 {
                     p_employee_id = order.EmployeeId,


                 });
                    foreach (var item in res)
                    {
                        emailDto.UserFullname = item.Fullname;
                        emailDto.User = item.Username;
                    }
                    if (order.EmployeeUsingId != null || order.EmployeeUsingId != 0)
                    {
                        emailDto.UserusingFullname = "Qonaq";
                    }


                }


                //if (order.EmployeeId != null)
                //{
                //    Employee user = con.GetById<Employee>(order.EmployeeId);
                //    emailDto.User = user.UserName;
                //}

                //if (order.EmployeeUsingId != null && order.EmployeeUsingId != 0)
                //{
                //    Employee userusing = con.GetById<Employee>(order.EmployeeUsingId);
                //    emailDto.Userusing = userusing.UserName;
                //}

                //if (order.DriverId != null)
                //{
                //    Employee driver = con.GetById<Employee>(order.DriverId);
                //    emailDto.Driver = driver.UserName;
                //}

                emailDto.Status = order.Status;
                emailDto.Time = (order.DepartureTimeDay + order.DepartureTimeHours).ToString("dd.MM.yyyy HH:mm");

                if (order.CarId != null)
                {
                    Car car = con.GetById<Car>(order.CarId);
                    emailDto.Car = car.Brand + " " + car.Model + " " + car.Number;
                }

                emailDto.Reject = order.RejectionReason;
                con.Close();

            }
            return emailDto;
        }

        public async Task<List<OrderGetAllReturnDto>> GetAllFilter(OrderGetAllDto request)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                string DepartureTimeDay = null;
                string ReturnTimeDay = null;

                string DepartureTimeHours = null;
                string ReturnTimeHours = null;

                if (request.DepartureTimeDay != null)
                {
                    DepartureTimeDay = string.Format("{0:yyyy-MM-dd}", request.DepartureTimeDay);
                }

                if (request.ReturnTimeDay != null)
                {
                    ReturnTimeDay = string.Format("{0:yyyy-MM-dd}", request.ReturnTimeDay);
                }

                if (request.DepartureTimeHours != null)
                {
                    DepartureTimeHours = request.DepartureTimeHours.ToString();
                }

                if (request.ReturnTimeHours != null)
                {
                    ReturnTimeHours = request.ReturnTimeHours.ToString();

                }
                string baza = "\"AB_CAR\".get_registration_filter";
                string baza2 = "\"AB_CAR\".get_registration_filter_two";

                if (request.Userrole.Split(", ").Contains("user") || request.Userrole.Split(", ").Contains("RoomUser"))
                {
                    var res = con
                        .GetAllPostgreTableValuedFunctionData<OrderGetAllReturnDto>($@"{baza2}(
                       @p_employee_id,  @p_pasenger_kind, @p_employee_using_id, @p_type_of_departure,
                       @p_departure_time, @p_return_time, @p_departure_hour, @p_return_hour,
                       @p_direction, @p_car_id, @p_driver_id, @p_status, @p_id)",

                    new
                    {
                        p_employee_id = request.EmployeeId,
                        p_pasenger_kind = request.PassengerType,
                        p_employee_using_id = request.EmployeeUsingId,
                        p_type_of_departure = request.DepartureType,
                        p_departure_time = DepartureTimeDay,
                        p_return_time = ReturnTimeDay,
                        p_departure_hour = DepartureTimeHours,
                        p_return_hour = ReturnTimeHours,
                        p_direction = request.Direction,
                        p_car_id = request.CarId,
                        p_driver_id = request.DriverId,
                        p_status = request.Status,
                        p_id = request.Id,
                        p_userrole = request.Userrole

                    });
                    con.Close();
                    return await Task.FromResult(res.ToList());
                }
                else
                {
                    var res = con
                                            .GetAllPostgreTableValuedFunctionData<OrderGetAllReturnDto>($@"{baza}(
                       @p_employee_id,  @p_pasenger_kind, @p_employee_using_id, @p_type_of_departure,
                       @p_departure_time, @p_return_time, @p_departure_hour, @p_return_hour,
                       @p_direction, @p_car_id, @p_driver_id, @p_status)",

                                        new
                                        {
                                            p_employee_id = request.EmployeeId,
                                            p_pasenger_kind = request.PassengerType,
                                            p_employee_using_id = request.EmployeeUsingId,
                                            p_type_of_departure = request.DepartureType,
                                            p_departure_time = DepartureTimeDay,
                                            p_return_time = ReturnTimeDay,
                                            p_departure_hour = DepartureTimeHours,
                                            p_return_hour = ReturnTimeHours,
                                            p_direction = request.Direction,
                                            p_car_id = request.CarId,
                                            p_driver_id = request.DriverId,
                                            p_status = request.Status
                                        });
                    con.Close();
                    return await Task.FromResult(res.ToList());
                }



            }
        }


        public async Task<List<UsernameDto>> GetAdminFind()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                string baza = "\"AB_ORG\".get_admin_usernames";

                var res = con
                         .GetAllPostgreTableValuedFunctionData<UsernameDto>($@"{baza}()");
                con.Close();

                return await Task.FromResult(res.ToList());
            }
        }

        public async Task<List<UsernameDto>> GetCarAdminFind()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                string baza = "\"AB_ROOM\".get_room_admin_usernames";

                var res = con
                         .GetAllPostgreTableValuedFunctionData<UsernameDto>($@"{baza}()");
                con.Close();

                return await Task.FromResult(res.ToList());
            }
        }


        public async Task<EmailDto?> InsertRoom(RoomRegistrationModel model)
        {
            EmailDto emailDto = new EmailDto();
            try
            {
                long resultnum = 0;
                using (var con = DbHelper.GetConn(_conString))
                {
                    con.Open();

                    model.Status = "Gözləmədə";
                    model.InsertedDate = DateTime.Now.ToUniversalTime();

                    resultnum = con.Insert(model);
                    string baza = "\"AB_ORG\".get_fullname";
                    if (model.InsertBy != null)
                    {
                        var res = con
                         .GetAllPostgreTableValuedFunctionData<EmailBazaDto>($@"{baza}(
                       @p_employee_id)",

                     new
                     {
                         p_employee_id = model.InsertBy,


                     });
                        foreach (var item in res)
                        {
                            emailDto.User = item.Fullname;
                        }

                    }

                    con.Close();

                }
                return await Task.FromResult(emailDto);
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> UpdateRoom(RoomRegistrationModel model)
        {
            int result;


            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                RoomRegistrationModel order = con.GetById<RoomRegistrationModel>(model.Id);

                if (order == null)
                {
                    result = 0;
                    con.Close();

                    return result;
                }
                else if (model.Status == null)
                {

                    order.UpdateBy = model.UpdateBy;
                    order.Time = model.Time;
                    order.StartHours = model.StartHours;
                    order.EndHours = model.EndHours;
                    order.MeetName = model.MeetName;
                    order.ParticipantCount = model.ParticipantCount;
                    order.RoomId = model.RoomId;
                    order.Note = model.Note;
                    order.UpdatedDate = DateTime.UtcNow;

                }

                con.Update(order);

                result = 1;

                con.Close();
            }
            return result;
        }

        public async Task<RoomRegistrationModel> GetByRoomId(int id)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                RoomRegistrationModel model = con.GetById<RoomRegistrationModel>(id);
                con.Close();
                return await Task.FromResult(model);
            }
        }

        public async Task<List<GetAllByFilterRoomDto>> GetAllRoomFilter(GetAllRequestByFilterRoomDto request)
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                string baza = "\"AB_ROOM\".get_room_info";
                string baza2 = "\"AB_ROOM\".get_room_info_two";


                if (request.Userrole.Split(", ").Contains("user") || request.Userrole.Split(", ").Contains("RoomUser"))
                {
                    var res = con.GetAllPostgreTableValuedFunctionData<GetAllByFilterRoomDto>($@"{baza2}(
                       @p_start_registration_time,  @p_end_registration_time, @p_room_id,  @p_id)",
                    new
                    {
                        p_start_registration_time = request.StartDay,
                        p_end_registration_time = request.EndDay,
                        p_room_id = request.RoomId,
                        p_id = request.Id,
                    });
                    con.Close();
                    return await Task.FromResult(res.ToList());
                }
                else
                {
                    var res = con.GetAllPostgreTableValuedFunctionData<GetAllByFilterRoomDto>($@"{baza}(
                        @p_start_registration_time,  @p_end_registration_time, @p_room_id)",
                    new
                    {
                        p_start_registration_time = request.StartDay,
                        p_end_registration_time = request.EndDay,
                        p_room_id = request.RoomId,
                    });
                    con.Close();
                    return await Task.FromResult(res.ToList());
                }
            }
        }


        public async Task<EmailDto> ConfirmRoom(RoomRegistrationModel model)
        {
            EmailDto emailDto = new EmailDto();

            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();
                string baza = "\"AB_ROOM\".get_room_fullname";
                RoomRegistrationModel order = con.GetById<RoomRegistrationModel>(model.Id);

                if (order == null)
                {
                    con.Close();

                    return emailDto;
                }
                else
                {
                    order.Status = model.Status;
                    order.RejectReason = model.RejectReason;
                    order.UpdateBy = model.UpdateBy;
                    order.UpdatedDate = DateTime.UtcNow;
                }
                con.Update(order);

                if (order.InsertBy != null)
                {
                    var res = con
                         .GetAllPostgreTableValuedFunctionData<EmailBazaDto>($@"{baza}(
                       @p_user_id, @p_room_id)",

                     new
                     {
                         p_user_id = order.InsertBy,
                         p_room_id = order.RoomId


                     });

                    foreach (var item in res)
                    {
                        emailDto.User = item.Username;
                        emailDto.UserFullname = item.Fullname;
                        emailDto.RoomName = item.Roomname;
                    }

                }

                emailDto.Status = order.Status;
                emailDto.Time = (order.Time + order.StartHours).ToString("dd.MM.yyyy HH:mm");
                emailDto.Reject = order.RejectReason;
                emailDto.MeetingName = order.MeetName;


                con.Close();

            }
            return emailDto;
        }
    }
}


