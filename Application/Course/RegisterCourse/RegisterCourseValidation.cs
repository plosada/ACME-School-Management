using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Application.Course.RegisterCourse
{
    public class RegisterCourseValidation : AbstractValidator<RegisterCourseCommand>
    {
        public RegisterCourseValidation()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Empty()
                .WithMessage("Name is required");

            RuleFor(x => x.Start)
                .Cascade(CascadeMode.Stop)
                .Must(c => c >= DateTime.Now)
                .WithMessage("StartDate must be greater or equal to Now");
        }
    }
}