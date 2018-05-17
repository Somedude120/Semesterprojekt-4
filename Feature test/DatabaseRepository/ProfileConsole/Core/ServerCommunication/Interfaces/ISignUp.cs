using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface ISignUp
    {
        string CreateProfile(string Username, string salt, string hash);
    }
}
