using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jammer_1
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync();

        Task<bool> LogoutAsync();
    }
}
