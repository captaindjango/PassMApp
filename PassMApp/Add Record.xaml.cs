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
            if (pbox1.Password.Normalize() == pbox2.Password.Normalize())
            {
                djOP.AddRecord(tbAcc.Text, pbox1.Password);
                this.Close();
            }
            else
            {
                pbox1.Clear();
                pbox2.Clear();
            }
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
            if (chkbPass.IsChecked == true)
            {
                
            }
            else
            {
            }
        }
    }
}
