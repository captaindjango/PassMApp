using System.ComponentModel;
using System.Windows.Media;

namespace PassMApp.Model
{
    public class AccountModel { }
   
#pragma warning disable IDE1006 // Naming Styles
    /// <summary>
    /// This class is for the basic properties of having an account.
    /// </summary>
    public class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Properties of an Account

        private int _attempts;
        private string _account;
        private float _progress;
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

        /// <summary>
        /// The progress user has made on rehearsing <see cref="account"/>.
        /// </summary>
        public float progress
        {
            get { return _progress; }
            set { _progress = value; }
        }


        /// <summary>
        /// The number of attempts the user has made on rehearsing <see cref="account"/>.
        /// </summary>
        public int attempts
        {
            get { return _attempts; }
            set { _attempts = value; }
        }


        //public Brush RandomBrush 
        //{ 
        //    get { return RColourGenerator.generateRandomColour(); } 
        //    set { }
        //}

        #endregion

        private void OnPropertyChanged(string propertyname)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
