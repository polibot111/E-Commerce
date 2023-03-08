using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.User
{
    public class ListUserDTO
    {
        public string Id { get; set; }
        public string Email{ get; set; }
        public string NameSurName{ get; set; }
        public bool TwoFactorEnabled{ get; set; }
        public string UserName { get; set; }


    }
}
