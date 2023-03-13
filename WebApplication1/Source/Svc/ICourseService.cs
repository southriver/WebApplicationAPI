using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Source.Svc
{
    public interface ICourseService
    {
        public List<Course> getCourses();
        public Course getCourseById(int id);
        public void insertCourse(Course course);
        public void updateCourse(Course course);
        public List<Course> searchCourse(Course course);
    }
}
