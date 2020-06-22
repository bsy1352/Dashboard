using Menu.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// LogIn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LogIn : UserControl
    {
        private string LoginID { get; set; }
        private string LoginPW { get; set; }

        private string UserGender { get; set; }
        private string UserID { get; set; }
        private string UserPW { get; set; }
        private string UserRank { get; set; }
        private string UserName { get; set; }

        private byte[] UserImg { get; set; }


        private BitmapImage UserProfile { get; set; }

        LoginData loginData;
        GenderImage genderImage;
        public LogIn()
        {
            InitializeComponent();

            loginData = LoginData.getInstance;
            genderImage = GenderImage.getInstance;
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginID = IDInputtxt.Text;
            LoginPW = PWInputtxt.Password;
            if(string.IsNullOrEmpty(LoginID) || string.IsNullOrEmpty(LoginPW))
            {
                MessageBox.Show("아이디와 비밀번호를 확인해주세요");
                return;
            }

            bool isIDcorrect = DoLogin();
            if (isIDcorrect == false)
            {
                MessageBox.Show("아이디 혹은 비밀번호가 틀립니다. 다시 시도해주세요");
                return;
            }
            else
            {
                
                Window.GetWindow(this).Hide();
                IDInputtxt.Clear();
                PWInputtxt.Clear();
                IDInputtxt_LostFocus(sender, e);
                PWInputtxt_LostFocus(sender, e);
                OnCloseButtonClicked(e);
                return;
            }



            
        }

        private bool DoLogin()
        {
            

            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=dashboard;Uid=root;Pwd=0000"))
            {

                string selectID = "Select UserID, UserPW,UserName,UserGender,UserRank, UserImg from users where UserID = '" + LoginID + "'";
               

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(selectID, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        UserID = reader.IsDBNull(0) ? "NULL" : reader.GetString(0);
                        UserPW = reader.IsDBNull(1) ? "NULL" : reader.GetString(1);
                        UserName = reader.IsDBNull(2) ? "NULL" : reader.GetString(2);
                        UserGender = reader.IsDBNull(3) ? "NULL" : reader.GetString(3);
                        UserRank = reader.IsDBNull(4) ? "NULL" : reader.GetString(4);
                        UserImg = (byte[])(reader["UserImg"]);
                    }
                    

                 
                    connection.Close();

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }

            }

            if(UserID == "NULL" || UserID == "")
            {
                return false;
            }
            else if(UserPW != LoginPW)
            {
                return false;
            }
            else
            {
                if(UserImg == null)
                {
                    loginData.setUser(UserName, UserGender, null, UserRank);
                }
                else
                {
                    UserProfile = ConvertoBitmap(UserImg);

                    loginData.setUser(UserName, UserGender, UserProfile, UserRank);

                }
                
                
                return true;
            }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("종료하시겠습니까?", "프로그램 종료", MessageBoxButton.YesNo);
            if(Result == MessageBoxResult.Yes)
            {
                
                Environment.Exit(0);
                Application.Current.Shutdown();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }else if(Result == MessageBoxResult.No)
            {
                return;
            }
            
        }

        private void IDInputtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            helptxt_ID.Text = "";
        }

        private void PWInputtxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            helptxt_PW.Text = "";
        }

        private void IDInputtxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if(IDInputtxt.Text == "")
            {
                helptxt_ID.Text = "아이디를 입력해주세요";
            }
        }
        private void PWInputtxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PWInputtxt.Password == "")
            {
                helptxt_PW.Text = "비밀번호를 입력해주세요";
            }
        }


        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid1.Focus();

            if (IDInputtxt.Text == "")
            {
                helptxt_ID.Text = "아이디를 입력해주세요";
            }

            if (PWInputtxt.Password == "")
            {
                helptxt_PW.Text = "비밀번호를 입력해주세요";
            }

        }

        public event EventHandler ButtonClickedEvent;
        protected virtual void OnCloseButtonClicked(EventArgs e)
        {
            var handler = ButtonClickedEvent;
            if (handler != null)
                handler(this, e);
        }

        private void PWInputtxt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    this.Button_Click(sender, e);
                    return;

            }
        }
    }
}
