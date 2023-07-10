using AddressBook.Domain.Models.Department;
using AddressBook.Infrastructure.Repositories.DepartmentRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Update
{
    public class DepartmentUpdateCommandHandler : IRequestHandler<DepartmentUpdateCommand, int>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IValidator<DepartmentUpdateCommand> _validator;
        private readonly IMapper _mapper;

        public DepartmentUpdateCommandHandler(IDepartmentRepository departmentRepository, IValidator<DepartmentUpdateCommand> validator, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<int> Handle(DepartmentUpdateCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var department = _mapper.Map<Department>(request);
            var res = departmentRepository.UpdateDepartment(department).Result;
            if (res != 0)
            {
                return 1;
            }
            else
            { return 0; }


        }
    }
}
