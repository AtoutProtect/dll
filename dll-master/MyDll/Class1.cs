using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace MyDll
{
    public class Class1
    {
        public int Addition(int a, int b)
        {
            return a + b;
        }

        public string Verification(string key)
        {
            string html;
            string request = "http://127.0.0.1/validLicence.php?key=" + key;
            HttpWebRequest webrequest = WebRequest.Create(request) as HttpWebRequest;

            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            using (Stream stream = response.GetResponseStream())

            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }
    }

    public class Crypto
    {
        public static void EncryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only.
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to encrypt file
            }
        }


        public static void DecryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only.
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to decrypt file
            }
        }

        public static bool readInReg()
        {
            bool check = false;


            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AtoutProtect");

            //if it does exist, retrieve the stored values 
            if (key != null)
            {
                string Security = "";
                string Activate = "";
                Security = key.GetValue("security").ToString();
                Activate = key.GetValue("Activate").ToString();
                if (Security == "0" && Activate == "0")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
                key.Close();
            }

            return check;
        }

        public static void WriteInReg()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AtoutProtect");

            //storing the values          
            key.SetValue("security", "0");
            key.SetValue("CopyRight", "ATOUTSA");
            key.SetValue("Activate", "0");

            key.Close();

        }

        public static void WriteInRegTry()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AtoutProtect");

            //storing the values          
            key.SetValue("security", "1");
            key.SetValue("Activate", "1");

            key.Close();

        }
    }
}
