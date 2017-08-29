using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceController
{
    public class Path
    {
        // Retourne le nom du dossier utilisateur
        public string getUser()
        {
            string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            return user;
        }

        // Retourne le chemin complet du dossier Atout Protect
        public string getPath()
        {
            return getUser() + "\\AppData\\Roaming\\Atout Protect";
        }

        // Creer le repertoire Atout Protect
        public void createDir()
        {
            DirectoryInfo di = Directory.CreateDirectory(getPath());
        }
    }
}
