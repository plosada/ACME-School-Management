using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Course.RegisterCourse
{
    public class RegisterCourseResponse
    {
        public bool Successful { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public double RegistrationFee { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
