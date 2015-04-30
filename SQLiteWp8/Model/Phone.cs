using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWp8.Model
{
   public  class Phone
    {
       /*  "mobile": "+91 0000000000",
                    "home": "00 000000",
                    "office": "00 000000"
        * */
       [JsonProperty("mobile")]
       public string Mobile { set; get; }

       [JsonProperty("home")]
       public string Home { set; get; }

       [JsonProperty("office")]
       public string Office { set; get; }
    }
}
