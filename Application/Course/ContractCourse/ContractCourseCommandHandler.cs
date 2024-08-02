using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ServicesContracts;
using Domain.Courses;
using Domain.Students;
using MediatR;

namespace Application.Course.ContractCourse
{
    public class ContractCourseCommandHandler : IRequestHandler<ContractCourseCommand, ContractCourseCommandResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPaymentService _paymentService;

        public ContractCourseCommandHandler(ICourseRepository courseRepository, IStudentRepository studentRepository, IPaymentService paymentService)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _paymentService = paymentService;
        }

        public async Task<ContractCourseCommandResponse> Handle(ContractCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourseAsync(request.CourseId);

                if (course is null) return new ContractCourseCommandResponse { Successful = false };

                var student = await _studentRepository.GetStudentAsync(request.StudentId);

                if (student is null) return new ContractCourseCommandResponse { Successful = false };

                if (course.PaymentRequired())
                {
                    var validPayment = await _paymentService.ExecutePayment(student.Name, course.RegistrationFee);

                    if (!validPayment) return new ContractCourseCommandResponse { Course = course, Successful = false };
                }

                course.AddStudent(student);
                await _courseRepository.UpdateCourseAsync(course);

                return new ContractCourseCommandResponse { Course = course, Successful = true };
            }
            catch (Exception ex)
            {
                return new ContractCourseCommandResponse { Successful = false };
            }
        }
    }
}
