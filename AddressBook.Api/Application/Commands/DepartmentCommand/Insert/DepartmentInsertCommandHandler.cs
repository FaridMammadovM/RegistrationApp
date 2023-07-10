using AddressBook.Domain.Models.Department;
using AddressBook.Infrastructure.Repositories.DepartmentRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Insert
{
    public class DepartmentInsertCommandHandler : IRequestHandler<DepartmentInsertCommand, long?>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IValidator<DepartmentInsertCommand> _validator;
        private readonly IMapper _mapper;

        public DepartmentInsertCommandHandler(IDepartmentRepository departmentRepository, IValidator<DepartmentInsertCommand> validator, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<long?> Handle(DepartmentInsertCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            Department model = _mapper.Map<Department>(request);
            long? id = await departmentRepository.InsertDepartment(model);
            return id;


        }
    }
}
