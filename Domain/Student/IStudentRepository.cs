using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Students
{
    public interface IStudentRepository
    {
        Task<Student> CreateStudentAsync(Student student);
        Task<Student> GetStudentAsync(int studentId);
    }
}
