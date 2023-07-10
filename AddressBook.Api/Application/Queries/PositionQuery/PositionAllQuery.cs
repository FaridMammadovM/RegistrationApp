using AddressBook.Domain.Dtos.Position;
using MediatR;

namespace AddressBook.Api.Application.Queries.PositionQuery
{
    public class PositionAllQuery : IRequest<List<PositionReturnDto>>
    {
    }
}
