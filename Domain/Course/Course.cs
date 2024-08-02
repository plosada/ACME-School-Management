using Domain.Students;

namespace Domain.Courses
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double RegistrationFee { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IList<Student> Students { get; }

        public Course()
        {
            Students = new List<Student>();
        }

        public bool PaymentRequired()
        {
            return RegistrationFee > 0;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
    }
}