using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS23Handledning19JanWPFDatakoppling.Items
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public Course(int id, string name, int points)
        {
            Id = id;
            Name = name;
            Points = points;
        }
    }
}
