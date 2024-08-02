using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Course.ContractCourse
{
    public class ContractCourseCommand : IRequest<ContractCourseResponse>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
