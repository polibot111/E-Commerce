using E_Commerce.Application.Enıums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.CustomAttributes
{
    public class AuthorizeDefinationAttribute : Attribute
    {
        public string Menu { get; set; }
        public string Definition { get; set; }
        public ActionType ActionType  { get; set; }
    }
}
