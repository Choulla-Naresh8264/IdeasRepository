using Microsoft.Owin.Security;

namespace IdeasRepository.BL.Interfaces
{
    /// <summary>
    /// Interface that represents an authorization logic.
    /// </summary>
    public interface IAuthManager
    {
        /// <summary>
        /// Used to interact with authentication provider instance.
        /// </summary>
        IAuthenticationManager AuthManager { get; }
    }
}
