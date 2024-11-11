using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndentityManagement.Utilities
{
    public enum UserStatus
    {
        Pending,
        Active,
        LockOut,
        Close,
        Banned,
        CannotAccess
    }
}
