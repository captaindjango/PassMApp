using PassMApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PassMApp.ViewModel
{
    public class AccountViewModel : INotifyCollectionChanged
    {

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public Account _selectedAccount;
        private static string rpConn = ConfigurationManager.ConnectionStrings["RP_Connection1"].ConnectionString;
        private djane.Operations djOP = new djane.Operations();


        public AccountViewModel()
        {
            this.LoadAccountsToListBox();
            this.Accounts.CollectionChanged += this.OnCollectionChanged;
        }   
        public ObservableCollection<Account> Accounts 
        {     
            get; 
            set; 
        }

        public void AddAccountRecord(System.Windows.Controls.TextBox tbAcc, System.Windows.Controls.PasswordBox passBox)
        {
            djOP.AddRecord(tbAcc.Text, passBox.Password);
        }
        /// <summary>
        /// Selected account property of <see cref="Account"/> from ListBox control
        /// </summary>
        public Account SelectedAccount
        {
            get {return _selectedAccount; }
            set
            {
                _selectedAccount =  value;
                RaisePropertyChanged("SelectedAccount");
            }
        }

        public bool CheckPassword(string v1, string v2)
        {
            return djOP.CheckPassword(v1, v2);
        }

        public object[] GetStats(string v)
        {
            return djOP.GetStats(v);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetPasswordHint()
        {
            return djOP.GetPasswordHint();
        }

        public void DELETE_ALL_FILES()
        {
            djOP.DELETE_ALL_FILES();
            this.LoadAccountsToListBox();
           // this.CollectionChanged += this.OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<Account> AccountsSender = sender as ObservableCollection<Account>;
            ObservableCollection<Account> editedOrRemovedItems = new ObservableCollection<Account>();
            foreach(Account newItem in e.NewItems)
            {
                editedOrRemovedItems.Add(new Account 
                { 
                    account = newItem.account,
                    attempts = newItem.attempts,
                    progress = newItem.progress
                });
            }
            foreach(Account oldItem in e.OldItems)
            {
                editedOrRemovedItems.Add(oldItem);
            }
           // RefreshView();
            Accounts.CollectionChanged += OnCollectionChanged;
            NotifyCollectionChangedAction action = e.Action;
            //CollectionChanged?.Invoke(this, new CollectionChangeEventHandler(propertyName));
        }

        internal void DeleteRecord(string v)
        {
            djOP.DeleteRecord(v);
            this.LoadAccountsToListBox();
            //this.Accounts.CollectionChanged += this.OnCollectionChanged;
        }

        /// <summary>
        /// Loads <see cref="Account"/> to ObservableCollection from SQL database.
        /// </summary>
        public void LoadAccountsToListBox()
        {
            ObservableCollection<Account> acc = new ObservableCollection<Account>();

            using (SqlConnection Conn = new SqlConnection(rpConn))
            {
                string query = "SELECT account,attempts, progress from UserStash";
                using (SqlCommand cmd = new SqlCommand(query, Conn))
                {
                    cmd.CommandType = CommandType.Text;

                    Conn.Open();
                    SqlDataReader sda = cmd.ExecuteReader();

                    if (sda.HasRows)
                    {
                        while (sda.Read())
                        {

                            acc.Add(new Account
                            {
                                account = sda.GetValue(sda.GetOrdinal("account")).ToString(),
                                attempts = (int)sda.GetValue(sda.GetOrdinal("attempts")),
                                progress = (int)sda.GetValue(sda.GetOrdinal("progress")),
                            });
                        }
                        Accounts = acc;
                        this.CollectionChanged += this.OnCollectionChanged;
                    }
                }
            }

        }

        public void GetAccountName(Label tbAccHint)
        {
            tbAccHint.Content = djOP.GetPasswordHint();
        }
    }
}
