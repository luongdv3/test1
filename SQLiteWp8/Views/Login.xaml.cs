using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLiteWp8.Libs.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLiteWp8.ViewModel;
using SQLiteWp8.Model;
using System.Diagnostics;
using System.Collections.ObjectModel;


namespace SQLiteWp8.Views
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
            this.Loaded += Login_Loaded;
            App.isLoaded = false;
        }

        void Login_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordBx.Password = passwordBx.Password.Trim();

            if (passwordBx.Password.Trim().Length > 0)
            {
                passwordBx.Background.Opacity = 1;
                txtPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                passwordBx.Background.Opacity = 0;
                txtPassword.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<LoginTable> db = new ObservableCollection<LoginTable>();
            ReadAllLoginItem dbLogin = new ReadAllLoginItem();
            //db = dbLogin.GetAllLoginItems();//Get all DB Login

            string user = txtUsername.Text.Trim();
            string pass = passwordBx.Password.Trim();

            LoginTable loginObj = new LoginTable();
            loginObj = dbLogin.GetExistUserLogin(user, pass);

            bool isLoginSuccess = this.validateLoginUser(user, pass);

            if (isLoginSuccess)
            {
                NavigationService.Navigate(new Uri("/Views/ReadContactList.xaml", UriKind.Relative));
            }

            /*if (loginObj != null)
            {
                NavigationService.Navigate(new Uri("/Views/ReadContactList.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }*/
            
        }
        private bool validateLoginUser(string user, string pass)
        {
            ReadAllLoginItem dbLogin = new ReadAllLoginItem();
            LoginTable loginObj = new LoginTable();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập hoặc mật khẩu");
                return false;
            }

            loginObj = dbLogin.GetExistUserLogin(user, pass);

            if (loginObj == null)
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu, vui lòng thử lại!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    
     
}