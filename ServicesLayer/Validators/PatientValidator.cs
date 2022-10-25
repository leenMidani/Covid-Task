using COVID.Resources;
using FluentValidation;
using System.Text.RegularExpressions;

namespace COVID.Validators
{
    public class PatientValidator:AbstractValidator<PatientDto>
    {
        public PatientValidator()
        {
            RuleFor(u => u.PatientName).NotEmpty().NotNull().WithMessage("Required");
            RuleFor(u => u.Nationality).NotEmpty().NotNull().WithMessage("Required");
            RuleFor(u => u.Gender).NotEmpty().NotNull().WithMessage("Required");
            RuleFor(u => u.DateOfBirth).NotEmpty().NotNull()
                .Must(a=>(DateTime.Now-a).Days>= 6400).WithMessage("Must Be Above 18");
            RuleFor(u=>u.PhoneNumber).NotNull().WithMessage("Phone Number is required.")
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .Matches(new Regex(@"^[0-9]{10}"))
                .WithMessage("PhoneNumber not valid");
            RuleFor(u => u.EmiratesID).Matches(new Regex(@"^784-?[0-9]{4}-?[0-9]{7}-?[0-9]{1}$"))
                .WithMessage("Enter A Valid ID");
        }

    }
}
