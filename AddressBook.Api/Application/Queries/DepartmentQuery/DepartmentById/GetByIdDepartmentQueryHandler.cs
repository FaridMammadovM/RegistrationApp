using AddressBook.Domain.Dtos.Department;
using AddressBook.Infrastructure.Repositories.DepartmentRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.DepartmentQuery.DepartmentById
{
    public class GetByIdDepartmentQueryHandler : IRequestHandler<GetByIdDepartmentQuery, DepartmentIdDto>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IValidator<GetByIdDepartmentQuery> _validator;
        private readonly IMapper _mapper;


        public GetByIdDepartmentQueryHandler(IDepartmentRepository departmentRepository, IValidator<GetByIdDepartmentQuery> validator, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<DepartmentIdDto> Handle(GetByIdDepartmentQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            DepartmentIdDto result = new();
            var res = departmentRepository.GetById(request.Id).Result;
            if (res != null)
            {
                DepartmentIdDto department = _mapper.Map<DepartmentIdDto>(res);
                result = department;
            }
            return await Task.FromResult(result);

        }
    }
}
