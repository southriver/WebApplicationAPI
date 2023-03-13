using System;
using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Source.Db
{
    public class CourseAccess
    {
        private static List<Course> sampleCourses = new List<Course>(new Course[]
        {
            new Course() { Id=1, DeptCode="SE", CourseCode = "1010", CourseDesc = "Introduction to Programming" },
            new Course() { Id=2, DeptCode="CE", CourseCode = "2010", CourseDesc = "Algorithms" },
            new Course() { Id=3, DeptCode="CE", CourseCode = "3011", CourseDesc = "Microprocessors" }
        });

        public List<Course> getCourses() { 
            return sampleCourses; 
        }

        public Course getCourseById(int id)
        {
            return sampleCourses.Find(c => c.Id == id);
        }

        public void insertCourse(Course course)
        {
            validateCourse(course);
            sampleCourses.Add(course);
        }

        public int deleteCourse(int id)
        {
            return sampleCourses.RemoveAll(c => c.Id == id);
        }

        public void updateCourse(Course course)
        {
            validateCourse(course);
            Course courseEx = getCourseById(course.Id);
            if (courseEx != null)
            {
                courseEx.DeptCode = course.DeptCode;
                courseEx.CourseCode = course.CourseCode;
                courseEx.CourseDesc = course.CourseDesc;
            }
        }

        public List<Course> searchCourse(Course course)
        {
            List<Course> ret  = getCourses().FindAll(c => c.DeptCode.StartsWith(course.DeptCode));

            return ret;
        }


        private void validateCourse(Course course)
        {
            if (course.CourseCode == null)
            {
                throw new Exception("Course code cannot be empty");
            }
    
        }

    }
}
