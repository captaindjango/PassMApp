/*
	Description: An application to help users remember their passwords.
	Written by: DJANGO JANE ʕ•́ᴥ•̀ʔ
	Date: 10/11/2020
*/




using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using log4net;

namespace PassMApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log_Email = log4net.LogManager.GetLogger("EmailLogger");
        private static readonly ILog log_File = log4net.LogManager.GetLogger("RollingFile");
        public static string rpConn = ConfigurationManager.ConnectionStrings["RP_Connection"].ConnectionString;
        readonly djane.Operations djOp = new djane.Operations();
        public string _load;

        public string isLoaded
        {
            get { return _load; }
            set
            {
                _load = value;
                INotifyPropertyChanged("Loaded");
            }
        }

        private void INotifyPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }

        public MainWindow()
        {
            InitializeComponent();
            lblCon.Content = djOp.GetRunningVersion();
        }

        private void CheckDatabaseConnection()
        {
            using (SqlConnection conn = new SqlConnection(rpConn))
            {

            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            djane.Operations.PopulateAccountList(lbMain);
            
           // addRecBtnImg.Source = "Creative-Freedom-Shimmer-Document-Add.ico.png");
            //log_Email.Info($"Application ran at [{DateTime.Now.TimeOfDay}]");
            log_File.Info($"Application ran at [{DateTime.Now.TimeOfDay}]");
            djOp.Raise("Loaded");
        }

        private void btnAddRe_Click(object sender, RoutedEventArgs e)
        {
            Add_Record form = new Add_Record();
            form.ShowDialog();
            djane.Operations.PopulateAccountList(lbMain);
        }

        private void btnPractice_Click(object sender, RoutedEventArgs e)
        {
            Practice form = new Practice();
            form.ShowDialog();
            djane.Operations.PopulateAccountList(lbMain);
        }

        private void btnEmtpy_Click(object sender, RoutedEventArgs e)
        {
            var j = MessageBox.Show( "Are you sure you want to delete all data?", "WARNING!!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (MessageBoxResult.Yes == j)
            {
                djOp.DELETE_ALL_FILES();
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
            }
        }

        private void btnDelAcc_Click(object sender, RoutedEventArgs e)
        {
            var del = System.Windows.Forms.MessageBox.Show($"Are you sure you want to delete {lbMain.SelectedItem}",
                "Warning!!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            if (System.Windows.Forms.DialogResult.Yes == del)
            {
                // lbMain.Items;
                //lbMain.ItemsControl.ItemsSource;
                lbMain.ClearValue(ItemsControl.ItemsSourceProperty);
                //lbMain.ItemsSource = NewSource;
                var itm = lbMain.SelectedItem.ToString();
                while(lbMain.SelectedItems.Count>0)
                {
                    lbMain.Items.Remove(lbMain.Items[lbMain.SelectedIndex]);
                }
                //lbMain.Items.Remove(SelectedItem);
                //djOp.DeleteRecord()
            }
        }
    }
}
