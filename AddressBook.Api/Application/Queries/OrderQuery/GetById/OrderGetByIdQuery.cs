using AddressBook.Domain.Dtos.Order;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.GetById
{
    public class OrderGetByIdQuery : IRequest<GetByIdRetunDto>
    {
        public long Id { get; set; }
    }
}

