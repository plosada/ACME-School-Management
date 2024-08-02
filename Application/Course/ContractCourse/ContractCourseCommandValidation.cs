using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Application.Course.ContractCourse
{
    public class ContractCourseCommandValidation : AbstractValidator<ContractCourseCommand>
    {
        public ContractCourseCommandValidation()
        {
            RuleFor(x => x.CourseId)
                .Cascade(CascadeMode.Stop)
                .Empty()
                .WithMessage("CourseId is required");

            RuleFor(x => x.StudentId)
                .Cascade(CascadeMode.Stop)
                .Empty()
                .WithMessage("CourseId is required");
        }
    }
}