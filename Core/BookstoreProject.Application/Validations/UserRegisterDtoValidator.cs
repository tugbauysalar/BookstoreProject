using BookstoreProject.Application.DTOs;
using FluentValidation;

namespace BookstoreProject.Application.Validations;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad-Soyad alanı boş bırakılamaz!")
            .NotNull().WithMessage("Ad-Soyad alanı boş bırakılamaz!");
        RuleFor(x=>x.UserName).NotEmpty().WithMessage("Kullanıcı adı alanı boş bırakılamaz!")
            .NotNull().WithMessage("Kullanıcı adı alanı boş bırakılamaz!");
        RuleFor(x=>x.Email).NotEmpty().WithMessage("E-posta alanı boş bırakılamaz!")
            .NotNull().WithMessage("E-posta alanı boş bırakılamaz!");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir email adresi girin!");
        RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre alanı boş bırakılamaz!")
            .NotNull().WithMessage("Şifre alanı boş bırakılamaz!");
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
            .WithMessage("Şifreler eşleşmiyor!");
    }
}