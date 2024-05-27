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
    internal class TimeTable
    {
        private DateTime _StartDate;
        private DateTime _EndDate;
        private MultiLangItem _Name;

        //##########################################################

        public DateTime start_date 
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime end_date 
        {
            get { return _EndDate; }
            set { _EndDate = value; } 
        }
        public MultiLangItem name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //##########################################################

        public TimeTable()
        {
            _StartDate = DateTime.MinValue;
            _EndDate = DateTime.MinValue;
            _Name = new MultiLangItem();
        }

        //##########################################################
        public string Show()
        {
            return $"Start Date: {_StartDate.ToString("HH:mm")}\nEnd Date: {_EndDate.ToString("HH:mm")}\nName: {_Name.pl}";
        }
    }

    internal class TimeTables
    {
        private List<TimeTable> _Tables;
        public List<TimeTable> tables
        { 
            get { return _Tables; } 
            set { _Tables = value; }
        }

        //##########################################################

        public TimeTables()
        {
            _Tables = new List<TimeTable>();
        }

        //##########################################################

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
                tt.name = new MultiLangItem
                {
                    pl = time.name.pl,
                    en = time.name.en
                };
                TTs.tables.Add(tt);
            }
            return TTs;
        }

        //##########################################################
        public string Show()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var table in tables)
            {
                sb.AppendLine(table.Show());
            }

            return sb.ToString();
        }
    }
}
