using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LicenceController
{
    public class Check
    {
        Path filePath = new Path();
        Crypto cryptFile = new Crypto();

        public bool verification()
        {
            bool res = false;
            
            if (dirExist() == true)
            {
                if (fileExist() == true)
                {
                    Crypto.DecryptFile(@filePath.getPath() + "\\registration.lic", @filePath.getPath() + "\\AP-DECRYPT.lic", "1234512345678976");

                    System.IO.StreamReader file = new System.IO.StreamReader(@filePath.getPath() + "\\AP-DECRYPT.lic");

                    string MAC = file.ReadLine();
                    string dateActivation = file.ReadLine();

                    file.Close();
                    File.Delete(filePath.getPath() + "\\AP-DECRYPT.lic");

                    if (checkMac(MAC) == true)
                    {
                        if (compareDate(dateActivation)==true)
                        {
                            res = true;
                        }
                        else
                        {
                            MessageBox.Show("Votre licence est arrivé à terme. Saisir une autre clé d'activation", "Erreur");
                        }
                    }
                }
            }

            return res;
        }

        public bool compareDate(string dateActive)
        {
            var dateA = Convert.ToDateTime(dateActive);
            bool res;
            DateTime dateNow = DateTime.Now;
            int result = DateTime.Compare(dateA, dateNow);

            if (result < 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }

            return res;
        }

        public void writeInFile(string key, string email, string dateFin)
        {
            filePath.createDir();
            string[] lines = { GetMACAddress2(), dateFin, email, key, dateActivation()};
            System.IO.File.WriteAllLines(filePath.getPath() + "\\AP.lic", lines);
            Crypto.EncryptFile(@filePath.getPath() + "\\AP.lic", @filePath.getPath() + "\\registration.lic", "1234512345678976");
            File.Delete(filePath.getPath() + "\\AP.lic");
        }

        public string dateActivation()
        {
            String Date = DateTime.Now.ToString("yyyy-MM-dd");

            return Date;
        }
		
		// Renvoi l'adresse MAC sous forme de chaine de caractères
        public static string GetMACAddress2()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
		
		// Verifie si le fichier licence existe
        public bool fileExist()
        {
            if (!File.Exists(filePath.getPath() + "\\registration.lic"))
            {
                return false;
            }

            return true;
        }
		
		// Verifie si le dossier Atout Protect existe
        public bool dirExist()
        {
            if (!Directory.Exists(filePath.getPath()))
            {
                return false;
            }

            return true;
        }
		
		// Compare si l'adresse MAC enregistrer à l'activation de la licence correspond a celle du poste executant le programme
        public bool checkMac (string MAC)
        {
            if(GetMACAddress2() == MAC)
            {
                return true;
            }
            return false;
        }
    }
}
