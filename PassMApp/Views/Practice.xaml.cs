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
        #pragma warning disable IDE1006 // Naming Styles
        public System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer practice_timer = new System.Windows.Forms.Timer();

            int max_time = 30000,total_time;

        public Practice()
        {
            InitializeComponent();
            timer1.Interval = 1000;//1 second
            practice_timer.Interval = 1000;
            timer1.Tick += new System.EventHandler(timer1_Tick);
            practice_timer.Tick += new System.EventHandler(practice_timer_Tick);
            pbarStats.Maximum = 100;
        }

        private void practice_timer_Tick(object sender, EventArgs e)
        {
            if(total_time ==max_time)
            {
                practice_timer.Stop();
                total_time = 0;
                btnGo_Click(null, null);
            }
            else
            {
                total_time += 1000;
            }
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
            practice_timer.Start();

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
