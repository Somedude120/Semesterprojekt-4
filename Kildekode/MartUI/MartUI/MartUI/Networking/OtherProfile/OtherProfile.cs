using System.Collections.Generic;
using MartUI.Me;

namespace MartUI.Networking.OtherProfile
{
    public class OtherProfile
    {
        //Objectification of OtherProfile, ready for serialization
        private MyData _userData = MyData.GetInstance();

        public OtherProfile()
        {
            Username = _userData.Username;
            Description = _userData.Description;
            Tags = _userData.Tags;
        }

        public string Username { get; set; }
        public string Description { get; set; }

        private ICollection<string> _tags;
        public ICollection<string> Tags
        {
            get => _tags ?? (_tags = new List<string>());
            set
            {
                foreach (var tag in value)
                {
                    _tags.Add(tag);
                }
            }
        } 
    }
    
}
