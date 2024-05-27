using Newtonsoft.Json;
using RestSharp;

namespace USOSmobile.Models
{
    internal class User
    {
        private int _Id;
        private string _LastName;
        private string _FirstName;

        //##########################################################

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string last_name 
        { 
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string first_name
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        //##########################################################

        public User()
        {
            _Id = 0;
            _LastName = string.Empty;
            _FirstName = string.Empty;
        }

        //##########################################################

        static public bool deserializeUserData(RestResponse data, ref User user)
        {
            if (data == null)
                return false;

            user = JsonConvert.DeserializeObject<User>(data.Content);
            return true;
        }

        //##########################################################
        public string Show()
        {
            return $"ID: {_Id}\nLast Name: {_LastName}\nFirst Name: {_FirstName}";
        }
    }
}
