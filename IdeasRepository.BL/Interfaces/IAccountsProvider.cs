namespace IdeasRepository.BL.Interfaces
{
    /// <summary>
    /// Interface that represents an account management logic.
    /// </summary>
    public interface IAccountsProvider : IUsersManager, IRolesManager, IAuthManager
    { }
}
