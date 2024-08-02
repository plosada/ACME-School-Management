using Application.Student.RegisterStudent;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.RegisterStudent
{
    public class RegisterStudentCommand : IRequest<RegisterStudentCommandResponse>
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
    }
}
