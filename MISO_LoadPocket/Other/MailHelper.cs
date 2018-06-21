using System;
using System.Net.Mail;

namespace MISO_LoadPocket.Other
{
    public class MailHelper
    {
        public static void SendAlert(string username, string password,string toAddress, string phone){
            try
            {


                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(toAddress));
                msg.From = new MailAddress(username);
                msg.Subject = "LPV Success - " + DateTime.Now.ToString("MMddyyyy");
                msg.Body = "LPV automation was successful.";
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.office365.com";
                client.Credentials = new System.Net.NetworkCredential(username, password);
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(msg);


                Console.Write("First message sent to " + toAddress + "\n");
                //Split to text

                MailMessage text = new MailMessage();
                text.To.Add(new MailAddress(phone));
                text.From = new MailAddress(username);
                text.Subject = "LPV Success - " + DateTime.Now.ToString("MMddyyyy");
                text.Body = "LPV automation was successful.";
                text.IsBodyHtml = true;
                SmtpClient client2 = new SmtpClient();
                client2.Host = "smtp.office365.com";
                client2.Credentials = new System.Net.NetworkCredential(username, password);
                client2.Port = 587;
                client2.EnableSsl = true;
                client2.Send(text);

                Console.Write("Second message sent to " + phone + "\n");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }



        }
    }
}
