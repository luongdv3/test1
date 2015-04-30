using SQLiteWp8.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWp8.ViewModel
{
    class ReadAllLoginItem
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
        public ObservableCollection<LoginTable> GetAllLoginItems()
        {
            return Db_Helper.ReadLogins();
        }

        public LoginTable GetExistUserLogin(string userName, string password)
        {
            return Db_Helper.GetExistUser(userName, password);
        }
    }
}
