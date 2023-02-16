using FluentValidation;
using Microsoft.AspNetCore.Http;
using Ufynd.Task2.Application.Common;

namespace Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcel
{
    public class ConvertJsonToExcelCommandValidator : AbstractValidator<ConvertJsonToExcelCommand>
    {
        public ConvertJsonToExcelCommandValidator()
        {
            RuleFor(p => p.FormFile)
                .NotNull().WithMessage("The {formFile} is required.")
                .Must(BeValidFormFile).WithMessage("The file is invalid");

            RuleFor(p => p.FileName).Must(BeValidChars).WithMessage("The {fileName} has invalid chars.");
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
