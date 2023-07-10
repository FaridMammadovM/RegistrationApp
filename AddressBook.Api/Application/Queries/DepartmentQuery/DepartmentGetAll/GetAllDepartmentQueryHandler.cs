using AddressBook.Domain.Dtos.Department;
using AddressBook.Infrastructure.Repositories.DepartmentRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.DepartmentQuery.DepartmentGetAll
{
    public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, List<DepartmentGetAllDto>>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IValidator<GetAllDepartmentQuery> _validator;
        private readonly IMapper _mapper;


        public GetAllDepartmentQueryHandler(IDepartmentRepository departmentRepository, IValidator<GetAllDepartmentQuery> validator, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<DepartmentGetAllDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var result = departmentRepository.GetAll().Result;
            List<DepartmentGetAllDto> resultList = new List<DepartmentGetAllDto>();
            foreach (var item in result)
            {
                DepartmentGetAllDto model = _mapper.Map<DepartmentGetAllDto>(item);
                resultList.Add(model);
            }

            return await Task.FromResult(resultList);

        }
    }
}