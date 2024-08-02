using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Course.ListCourses
{
    public class ListCoursesQuery : IRequest<ListCoursesQueryResponse>
    {
        public required DateTime DateFrom { get; set; }
        public required DateTime DateTo { get; set; }
    }
}
