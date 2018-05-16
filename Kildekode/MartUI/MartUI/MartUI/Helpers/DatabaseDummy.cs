using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartUI.CreateUser;
using MartUI.Login;

namespace MartUI.Helpers
{
    public static class DatabaseDummy
    {
        public static List<DetailedPersonModel> People { get; set; } = new List<DetailedPersonModel>();
    }
}
