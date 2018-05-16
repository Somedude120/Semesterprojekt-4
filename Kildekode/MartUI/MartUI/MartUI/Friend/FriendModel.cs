using Prism.Mvvm;

namespace MartUI.Friend
{
    public class FriendModel : BindableBase
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
            }

        }
    }
}
