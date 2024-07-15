using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

public class EmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;

        // Replace {EnvironmentVariable:SMTP_PASSWORD} with the actual environment variable value
        _emailSettings.SmtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? string.Empty;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        email.To.Add(new MailboxAddress("", toEmail));
        email.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        email.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
        await client.SendAsync(email);
        await client.DisconnectAsync(true);
    }
}

public class EmailSettings
{
    public string? SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string? SmtpUsername { get; set; } = string.Empty;
    public string? SmtpPassword { get; set; } = string.Empty;
    public string? SenderEmail { get; set; } = string.Empty;
    public string? SenderName { get; set; } = string.Empty;
}
