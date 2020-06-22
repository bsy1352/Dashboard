using MaterialDesignThemes.Wpf;
using Menu.Dashboard.Pages;
using Menu.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Menu
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        UserControlMenuItem Home;
        UserControlMenuItem Dashboard;
        UserControlMenuItem Settings;
        UserControlMenuItem Create;
        UserControlMenuItem Message;
        UserControlMenuItem Ticket;

        LoginData loginData;

        LoginWindow Login;

        Home home;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

           

            this.Loaded += (_sender, _e) =>
            {
                this.Login.ButtonClickedEvent += LoginWindow_LoginButtonClicked;
            };

        }
        
        internal void SwitchScreens(object sender)
        {
            var screen = (UserControl)sender;

            
            if (screen != null)
            {
                GridMain.Children.Clear();
                GridMain.Children.Add(screen);
                return;
            }
        }

      

        private string _username;

        public string Users
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Users");
            }
        }

        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private string _rank;

        public string Rank
        {
            get { return _rank; }
            set
            {
                _rank = value;
            }
        }

        private BitmapImage _profile;

        public BitmapImage Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged("Profile");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonPopupLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            CollapseAll();

        }

        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            ListView list = FindVisualChildByName<ListView>(Menu, "Home");

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new CurrentState();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new UserControlCreate();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }


        private void CollapseAll()
        {
            Home.ExpanderMenu.IsExpanded = false;
            Dashboard.ExpanderMenu.IsExpanded = false;
            Settings.ExpanderMenu.IsExpanded = false;
            Create.ExpanderMenu.IsExpanded = false;
            Ticket.ExpanderMenu.IsExpanded = false;
            Message.ExpanderMenu.IsExpanded = false;
        }

       

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Login = new LoginWindow(this);
            home = new Home();
            GridMain.Children.Add(home);
            Login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Login.Owner = this;
            Login.Show();
            


        }

        private void LoginWindow_LoginButtonClicked(object sender, EventArgs e)
        {

            Users = loginData.getUsername();
            Gender = loginData.getGender();
            Profile = loginData.getProfile();
            Profile_Img.Visibility = Visibility.Visible;
            Rank = loginData.getRank();
            if (Rank != "최고관리자")
            {
                ((ListViewItem)((UserControlMenuItem)(VisualTreeHelper.GetChild(Menu,2))).ListViewMenu.ItemContainerGenerator.ContainerFromIndex(0)).Visibility= Visibility.Collapsed;

            }
            else
            {
                ((ListViewItem)((UserControlMenuItem)(VisualTreeHelper.GetChild(Menu, 2))).ListViewMenu.ItemContainerGenerator.ContainerFromIndex(0)).Visibility = Visibility.Visible;
            }


        }


        private T FindVisualChildByName<T>(DependencyObject _Control, string _FindControlName) where T : DependencyObject 
        { for (int i = 0; i < VisualTreeHelper.GetChildrenCount(_Control); i++) 
            { var child = VisualTreeHelper.GetChild(_Control, i); 
              string controlName = child.GetValue(Control.NameProperty) as string; 
                if (controlName == _FindControlName) 
                { 
                    return child as T; 
                } 
                else 
                { T result = FindVisualChildByName<T>(child, _FindControlName); 
                    if (result != null) 
                    { 
                        return result; 
                    } 
                } 
            } return null; 
        }

        



        private void LogOut_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("로그아웃하시겠습니까?", "로그아웃", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Users = string.Empty;
                Profile_Img.Visibility = Visibility.Hidden;
                Login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Login.Owner = this;
                Login.Show();
                
                

            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            loginData = LoginData.getInstance;
            Login = new LoginWindow(this);


            var menuSetting = new List<SubItem>();
            menuSetting.Add(new SubItem("사용자관리",UsersDataGrid.getInstance));
            menuSetting.Add(new SubItem("Provider"));
            //menuSetting.Add(new SubItem("Employees"));
            //menuSetting.Add(new SubItem("Products"));

            var item0 = new ItemMenu("Settings", menuSetting, MaterialDesignThemes.Wpf.PackIconKind.Settings);

            var menuControl = new List<SubItem>();
            menuControl.Add(new SubItem("Customer"));
            menuControl.Add(new SubItem("Provider"));
            menuControl.Add(new SubItem("Employees"));
            menuControl.Add(new SubItem("Products"));

            var item1 = new ItemMenu("Control", menuControl, MaterialDesignThemes.Wpf.PackIconKind.Pencil);

            var menuTickets = new List<SubItem>();
            menuTickets.Add(new SubItem("Customer"));
            menuTickets.Add(new SubItem("Provider"));
            menuTickets.Add(new SubItem("Employees"));
            menuTickets.Add(new SubItem("Products"));

            var item2 = new ItemMenu("Tickets", menuTickets, MaterialDesignThemes.Wpf.PackIconKind.Ticket);

            var menuMessage = new List<SubItem>();
            menuMessage.Add(new SubItem("Customer"));
            menuMessage.Add(new SubItem("Provider"));
            menuMessage.Add(new SubItem("Employees"));
            menuMessage.Add(new SubItem("Products"));

            var item3 = new ItemMenu("Message", menuMessage, MaterialDesignThemes.Wpf.PackIconKind.Message);

            var item4 = new ItemMenu("Dashboard", new CurrentState(), PackIconKind.ViewDashboard);
            var item5 = new ItemMenu("Home", new Home(), PackIconKind.Home);


            Home = new UserControlMenuItem(item5, this);
            Dashboard = new UserControlMenuItem(item4, this);
            Settings = new UserControlMenuItem(item0, this);
            Create = new UserControlMenuItem(item1, this);
            Ticket = new UserControlMenuItem(item2, this);
            Message = new UserControlMenuItem(item3, this);



            
            Menu.Children.Add(Home);
            Menu.Children.Add(Dashboard);
            Menu.Children.Add(Settings);
            Menu.Children.Add(Create);
            Menu.Children.Add(Ticket);
            Menu.Children.Add(Message);
            DataContext = this;

        }
    }
}
