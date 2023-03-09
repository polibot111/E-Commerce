using E_Commerce.Application.Features.Queries.AppUser.GetAllUsers;
using MediatR;

namespace E_Commerce.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserQueryRequest : IRequest<GetRolesToUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}