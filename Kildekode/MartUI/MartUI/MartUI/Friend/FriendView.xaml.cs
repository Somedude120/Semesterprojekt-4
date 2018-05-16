using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MartUI.Events;
using Prism.Events;

namespace MartUI.Friend
{
    /// <summary>
    /// Interaction logic for FriendView.xaml
    /// </summary>
    public partial class FriendView : UserControl
    {
        public FriendView()
        {
            InitializeComponent();
        }

        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender,
            MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

        private void AddFriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AddFriendTextBox.Visibility == Visibility.Hidden)
                AddFriendTextBox.Visibility = Visibility.Visible;
            else
                AddFriendTextBox.Visibility = Visibility.Hidden;
        }
    }
}
