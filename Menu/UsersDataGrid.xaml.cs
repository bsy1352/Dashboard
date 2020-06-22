using Menu.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
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

namespace Menu
{
    /// <summary>
    /// UsersDataGrid.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UsersDataGrid : UserControl, INotifyPropertyChanged
    {
        LoginData loginData = LoginData.getInstance;

        ObservableCollection<User> user = new ObservableCollection<User>();
        

        private static UsersDataGrid instance = null;

        public static UsersDataGrid getInstance
        {
            get
            {
                if(instance == null)
                {
                    instance = new UsersDataGrid();
                }
                return instance;
            }
        }
        private UsersDataGrid()
        {
            InitializeComponent();

            

            dgUsers.ItemsSource = user;
        }

        private BitmapImage ConvertoBitmap(byte[] address)
        {
            using (var stream = new MemoryStream(address))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }

        public User AddUser(int id, string name, bool smoker, DateTime bday, MyEnum display, BitmapImage Profile_)
        {
           return new User() { Id = id, Name = name, Bday = bday, Smoker = smoker, Display = display, Profile = Profile_ };
        }

        public User AddUser(int id, string name, bool smoker, DateTime bday, MyEnum display, Version Profile)
        {
            return new User() { Id = id, Name = name, Bday = bday, Smoker = smoker, Display = display, Profile_ = Profile };
        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            user.Add(AddUser(2, "봉시윤", false, new DateTime(1994, 2, 8), MyEnum.Percent,loginData.getProfile()));
        }

        private User _user;
        public User Profile
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("users");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Version
    {
        public string FileIcon { get; set; }
    }
    //new Version() { FileIcon = "C:/Users/bong/source/repos/ChartPractice/Menu/Icons/Female Profile icon.png" })

    public enum MyEnum { Percent = 1, Value = 2 }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Smoker { get; set; }
        public DateTime Bday { get; set; }
        public MyEnum Display { get; set; }
        public BitmapImage Profile { get; set; }

        public Version Profile_ { get; set; }
    }
}


    

