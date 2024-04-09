using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace USOSmobile.Models
{
    internal class Name
    {
        public string pl { get; set; }
        public string en { get; set; }
    }
    internal class TimeTable
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public Name name { get; set; } = new Name();

        public TimeTable deserializeTimeTableData(RestResponse data)
        {
            dynamic ttData = JsonConvert.DeserializeObject<dynamic>(data.Content);
            if (ttData == null)
                return null;

            TimeTable tt = new TimeTable();
            tt.start_date = DateTime.ParseExact(ttData.start_date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            tt.end_date = DateTime.ParseExact(ttData.end_date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            tt.name = new Name { pl = ttData.name.pl, en = ttData.name.en };
            return tt;
        }
    }
}
