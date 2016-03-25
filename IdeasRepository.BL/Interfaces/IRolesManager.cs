using IdeasRepository.DAL.Managers;

namespace IdeasRepository.BL.Interfaces
{
    public interface IRolesManager
    {
        ApplicationRoleManager RoleManager { get; }
    }
}
