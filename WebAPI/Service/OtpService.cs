using MimeKit;

using MailKit.Net;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace WebAPI.Service
{
    public class OtpService
    {
        


        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public OtpService(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _senderEmail = configuration["EmailSettings:SenderEmail"];
            _senderPassword = configuration["EmailSettings:SenderPassword"];
        }

        public string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // Generates a 6-digit OTP
        }

        public async Task SendOtpEmailAsync(string recipientEmail, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your App Name", "your-email@example.com"));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = "Your OTP Code";

            message.Body = new TextPart("plain")
            {
                Text = $"Your OTP code is: {otp}"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    // Custom server certificate validation callback
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync("" +
                        "gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("priyankarangole1008@gmail.com", "Rio@1008");
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }

}



