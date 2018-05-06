using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace MartoDatabasePrototype
{
    public partial class formMain : Form
    {
        SqlConnection connection;
        string connectionString;
        public formMain()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["MartoDatabasePrototype.Properties.Settings.FriendlistConnectionString"].ConnectionString;
        }


        private void formMain_Load(object sender, EventArgs e)
        {
            PopulateFriendList();
        }

        private void ListBoxFriends_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(ListBoxFriends.SelectedValue.ToString());
            ListBoxFriendInfo.DisplayMember = "Id";
            ListBoxFriendInfo.DisplayMember = "Name";
            ListBoxFriendInfo.DisplayMember = "Username";
            ListBoxFriendInfo.DisplayMember = "Tag";
        }

        private void PopulateFriendList()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Friends", connection))
            {
                connection.Open();

                DataTable friendsTable = new DataTable();
                adapter.Fill(friendsTable);

                ListBoxFriends.DisplayMember = "Name";
                ListBoxFriends.ValueMember = "Id";
                ListBoxFriends.DataSource = friendsTable;
            }
        }
    }
}
