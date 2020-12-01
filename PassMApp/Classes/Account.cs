namespace PassMApp.Classes
{
     class Account
    {
        private string _account;

        public string account
        {
            get { return _account; }
            set { _account = value; }
        }

        private int _progress;

        public int progress
        {
            get { return _progress; }
            set { _progress = value; }
        }

        private int _attempts;

        public int attempts
        {
            get { return _attempts; }
            set { _attempts = value; }
        }


    }
}