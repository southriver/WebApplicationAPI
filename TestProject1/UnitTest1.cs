using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework.Internal;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Source.Svc;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodCourse()
        {
            CourseService service = new CourseService();
            Assert.IsTrue(service.getCourses().Count > 0);
        }

        [TestMethod]
        public async Task TestMethodStudent()
        {
            UniversityContext mockContext = await GetDatabaseContext();
            StudentService studentService = new StudentService(mockContext, null);
            Assert.IsTrue(studentService.getStudents().Count() == 10);
        }

        private async Task<UniversityContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<UniversityContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new UniversityContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Students.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Students.Add(new Student()
                    {
                        Id = i,
                        TCKimlik = "1212121212" + i,
                        StudentId = "1212121212" + i,
                        FullName = "SYSTEM " + i,
                        StudentStatus = "ACTIVE"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}