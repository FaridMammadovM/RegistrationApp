using AddressBook.Api.Infrastructure.MappingProfiles;
using Autofac;

namespace AddressBook.Api.Infrastructure.AutofacModules
{
    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();

        }
    }
}
