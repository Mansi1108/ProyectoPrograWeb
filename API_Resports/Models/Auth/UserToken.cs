using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Resports.Models.Auth
{
    public class UserToken
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
