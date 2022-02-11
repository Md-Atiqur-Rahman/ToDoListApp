using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.Requirements
{
    
    public class UserBlockedStatusRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; }
        public UserBlockedStatusRequirement(bool isBlocked)
        {
            IsBlocked = isBlocked;
        }

    }
}
