using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace teruzuki.Twitter
{
    public static class Users
    {

        public static List<User> Lookup(ICollection<long> user_id)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("user_id", String.Join("%2C", user_id.ToArray().Select(i => i.ToString()).ToArray()));
            return Client.GetUsers("users/lookup", parameters);
        }

        public static List<User> Lookup(ICollection<string> screen_name)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("screen_name", String.Join("%2C", screen_name.ToArray()));
            return Client.GetUsers("users/lookup", parameters);
        }

        public static User Show(long user_id)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("user_id", user_id.ToString());
            return Client.GetUser("users/show", parameters);
        }

        public static User Show(string screen_name)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("screen_name", screen_name);
            return Client.GetUser("users/show", parameters);
        }

        public static List<User> Search(string query, int page = 0, int count = 0)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("q", query);
            if (page > 0)
                parameters.Add("page", page.ToString());
            if (count > 0)
                parameters.Add("count", count.ToString());

            return Client.GetUsers("users/search", parameters);
        }


        /*
         * Not yet implemeted
         * 
         * GET users/profile_banner - Do we need this?
         * GET users/suggestions/:slug - Do we need this?
         * GET users/suggestions - Do we need this?
         * GET users/suggestions/:slug/ - Do we need this?
         * 
         * POST mutes/users/create - require POST function
         * POST mutes/users/destroy - require POST function
         * POST users/report_spam - require POST function
         */

        /*
         * Probably won't implement
         * 
         */
    }
}
