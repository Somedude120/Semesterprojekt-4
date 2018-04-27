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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using MahApps.Metro.Controls;
using MartUI.Main;

namespace MartUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView
    {
        private bool Maximized;
        private double PrevLeft, PrevTop, PrevWidth, PrevHeight;

        public MainView()
        {
            InitializeComponent();
            // ------ REMOVE THIS SHIT WHEN CHECKED OUT --------
            DataContext = new MainViewModel();
            SizeChanged += MainView_SizeChanged;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if(Maximized)
                {
                        ExpandApplication(sender, e);
                        Point Position = Mouse.GetPosition(TitleBar);
                        Left = Position.X - 7 - Width / 2;
                        Top = Position.Y - 10;
                }
            }
                DragMove();
        }

        private void MainView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                PrevLeft = Left;
                PrevTop = Top;
                PrevWidth = Width;
                PrevHeight = Height;
                Width = SystemParameters.WorkArea.Width;
                Height = SystemParameters.WorkArea.Height;
                Top = SystemParameters.WorkArea.Top;
                Left = SystemParameters.WorkArea.Left;
                Maximized = true;
            }
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeApplication(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExpandApplication(object sender, RoutedEventArgs e)
        {
            if (Maximized)
            {
                Left = PrevLeft;
                Top = PrevTop;
                Width = PrevWidth;
                Height = PrevHeight;
                Maximized = false;
            }
            else
            {
                PrevLeft = Left;
                PrevTop = Top;
                PrevWidth = Width;
                PrevHeight = Height;
                Width = SystemParameters.WorkArea.Width;
                Height = SystemParameters.WorkArea.Height;
                Top = SystemParameters.WorkArea.Top;
                Left = SystemParameters.WorkArea.Left;
                Maximized = true;
            }
        }
    }
}
