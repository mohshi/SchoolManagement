using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SchoolManagement.Services
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SchoolDbContext _db;

        public SchoolRepository(SchoolDbContext db)
        {
            _db = db;
        }

        // Students
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _db.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _db.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            // Remove any null entries in the Enrollments collection to avoid EF throwing
            // ArgumentNullException when processing the entity graph.
            if (student.Enrollments != null)
            {
                student.Enrollments = student.Enrollments.Where(e => e != null).ToList();
            }

            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            _db.Students.Update(student);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var existing = await _db.Students.FindAsync(id);
            if (existing == null) return false;
            _db.Students.Remove(existing);
            return await _db.SaveChangesAsync() > 0;
        }

        // Courses
        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _db.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course?> GetCourseAsync(int id)
        {
            return await _db.Courses.Include(c => c.Enrollments).ThenInclude(e => e.Student).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            _db.Courses.Add(course);
            await _db.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            _db.Courses.Update(course);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var existing = await _db.Courses.FindAsync(id);
            if (existing == null) return false;
            _db.Courses.Remove(existing);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
