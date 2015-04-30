using SQLite;
using SQLiteWp8.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWp8.ViewModel
{
    //Cài đặt sqlite for windows phone
    // add refrence của dll vào
    //This class for perform all database CRUID operations
    public class DatabaseHelperClass
    {
        SQLiteConnection dbConn;
       
        //Create Tabble
        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Contacts>();
                    }
                } 
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Retrieve the specific contact from the database.
        public Contacts ReadContact(int contactid)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Contacts>("select * from Contacts where Id =" + contactid).FirstOrDefault();
                return existingconact;
            }
        }
        // Retrieve the all contact list from the database.
        public ObservableCollection<Contacts> ReadContacts()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Contacts> myCollection  = dbConn.Table<Contacts>().ToList<Contacts>();
                ObservableCollection<Contacts> ContactsList = new ObservableCollection<Contacts>(myCollection);
                return ContactsList;
            }
        }
        
        //Update existing conatct
        public void UpdateContact(Contacts contact)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existContact = dbConn.Query<Contacts>("select * from Contacts where Id =" + contact.Id).FirstOrDefault();
                if (existContact != null)
                {
                    existContact.Name = contact.Name;
                    existContact.PhoneNumber = contact.PhoneNumber;
                    existContact.Address = contact.Address;
                    existContact.Gender = contact.Gender;
                    existContact.Email = contact.Email;

                    existContact.CreationDate = contact.CreationDate;

                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existContact);
                    });
                }
            }
        }
        // Insert the new contact in the Contacts table.
        public void Insert(Contacts newcontact)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                    {
                        dbConn.Insert(newcontact);
                    });
            }
        }
       
        //Delete specific contact
        public bool DeleteContact(int Id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Contacts>("select * from Contacts where Id =" + Id).FirstOrDefault();
                if (existingconact != null)
                {
                    //dbConn.RunInTransaction(() =>
                    //{
                    //    dbConn.Delete(existingconact);
                    //});
                    int i = dbConn.Delete(existingconact); 
                    if (i == 1)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }
        //Delete all contactlist or delete Contacts table
        public void DeleteAllContact()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                //dbConn.RunInTransaction(() =>
                //   {
                       dbConn.DropTable<Contacts>();
                       dbConn.CreateTable<Contacts>();
                       dbConn.Dispose();
                       dbConn.Close();
                   //});
            }
        }

        // Retrieve the specific contact from the database.
        public LoginTable ReadLogin(int loginId)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var list = dbConn.Query<LoginTable>("select * from Login where Id =" + loginId).FirstOrDefault();
                return list;
            }
        }
        // Retrieve the all contact list from the database.
        public ObservableCollection<LoginTable> ReadLogins()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<LoginTable> myCollection = dbConn.Table<LoginTable>().ToList<LoginTable>();
                ObservableCollection<LoginTable> list = new ObservableCollection<LoginTable>(myCollection);
                return list;
            }
        }

        public LoginTable GetExistUser(string userName, string password)
        {
            LoginTable user = null;
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingUser = dbConn.Query<LoginTable>("select * from LoginTable where username ='" + userName + "' and password ='" + password + "'").FirstOrDefault();
                user = (LoginTable)existingUser;
            }
            return user;
        }
    }
}
