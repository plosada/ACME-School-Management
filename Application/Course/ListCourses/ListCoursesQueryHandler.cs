using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.ServicesContracts;
using Domain.Courses;
using Domain.Students;
using MediatR;

namespace Application.Course.ListCourses
{
    public class ListCoursesQueryHandler : IRequestHandler<ListCoursesQuery, ListCoursesQueryResponse>
    {
        private readonly ICourseRepository _courseRepository;

        public ListCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ListCoursesQueryResponse> Handle(ListCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courses = await _courseRepository.GetAllCoursesAsync();

                courses = courses.FindAll(c => 
                    c.Start > request.DateFrom && c.Start < request.DateTo ||
                    c.Start <= request.DateFrom && c.End >= request.DateTo ||
                    c.Start <= request.DateFrom && c.End < request.DateTo);

                return new ListCoursesQueryResponse { Courses = courses, Successful = true };
            }
            catch (Exception ex)
            {
                return new ListCoursesQueryResponse { Successful = false };
            }
        }
    }
}
