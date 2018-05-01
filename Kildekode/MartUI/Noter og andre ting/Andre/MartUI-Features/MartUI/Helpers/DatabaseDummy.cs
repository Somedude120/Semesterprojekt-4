using System.Collections.Generic;
using System.Windows.Documents;
using MartUI.Model;

namespace MartUI.Helpers
{
    public class DatabaseDummy
    {
        private List<PersonModel> _personList = new List<PersonModel>();

        public List<PersonModel> PersonList
        {
            get { return _personList; }
            set { _personList = value; }
        }
    }
}
