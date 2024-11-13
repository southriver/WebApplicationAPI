using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebApplication1.Model;
using WebApplication1.Model.Dto;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {

        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        //gets all courses
        [HttpGet]
        public IEnumerable<CourseDto> Get()
        {
            /*
             * SELECT * FROM course
             * 
             */
            List<Course> datas = _courseService.getCourses();
            List<CourseDto> ret = new List<CourseDto> ();
            datas.ForEach(data => ret.Add(createDto(data)));
            return ret;
        }

        //gets a course with given id
        [HttpGet("{id}")]
        public CourseDto Get(int id)
        {
            Course data = _courseService.getCourseById(id);
            return createDto(data);
        }

        // inserts a course 
        [HttpPost]
        [Authorize]
        public CourseInsertResultDto Post([FromBody] CourseDto courseDto)
        {

            CourseInsertResultDto ret = new CourseInsertResultDto();
            if (!ModelState.IsValid)
            {
                ret.statusMessage = "Model is not valid";
                return ret;
            }
            try
            {
                ret.Id =  _courseService.insertCourse(createCourse(courseDto));
            }
            catch (Exception ex)
            {
                ret.statusMessage = ex.Message;
                return ret;
            }
            return ret;
        }

        [HttpPost("SearchCourse")]
        public List<CourseDto> SearchCourse([FromBody] CourseDto courseSearchDto)
        {
            Course searchDto = createCourse(courseSearchDto);
            List<Course> courses = _courseService.searchCourse(searchDto);

            List<CourseDto> ret = new List<CourseDto>();
            courses.ForEach(data => ret.Add(createDto(data)));
            return ret;
        }

        //// updates a course with given id
        //[HttpPut]
        //public IActionResult Put([FromBody] CourseDto courseDto)
        //{
        //    if (courseDto.Id == 0)
        //    {
        //        return BadRequest("Cant update a course with no id");
        //    }
        //    if (courseDto != null)
        //    {
        //        using (var scope = new TransactionScope())
        //        {
        //            try
        //            {
        //                _courseService.updateCourse(createCourse(courseDto));
        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest(ex);
        //            }
        //            scope.Complete();
        //            return new OkResult();
        //        }
        //    }
        //    return new NoContentResult();
        //}

        // courseCode  = S  => 


        private CourseDto createDto(Course course)
        {
            CourseDto dto = new CourseDto()
            {
                Id = course.Id,
                DeptCode = course.DeptCode,
                CourseCode = course.CourseCode,
                CourseDesc = course.CourseDesc
            };
            return dto;
        }

        private Course createCourse(CourseDto dto)
        {
            Course course = new Course()
            {
                Id = dto.Id,
                DeptCode = dto.DeptCode,
                CourseCode = dto.CourseCode,
                CourseDesc = dto.CourseDesc
            };
            return course;
        }

    }
}
