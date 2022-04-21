using System.Net;
using System.Net.Mail;

static class Mail
{
    public static string DefaultMail { get; set; } = "aqa.akberzade@gmail.com";
    public static void Send(string to, string message, string subject = "", string from = "someamazingbot2214@gmail.com")
    {
        try
        {

            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.From = new MailAddress(from);
            msg.Body = message;
            msg.To.Add(new MailAddress(to));

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, "FBAS2214");
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Can not sent LOG info to ADMIN due to: {ex.Message}");
            Thread.Sleep(1000);
        }


    }
}
