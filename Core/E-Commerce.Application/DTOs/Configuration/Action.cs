using E_Commerce.Application.Enıums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.Configuration
{
    public class Action
    {
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Defination { get; set; }
        public string Code { get; set; }
    }
}
