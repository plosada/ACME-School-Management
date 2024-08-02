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
    public class RegisterCourseHandler : IRequestHandler<RegisterCourseCommand, RegisterCourseResponse>
    {
        private readonly ICourseRepository _CourseRepository;

        public RegisterCourseHandler(ICourseRepository CourseRepository)
        {
            _CourseRepository = CourseRepository;
        }

        public async Task<RegisterCourseResponse> Handle(RegisterCourseCommand request, CancellationToken cancellationToken)
        {
            var newCourse = new Domain.Courses.Course { Name = request.Name, RegistrationFee = request.RegistrationFee, Start = request.Start, End = request.End };

            try
            {
                var course = await _CourseRepository.CreateCourseAsync(newCourse);

                return new RegisterCourseResponse { Id = course.Id, Name = course.Name, RegistrationFee = course.RegistrationFee, Start = course.Start, End = course.End, Successful = true };
            }
            catch (Exception ex)
            {
                return new RegisterCourseResponse { Successful = false };
            }
        }
    }
}
