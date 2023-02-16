using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using Ufynd.Task2.Application.Common;

namespace Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcelAndEmail
{
    public class ConvertJsonToExcelAndEmailCommandValidator : AbstractValidator<ConvertJsonToExcelAndEmailCommand>
    {
        public ConvertJsonToExcelAndEmailCommandValidator()
        {
            RuleFor(p => p.FormFile)
                .NotNull().WithMessage("The {formFile} is required.")
                .Must(BeValidFormFile).WithMessage("The file is invalid");

            RuleFor(p => p.FileName).Must(BeValidChars).WithMessage("The {fileName} has invalid chars.");

            RuleFor(p => p.Email)
                .NotNull().NotEmpty().WithMessage("The {email} is required.")
                .EmailAddress().WithMessage("The {email} is invalid.");

            RuleFor(p => p.SendTime).Must(BeValidTime).WithMessage("The {sendTime} is invalid.");
        }

        private bool BeValidTime(DateTime? sendTime)
        {
            if (sendTime.HasValue == false)
                return true;

            return sendTime.Value > DateTime.Now;
        }

        private bool BeValidChars(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return true;

            return input.HasInvalidFileNameChars() == false;
        }

        private bool BeValidFormFile(IFormFile formFile)
        {
            if (formFile != null)
                if (string.IsNullOrEmpty(formFile.FileName) || formFile.Length == 0)
                    return false;
            return true;
        }
    }
}
