using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Application.Students.RegisterStudent
{
    public class RegisterStudentCommandValidation : AbstractValidator<RegisterStudentCommand>
    {
        public RegisterStudentCommandValidation()
        {
            RuleFor(x => x.Age)
                .Cascade(CascadeMode.Stop)
                .Must(x => x >= 18)
                .WithMessage("Must be an adult");
        }
    }
}