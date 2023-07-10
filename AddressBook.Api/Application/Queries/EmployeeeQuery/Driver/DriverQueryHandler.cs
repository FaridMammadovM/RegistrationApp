using AddressBook.Domain.Dtos.Employee;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.Driver
{
    public class DriverQueryHandler : IRequestHandler<DriverQuery, List<DriverDto>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<DriverQuery> _validator;
        private readonly IMapper _mapper;


        public DriverQueryHandler(IEmployeeRepository employeeRepository, IValidator<DriverQuery> validator, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<DriverDto>> Handle(DriverQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var result = employeeRepository.Driver().Result;
            List<DriverDto> returnDtos = new List<DriverDto>();

            foreach (var item in result)
            {
                DriverDto model = _mapper.Map<DriverDto>(item);
                model.Fullname = item.Firstname + " " + item.Surname + " " + item.Patronymic + " " + item.MobilPhone;
                returnDtos.Add(model);
            }
            return await Task.FromResult(returnDtos);

        }
    }
}
