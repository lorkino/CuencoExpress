using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{   [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorize : Attribute
    {

        
        public CustomAuthorize()
        {
            
        }

        
    }
}
