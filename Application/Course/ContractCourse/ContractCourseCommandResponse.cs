using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Course.ContractCourse
{
    public class ContractCourseCommandResponse
    {
        public bool Successful { get; set; }
        public Domain.Courses.Course? Course { get; set; }
    }
}
