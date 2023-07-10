using AddressBook.Api.Application.Commands.DepartmentCommand.Delete;
using AddressBook.Api.Application.Commands.DepartmentCommand.Insert;
using AddressBook.Api.Application.Commands.EmployeeCommand.Delete;
using AddressBook.Api.Application.Commands.EmployeeCommand.Insert;
using AddressBook.Api.Application.Commands.OrderCommand.Insert;
using Autofac;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace AddressBook.Api.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
               .AsImplementedInterfaces();


            builder.RegisterAssemblyTypes(typeof(DepartmentInsertCommand).GetTypeInfo().Assembly)
          .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(DepartmentDeleteCommand).GetTypeInfo().Assembly)
         .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(InsertEmployeeCommand).GetTypeInfo().Assembly)
         .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(DeleteEmployeeCommand).GetTypeInfo().Assembly)
        .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(OrderInsertCommand).GetTypeInfo().Assembly)
          .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(DepartmentInsertValidator).GetTypeInfo().Assembly)
          .Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(DepartmentDeleteValidator).GetTypeInfo().Assembly)
         .Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(InsertEmployeeValidator).GetTypeInfo().Assembly)
         .Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(DeleteEmployeeValidator).GetTypeInfo().Assembly)
         .Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(OrderInsertValidator).GetTypeInfo().Assembly)
        .Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

        }
    }
}