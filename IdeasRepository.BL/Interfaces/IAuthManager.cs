using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace IdeasRepository.BL.Interfaces
{
    public interface IAuthManager
    {
        IAuthenticationManager AuthManager { get; }
    }
}
