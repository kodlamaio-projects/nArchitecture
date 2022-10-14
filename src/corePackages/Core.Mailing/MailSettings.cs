namespace Core.Mailing;

public class MailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string SenderFullName { get; set; }
    public string SenderEmail { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool AuthenticationRequired { get; set; }

    public MailSettings()
    {
        AuthenticationRequired = false;
    }

    public MailSettings(string server, int port, string senderFullName, string senderEmail, string userName,
                        string password, bool authenticationRequired=false)
    {
        Server = server;
        Port = port;
        SenderFullName = senderFullName;
        SenderEmail = senderEmail;
        UserName = userName;
        Password = password;
        AuthenticationRequired = authenticationRequired;
    }
}