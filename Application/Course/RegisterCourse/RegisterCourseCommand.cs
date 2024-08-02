using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Course.RegisterCourse
{
    public class RegisterCourseCommand : IRequest<RegisterCourseResponse>
    {
        public required string Name { get; set; }
        public double RegistrationFee { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
    }
}
