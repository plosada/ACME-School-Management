using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Student.RegisterStudent;
using Domain.Students;
using MediatR;

namespace Application.Students.RegisterStudent
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, RegisterStudentCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;

        public RegisterStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<RegisterStudentCommandResponse> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            var newStudent = new Domain.Students.Student { Name = request.Name, Age = request.Age };

            try
            {
                var student = await _studentRepository.CreateStudentAsync(newStudent);

                return new RegisterStudentCommandResponse { Id = student.Id, Name = student.Name, Age = student.Age, Successful = true };
            }
            catch (Exception ex)
            {
                return new RegisterStudentCommandResponse { Successful = false };
            }            
        }
    }
}
