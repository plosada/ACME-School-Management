using Application.Course.ListCourses;
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
    public class ListCoursesTest
    {
        private readonly Mock<ICourseRepository> _courseRepository;

        private readonly ListCoursesHandler _handler;

        private readonly List<Course> courses;

        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        private readonly ListCoursesValidation _listCoursesValidator;

        public ListCoursesTest()
        {
            _courseRepository = new Mock<ICourseRepository>();

            _handler = new ListCoursesHandler(_courseRepository.Object);

            _listCoursesValidator = new ListCoursesValidation();

            courses = new List<Course> {
                new()
                {
                    Id = 1, 
                    Name = "Course1", 
                    RegistrationFee = 1,
                    Start = DateTime.Parse("2024-01-01"), 
                    End = DateTime.Parse("2024-12-31"),
                    Students = { 
                        new Student { Id = 1, Age = 18, Name = "Pablo" },
                        new Student { Id = 2, Age = 32, Name = "Pablo" }
                    },
                },
                new()
                {
                    Id = 2,
                    Name = "Course2",
                    RegistrationFee = 1,
                    Start = DateTime.Parse("2024-06-01"),
                    End = DateTime.Parse("2024-12-31"),
                    Students = {
                        new Student { Id = 1, Age = 18, Name = "Pablo" },
                        new Student { Id = 2, Age = 32, Name = "Pablo" }
                    }
                }
            };
        }

        [Fact]
        public async Task Handle_DateRange_One()
        {
            var request = new ListCoursesCommand { DateFrom = DateTime.Parse("2024-01-01"), DateTo = DateTime.Parse("2024-12-31") };

            // Se configura el Mock
            _courseRepository.Setup(_ => _.GetAllCoursesAsync()).Returns(() => Task.FromResult(courses));

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(true);
            result.Courses.Count.Should().Be(2);
        }

        [Fact]
        public async Task Handle_DateRange_Two()
        {
            var request = new ListCoursesCommand { DateFrom = DateTime.Parse("2024-01-01"), DateTo = DateTime.Parse("2024-06-01") };

            // Se configura el Mock
            _courseRepository.Setup(_ => _.GetAllCoursesAsync()).Returns(() => Task.FromResult(courses));

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(true);
            result.Courses.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_DateRange_Three()
        {
            var request = new ListCoursesCommand { DateFrom = DateTime.Parse("2023-01-01"), DateTo = DateTime.Parse("2023-12-31") };

            // Se configura el Mock
            _courseRepository.Setup(_ => _.GetAllCoursesAsync()).Returns(() => Task.FromResult(courses));

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(true);
            result.Courses.Count.Should().Be(0);
        }

        [Fact]
        public async Task Handle_ThrowException()
        {
            var request = new ListCoursesCommand { DateFrom = DateTime.Parse("2024-01-01"), DateTo = DateTime.Parse("2024-12-31") };

            // Se configura el Mock
            _courseRepository.Setup(_ => _.GetAllCoursesAsync()).Throws(new Exception());

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.Successful.Should().Be(false);
        }

        [Fact]
        public void Handle_ToDateLessThanFromDate()
        {
            var request = new ListCoursesCommand { DateFrom = DateTime.Parse("2024-12-31"), DateTo = DateTime.Parse("2024-01-01") };

            // Act
            var result = _listCoursesValidator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}