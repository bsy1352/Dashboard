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
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {

        LogIn usc;
        MainWindow parent_;
        public LoginWindow(MainWindow parent)
        {
            InitializeComponent();
            parent_ = parent;
            usc = new LogIn();
            GridMain.Children.Add(usc);
           
        }

        private void LoginUSC_LoginButtonClicked(object sender, EventArgs e)
        {
            OnCloseButtonClicked(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            usc.ButtonClickedEvent += LoginUSC_LoginButtonClicked;

        }

        public event EventHandler ButtonClickedEvent;
        protected virtual void OnCloseButtonClicked(EventArgs e)
        {
            var handler = ButtonClickedEvent;
            if (handler != null)
                handler(this, e);
        }
    }
}
