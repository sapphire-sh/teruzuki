using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teruzuki.Twitter
{
    public class User
    {
        public long id { get; set; }
        public string name { get; set; }
        public string id_str { get; set; }
        public string screen_name { get; set; }
        public string profile_image_url { get; set; }
        public Tweet status { get; set; }
    }
}
