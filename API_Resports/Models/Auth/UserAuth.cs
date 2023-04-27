using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Resports.Models.Auth
{
    public class UserAuth
    {
        [DisplayName("Usuario")]
        public string User { get; set; } = null!;

        [DisplayName("Contraseña")]
        public string Password { get; set; } = null!;
    }
}
