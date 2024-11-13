using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using WebApplication1.Model;
using WebApplication1.Source.Db;
using System.Threading.Tasks;

namespace WebApplication1.Source.Svc
{
    public interface IStudentService
    {
        public List<Student> getStudents();
        public List<Student> getStudentsWithCache();
        public Student getStudentByTCKimlik(string tckimlik);

        public Student getStudentById(int id);

        public int insertStudent(Student student);

        public double calculateGPA(string tcKimlik);

        public Task<double>  calculateGPAAsync(string tcKimlik);


    }
}
