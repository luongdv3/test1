using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLiteWp8.ViewModel;
using SQLiteWp8.Model;
using SQLite;

namespace SQLiteWp8.Views
{
    public partial class AddContact : PhoneApplicationPage
    {

        public AddContact()
        {
            InitializeComponent();
        }

        private async void AddContact_Click(object sender, RoutedEventArgs e)
        {

            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            if (NametxtBx.Text != "" && PhonetxtBx.Text != "" )
            {
                string gendertext;
                if (GenderMaleRd.IsChecked == true)
                {
                    gendertext = "Nam";
                }
                else if (GenderFemaleRd.IsChecked == true)
                {
                    gendertext = "Nữ";
                }
                else
                {
                    gendertext = "Nam";
                }
                Db_Helper.Insert(new Contacts(NametxtBx.Text.Trim(), PhonetxtBx.Text.Trim(), EmailtxtBx.Text.Trim(), AddresstxtBx.Text.Trim(), gendertext));//
                //NavigationService.Navigate(new Uri("/Views/ReadContactList.xaml", UriKind.Relative));
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập họ tên hoặc số điện thoại!");
            }
        }
    }
}