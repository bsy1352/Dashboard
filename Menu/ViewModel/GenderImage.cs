using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Menu.ViewModel
{
    class GenderImage
    {

        private static GenderImage instance = null;


        public static GenderImage getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GenderImage();
                }
                return instance;
            }
        }

        private GenderImage()
        {

        }

        

        private BitmapImage _Female;
        public BitmapImage Female 
        { get 
            {

                _Female = new BitmapImage();
                _Female = new BitmapImage();
                _Female.BeginInit();
                _Female.UriSource = new Uri(@"Icons/Female Profile icon.png", UriKind.Relative);
                _Female.EndInit();
                return _Female; 
           
            }
            
        }

        private BitmapImage _Male;
        public BitmapImage Male
        {
            get
            {
                _Male = new BitmapImage();
                _Male.BeginInit();
                _Male.UriSource = new Uri(@"Icons/Male Profile icon.png", UriKind.Relative);
                _Male.EndInit();
                return _Male;

            }
            
        }
    }
}
