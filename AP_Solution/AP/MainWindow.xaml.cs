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
using LicenceController;

namespace AP
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string carac = "";
        double tmp = 0;
        double tmp2 = 0;
        double total = 0;
        bool edit = true; //Variable used when a result is displayed to reinit the display when the user clic on a new number
        bool change = false;

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btNumber_Click(object sender, RoutedEventArgs e)
        {
            Button btSender = (Button)sender;

            if (lbl.Content == "0" || !edit)
            {
                lbl.Content = btSender.Content;
                edit = true;
                change = true;
            }
            else
            {
                lbl.Content = lbl.Content.ToString() + btSender.Content;
            }
        }

        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            int cptDot = 0;

            var str = lbl.Content.ToString();

            if (str.Contains(","))
            {
                cptDot++;
            }

            if (cptDot == 0)
            {
                lbl.Content = lbl.Content + ",";
            }
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            Button btSender = (Button)sender;
            //carac = btSender.Text;

            if (tmp == 0)
            {
                var str = lbl.Content.ToString();
                Double.TryParse(str, out tmp);
                total = tmp;
            }
            else if (change)
            {
                btEgal_Click(sender, e);
            }
            carac = btSender.Content.ToString();
            lbl.Content = total.ToString();
            edit = false;
            change = false;
        }

        private void btEgal_Click(object sender, RoutedEventArgs e)
        {
            Double.TryParse(lbl.Content.ToString(), out tmp2);

            if (tmp != 0)
            {
                switch (carac)
                {
                    case "/":
                        if (tmp2 != 0) total = tmp / tmp2;
                        else total = 0;
                        break;
                    case "x":
                        total = tmp * tmp2;
                        break;
                    case "-":
                        total = tmp - tmp2;
                        break;
                    case "+":
                        total = tmp + tmp2;
                        break;
                    default:
                        break;
                }
                tmp = total;
                tmp2 = 0;
                lbl.Content = total.ToString();
                edit = false;
                change = false;
            }
        }

        private void btPlusMoins_Click(object sender, RoutedEventArgs e)
        {
            if (lbl.Content != "0")
            {
                if (tmp == 0)
                {
                    Double.TryParse(lbl.Content.ToString(), out tmp);
                    tmp *= -1;
                    lbl.Content = tmp.ToString();
                }
                else
                {
                    Double.TryParse(lbl.Content.ToString(), out tmp2);
                    tmp2 *= -1;
                    lbl.Content = tmp2.ToString();
                }
            }
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            lbl.Content = "0";
            tmp = 0;
            tmp2 = 0;
            edit = true;
            change = false;
        }
    }
}
