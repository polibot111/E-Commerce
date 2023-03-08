using MediatR;

namespace E_Commerce.Application.Features.Queries.AppUser
{
    public class GetAllUserQueryRequest: IRequest<GetAllUserQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}