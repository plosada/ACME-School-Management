using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Student.RegisterStudent
{
    public class RegisterStudentCommandResponse
    {
        public bool Successful { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
