using AddressBook.Domain.Dtos.Order;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.GetAll
{
    public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, List<OrderGetAllDto>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<OrderGetAllQuery> _validator;
        private readonly IMapper _mapper;


        public OrderGetAllQueryHandler(IOrderRepository orderRepository, IValidator<OrderGetAllQuery> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<OrderGetAllDto>> Handle(OrderGetAllQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var result = orderRepository.GetAll().Result;
            List<OrderGetAllDto> returnDtos = new List<OrderGetAllDto>();

            foreach (var item in result)
            {
                OrderGetAllDto model = _mapper.Map<OrderGetAllDto>(item);
                returnDtos.Add(model);
            }
            return await Task.FromResult(returnDtos);

        }
    }
}
