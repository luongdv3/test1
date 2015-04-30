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
using System.Collections.ObjectModel;
using SQLiteWp8.Libs.Networking;
using Newtonsoft.Json;
using SQLiteWp8.Libs;
using SQLiteWp8.ViewModel;
using System.Net.NetworkInformation;

namespace SQLiteWp8.Views
{
    public partial class ReadContactList : PhoneApplicationPage
    {
        public ObservableCollection<Contacts> DB_ContactList { set; get; }
            

        public ReadContactList()
        {
            InitializeComponent();

            DB_ContactList = new ObservableCollection<Contacts>();
            this.DataContext = this;

            //this.Loaded += ReadContactList_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Loaded += ReadContactList_Loaded;
        }

        private void ReadContactList_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            ProgressIndicator progress = new ProgressIndicator
            {
                IsVisible = true,
                IsIndeterminate = true,
                Text = "Đang tải dữ liệu về từ Server..."
            };

            ReadAllContactsList dbcontacts = new ReadAllContactsList();
            var template = dbcontacts.GetAllContacts();
            
            //neu ko co du lieu ta lay tu webservice ve va insert vao database
            if (template.Count == 0)
            {
                //Chi load tu Service khi tu man hinh login vao
                if (App.isLoaded == false)
                {
                    App.isLoaded = true;
                }
                else
                {
                    return;
                }
                if (NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    ProgressHub.ShowProgress(this.LayoutRoot);
                    SystemTray.SetProgressIndicator(this, progress);
                    ApiHelper.Instance.ExecuteGetAsyn("http://api.androidhive.info/contacts/",
                        (response) =>
                        {
                            //parse string response thanh doi tuong cua minh
                            RootObject dataJson = JsonConvert.DeserializeObject<RootObject>(response);
                            //ta co duoc du lieu roi gio insert xuong database nhe
                            foreach (ServiceContacts item in dataJson.ListRoot)
                            {
                                Contacts contact = new Contacts(item.Name, item.Phone.Mobile, item.Email, item.Address, item.Gender);
                                Db_Helper.Insert(contact);
                                DB_ContactList.Add(contact);
                            }

                            //Doc len tu database lai

                            //this.listBoxobj.ItemsSource = DB_ContactList;
                            progress.IsVisible = false;
                            ProgressHub.CloseProgress();
                        }, (statuscode, message) =>
                        {
                            progress.IsVisible = false;
                        });
                }
                else
                {
                    MessageBox.Show("không có kết nối mạng, vui lòng kiểm tra kết nối mạng", "THÔNG BÁO", MessageBoxButton.OK);
                }

                
            }
            else // con khong thi do du lieu len
            {
                if (App.isLoaded == false)
                {
                    App.isLoaded = true;
                }
               //listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Id).ToList();//Latest contact ID can Display first
                foreach(var item in template)
                {

                    DB_ContactList.Add(item);
                }
            }

        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                Contacts listitem = listBoxobj.SelectedItem as Contacts;//Get slected listbox item contact ID
                NavigationService.Navigate(new Uri("/Views/Delete_UpdateContacts.xaml?SelectedContactID=" + listitem.Id, UriKind.Relative));
            }
        }

        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {

            ReadAllContactsList dbcontacts = new ReadAllContactsList();
            var listtemplate = dbcontacts.GetAllContacts();

            if (listtemplate.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu danh bạ, không thể xóa được!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa tất cả danh bạ không?", "THÔNG BÁO", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
                Db_Helper.DeleteAllContact();//delete all DB contacts
                DB_ContactList.Clear();//Clear collections
                //listBoxobj.ItemsSource = DB_ContactList;
            }
        }
        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/AddContact.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?","THÔNG BÁO",MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                base.OnBackKeyPress(e);
                App.isLoaded = false;
            }else
            {
                e.Cancel = true;
            }
            
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            MenuItem item = (MenuItem)sender;
            Contacts contact = item.DataContext as Contacts;
            bool checkDelete = Db_Helper.DeleteContact(contact.Id);//Delete selected DB contact Id.
            DB_ContactList.Remove(contact);
            //listBoxobj.ItemsSource = DB_ContactList;
            if (checkDelete)
            {
                MessageBox.Show("Xóa thành công!", "THÔNG BÁO", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!", "THÔNG BÁO", MessageBoxButton.OK);
            }
        }

    }
}