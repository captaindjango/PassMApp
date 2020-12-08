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
    /// Interaction logic for Practice.xaml
    /// </summary>
    public partial class Practice : Window
    {
        public System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public Practice()
        {
            InitializeComponent();
            timer1.Interval = 1000;//1 second
            timer1.Tick += new System.EventHandler(timer1_Tick);
            pbarStats.Maximum = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //do whatever you want
            if (pbarStats.Value == pbarStats.Maximum)
            {
                timer1.Stop();
                pbarStats.Value = 0;
                txbAcc.Text = "";
                txbAttM.Text = "";
                txbAttP.Text = "";
                txbPass.Text = "";
            }
            else
                pbarStats.Value += 25;
        }
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            timer1.Start();
            AccountViewModel avm = new AccountViewModel();
            
            bool results = avm.CheckPassword(txbAccHint.Content.ToString(), pbPractice.Password.Normalize());

            if (results)
            {
                txbPass.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                txbPass.Foreground = new SolidColorBrush(Colors.Red);
            }

            object[] obj = new object[5];

            obj = avm.GetStats(txbAccHint.Content.ToString());
            txbAcc.Text = obj[0].ToString();
            txbAttM.Text = obj[1].ToString();
            txbAttP.Text = obj[2].ToString();

            if (results)
            {
                txbPass.Text = pbPractice.Password.Normalize();

            }
            else
            {
                txbPass.Text = obj[3].ToString();
            }

            txbAccHint.Content = avm.GetPasswordHint();

            pbarStats.Value = 0;
            pbPractice.Clear();

            avm.LoadAccountsToListBox();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pbPractice.Focus();
            AccountViewModel avm = new AccountViewModel();
            avm.GetAccountName(txbAccHint);
            
        }
    }
}
