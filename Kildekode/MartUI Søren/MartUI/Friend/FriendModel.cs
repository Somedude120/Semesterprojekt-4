using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartUI.Friend
{
    public class FriendModel : IFriendModel
    {
        public string ReferenceName { get { return "FriendModel"; } }

        private string _userID;
        private string _userName;

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
    }
}
