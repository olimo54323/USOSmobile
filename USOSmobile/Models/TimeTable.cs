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
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public Name? name { get; set; }
    }

    internal class TimeTables
    {
        public List<TimeTable> tables = new List<TimeTable>();
        static public TimeTables DeserializeTimeTableData(RestResponse data)
        {
            dynamic ttData = JsonConvert.DeserializeObject<dynamic>(data.Content);

            if (ttData == null)
                return null;

            TimeTables TTs = new TimeTables();

            foreach (dynamic time in ttData)
            {
                TimeTable tt = new TimeTable();
                tt.start_date = DateTime.ParseExact(time.start_time.Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                tt.end_date = DateTime.ParseExact(time.end_time.Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                tt.name = new Name
                {
                    pl = time.name.pl,
                    en = time.name.en
                };
                TTs.tables.Add(tt);
            }
            return TTs;
        }
    }
}
