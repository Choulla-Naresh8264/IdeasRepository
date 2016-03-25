using Microsoft.Owin.Security;

namespace IdeasRepository.BL.Interfaces
{
    public interface IAuthManager
    {
        IAuthenticationManager AuthManager { get; }
    }
}
