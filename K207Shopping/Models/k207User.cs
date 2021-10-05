using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Models
{
    public class k207User:IdentityUser
    {
        public string addres { get; set; }
    }
}
