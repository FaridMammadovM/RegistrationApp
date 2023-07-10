using AddressBook.Domain.Dtos.Employee;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeById
{
    public class GetByIdEmployeeQueryHandler : IRequestHandler<GetByIdEmployeeQuery, EmployeeIdDto>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<GetByIdEmployeeQuery> _validator;
        private readonly IMapper _mapper;


        public GetByIdEmployeeQueryHandler(IEmployeeRepository employeeRepository, IValidator<GetByIdEmployeeQuery> validator, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<EmployeeIdDto> Handle(GetByIdEmployeeQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            EmployeeIdDto result = new();
            var res = employeeRepository.GetById(request.Id).Result;
            if (res != null)
            {
                EmployeeIdDto employee = _mapper.Map<EmployeeIdDto>(res);
                result = employee;
            }
            return await Task.FromResult(result);

        }
    }
}
