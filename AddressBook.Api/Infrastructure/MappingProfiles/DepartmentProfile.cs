using AddressBook.Api.Application.Commands.DepartmentCommand.Insert;
using AddressBook.Api.Application.Commands.DepartmentCommand.Update;
using AddressBook.Domain.Dtos.Car;
using AddressBook.Domain.Dtos.Department;
using AddressBook.Domain.Dtos.Position;
using AddressBook.Domain.Models;
using AddressBook.Domain.Models.Department;
using AutoMapper;

namespace AddressBook.Api.Infrastructure.MappingProfiles
{
    public sealed class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentCreateDto>().ReverseMap();
            CreateMap<DepartmentInsertCommand, DepartmentCreateDto>().ReverseMap();
            CreateMap<Department, DepartmentInsertCommand>().ReverseMap();
            CreateMap<Department, DepartmentUpdateCommand>().ReverseMap();

            CreateMap<Department, DepartmentUpdateDto>().ReverseMap();
            CreateMap<Department, DepartmentIdDto>().ReverseMap();

            CreateMap<Department, DepartmentGetAllDto>().ReverseMap();

            CreateMap<Position, PositionReturnDto>().ReverseMap();
            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.MapFrom(src => src.Brand + " " + src.Model + " " + src.Number);
                })
                .ReverseMap();

        }
    }
}
