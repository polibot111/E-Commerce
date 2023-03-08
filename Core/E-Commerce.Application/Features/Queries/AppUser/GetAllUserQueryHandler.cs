using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Repositories.ElementsRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Queries.AppUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        readonly IUserService _userService;

        public GetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var totalUserCount = await _userService.GetAllUserCount();
            var users = await _userService.GetAllUsersAsync(new() { Page = request.Page, Size = request.Size});

            return new()
            {
                TotalUsersCount = totalUserCount,
                Users = users
            };
        }
    }
}
