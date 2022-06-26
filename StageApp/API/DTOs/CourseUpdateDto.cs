using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class CourseUpdateDto
    {
        public string CourseName { get; set; }
        public bool CourseStatus { get; set; }
    }
}