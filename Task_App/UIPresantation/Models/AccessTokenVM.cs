using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIPresantation.Models
{
    public class AccessTokenVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
