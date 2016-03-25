using IdeasRepository.DAL.Managers;

namespace IdeasRepository.BL.Interfaces
{
    /// <summary>
    /// Interface that represents an user roles management logic.
    /// </summary>
    public interface IRolesManager
    {
        /// <summary>
        /// An instanse of the Identity role manager context.
        /// </summary>
        ApplicationRoleManager RoleManager { get; }
    }
}
