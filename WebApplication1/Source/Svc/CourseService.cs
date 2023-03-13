using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Source.Db;

namespace WebApplication1.Source.Svc
{
    public class CourseService : ICourseService
    {

        public Course getCourseById(int id)
        {
            return new CourseAccess().getCourseById(id);
        }

        public List<Course> getCourses()
        {
            return new CourseAccess().getCourses();
        }

        public void insertCourse(Course course)
        {
            new CourseAccess().insertCourse(course);
        }

        public List<Course> getCourses(string dept)
        {
            List<Course> courses = new CourseAccess().getCourses();
            return courses.FindAll(c => c.DeptCode.Equals(dept));
        }

        public void updateCourse(Course course)
        {
            new CourseAccess().updateCourse(course);
        }

        public List<Course> searchCourse(Course courseSearch)
        {
            return new CourseAccess().searchCourse(courseSearch);
        }
    }
}
