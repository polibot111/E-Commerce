using E_Commerce.Application.Features.Commands.AppUser.UpdatePassword;
using E_Commerce.Application.ViewModels.Example;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Validators.AppUser
{
    public class UpdatePasswordValidation : AbstractValidator<UpdatePasswordCommandRequest>
    {
        public UpdatePasswordValidation()
        {
            RuleFor(x => x.Password).NotEqual(x=> x.PasswordConfirm).WithMessage("Şifreniz doğrumala şifresi ile aynı değil.");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifreniz 6 karakterden kısa olamaz.");
        }
    }
}
