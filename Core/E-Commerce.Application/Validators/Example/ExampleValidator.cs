using E_Commerce.Application.ViewModels.Example;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Validators.Example
{
    public class ExampleValidator : AbstractValidator<ExampleCommand>
    {
        public ExampleValidator()
        {
            RuleFor(x => x.Count).NotEmpty().LessThanOrEqualTo(0).WithMessage("olmaz arkadaşım");
        }
    }
}
