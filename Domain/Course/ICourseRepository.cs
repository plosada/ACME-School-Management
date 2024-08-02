using Domain.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Courses
{
    public interface ICourseRepository
    {
        Task<Course> CreateCourseAsync(Course course);
        Task<Course> GetCourseAsync(int courseId);
        Task<Course> UpdateCourseAsync(Course course);
        Task<List<Course>> GetAllCoursesAsync();
    }
}
