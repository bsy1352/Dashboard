using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Menu.ViewModel
{
    public class LoginData
    {
        private static LoginData instance = null;


        public static LoginData getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginData();
                }
                return instance;
            }
        }

        private LoginData()
        {
        }

        public void setUser(string Username, string Gender, BitmapImage Profile, string Rank)
        {
            this.Username = Username;
            this.Gender = Gender;
            this.Rank = Rank;
            this.Profile = Profile;
        }


        public string getUsername()
        {
            return this.Username;
        }

        

        public string getGender()
        {
            return this.Gender;
        }

        public string getRank()
        {
            return this.Rank;
        }

        public BitmapImage getProfile()
        {
            return this.Profile;
        }



        public string Username { get; private set; }
        public string Gender { get; private set; }
        public string Rank { get; private set; }

        public BitmapImage Profile { get; private set; }
    }
}
