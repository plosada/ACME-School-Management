using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Course.ListCourses
{
    public class ListCoursesQueryResponse
    {
        public bool Successful { get; set; }
        public IList<Domain.Courses.Course> Courses { get; set; }

        public ListCoursesQueryResponse()
        {
            Courses = new List<Domain.Courses.Course>();
        }
    }
}
