using System;
using System.Collections.Generic;
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
using MyDll;
using System.Diagnostics;

using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace AP_1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDll.Class1 AP = new MyDll.Class1();

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Atout Protect";

            DirectoryInfo di = Directory.CreateDirectory(getPath() + "\\AppData\\Roaming\\Atout Protect");

            
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            crtLic();

            lblRes.Content = AP.Verification(getKey());

            msgBox(AP.Verification(getKey()));

        }

        private string getKey()
        {
            string key = txtBoxLic1.Text + txtBoxLic2.Text + txtBoxLic3.Text + txtBoxLic4.Text;

            return key;
        }

        private string getEmail()
        {
            string email = txtBoxEmail.Text;

            return email;
        }

        private void crtLic()
        {
            string[] lines = { getKey(), getEmail() };
            System.IO.File.WriteAllLines(@getPath() + "\\AppData\\Roaming\\Atout Protect\\atoutprotect.lic", lines);
        }

        private string getPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            return path;
        }

        private void msgBox(string bl)
        {
            if (bl == "true")
            {
                msgBoxVal();
            }
            else
            {
                msgBoxNotVal();
            }
        }

        private void msgBoxVal()
        {
            MessageBox.Show("La licence est valide", "Verification");
        }

        private void msgBoxNotVal()
        {
            MessageBoxResult result = MessageBox.Show("Voulez vous acheter une Licence ?", "Verification", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("Vous allez etre rediriger vers le site de Atout Protect", "Verification");
                    Process.Start("http://www.opisto.fr");
                    DirectoryInfo di = Directory.CreateDirectory("C:\\AP");
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Oh c'est pas très gentil", "Verification");
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Tant pis...", "Verification");
                    break;
            }
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
    }

}
