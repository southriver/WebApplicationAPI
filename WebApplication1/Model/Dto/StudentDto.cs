using System;

namespace WebApplication1.Model.Dto
{
    public class QueryWithPagingDto
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
        public class StudentDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string TCKimlik { get; set; }
        public string FullName { get; set; }
        public string StudentStatus { get; set; }
    }

    public class StudentResultDto : APIResultDto
    {
        public int Id { get; set; }

    }

    public class StudentGPAQueryDto
    {
        public string TCKimlik { get; set; }
    }

    public class StudentGPAResultDto : APIResultDto
    {
        public double GPA { get; set; }
    }

    public class APIResultDto
    {
        public APIResultDto()
        {
            Status = "SUCCESS";
        }
        public string Status { get; set; }
        public string Message { get; set; }

    }
}
