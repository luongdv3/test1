using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWp8.Model
{
    public class ServiceContacts
    {
        /*
         * 
         * "id": "c200",
                "name": "Ravi Tamada",
                "email": "ravi@gmail.com",
                "address": "xx-xx-xxxx,x - street, x - country",
                "gender" : "male",
                "phone": {
                    "mobile": "+91 0000000000",
                    "home": "00 000000",
                    "office": "00 000000"
                }
         * */
        [JsonProperty("id")]
        public string Id { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("email")]
        public string Email { set; get; }

        [JsonProperty("address")]
        public string Address { set; get; }

        [JsonProperty("gender")]
        public string Gender { set; get; }

        [JsonProperty("phone")]
        public Phone Phone { set; get; }


        
    }
}
