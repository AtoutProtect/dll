using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LicenceController;

namespace AP
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        LicenceController.Check verif = new LicenceController.Check();

        public LicenceWindow lw = new LicenceWindow();
        public MainWindow mw = new MainWindow();

        protected override void OnStartup(StartupEventArgs e)
        {
            if (verif.verification() == false)
            {
                lw.Show();
            }
            else
            {                    
                mw.Show();
            }            
        }
    }    
}
