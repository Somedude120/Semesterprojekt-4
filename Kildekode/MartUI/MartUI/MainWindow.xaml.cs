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
        private WindowState PrevState { get; set; }
        private bool Maximized;
        private double PrevLeft, PrevTop, PrevWidth, PrevHeight;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
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
