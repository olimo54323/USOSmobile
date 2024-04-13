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
        public string course_unit_id{ get; set; } 
        public int group_number { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public Name? name { get; set; } = new Name();
        public TimeTable() 
        {
            group_number = -1;
        }
    }

    internal class TimeTables
    {
        IList<TimeTable> tables = new List<TimeTable>();
        public TimeTables DeserializeTimeTableData(RestResponse data, string courseUnitId, int groupNumber)
        {
            dynamic ttData = JsonConvert.DeserializeObject<dynamic>(data.Content);

            if (ttData == null)
                return null;

            TimeTables ttList = new TimeTables();

            foreach (dynamic time in ttData)
            {
                TimeTable tt = new TimeTable { course_unit_id = courseUnitId, group_number = groupNumber };
                tt.start_date = DateTime.ParseExact(time.start_date.Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                tt.end_date = DateTime.ParseExact(time.end_date.Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                tt.name = new Name { pl = time.name.pl, en = time.name.en };
                ttList.tables.Add(tt);
            }
            return ttList;
        }

        public TimeTable GetTimeTable(string courseUnitId, int groupNumber)
        {
            foreach (TimeTable timetable in Helpers.timeTables.tables)
                if(timetable.course_unit_id == courseUnitId && timetable.group_number == groupNumber)
                    return timetable;

            return new TimeTable();
        }
    }
}
