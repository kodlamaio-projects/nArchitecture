using MimeKit;

namespace Core.Mailing;

public class Mail
{
    public string Subject { get; set; }
    public string TextBody { get; set; }
    public string HtmlBody { get; set; }
    public AttachmentCollection? Attachments { get; set; }
    public string ToFullName { get; set; }
    public string ToEmail { get; set; }
    public List<MailboxAddress>? CcList { get; set; }
    public List<MailboxAddress>? BccList { get; set; }
    
    public Mail()
    {
    }

    public Mail(string subject, string textBody, string htmlBody, AttachmentCollection? attachments, string toFullName,
                string toEmail, List<MailboxAddress>? ccList = null, List<MailboxAddress>? bccList = null)
    {
        Subject = subject;
        TextBody = textBody;
        HtmlBody = htmlBody;
        Attachments = attachments;
        ToFullName = toFullName;
        ToEmail = toEmail;
        CcList = ccList;
        BccList = bccList;
    }
}