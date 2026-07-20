using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagement.Models;

namespace SchoolManagement.Services
{
    public interface ISchoolRepository
    {
        // Students
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student?> GetStudentAsync(int id);
        Task<Student> AddStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);

        // Courses
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course?> GetCourseAsync(int id);
        Task<Course> AddCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int id);
    }
}
