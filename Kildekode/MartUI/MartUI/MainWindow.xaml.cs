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

namespace MartUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool Maximized;
        private double PrevLeft, PrevTop, PrevWidth, PrevHeight;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if(Maximized == true)
                {
                    ExpandApplication(sender, e);
                    Point Position = Mouse.GetPosition(TitleBar);
                    this.Left = Position.X - 7 - Width/2;
                    this.Top = Position.Y - 10;
                }
            }
                this.DragMove();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                PrevLeft = this.Left;
                PrevTop = this.Top;
                PrevWidth = this.Width;
                PrevHeight = this.Height;
                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                this.Top = SystemParameters.WorkArea.Top;
                this.Left = SystemParameters.WorkArea.Left;
                Maximized = true;
            }
        }

        private void ExitHover(object sender, MouseEventArgs e)
        {
            ExitImage.Source = (BitmapImage)this.Resources["ExitHover"];
        }

        private void ExitNormal(object sender, MouseEventArgs e)
        {
            ExitImage.Source = (BitmapImage)this.Resources["ExitNormal"];
        }

        private void ExpandHover(object sender, MouseEventArgs e)
        {
            ExpandImage.Source = (BitmapImage)this.Resources["ExpandHover"];
        }

        private void ExpandNormal(object sender, MouseEventArgs e)
        {
            ExpandImage.Source = (BitmapImage)this.Resources["ExpandNormal"];
        }

        private void MinimizeHover(object sender, MouseEventArgs e)
        {
            MinimizeImage.Source = (BitmapImage)this.Resources["MinimizeHover"];
        }

        private void MinimizeNormal(object sender, MouseEventArgs e)
        {
            MinimizeImage.Source = (BitmapImage)this.Resources["MinimizeNormal"];
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeApplication(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExpandApplication(object sender, RoutedEventArgs e)
        {
            if (Maximized == true)
            {
                this.Left = PrevLeft;
                this.Top = PrevTop;
                this.Width = PrevWidth;
                this.Height = PrevHeight;
                Maximized = false;
            }
            else
            {
                PrevLeft = this.Left;
                PrevTop = this.Top;
                PrevWidth = this.Width;
                PrevHeight = this.Height;
                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                this.Top = SystemParameters.WorkArea.Top;
                this.Left = SystemParameters.WorkArea.Left;
                Maximized = true;
            }
        }
    }
}
