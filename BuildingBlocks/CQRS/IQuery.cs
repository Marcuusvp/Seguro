
using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQuery<out TREsponse> : IRequest<TREsponse>
    where TREsponse : notnull
{
}