using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIPresantation.Models
{
    public class UserResponseVM
    {
        public List<UserVM> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class GetByIdUserResponseVM
    {
        public UserVM Data { get; set; }
        public bool Success { get; set; }
    }

    public class UserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
    }
   
}
