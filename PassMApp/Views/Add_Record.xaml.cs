using PassMApp.ViewModel;
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
using System.Windows.Shapes;

namespace PassMApp.Views
{
    /// <summary>
    /// Interaction logic for Add_Record.xaml
    /// </summary>
    public partial class Add_Record : Window
    {
        public Add_Record()
        {
            InitializeComponent();
            AccountViewModel avm = new AccountViewModel();
            lbRec.DataContext = avm;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((pbox1.Password.Equals(string.Empty)) && pbox2.Password.Equals(string.Empty) || tbAcc.Text.Equals(string.Empty))
            {
                MessageBox.Show("All fields are required.", "Warning !!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!pbox1.Password.Equals(pbox2.Password))
                {
                    MessageBox.Show("Passwords are not the same.", "Warning !!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AccountViewModel am = new AccountViewModel();
                    am.AddAccountRecord(tbAcc, pbox1);
                   // djOP.AddRecord(tbAcc.Text, pbox1.Password);
                    this.Close();
                }
            }

            pbox1.Clear();
            pbox2.Clear();
        }

        private void tbAcc_LostFocus(object sender, RoutedEventArgs e)
        {
            tbAcc.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbAcc.Text.ToLower());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbAcc.Focus();
        }
    }
}
