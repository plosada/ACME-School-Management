using Application.Student.RegisterStudent;
using Application.Students.RegisterStudent;
using Domain.Students;
using FluentAssertions;
using Moq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading;

namespace Tests.Application
{
    public class RegisterStudentTest
    {
        private readonly Mock<IStudentRepository> _studentRepository;

        private readonly RegisterStudentHandler _handler;

        private readonly Student student;
        private readonly RegisterStudentResponse studentResponse;
        private RegisterStudentCommand request;
        private readonly CancellationToken _cancellationToken;


        private readonly RegisterStudentValidation _StudentValidator;

        public RegisterStudentTest()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _handler = new RegisterStudentHandler(_studentRepository.Object);

            _StudentValidator = new RegisterStudentValidation();

            student = new Student {  Id = 1 , Age = 18 , Name = "Pablo" };
            studentResponse = new RegisterStudentResponse { Id = 1, Age = 18, Name = "Pablo" };
            request = new RegisterStudentCommand { Name = "Pablo", Age = 18 };
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handle_OK()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.CreateStudentAsync(It.IsAny<Student>())).ReturnsAsync(student);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(true);
            result.Id.Should().Be(studentResponse.Id);
            result.Name.Should().Be(studentResponse.Name);
            result.Age.Should().Be(studentResponse.Age);
        }

        [Fact]
        public void Handle_IsNotAdult()
        {
            request = new RegisterStudentCommand { Name = "Pablo", Age = 5 };

            // Se configura el Mock
            _studentRepository.Setup(_ => _.CreateStudentAsync(It.IsAny<Student>())).ReturnsAsync(student);

            // Act
            var result = _StudentValidator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_ThrowException()
        {
            // Se configura el Mock
            _studentRepository.Setup(_ => _.CreateStudentAsync(It.IsAny<Student>())).Throws(new Exception());

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }
    }
}