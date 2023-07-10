using AddressBook.Domain.Dtos.Employee;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeGetAllFilter
{
    public class GetEmployeeFilterQueryHandler : IRequestHandler<GetEmployeeFilterQuery, List<EmployeeGetAllReturnDto>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<GetEmployeeFilterQuery> _validator;
        private readonly IMapper _mapper;


        public GetEmployeeFilterQueryHandler(IEmployeeRepository employeeRepository, IValidator<GetEmployeeFilterQuery> validator, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<EmployeeGetAllReturnDto>> Handle(GetEmployeeFilterQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            EmployeeGetAllDto requestdto = new EmployeeGetAllDto();
            if (request is null)
            {

            }
            requestdto = _mapper.Map<EmployeeGetAllDto>(request);
            var result = employeeRepository.GetAllFilter(requestdto);
            return await Task.FromResult(result.Result);

        }
    }
}
