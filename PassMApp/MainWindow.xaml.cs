/***************************************************
	Description: Password Memorising Application
	Written by: DJANGO JANE ʕ•́ᴥ•̀ʔ
    Date: 11/2020


***************************************************/

using PassMApp.Model;
using PassMApp.ViewModel;
using PassMApp.Views;
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

namespace PassMApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        #pragma warning disable IDE1006 // Naming Styles

        public MainWindow()
        {
                InitializeComponent();
            lblCon.Content = djane.Operations.GetRunningVersion();
            djane.Operations.IsServerConnected();

            try
            {
                AccountViewModel am = new AccountViewModel();
                DataContext = am;
            }
            catch
            {

            }
            if (IsConnected())
            {
                lblInternet.Foreground = new SolidColorBrush(Colors.Red);
                lblInternet.Content = "CONNECTED";
            }
            else
            {
                lblInternet.Foreground = new SolidColorBrush(Colors.ForestGreen);
                lblInternet.Content = "DISCONNECTED";
            }

        }




        public bool IsConnected()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        private void btnAddRe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Add_Record form = new Add_Record();
                form.ShowDialog();
                AccountViewModel avm = new AccountViewModel();
                lbMain.DataContext = avm;
            }
            catch
            {

            }
        }

        private void btnPractice_Click(object sender, RoutedEventArgs e)
        {
            Practice form = new Practice();
            form.ShowDialog();

            AccountViewModel avm = new AccountViewModel();
            lbMain.DataContext = avm;

        }

        private void btnEmtpy_Click(object sender, RoutedEventArgs e)
        {
            var j = MessageBox.Show("Are you sure you want to delete all data?", "WARNING!!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (MessageBoxResult.Yes == j)
            {
                AccountViewModel avm = new AccountViewModel();
                avm.DELETE_ALL_FILES();
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
            }
        }

        private void btnDelAcc_Click(object sender, RoutedEventArgs e)
        {
                string myVar = lblInvisBucket.Content.ToString();
            var del = System.Windows.Forms.MessageBox.Show($"Are you sure you want to delete {myVar.ToUpper()}?","Warning!!", 
                System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            if (System.Windows.Forms.DialogResult.Yes == del)
            {

                AccountViewModel avm = new AccountViewModel();
                avm.DeleteRecord(myVar.ToString());
                lbMain.DataContext = avm;
            }
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbMain.SelectedItems.Count>0)
            {
                btnDelAcc.Visibility = Visibility.Visible;
            }
            else
            {
                btnDelAcc.Visibility = Visibility.Hidden;
            }

        }
    }
}
