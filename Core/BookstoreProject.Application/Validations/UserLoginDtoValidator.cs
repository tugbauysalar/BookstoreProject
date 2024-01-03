using BookstoreProject.Application.DTOs;
using FluentValidation;

namespace BookstoreProject.Application.Validations;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email).NotNull().WithMessage("E-posta alanı boş bırakılamaz!")
            .NotEmpty().WithMessage("E-posta alanı boş bırakılamaz!")
            .EmailAddress().WithMessage("Geçerli bir email adresi girin!");
        RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre alanı boş bırakılamaz!")
            .NotNull().WithMessage("Şifre alanı boş bırakılamaz!");
    }
}