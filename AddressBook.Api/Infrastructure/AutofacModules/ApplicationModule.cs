using AddressBook.Infrastructure.Repositories.CarRepository;
using AddressBook.Infrastructure.Repositories.DepartmentRepository;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AddressBook.Infrastructure.Repositories.RoomRepository;
using Autofac;

namespace AddressBook.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        public IConfiguration Configuration { get; }
        public ApplicationModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DepartmentRepository(Configuration["ConnectionStrings:AppCon"]))
            .As<IDepartmentRepository>()
            .InstancePerLifetimeScope();
            builder.Register(c => new EmployeeRepository(Configuration["ConnectionStrings:AppCon"]))
           .As<IEmployeeRepository>()
           .InstancePerLifetimeScope();
            builder.Register(c => new OrderRepository(Configuration["ConnectionStrings:AppCon"]))
          .As<IOrderRepository>()
          .InstancePerLifetimeScope();
            builder.Register(c => new CarRepository(Configuration["ConnectionStrings:AppCon"]))
         .As<ICarRepository>()
         .InstancePerLifetimeScope();
            builder.Register(c => new RoomRepository(Configuration["ConnectionStrings:AppCon"]))
   .As<IRoomRepository>()
   .InstancePerLifetimeScope();
        }
    }
}
