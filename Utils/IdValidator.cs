using FluentValidation;

namespace API_.NET_CRUD_Minimal_E.F_ORM.Utils
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(id => id)
                .GreaterThan(0).WithMessage("Invalid Id");
        }
    }
}
