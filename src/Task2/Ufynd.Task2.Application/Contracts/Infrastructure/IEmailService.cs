namespace Ufynd.Task2.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        bool SendEmailWithAttachment(string recipient, string attachmentFilePath);
    }
}
