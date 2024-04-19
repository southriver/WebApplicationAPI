using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public String DeptCode { get; set; }
        public String CourseCode { get; set; }
        public String CourseDesc { get; set; }
        
    }

    public class CourseInsertResultDto 
    {
        public int Id { get; set; }
        public String statusMessage { get; set; }

    }
}
