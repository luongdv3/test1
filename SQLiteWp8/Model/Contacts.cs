using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWp8.Model
{
    public class Contacts : INotifyPropertyChanged
    {
       
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        //The Id property is marked as the Primary Key
        private int idValue;

        private string NameValue = String.Empty;
        private string phoneNumberValue = String.Empty;
        private string emailValue = String.Empty;
        private string genderValue = String.Empty;
        private string addressValue = String.Empty;

        public string ImageLink { get { return "http://uniforcesecurity.co.uk/wp-content/uploads/2014/07/contact_us_icon.png"; } }

        public string Name
        {
            get { return this.NameValue; }

            set
            {
                if (value != this.NameValue)
                {
                    this.NameValue = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        public string PhoneNumber
        {
            get { return this.phoneNumberValue; }

            set
            {
                if (value != this.phoneNumberValue)
                {
                    this.phoneNumberValue = value;
                    NotifyPropertyChanged("PhoneNumber");
                }
            }
        }

        public string Address
        {
            get { return this.addressValue; }

            set
            {
                if (value != this.addressValue)
                {
                    this.addressValue = value;
                    NotifyPropertyChanged("Address");
                }
            }
        }

        public string Gender
        {
            get { return this.genderValue; }

            set
            {
                if (value != this.genderValue)
                {
                    this.genderValue = value;
                    NotifyPropertyChanged("Gender");
                }
            }
        }

        public string Email
        {
            get { return this.emailValue; }

            set
            {
                if (value != this.emailValue)
                {
                    this.emailValue = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public string CreationDate {
            get; set; 
        }
        public Contacts()
        {
        }
        public Contacts(string name,string phone_no, string email, string address, string gender)
        {
            Name = name;
            PhoneNumber = phone_no;
            Email = email;
            Address = address;
            Gender = gender;

            CreationDate = DateTime.Now.ToString();
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
