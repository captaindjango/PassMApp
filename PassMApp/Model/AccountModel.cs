using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassMApp.Model
{
    public class AccountModel { }
   
    /// <summary>
    /// This class is for the basic properties of having an account.
    /// </summary>
    public class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Properties of an Account

        private string _account;
        /// <summary>
        /// The account name. For example, yahoo.
        /// </summary>
        public string account
        {
            get { return _account; }
            set { _account = value;
                //NotifyPropertyChanged("Student");

            }
        }

        private int _progress;
        /// <summary>
        /// The progress user has made on rehearsing <see cref="account"/>.
        /// </summary>
        public int progress
        {
            get { return _progress; }
            set { _progress = value; }
        }


        private int _attempts;
        /// <summary>
        /// The number of attempts the user has made on rehearsing <see cref="account"/>.
        /// </summary>
        public int attempts
        {
            get { return _attempts; }
            set { _attempts = value; }
        }

        #endregion

        private void OnPropertyChanged(string propertyname)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
