//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MartUI.Events;
//using MartUI.Friend;

//namespace MartUI.Group
//{
//    class GroupModel
//    {
//        private GroupChat _chat;
//        public string GroupName { get; set; }
//        public FriendModel GroupLeader { get; set; }
//        public string GroupReference { get; set; }
//        private ObservableCollection<FriendModel> _groupMembers;


//        public GroupChat Chat
//        {
//            get
//            {
//                if (_chat == null)
//                    _chat = new GroupChat();
//                return _chat;
//            }
//            set { _chat = value; }
//        }

//        public ObservableCollection<FriendModel> GroupMembers
//        {
//            get
//            {
//                if (_groupMembers == null)
//                    _groupMembers = new ObservableCollection<FriendModel>();
//                return _groupMembers;
//            }
//            set { _groupMembers = value; }
//        }
//    }
//}
