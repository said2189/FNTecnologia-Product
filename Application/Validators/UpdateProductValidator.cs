using Application.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(c => c.Product.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio");

            RuleFor(c => c.Product.Categoria)
                .NotEmpty().WithMessage("La categoría es obligatoria");

            RuleFor(c => c.Product.Precio)
                .NotNull().WithMessage("El precio es obligatorio")
                .GreaterThanOrEqualTo(0).WithMessage("El precio no puede ser negativo");

            RuleFor(c => c.Product.Estado)
                .NotNull().WithMessage("El estado es obligatorio")
                .Must(e => e == true || e == false)
                .WithMessage("El estado debe ser booleano");
        }
    }

}
