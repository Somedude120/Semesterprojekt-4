using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MartUI.Events;
using Prism.Events;

namespace MartUI.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PassBx_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            GetEventAggregator.Get().GetEvent<PasswordChangedInLogin>().Publish(PassBx.Password);
        }
    }
}
