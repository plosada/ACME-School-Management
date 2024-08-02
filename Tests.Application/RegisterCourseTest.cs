using Application.Course.RegisterCourse;
using Application.Students;
using Application.Students.RegisterStudent;
using Domain.Courses;
using Domain.Students;
using FluentAssertions;
using Moq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading;

namespace Tests.Application
{
    public class RegisterCourseTest
    {
        private readonly Mock<ICourseRepository> _courseRepository;

        private readonly RegisterCourseHandler _handler;

        private readonly Course course;
        private readonly RegisterCourseResponse registerCourseResponse;
        private RegisterCourseCommand request;
        private readonly CancellationToken _cancellationToken;


        private readonly RegisterCourseValidation _registerCourseValidator;

        public RegisterCourseTest()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _handler = new RegisterCourseHandler(_courseRepository.Object);

            _registerCourseValidator = new RegisterCourseValidation();

            course = new Course {  Id = 1, Name = "Course", Start = DateTime.Parse("2024-01-01"), End = DateTime.Parse("2024-12-31") };

            registerCourseResponse = new RegisterCourseResponse { Id = 1, Name = "Course", Start = DateTime.Parse("2024-01-01"), End = DateTime.Parse("2024-12-31") };

            request = new RegisterCourseCommand { Name = "Course", RegistrationFee = 1, Start = DateTime.Parse("2024-01-01"), End = DateTime.Parse("2024-12-31") };
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handle_OK()
        {
            // Se configura el Mock
            _courseRepository.Setup(_ => _.CreateCourseAsync(It.IsAny<Course>())).ReturnsAsync(course);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(true);
            result.Id.Should().Be(registerCourseResponse.Id);
            result.Name.Should().Be(registerCourseResponse.Name);
            result.RegistrationFee.Should().Be(registerCourseResponse.RegistrationFee);
            result.Start.Should().Be(registerCourseResponse.Start);
            result.End.Should().Be(registerCourseResponse.End);
        }

        [Fact]
        public void Handle_NameEmpty()
        {
            request = new RegisterCourseCommand { Name = "", RegistrationFee = 1, Start = DateTime.Parse("2024-01-01"), End = DateTime.Parse("2024-12-31") };

            // Act
            var result = _registerCourseValidator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_InvalidStartDate()
        {
            request = new RegisterCourseCommand { Name = "Course", RegistrationFee = 1, Start = DateTime.Now.AddDays(-1), End = DateTime.Parse("2024-12-31") };

            // Act
            var result = _registerCourseValidator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_ThrowException()
        {
            // Se configura el Mock
            _courseRepository.Setup(_ => _.CreateCourseAsync(It.IsAny<Course>())).Throws(new Exception());

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }
    }
}