using Fujino.KCLauncher.Connect;
using Fujino.KCLauncher.XML;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FiveM_Launcher
{
    public partial class MainWindow : Window
    {
        Data_Settings _Settings = new Data_Settings();
        KC_Connection _Connection = new KC_Connection();

        public MainWindow()
        {
            InitializeComponent();
            this.Title = Assembly.GetExecutingAssembly().GetName().Name;
            this.lbl_title.Content = " " + Assembly.GetExecutingAssembly().GetName().Name + " V" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (File.Exists("DataSettings.xml"))
            {
                _Settings = KC_XmlManager.Data_Settings_Reader("DataSettings.xml");
                this.txt_server.Text = _Settings.URL;
            }
            else
            {
                _Settings.URL = "";
                KC_XmlManager.KC_XmlDataWriter(_Settings, "DataSettings.xml");
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btn_exit_app_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            this.ConnnectFiveM();
        }

        private async void ConnnectFiveM()
        {
            try
            {
                if (this.txt_server.Text == "")
                {
                    MessageBox.Show("Please enter server IP or JoinID before connecting to server!\n(โปรดใส่ ไอพีเซิร์ฟเวอร์ หรือJoinID ก่อนเชื่อมต่อเซิร์ฟเวอร์!)", "แจ้งเตือน!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _Settings.URL = this.txt_server.Text;
                    KC_XmlManager.KC_XmlDataWriter(_Settings, "DataSettings.xml");

                    _Settings = KC_XmlManager.Data_Settings_Reader("DataSettings.xml");
                    txt_server.Text = _Settings.URL;

                    _Connection.CMDConnectionFiveM(_Settings.URL);

                    await Task.Delay(2000);
                    Application.Current.Shutdown();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.GetKeyStates(Key.Enter) == KeyStates.Down)
            {
                this.ConnnectFiveM();
            }
        }
    }
}
