using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Model.Dto;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers.v2
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
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
            datas.ForEach(data => ret.Add(new StudentDto { Id=-1,FullName="In Progress"}));
            return ret;
        }

    }
}
