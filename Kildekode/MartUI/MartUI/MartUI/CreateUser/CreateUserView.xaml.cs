﻿using System;
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

namespace MartUI.CreateUser
{
    /// <summary>
    /// Interaction logic for CreateUserView.xaml
    /// </summary>
    public partial class CreateUserView
    {
        public CreateUserView()
        {
            InitializeComponent();
        }

        private void CreateUserPasswordBx_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            GetEventAggregator.Get().GetEvent<PasswordChangedInCreate>().Publish(CreateUserPasswordBx.Password);
        }
    }
}
