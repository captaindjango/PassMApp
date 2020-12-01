﻿using System;
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
using log4net.Config;


namespace PassMApp
{
    /// <summary>
    /// Interaction logic for Practice.xaml
    /// </summary>
    public partial class Practice : Window
    {
        readonly djane.Operations djOP = new djane.Operations();
        private static readonly ILog log_Email = log4net.LogManager.GetLogger("SmtpAppender");
        private static readonly ILog log_File = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public Practice()
        {
            InitializeComponent();
            timer1.Interval = 1000;//1 second
            timer1.Tick += new System.EventHandler(timer1_Tick);
            pbarStats.Maximum = 100;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            log_File.Info($"Application ran at [{DateTime.Now.TimeOfDay}]");
            pbPractice.Focus();

            string var = djOP.GetPasswordHint();
          if(var == string.Empty)
            {
                var dialog = System.Windows.Forms.MessageBox.Show("You haven't added a record yet. " +
                    "Would you like to add one?",
                                            "Information !!", 
                                            System.Windows.Forms.MessageBoxButtons.YesNo,
                                            System.Windows.Forms.MessageBoxIcon.Information);

                if (System.Windows.Forms.DialogResult.Yes == dialog)
                {
                    Add_Record wndow = new Add_Record();

                    wndow.ShowDialog();

                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {

                txbAccHint.Content = djOP.GetPasswordHint();
            }
            //log_Email.Info($"{Application.Current.Windows} ran at [{DateTime.Now.TimeOfDay}]");
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
            
            bool results = djOP.CheckPassword(txbAccHint.Content.ToString(), pbPractice.Password.Normalize());

            if (results)
            {
                txbPass.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                txbPass.Foreground = new SolidColorBrush(Colors.Red);
            }

            object[] obj = new object[5];

            obj = djOP.GetStats(txbAccHint.Content.ToString());
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

            txbAccHint.Content = djOP.GetPasswordHint().ToUpper();

            pbarStats.Value = 0;
            pbPractice.Clear();
        }
    }
}

