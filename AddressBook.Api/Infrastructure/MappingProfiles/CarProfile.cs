using AddressBook.Api.Application.Commands.CarCommand.Insert;
using AddressBook.Api.Application.Commands.CarCommand.Update;
using AddressBook.Domain.Dtos.Car;
using AddressBook.Domain.Models;
using AutoMapper;

namespace AddressBook.Api.Infrastructure.MappingProfiles
{
    public sealed class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarInsertCommand>().ReverseMap();
            CreateMap<Car, CarUpdateCommand>().ReverseMap();
            CreateMap<Car, CarByIdDto>().ReverseMap();


            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.MapFrom(src => src.Brand + " " + src.Model + " " + src.Number);
                })
                .ReverseMap();

        }
    }
}
