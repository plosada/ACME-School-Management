using Application.Course.ContractCourse;
using Application.ServicesContracts;
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
    public class ContractCourseTest
    {
        private readonly Mock<ICourseRepository> _courseRepository;
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<IPaymentService> _paymentService;

        private readonly ContractCourseHandler _handler;

        private readonly Student student;
        private readonly Course course;

        private ContractCourseCommand request;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        private readonly ContractCourseValidation _contractCourseValidator;

        public ContractCourseTest()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _studentRepository = new Mock<IStudentRepository>();
            _paymentService = new Mock<IPaymentService>();

            _handler = new ContractCourseHandler(_courseRepository.Object, _studentRepository.Object, _paymentService.Object);

            _contractCourseValidator = new ContractCourseValidation();

            course = new Course {  Id = 1, Name = "Course", Start = DateTime.Parse("2024-01-01"), End = DateTime.Parse("2024-12-31") };
            student = new Student { Id = 1, Age = 18, Name = "Pablo" };

            request = new ContractCourseCommand { CourseId = 1, StudentId = 1 };
        }

        [Fact]
        public async Task Handle_OK()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.GetStudentAsync(It.IsAny<int>())).ReturnsAsync(student);
            _courseRepository.Setup(_ => _.GetCourseAsync(It.IsAny<int>())).ReturnsAsync(course);
            _courseRepository.Setup(_ => _.UpdateCourseAsync(It.IsAny<Course>())).ReturnsAsync(course);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(true);
            result.Course.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_CourseNotExists()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.GetStudentAsync(It.IsAny<int>())).ReturnsAsync(student);
            _courseRepository.Setup(_ => _.GetCourseAsync(It.IsAny<int>())).ReturnsAsync((Course)null);
            _courseRepository.Setup(_ => _.UpdateCourseAsync(It.IsAny<Course>())).ReturnsAsync(course);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }

        [Fact]
        public async Task Handle_StudentNotExists()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.GetStudentAsync(It.IsAny<int>())).ReturnsAsync((Student)null);
            _courseRepository.Setup(_ => _.GetCourseAsync(It.IsAny<int>())).ReturnsAsync(course);
            _courseRepository.Setup(_ => _.UpdateCourseAsync(It.IsAny<Course>())).ReturnsAsync(course);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }

        [Fact]
        public void Handle_CourseIdRequired()
        {
            request = new ContractCourseCommand { StudentId = 1 };

            // Act
            var result = _contractCourseValidator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_StudentIdRequired()
        {
            request = new ContractCourseCommand { CourseId = 1 };

            // Act
            var result = _contractCourseValidator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_GetStudent_ThrowException()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.GetStudentAsync(It.IsAny<int>())).Throws(new Exception());
            _courseRepository.Setup(_ => _.GetCourseAsync(It.IsAny<int>())).ReturnsAsync(course);
            _courseRepository.Setup(_ => _.UpdateCourseAsync(It.IsAny<Course>())).ReturnsAsync(course);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }

        [Fact]
        public async Task Handle_GetCourse_ThrowException()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.GetStudentAsync(It.IsAny<int>())).ReturnsAsync(student);
            _courseRepository.Setup(_ => _.GetCourseAsync(It.IsAny<int>())).Throws(new Exception());
            _courseRepository.Setup(_ => _.UpdateCourseAsync(It.IsAny<Course>())).ReturnsAsync(course);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }

        [Fact]
        public async Task Handle_UpdateCourse_ThrowException()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.GetStudentAsync(It.IsAny<int>())).ReturnsAsync(student);
            _courseRepository.Setup(_ => _.GetCourseAsync(It.IsAny<int>())).ReturnsAsync(course);
            _courseRepository.Setup(_ => _.UpdateCourseAsync(It.IsAny<Course>())).Throws(new Exception());

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }
    }
}