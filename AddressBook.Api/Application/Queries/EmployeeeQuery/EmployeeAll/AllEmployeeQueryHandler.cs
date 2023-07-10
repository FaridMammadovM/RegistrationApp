using AddressBook.Domain.Dtos.Employee;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeAll
{
    public class AllEmployeeQueryHandler : IRequestHandler<AllEmployeeQuery, List<EmployeeAllReturnDto>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<AllEmployeeQuery> _validator;
        private readonly IMapper _mapper;


        public AllEmployeeQueryHandler(IEmployeeRepository employeeRepository, IValidator<AllEmployeeQuery> validator, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<EmployeeAllReturnDto>> Handle(AllEmployeeQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var result = employeeRepository.GetAll().Result;
            List<EmployeeAllReturnDto> returnDtos = new List<EmployeeAllReturnDto>();

            foreach (var item in result)
            {
                EmployeeAllReturnDto model = _mapper.Map<EmployeeAllReturnDto>(item);
                model.Fullname = item.Firstname + " " + item.Surname + " " + item.Patronymic;
                returnDtos.Add(model);
            }
            return await Task.FromResult(returnDtos);

        }
    }
}
