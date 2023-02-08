using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Source.Db;

namespace WebApplication1.Source.Svc
{
    public class StudentService : IStudentService
    {
        private UniversityContext _context;
        private readonly IMemoryCache _memoryCache;

        public StudentService(UniversityContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public List<Student> getStudents()
        {
            StudentAccess access = new StudentAccess(_context);
            return access.getAllStudents().ToList();
        }

        public List<Student> getStudentsWithCache()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.Students, out List<Student> datas))
            {
                datas = getStudents(); ; // Get the data from database
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _memoryCache.Set(CacheKeys.Students, datas, cacheEntryOptions);
            }
            return datas;
        }
        public Student getStudentByTCKimlik(string tckimlik)
        {
            StudentAccess access = new StudentAccess(_context);
            return access.getStudentByTCKimlik(tckimlik);
        }

        public Student getStudentById(int id)
        {
            StudentAccess access = new StudentAccess(_context);
            return access.getStudent(id);
        }

        public int insertStudent(Student student)
        {
            StudentAccess access = new StudentAccess(_context);
            return access.insertStudent(student);
        }

        public double calculateGPA(string tcKimlik)
        {
            Student student = getStudentByTCKimlik(tcKimlik);
            if (student == null)
            {
                throw new Exception("Student not found : " + tcKimlik);
            }
            var rnd = new Random();
            return rnd.NextDouble() * 4;
        }
    }
}
