using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reflection;
using log4net;
using System.Deployment.Application;

namespace djane
{
    public class Operations : INotifyPropertyChanged
    {
        public static string rpConn = ConfigurationManager.ConnectionStrings["conn.str"].ConnectionString;
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string assemblyVersion1 = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion.ToString();


        #region INTERNAL OPERATIONS


        public string GetRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (Exception)
            {
                
                return assemblyVersion1;
            }
        }

        public void Raise(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void CheckDirectoryExists(System.Windows.Forms.NotifyIcon notification)
        {
            string nameOfDirectory = string.Empty;
            //AssemblyInfo assInfo = new AssemblyInfo();

                notification.ShowBalloonTip(2000, "Rehearse Passwords.", "Database Created !", System.Windows.Forms.ToolTipIcon.Info);

        }

        #endregion

        #region APP OPERATIONS
        public void AddRecord(string account, string password)
        {
            using (SqlConnection Conn = new SqlConnection(rpConn))
            {
                string query = "INSERT INTO UserStash (Account,Password) values(@Account,@Password)";
                using (SqlCommand cmd = new SqlCommand(query,Conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@Account", SqlDbType.VarChar).Value= (account);
                    cmd.Parameters.Add("@Password",SqlDbType.VarChar).Value = EncryptData(password);

                    try
                    {
                        Conn.Open();
                        cmd.ExecuteNonQuery();
                        //Raise(Loaded);
                        System.Windows.Forms.NotifyIcon notifyUser = new System.Windows.Forms.NotifyIcon();
                        log.Info($"Record Added :-");
                        notifyUser.ShowBalloonTip(2000, "Rehearse Passwords", "Record Created",  System.Windows.Forms.ToolTipIcon.Info);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
        public string GetPasswordHint()
        {

            string value="";
            using (SqlConnection Conn = new SqlConnection(rpConn))
            {
                string query = "SELECT top 1 account from UserStash order by newid()";
                using (SqlCommand cmd = new SqlCommand(query, Conn))
                {
                    cmd.CommandType = CommandType.Text;
                    Conn.Open();

                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            string j = read.GetValue(read.GetOrdinal("account")).ToString();
                            value = j;
                            return value;
                        }
                        read.Close();
                    }
                   
                    return value;
                }
            }
        }
        public bool CheckPassword(string Account, string password)
        {
            string value="";

            using (SqlConnection Conn = new SqlConnection(rpConn))
            {
                string query = "SELECT password from UserStash where account = @account";
                using (SqlCommand cmd = new SqlCommand(query, Conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@account", SqlDbType.NVarChar).Value = Account;
                    Conn.Open();

                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            string j = read.GetValue(read.GetOrdinal("password")).ToString();
                            value = j;
                        }
                        read.Close();
                    }
                }
            }
            if(ValidateEncryptedData(password,value))
            {
                WriteToRecord(Account, 1);
            }
            else
            {
                WriteToRecord(Account, 0);
            }
             return ValidateEncryptedData(password, value);
        }
        public static void PopulateAccountList(ListBox lv)
        {
            using (SqlConnection Conn = new SqlConnection(rpConn))
            {
                string query = "SELECT account,attempts, progress from UserStash";
                using (SqlCommand cmd = new SqlCommand(query, Conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        Conn.Open();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        lv.ItemsSource = dt.DefaultView;
                    }
                }
            }
        }
        void WriteToRecord(string account, int point)
        {

            using (SqlConnection Conn = new SqlConnection(rpConn))
            {
                string query = "UPDATE UserStash  SET Attempts=Attempts+1,Progress=Progress+@p where Account = @Account";
                using (SqlCommand cmd = new SqlCommand(query, Conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@Account", SqlDbType.VarChar).Value = account;
                    cmd.Parameters.Add("@p", SqlDbType.Int).Value = point;

                    try
                    {
                        Conn.Open();
                        cmd.ExecuteNonQuery();
                        //Raise(Loaded);
                        System.Windows.Forms.NotifyIcon notifyUser = new System.Windows.Forms.NotifyIcon();
                        notifyUser.ShowBalloonTip(2000, "Rehearse Passwords", "Record Updated", System.Windows.Forms.ToolTipIcon.Info);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
        public object[] GetStats(string account)
        {
            object[] myOb = new object[5];

            using (SqlConnection Conn = new SqlConnection(rpConn))
            {
                string query = "SELECT attempts,progress,password from UserStash where account = @account";
                using (SqlCommand cmd = new SqlCommand(query, Conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
                    Conn.Open();

                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            myOb[0] = account.ToString();
                            myOb[1] = read.GetValue(read.GetOrdinal("attempts")).ToString();
                            myOb[2] = read.GetValue(read.GetOrdinal("progress")).ToString();
                            myOb[3] = read.GetValue(read.GetOrdinal("password")).ToString();
                        }
                        read.Close();
                    }
                }
            }
            return myOb;
        }
        public bool DELETE_ALL_FILES()
        {
            using (SqlConnection cnn = new SqlConnection(rpConn))
            {
                string query = "truncate table UserStash";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        System.Windows.MessageBox.Show("ALL FILES HAVE BEEN DELETED.", "Information!!", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Warning);
                        return true;
                    }
                    catch(Exception)
                    {
                        throw;
                    }
                }
            }
        }
        #endregion

        #region ENCRYPTION
        string EncryptData(string valueToEncrypt)
        {
            string GenerateSalt()
            { 
                    RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                    byte[] salt = new byte[32];
                    crypto.GetBytes(salt);

                return Convert.ToBase64String(salt);
            }
            string EncryptValue(string strvalue)
            {
                string saltValue = GenerateSalt();
                byte[] saltedPassword = Encoding.UTF8.GetBytes(saltValue + strvalue);
                SHA256Managed hashstr = new SHA256Managed();
                byte[] hash = hashstr.ComputeHash(saltedPassword);

                return $"{Convert.ToBase64String(hash)}:{saltValue}";
            }
            return EncryptValue(valueToEncrypt);
        }
        bool ValidateEncryptedData(string valueToValidate, string valueFromDatabase)
        {
            string[] arrValues = valueFromDatabase.Split(':');
            string encryptedDbValue = arrValues[0];
            string salt = arrValues[1];
            byte[] saltedValue = Encoding.UTF8.GetBytes(salt + valueToValidate);
            SHA256Managed hashstr = new SHA256Managed();
            byte[] hash = hashstr.ComputeHash(saltedValue);
            string enteredValueToValidate = Convert.ToBase64String(hash);
            
            return encryptedDbValue == enteredValueToValidate; 
        }
        #endregion

    }
}
