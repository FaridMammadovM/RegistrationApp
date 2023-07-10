using AddressBook.Domain.Dtos.Order;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.GetById
{
    public class OrderGetByIdQueryHandler : IRequestHandler<OrderGetByIdQuery, GetByIdRetunDto>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<OrderGetByIdQuery> _validator;
        private readonly IMapper _mapper;


        public OrderGetByIdQueryHandler(IOrderRepository orderRepository, IValidator<OrderGetByIdQuery> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<GetByIdRetunDto> Handle(OrderGetByIdQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            GetByIdRetunDto result = new();
            var res = orderRepository.GetById(request.Id).Result;
            if (res != null)
            {
                GetByIdRetunDto employee = _mapper.Map<GetByIdRetunDto>(res);
                result = employee;
            }
            return await Task.FromResult(result);

        }
    }
}
