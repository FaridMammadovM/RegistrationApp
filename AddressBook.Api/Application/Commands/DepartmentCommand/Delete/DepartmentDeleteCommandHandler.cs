using AddressBook.Domain.Dtos.Department;
using AddressBook.Infrastructure.Repositories.DepartmentRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Delete
{
    public class DepartmentDeleteCommandHandler : IRequestHandler<DepartmentDeleteCommand, DepartmentIdDto>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IValidator<DepartmentDeleteCommand> _validator;
        private readonly IMapper _mapper;


        public DepartmentDeleteCommandHandler(IDepartmentRepository departmentRepository, IValidator<DepartmentDeleteCommand> validator, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<DepartmentIdDto> Handle(DepartmentDeleteCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            DepartmentIdDto result = new();
            var res = departmentRepository.Delete(request.Id).Result;

            return await Task.FromResult(result);

        }
    }
}