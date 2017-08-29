using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LicenceController
{
    class Connexion
    {
        public string Verification(string key, string email)
        {
            string htmlRep; // HTML Reponse

            string request = "http://atout-protect.tk/controller/validLicence.php?key=" + key + "&email=" + email;
            System.Net.HttpWebRequest webrequest = WebRequest.Create(request) as HttpWebRequest;

            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            using (Stream stream = response.GetResponseStream())

            using (StreamReader reader = new StreamReader(stream))
            {
                htmlRep = reader.ReadToEnd();
            }

            return htmlRep;
        }
    }
}
