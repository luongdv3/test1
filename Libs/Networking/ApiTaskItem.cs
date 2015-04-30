using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPNetwork.Libs.Networking;
namespace WPNetwork.Libs.Networking
{
    //class luu tru 1 task request len server
   public class ApiTaskItem
    {
       private ApiHelper.RequestType  type;
       private string url;
       private string parameter;
       private Action<string> onSuccessful;
       private Action<HttpStatusCode, string> onError;
       public ApiHelper.RequestType Type
       {
           set 
           { type = value; }
           get
           { return type; }
       }
       public string Url
       {
           set
           { url = value; }
           get
           { return url; }
       }
       public string Parameter
       {
           set
           { parameter = value; }
           get
           { return parameter; }
       }

       public Action< string> OnSuccesful
       {
           set { onSuccessful = value; }
           get { return onSuccessful; }
       }
       public Action<HttpStatusCode, string> OnError
       {
           set { onError = value; }
           get { return onError; }
       }

    }
}
