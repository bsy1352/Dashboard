using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Menu.ViewModel
{
    class Encrytion
    {
        private static Encrytion instance = null;


        public static Encrytion getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Encrytion();
                }
                return instance;
            }
        }

        private Encrytion()
        {

        }

        private static string EncryptString(string InputText, string Password)
        {
            string EncryptedData = "";

            try
            {
                RijndaelManaged RijndaelCipher = new RijndaelManaged();

                byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);



                ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(PlainText, 0, PlainText.Length);

                cryptoStream.FlushFinalBlock();

                byte[] CipherBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                EncryptedData = Convert.ToBase64String(CipherBytes);
            }
            catch 
            {
                MessageBox.Show("Failed Value");
            }

            return EncryptedData;
        }

        private static string DecryptString(string InputText, string Password)
        {
            string DecryptedData = "";

            try
            {
                RijndaelManaged RijndaelCipher = new RijndaelManaged();

                byte[] EncryptedData = Convert.FromBase64String(InputText);

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);



                ICryptoTransform Decryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(EncryptedData);

                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];

                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);


                memoryStream.Close();
                cryptoStream.Close();

                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                MessageBox.Show("Failed Value");
            }

            return DecryptedData;
        }

    }


}
