using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS23Handledning19JanWPFDatakoppling.Items
{
    public class CourseGrade
    {
        public string Grade {  get; set; }
        public Course Course { get; set; }

        public CourseGrade(string grade, Course course)
        {
            Grade = grade;
            Course = course;
        }
    }
}
