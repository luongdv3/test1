using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQLiteWp8.Libs.Networking
{
   public sealed class ApiHelper
   {
       /*
        *Author Duy Hoang
        *Class ho tro viec truy xuat toi webservice 1 cach de dang
        */
    #region Define constant
       private int TIME_OUT = 60;//60 seconds
       public  enum RequestType {
           REQUEST_TYPE_GET,
           REQUEST_TYPE_POST,
           REQUEST_TYPE_DELETE,
           REQUEST_TYPE_INSERT
       }
       #endregion

    #region ContructorSingleton
       ApiHelper()
       {
         
       }
       public static ApiHelper Instance
       { 
             get{
                    return Nested.instance;
                }
       }   
    class Nested{
        static Nested()
        {
        }
        internal static readonly ApiHelper instance = new ApiHelper();
    }

       #endregion

    #region public method

    public  void ExecuteGetAsyn(string url, Action<string> onSuccesful, Action<HttpStatusCode, string> onError)
    {
            ExecuteHttpRequest(new ApiTaskItem { 
                Type = RequestType.REQUEST_TYPE_GET, 
                Url = url, 
                OnSuccesful = onSuccesful, 
                OnError = onError });
    }
    public  void  ExecutePostAsyn(string url, string parameter, Action<string> onSuccesful, Action<HttpStatusCode, string> onError)
    {
        //Add vao hang doi
      
            ExecuteHttpRequest(
                new ApiTaskItem { 
                    Type = RequestType.REQUEST_TYPE_POST,
                    Parameter = parameter, 
                    Url = url, 
                    OnSuccesful = onSuccesful, 
                    OnError = onError 
                });
           
        
    }
    #endregion

    #region private method
    private async void ExecuteHttpRequest(ApiTaskItem itemRequest)
    {
        
        //Neu la post
        var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(20);
        HttpResponseMessage response = null;
        HttpContent contentPost = null;
        if (itemRequest.Type == RequestType.REQUEST_TYPE_POST)
        {
            contentPost = new StringContent(itemRequest.Parameter, Encoding.UTF8,
            "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "2e1d58c8-7e3-474e-bc61-a654d132ec77");
        }
        try
        {
            if (itemRequest.Type == RequestType.REQUEST_TYPE_POST)
            {
                response = await httpClient.PostAsync(itemRequest.Url, contentPost);
            }
            else if (itemRequest.Type == RequestType.REQUEST_TYPE_GET)
            {
                response = await httpClient.GetAsync(itemRequest.Url);
            }
            Thread.Sleep(10000);
            HttpStatusCode code = response.StatusCode;

            //***************Thanh cong***********************
            if (code == HttpStatusCode.OK)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                itemRequest.OnSuccesful(responseString);
               
                return;
            }

            //******************That bai **********************
            itemRequest.OnError(code,response.ReasonPhrase);
           
        }
        catch (Exception e)
        {
            //Truong hop loi khong biet
            itemRequest.OnError(HttpStatusCode.Gone,e.Message);
           
        }
    }

   
    #endregion
  
   }
}
