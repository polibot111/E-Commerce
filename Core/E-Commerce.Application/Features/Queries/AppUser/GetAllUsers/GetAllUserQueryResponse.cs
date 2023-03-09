using E_Commerce.Application.DTOs.User;

namespace E_Commerce.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUserQueryResponse
    {
        public List<ListUserDTO> Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}