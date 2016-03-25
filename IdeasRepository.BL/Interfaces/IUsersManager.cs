using IdeasRepository.DAL.Managers;

namespace IdeasRepository.BL.Interfaces
{
    public interface IUsersManager
    {
        /// <summary>
        /// An instanse of the Identity user manager context.
        /// </summary>
        ApplicationUserManager UserManager { get; }
    }
}
