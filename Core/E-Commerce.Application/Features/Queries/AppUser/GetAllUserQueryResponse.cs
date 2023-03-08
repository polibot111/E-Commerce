using E_Commerce.Application.DTOs.User;

namespace E_Commerce.Application.Features.Queries.AppUser
{
    public class GetAllUserQueryResponse
    {
        public List<ListUserDTO> Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}