using AddressBook.Domain.Dtos.Order;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.GetAllFilter
{
    public class GetAllByFilterOrderQueryHandler : IRequestHandler<GetAllByFilterOrderQuery, List<OrderGetAllReturnDto>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<GetAllByFilterOrderQuery> _validator;
        private readonly IMapper _mapper;


        public GetAllByFilterOrderQueryHandler(IOrderRepository orderRepository, IValidator<GetAllByFilterOrderQuery> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<OrderGetAllReturnDto>> Handle(GetAllByFilterOrderQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            OrderGetAllDto requestdto = new OrderGetAllDto();
            if (request is null)
            {

            }
            requestdto = _mapper.Map<OrderGetAllDto>(request);
            var result = orderRepository.GetAllFilter(requestdto);
            return await Task.FromResult(result.Result);

        }
    }
}