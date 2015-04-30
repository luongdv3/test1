using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLiteWp8.Model;
using SQLiteWp8.ViewModel;

namespace SQLiteWp8.Views
{
    public partial class Delete_UpdateContacts : PhoneApplicationPage
    {
        int Selected_ContactId = 0;
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
        Contacts currentcontact = new Contacts();
        public Delete_UpdateContacts()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Selected_ContactId = int.Parse(NavigationContext.QueryString["SelectedContactID"]);
            currentcontact = Db_Helper.ReadContact(Selected_ContactId);//Read selected DB contact

            NametxtBx.Text = currentcontact.Name.Trim();//get contact Name
            PhonetxtBx.Text = currentcontact.PhoneNumber.Trim();//get contact PhoneNumber
            EmailtxtBx.Text = currentcontact.Email.Trim();
            AddresstxtBx.Text = currentcontact.Address.Trim();

            string strGender = currentcontact.Gender.Trim();
            if(strGender == @"Nam" || strGender == "male") 
            {
                GenderMaleRd.IsChecked = true;
                GenderFemaleRd.IsChecked = false;
            }
            else
            {
                GenderMaleRd.IsChecked = false;
                GenderFemaleRd.IsChecked = true;
            }
        }
        
        private void UpdateContact_Click(object sender, RoutedEventArgs e)
        {
            currentcontact.Name = NametxtBx.Text;
            currentcontact.PhoneNumber = PhonetxtBx.Text;
            currentcontact.Email = EmailtxtBx.Text.Trim();
            currentcontact.Address = AddresstxtBx.Text.Trim();

            string strGender;
            if (GenderMaleRd.IsChecked == true)
            {
                strGender = "Nam";
            }
            else if (GenderFemaleRd.IsChecked == true)
            {
                strGender = "Nữ";
            }
            else
            {
                strGender = "Nam";
            }
            currentcontact.Gender = strGender;

            MessageBoxResult result = MessageBox.Show("Bạn có muốn cập nhật thông tin không?", "THÔNG BÁO", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                Db_Helper.UpdateContact(currentcontact);//Update selected DB contact Id
                //NavigationService.Navigate(new Uri("/Views/ReadContactList.xaml", UriKind.Relative)); 

                MessageBoxResult resultOK = MessageBox.Show("Cập nhật thành công!", "THÔNG BÁO", MessageBoxButton.OK);
                if (resultOK == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            } 
        }
        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa thông tin người này không?", "THÔNG BÁO", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                bool checkDelete = Db_Helper.DeleteContact(Selected_ContactId);//Delete selected DB contact Id.
                //NavigationService.Navigate(new Uri("/Views/ReadContactList.xaml", UriKind.Relative)); 

                if (checkDelete)
                {
                    MessageBoxResult resultOK = MessageBox.Show("Xóa thành công!", "THÔNG BÁO", MessageBoxButton.OK);
                    if (resultOK == MessageBoxResult.OK)
                    {
                        NavigationService.GoBack();
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa!", "THÔNG BÁO", MessageBoxButton.OK);
                }

                
               
            }
        }
    }
}