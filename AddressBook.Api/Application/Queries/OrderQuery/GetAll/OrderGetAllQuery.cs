using AddressBook.Domain.Dtos.Order;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.GetAll
{
    public class OrderGetAllQuery : IRequest<List<OrderGetAllDto>>
    {
    }
}
