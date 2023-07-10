using AutoMapper;

namespace AddressBook.Api.Infrastructure.MappingProfiles
{
    public sealed class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DepartmentProfile());
                mc.AddProfile(new EmployeeProfile());
                mc.AddProfile(new OrderProfile());
                mc.AddProfile(new CarProfile());
                mc.AddProfile(new RoomProfile());


            });
            return mapperConfig.CreateMapper();
        }
    }
}
