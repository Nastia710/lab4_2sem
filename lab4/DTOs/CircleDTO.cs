using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_4.Enum;

namespace Lab_4.DTOs
{
    public class CircleDTO
    {
        public string Name { get; set; }
        public Sections Section { get; set; }
        public ManagerDTO Manager { get; set; }
        public int Fee { get; set; }
        public int LessonsPerMonth { get; set; }
        public int StudentsCount { get; set; }
    }
}
