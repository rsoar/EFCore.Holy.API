using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.ComponentModel.DataAnnotations;
using EFCore.Holy.Data.Models.DTO;
using EFCore.Holy.Data.Config;
using EFCore.Holy.Business.Handling;

namespace EFCore.Holy.Business.Services
{
    public static class MailService
    {
        public static bool IsValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public static async Task<bool> SendAsync(MailData mailData, CancellationToken ct = default)
        {
            try
            {
                var mail = new MimeMessage();

                #region Sender / Receiver
                // Sender
                mail.From.Add(new MailboxAddress(MailSettings.DisplayName, mailData.From ?? MailSettings.From));
                mail.Sender = new MailboxAddress(mailData.DisplayName ?? MailSettings.DisplayName, mailData.From ?? MailSettings.From);

                // Receiver
                foreach (string mailAddress in mailData.To)
                    mail.To.Add(MailboxAddress.Parse(mailAddress));

                // Set Reply to if specified in mail data
                if (!string.IsNullOrEmpty(mailData.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

                if (mailData.Bcc != null)
                {
                    // Get only addresses where value is not null or with whitespace. x = value of address
                    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                if (mailData.Cc != null)
                {
                    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
                #endregion

                #region Content

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = mailData.Subject;
                body.HtmlBody = mailData.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Send Mail

                using var smtp = new SmtpClient();

                if (MailSettings.UseSSL)
                {
                    await smtp.ConnectAsync(MailSettings.Host, MailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (MailSettings.UseStartTls)
                {
                    await smtp.ConnectAsync(MailSettings.Host, MailSettings.Port, SecureSocketOptions.StartTls, ct);
                }
                await smtp.AuthenticateAsync(MailSettings.Username, MailSettings.Password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                #endregion

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}