using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using log4net;

namespace PassMApp
{
    /// <summary>
    /// Interaction logic for Add_Record.xaml
    /// </summary>
    public partial class Add_Record : Window
    {
        readonly djane.Operations djOP = new djane.Operations();
        private static readonly ILog log_Email = log4net.LogManager.GetLogger("EmailLogger");
        private static readonly ILog log_File = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
   
        public Add_Record()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if((pbox1.Password.Equals(string.Empty)) && pbox2.Password.Equals(string.Empty) ||  tbAcc.Text.Equals(string.Empty))
            {
                MessageBox.Show("All fields are required.", "Warning !!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!pbox1.Password.Equals(pbox2.Password))
                {
                    MessageBox.Show("Passwords are not the same.","Warning !!",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else
                {
                    djOP.AddRecord(tbAcc.Text, pbox1.Password);
                    this.Close(); 
                }
            }

                pbox1.Clear();
                pbox2.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbAcc.Focus();
            djane.Operations.PopulateAccountList(lbRec);
            //log_Email.Info($"{Application.Current.Windows} ran at [{DateTime.Now.TimeOfDay}]");
            log_File.Info($"Application ran at [{DateTime.Now.TimeOfDay}]");

        }

        private void chkbPass_Click(object sender, RoutedEventArgs e)
        {
            var password = pbox1 as PasswordBox;
            tbmask1.Text = password.Password;

            var pass2 = pbox2 as PasswordBox;
            tbmask2.Text = pass2.Password;


          if (chkbPass.IsChecked == true)
            {
                pbox1.Visibility = Visibility.Collapsed;
                tbmask1.Visibility = Visibility.Visible;

                pbox2.Visibility = Visibility.Collapsed;
                tbmask2.Visibility = Visibility.Visible;
            }
            else
            {
                pbox1.Visibility = Visibility.Visible;
                tbmask1.Visibility = Visibility.Collapsed;

                pbox2.Visibility = Visibility.Visible;
                tbmask2.Visibility = Visibility.Collapsed;
            }
        }

        private void tbAcc_LostFocus(object sender, RoutedEventArgs e)
        {
            tbAcc.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbAcc.Text.ToLower());

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
