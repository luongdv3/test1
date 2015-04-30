using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SQLiteWp8.Model
{
    public class LoginTable : INotifyPropertyChanged
    {

         [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id
        {
            get;
            set;
        }

         private string userNameValue = String.Empty;

         private string passwordValue = String.Empty;

         public string username
         {
             get { return this.userNameValue; }

             set
             {
                 if (value != this.userNameValue)
                 {
                     this.userNameValue = value;
                     NotifyPropertyChanged("username");
                 }
             }
         }
         public string password
         {
             get { return this.passwordValue; }

             set
             {
                 if (value != this.passwordValue)
                 {
                     this.passwordValue = value;
                     NotifyPropertyChanged("password");
                 }
             }
         }

         public LoginTable()
        {
        }
        public LoginTable(string username,string password)
        {
            userNameValue = username;
            passwordValue = password;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    
    }
}
