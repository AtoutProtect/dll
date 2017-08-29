using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace LicenceController
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class LicenceWindow : Window
    {
        Connexion con = new Connexion();
        Crypto CR = new Crypto();
        Path filePath = new Path();
        Check verif = new Check();

        public LicenceWindow()
        {
            InitializeComponent();
            this.Title = "Atout Protect©";
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            String licValid = con.Verification(getKey(), getEmail());
            
            lblRes.Content = licValid;

            if(licValid != "false")
            {
                lblRes.Content = "Votre Licence est valide jusqu'au " + licValid;
                verif.writeInFile(getKey(), getEmail(), licValid);

            }
            else if(licValid == "false")
            {
                lblRes.Content = "Licence incorrecte, réessayer ou cliquer sur boutique.";
            }          
            
        }

        public string getKey()
        {
            string key = txtBoxLic1.Text + txtBoxLic2.Text + txtBoxLic3.Text + txtBoxLic4.Text;

            return key;
        }

        public string getEmail()
        {
            string email = txtBoxEmail.Text;

            return email;
        }

        private void txtBoxLic1_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBoxLic1.Text.Length == 4)
            {
                FocusManager.SetFocusedElement(this, txtBoxLic2);
            }
        }

        private void txtBoxLic2_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBoxLic2.Text.Length == 4)
            {
                FocusManager.SetFocusedElement(this, txtBoxLic3);
            }
        }

        private void txtBoxLic3_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBoxLic3.Text.Length == 4)
            {
                FocusManager.SetFocusedElement(this, txtBoxLic4);
            }
        }

        private void btnBoutique_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://atout-protect.tk");
        }

        private void txtBoxEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBoxEmail.Text = "";
        }

        private void txtBoxLic1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBoxLic1.Text = "";
            txtBoxLic2.Text = "";
            txtBoxLic3.Text = "";
            txtBoxLic4.Text = "";
        }
    }
}
