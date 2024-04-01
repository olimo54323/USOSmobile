using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class Course
    {
        public string courseUnitID { get; set; }
        public Dictionary<string, string> courseName { get; set; } //keys: pl, en
        public string termID { get; set; }
    }
}
