using AddressBook.Domain.Dtos.Position;
using AddressBook.Infrastructure.Repositories.DepartmentRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.PositionQuery
{
    public class PositionAllQueryHandler : IRequestHandler<PositionAllQuery, List<PositionReturnDto>>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IValidator<PositionAllQuery> _validator;
        private readonly IMapper _mapper;


        public PositionAllQueryHandler(IDepartmentRepository departmentRepository, IValidator<PositionAllQuery> validator, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<PositionReturnDto>> Handle(PositionAllQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var result = departmentRepository.GetAllPosition().Result;
            List<PositionReturnDto> returnDtos = new List<PositionReturnDto>();

            foreach (var item in result)
            {
                PositionReturnDto model = _mapper.Map<PositionReturnDto>(item);
                returnDtos.Add(model);
            }
            return await Task.FromResult(returnDtos);

        }
    }
}
