using System.Net;
using System.Net.Mail;

namespace SergorilaPAY2._0.HangFire;

public static class HangFireWorker
{
    public static void SendEmailAboutLoging(string email, string tgId)
    {
        MailAddress from = new MailAddress("mytestmail66@mail.ru", "SergorilaPAY");

        MailAddress to = new MailAddress(email);

        MailMessage m = new MailMessage(from, to);

        m.Subject = $"Произошла авторизация пользователя {tgId}";

        m.Body = $"Время авторизации: {DateTime.Now}";

        m.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);

        smtp.Credentials = new NetworkCredential("mytestmail66@mail.ru", "jC4WdrYa210wkNmkFaP4");
        smtp.EnableSsl = true;
        smtp.Send(m);
    }
}