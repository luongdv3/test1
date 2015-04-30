using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using SQLiteWp8.Libs;

namespace SQLiteWp8.Libs
{
   public static class ProgressHub
    {
           static Panel currentPanel;
           static bool isShow;
           public static bool ISSHOW 
           {
               get { return isShow; }
           }
           public static void CloseProgress()
           {
               if (currentPanel != null)
               {
                   currentPanel.Children.RemoveAt(currentPanel.Children.Count - 1);
                   currentPanel = null;
                   isShow = false;
               }
               Debug.WriteLine("ISSHOW: 2" + isShow);
           }
           
           public static void ShowProgress(Panel currentPage)
           {
               Debug.WriteLine("ISSHOW: "+isShow);
                if (isShow) return;
               double widScreen = Application.Current.RootVisual.RenderSize.Width;
               double heiScreen = Application.Current.RootVisual.RenderSize.Height;
                ProgressBar progress = new ProgressBar();
                progress.IsIndeterminate = true;
                progress.Width = widScreen;
                progress.Height = 20;
                progress.Visibility = Visibility.Visible;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Loading";
                textBlock.Width = widScreen;
                //textBlock.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
                textBlock.Foreground = new SolidColorBrush(Colors.White);
                textBlock.Height = 20;
                textBlock.Margin = new Thickness((widScreen - 50) / 2, (heiScreen - textBlock.Height) / 2 + 10, 0, 0);
               //Show locked root layout with alpha color background
               Canvas canvasForgotPass = new Canvas();
               SolidColorBrush color = new SolidColorBrush();
               color.Color = Color.FromArgb(120, 0, 0, 0);
               canvasForgotPass.Background = color;
               canvasForgotPass.Margin = new Thickness(0, 0, 0, 0);
               canvasForgotPass.Width = widScreen;
               canvasForgotPass.Height = heiScreen;

               //set popup can hide when touch outside
               
               //set center in screen
               progress.Margin = new Thickness((widScreen - progress.Width) / 2, (heiScreen - progress.Height) / 2, 0, 0);
               canvasForgotPass.Children.Add(progress);
               canvasForgotPass.Children.Add(textBlock);
               currentPage.Children.Add(canvasForgotPass);
               isShow = true;
               currentPanel = currentPage;
           }

          
       
    }
}
