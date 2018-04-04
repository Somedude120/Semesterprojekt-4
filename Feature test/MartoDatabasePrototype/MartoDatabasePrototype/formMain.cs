using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MartoDatabasePrototype
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void friendBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.friendBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.friendlistDataSet);

        }

        private void formMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'friendlistDataSet.Friend' table. You can move, or remove it, as needed.
            this.friendTableAdapter.Fill(this.friendlistDataSet.Friend);

        }
    }
}
