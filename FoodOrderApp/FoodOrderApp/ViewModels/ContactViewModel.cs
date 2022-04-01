using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    /* email
      try{  
  
                MailMessage mail = new MailMessage();  
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");  
  
                mail.From = new MailAddress("susairajs18@gmail.com");  
                mail.To.Add(txtTo.Text);  
                mail.Subject = txtSubject.Text;  
                mail.Body = txtBody.Text;  
  
                SmtpServer.Port = 587;  
                SmtpServer.Host = "smtp.gmail.com";  
                SmtpServer.EnableSsl = true;  
                SmtpServer.UseDefaultCredentials = false;  
                SmtpServer.Credentials = new System.Net.NetworkCredential("8@gmail.com", "***********");  
  
                SmtpServer.Send(mail);  
            }  
            catch(Exception ex)  
            {  
                DisplayAlert("Faild", ex.Message, "OK");  
            }  
    */

  
    public class ContactViewModel:BaseViewModel
    {
        private string _To;
        public string To
        {
            set { _To = value; OnPropertyChanged(); }
            get { return _To; }
        }

        private string _Subject;
        public string Subject
        {
            set { _Subject = value; OnPropertyChanged(); }
            get { return _Subject; }
        }

        private string _Body;
        public string Body
        {
            set { _Body = value; OnPropertyChanged(); }
            get { return _Body; }
        }

        public Command SendEmailCommand { get; set; }
        public ContactViewModel()
        {
            SendEmailCommand = new Command(async () => await SendEmailAsync());
            To = fromemail;
        }

        #region Private !
        string fromemail = "princejankevin@gmail.com";
        string frompass = "1234";
        #endregion

        private async Task SendEmailAsync()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(To))
                      await Application.Current.MainPage.DisplayAlert("Error", "Please specify at least one recipient.", "OK");
                else if (string.IsNullOrWhiteSpace(Body))
                     await Application.Current.MainPage.DisplayAlert("Error", "Message body is empty.", "OK");
                else if (string.IsNullOrWhiteSpace(Subject))
                {
                    var result = await Application.Current.MainPage.DisplayAlert("Error", "Send this message without a subject?", "Send","Cancel");
                    if(result)
                        SendEmail();
                }
                else
                     SendEmail();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", ex.Message, "OK");
            }
        }

        public async void SendEmail()
        {
            //MailMessage mail = new MailMessage();
            //mail.From = new MailAddress(fromemail);
            //mail.To.Add(To);
            //mail.Subject = Subject;
            //mail.Body = Body;
            //using (var SmtpServer = new SmtpClient("smtp.gmail.com"))
            //{
            //    SmtpServer.Port = 587;
            //    SmtpServer.Host = "smtp.gmail.com";
            //    SmtpServer.EnableSsl = true;
            //    SmtpServer.UseDefaultCredentials = false;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential(fromemail, frompass);

            //    await SmtpServer.SendMailAsync(mail);
            //}
            //CrossToastPopUp.Current.ShowToastMessage("Email has been sent", Plugin.Toast.Abstractions.ToastLength.Short);
            //To = Subject = Body = "";

            EmailMessage mail = new EmailMessage();
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            var fn = "Attachment.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, "Hello World");
            mail.Attachments.Add(new EmailAttachment(file));
            await Email.ComposeAsync(mail);
            //CrossToastPopUp.Current.ShowToastMessage("Email has been sent", Plugin.Toast.Abstractions.ToastLength.Short);
            Subject = Body = "";
        }
    }
}
