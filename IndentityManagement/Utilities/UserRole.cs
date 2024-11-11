using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndentityManagement.Utilities
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
