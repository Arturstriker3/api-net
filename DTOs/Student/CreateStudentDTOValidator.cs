using FluentValidation;

namespace API_.NET_CRUD_Minimal_E.F_ORM.DTOs.Student.Validation
{
    public class CreateStudentDTOValidator : AbstractValidator<CreateStudentDTO>
    {
        public CreateStudentDTOValidator()
        {
            var minLength = 2;
            var maxLength = 50;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .Length(minLength, maxLength).WithMessage($"FirstName must be between {minLength} and {maxLength} characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .Length(minLength, maxLength).WithMessage($"LastName must be between {minLength} and {maxLength} characters.");
        }
    }
}
