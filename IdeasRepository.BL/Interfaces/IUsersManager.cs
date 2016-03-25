using IdeasRepository.DAL.Managers;

namespace IdeasRepository.BL.Interfaces
{
    public interface IUsersManager
    {
        ApplicationUserManager UserManager { get; }
    }
}
