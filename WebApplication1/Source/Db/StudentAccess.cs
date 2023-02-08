using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Model;

namespace WebApplication1.Source.Db
{
    public class StudentAccess
    {
        private UniversityContext _context;

        public StudentAccess(UniversityContext context)
        {
            _context = context;
        }

        public Student getStudent(int id)
        {
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }

        public Student getStudentByTCKimlik(string tcKimlik)
        {
            return _context.Students.FirstOrDefault(s => s.TCKimlik == tcKimlik);
        }

        public int insertStudent(Student data)
        {
            validateData(data);
            _context.Students.Add(data);
            return _context.SaveChanges();

        }

        private void validateData(Student data)
        {
            if (String.IsNullOrEmpty( data.StudentStatus)) {
                throw new Exception("Student Status cannot be null");
            }
            else
            {
                if (data.StudentStatus.Equals("ACTIVE") && data.StudentStatus.Equals("INACTIVE"))
                {
                    throw new Exception("Status must be active, inactive");
                }
            }
        }

        public int deleteStudent(int id)
        {
            Student data = getStudent(id);
            if (data != null)
            {
                _context.Students.Remove(data);
                return _context.SaveChanges();
            }
            return 0;
        }
        public IEnumerable<Student> getAllStudents()
        {
            return _context.Students;
        }
    }
}
