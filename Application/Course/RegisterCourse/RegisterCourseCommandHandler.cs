using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Students;
using Domain.Courses;
using MediatR;

namespace Application.Course.RegisterCourse
{
    public class RegisterCourseCommandHandler : IRequestHandler<RegisterCourseCommand, RegisterCourseCommandResponse>
    {
        private readonly ICourseRepository _CourseRepository;

        public RegisterCourseCommandHandler(ICourseRepository CourseRepository)
        {
            _CourseRepository = CourseRepository;
        }

        public async Task<RegisterCourseCommandResponse> Handle(RegisterCourseCommand request, CancellationToken cancellationToken)
        {
            var newCourse = new Domain.Courses.Course { Name = request.Name, RegistrationFee = request.RegistrationFee, Start = request.Start, End = request.End };

            try
            {
                var course = await _CourseRepository.CreateCourseAsync(newCourse);

                return new RegisterCourseCommandResponse { Id = course.Id, Name = course.Name, RegistrationFee = course.RegistrationFee, Start = course.Start, End = course.End, Successful = true };
            }
            catch (Exception ex)
            {
                return new RegisterCourseCommandResponse { Successful = false };
            }
        }
    }
}
