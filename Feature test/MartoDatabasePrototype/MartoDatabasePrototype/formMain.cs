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

            connectionsString = ConfigurationManager.ConnectionStrings["MartoDatabasePrototype.Properties.Settings.FriendlistConnectionString"].ConnectionString;
        }


        private void formMain_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PopulateFriendList()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Friends", connection))
            {
                connection.Open()
            }
        }
    }
}
