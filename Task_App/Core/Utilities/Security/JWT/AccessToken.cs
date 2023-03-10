using System;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
