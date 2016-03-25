using IdeasRepository.DAL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.BL.Interfaces
{
    public interface IUsersManager
    {
        ApplicationUserManager UserManager { get; }
    }
}
