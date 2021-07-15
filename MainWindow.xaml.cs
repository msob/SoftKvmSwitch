using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftKvmSwitch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon SksNotifyIcon { get; set; }

        private Dictionary<string, Icon> IconHandles { get; set; } = new Dictionary<string, Icon>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            IconHandles.Add("QuickLaunch", new Icon(System.IO.Path.Combine(Environment.CurrentDirectory, @"Images\SKS.ico")));

            SksNotifyIcon = new NotifyIcon();
            SksNotifyIcon.DoubleClick += new EventHandler(SksNotifyIcon_DoubleClick);
            SksNotifyIcon.Icon = IconHandles["QuickLaunch"];
            SksNotifyIcon.Visible = true;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                ShowInTaskbar = false;
                Visibility = Visibility.Hidden;
            }
        }

        private void SksNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            Visibility = Visibility.Visible;
            WindowState = WindowState.Normal;
        }
    }
}
