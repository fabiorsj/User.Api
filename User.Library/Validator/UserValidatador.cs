using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace User.Library
{
    public class UserValidatador : AbstractValidator<User>
    {
        public UserValidatador()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(300).WithMessage("Nome deve ter no máximo 300 caracteres");

            RuleFor(x => x.Birthdate)
                .NotEmpty().WithMessage("Data de nascimento é obrigatório")
                .Must(CheckUnderage).WithMessage("Não pode ser menor de idade");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .MaximumLength(150).WithMessage("Email deve ter no máximo 150 caracteres")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Matches(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$").WithMessage("Telefone no formato (xx) xxxxx-xxxx");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Endereço é obrigatório");

            RuleFor(x => x.Password)
                .Length(6, 50).WithMessage("Senha deve ter no mínimo 6 e no máximo 50 caracteres")
                .NotEmpty().WithMessage("Senha é obrigatório")
                .Must(CheckPassword).WithMessage("Senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais");

        }

        private static bool CheckUnderage(DateTime birthdate)
        {
            return birthdate <= DateTime.Now.AddYears(-18);
        }

        private static bool CheckPassword(string password)
        {
            if (!Regex.Match(password, @"\d").Success)
                return false;
            if (!Regex.Match(password, @"[a-z]").Success)
                return false;
            if (!Regex.Match(password, @"[A-Z]").Success)
                return false;
            ////if (!Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
            ////  !Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
            ////    return false;
            if (!Regex.Match(password, @"[!@#$%^&*]", RegexOptions.ECMAScript).Success)
                return false;

            return true;
        }
    }
}
