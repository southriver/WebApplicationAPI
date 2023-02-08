using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Model.Dto;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class StudentController : ControllerBase
    {
        private UniversityContext _context;
        private StudentService _studentService;

        public StudentController(UniversityContext context)
        {
            _context = context;
            _studentService = new StudentService(_context);
        }

        // GET: /<StudentController>
        [HttpGet]
        public List<StudentDto> Get()
        {
            List<Student> datas = _studentService.getStudents();
            List<StudentDto> ret = new List<StudentDto>();
            datas.ForEach(data => ret.Add(createStudentDto(data)));
            return ret;
        }

        [HttpPost]
        [HttpPost("GetAllWithPaging")]
        public List<StudentDto> GetWithPaging(QueryWithPagingDto query)
        {
            List<Student> datas = _studentService.getStudents();
            List<Student> datasFiltered = datas.Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            List<StudentDto> ret = new List<StudentDto>();
            datasFiltered.ForEach(data => ret.Add(createStudentDto(data)));
            return ret;
        }

        [HttpPost]
        [HttpPost("GetAllWithCaching")]
        public List<StudentDto> GetAllWithCaching(QueryWithPagingDto query)
        {
            List<Student> datas = _studentService.getStudentsWithCache();
            List<Student> datasFiltered = datas.Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            List<StudentDto> ret = new List<StudentDto>();
            datasFiltered.ForEach(data => ret.Add(createStudentDto(data)));
            return ret;
        }

        // GET /<StudentController>/5
        [HttpGet("{id}")]
        public StudentDto Get(int id)
        {
            Student data = _studentService.getStudentById(id);
            return createStudentDto(data);
        }

        [HttpPost]
        [HttpPost("InsertStudent")]
        public StudentResultDto InsertStudent([FromBody] StudentDto student)
        {

            StudentResultDto ret = new StudentResultDto();
            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }
            try
            {
                int cnt = _studentService.insertStudent(createStudent(student));
                if (cnt > 0)
                {
                    ret.Id = _studentService.getStudentByTCKimlik(student.TCKimlik).Id;
                }
            }
            catch(Exception ex)
            {
                ret.Status = "FAILURE";
                ret.Message = ex.Message;
            }
            return ret;
        }

        [HttpPost("CalculateGPA")]
        [HttpPost]
        [Authorize]
        public StudentGPAResultDto CalculateGPA([FromBody] StudentGPAQueryDto gpaQuery)
        {
            StudentGPAResultDto ret = new StudentGPAResultDto();
            try
            {
                ret.GPA  = _studentService.calculateGPA(gpaQuery.TCKimlik);
            }
            catch (Exception ex)
            {
                ret.Status = "FAILURE";
                ret.Message = ex.Message;
            }
            return ret;
        }

        private StudentDto createStudentDto(Student student)
        {
            StudentDto ret = new StudentDto()
            {
                Id = student.Id,
                StudentId = student.StudentId,
                TCKimlik = student.TCKimlik,
                FullName = student.FullName,
                StudentStatus = student.StudentStatus
            };
            return ret;
        }

        private Student createStudent(StudentDto studentDto)
        {
            Student ret = new Student()
            {
                StudentId = studentDto.StudentId,
                TCKimlik = studentDto.TCKimlik,
                FullName = studentDto.FullName,
                StudentStatus = studentDto.StudentStatus
            };
            return ret;
        }
    }
}
