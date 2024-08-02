using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Application.Course.ListCourses
{
    public class ListCoursesQueryValidation : AbstractValidator<ListCoursesQuery>
    {
        public ListCoursesQueryValidation()
        {
            RuleFor(x => x.DateFrom)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("FromDate is required");

            RuleFor(x => x.DateTo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("ToDate is required");

            RuleFor(x => new { x.DateFrom, x.DateTo })
                .Cascade(CascadeMode.Stop)
                .Must(x => DateValidation(x.DateFrom, x.DateTo))
                .WithMessage("ToDate must not be less than FromDate");
        }


        public static bool DateValidation(DateTime dateFrom, DateTime dateTo)
        {            
            return dateFrom < dateTo;
        }
    }
}