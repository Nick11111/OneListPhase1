
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using OneListApplication.Models;
using System.Web.Mvc;

namespace OneListApplication.Service
{
    class SendGrid
    {
        static public void sendEmail(RegisteredUserVM userInfo, string confirmLink)
        {

            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(userInfo.Email, "Hi " + userInfo.UserName));

            // From
            mailMsg.From = new MailAddress("rainl@sfu.ca", "Nick");

            // Subject and multipart/alternative Body
            mailMsg.Subject = "Email Confirmation";
            string text = "text body";
            //string html = @"<p>Please confirm your account by clicking this link: < a href =\""
            //                      + comfirmLink + "\">Confirm Registration</a></p>";
            string body = "<p>Please confirm your account by clicking this link: < a href =\""
                              + confirmLink + "\"></a></p>";
            mailMsg.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(text,
                    null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(body,
                    null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient
            = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials
            = new System.Net.NetworkCredential("rainliu1991@gmail.com",
                                               "Ear20090706");
            smtpClient.Credentials = credentials;
            smtpClient.Send(mailMsg);

        }
        static public void sendResetEmail(string email, string userName, string confirmLink)
        {

            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(email, "Hi " + userName));

            // From
            mailMsg.From = new MailAddress("rainl@sfu.ca", "Nick");

            // Subject and multipart/alternative Body
            mailMsg.Subject = "Email Confirmation";
            string text = "text body";
            //string html = @"<p>Please confirm your account by clicking this link: < a href =\""
            //                      + comfirmLink + "\">Confirm Registration</a></p>";
            string body = "<p>Please reset your account password by clicking this link: < a href =\""
                              + confirmLink + "\"></a></p>";
            mailMsg.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(text,
                    null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(body,
                    null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient
            = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials
            = new System.Net.NetworkCredential("rainliu1991@gmail.com",
                                               "Ear20090706");
            smtpClient.Credentials = credentials;
            smtpClient.Send(mailMsg);

        }
    }
}
