using AddressBook.Api.Application.Commands.EmployeeCommand.Insert;
using AddressBook.Api.Application.Commands.EmployeeCommand.Update;
using AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeGetAllFilter;
using AddressBook.Api.Application.Queries.EmployeeeQuery.Login;
using AddressBook.Domain.Dtos.Employee;
using AddressBook.Domain.Models.Employee;
using AutoMapper;

namespace AddressBook.Api.Infrastructure.MappingProfiles
{
    public sealed class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
            CreateMap<Employee, EmployeeIdDto>()
                .ForMember(dest => dest.Gender, opt =>
                {
                    opt.MapFrom(src => src.Gender == "M" ? 1 : src.Gender == "F" ? 2 : 0);
                }).ForMember(dest => dest.UserRole, opt =>
                {
                    opt.MapFrom(src => src.UserRole == "Admin" ? 1 : src.UserRole == "user" ? 2 : src.UserRole == "CarAdmin" ? 3 : src.UserRole == "CarUser" ? 4 :
                    src.UserRole == "RoomAdmin" ? 5 : src.UserRole == "RoomUser" ? 6 : 0);
                });
            CreateMap<Employee, LoginReturnDto>().ReverseMap();
            CreateMap<LoginDto, LoginQuery>().ReverseMap();
            CreateMap<Employee, EmployeeAllReturnDto>().ReverseMap();
            CreateMap<EmployeeGetAllDto, GetEmployeeFilterQuery>().ReverseMap();
            CreateMap<Employee, DriverDto>().ReverseMap();
            CreateMap<InsertEmployeeCommand, Employee>()
                .ForMember(dest => dest.Gender, opt =>
                {
                    opt.MapFrom(src => src.Gender == 1 ? "M" : src.Gender == 2 ? "F" : null);
                })
                .ForMember(dest => dest.UserRole, opt =>
                {
                    opt.MapFrom(src => src.UserRole == 1 ? "Admin" : src.UserRole == 2 ? "user" : src.UserRole == 3 ? "CarAdmin" : src.UserRole == 4 ? "CarUser" :
                    src.UserRole == 5 ? "RoomAdmin" : src.UserRole == 6 ? "RoomUser" : null);
                });
            CreateMap<UpdateEmployeeCommand, Employee>()
                .ForMember(dest => dest.Gender, opt =>
                {
                    opt.MapFrom(src => src.Gender == 1 ? "M" : src.Gender == 2 ? "F" : null);
                })
                .ForMember(dest => dest.UserRole, opt =>
                {
                    opt.MapFrom(src => src.UserRole == 1 ? "Admin" : src.UserRole == 2 ? "user" : src.UserRole == 3 ? "CarAdmin" : src.UserRole == 4 ? "CarUser" :
                    src.UserRole == 5 ? "RoomAdmin" : src.UserRole == 6 ? "RoomUser" : null);
                });



        }
    }
}
