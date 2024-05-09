using AndroidX.Annotations;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USOSmobile.Models
{
    internal class Type_description
    {
        string pl;
        string en;
    }
    internal class Course_grade
    {
        string id;
        string type_id;
        Type_description type_description;

        public Dictionary<(dynamic, dynamic), Course_grade> deserializeCourseGradeData(RestResponse data)
        {
            dynamic cgData = JsonConvert.DeserializeObject<dynamic>(data.Content);

            if (cgData == null)
                return null;

            Course_grade CG = new Course_grade();

            foreach(dynamic item in cgData) 
            {
                
            }


            return CG;

        }
    }
}
