using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using OnlineClassRegister.Areas.Identity.Data;
using OnlineClassRegister.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;

namespace OnlineClassRegister.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly UserManager<OnlineClassRegisterUser> _userManager;
        private readonly ApplicationDbContext _context;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
            ILogger<EmailSender> logger, UserManager<OnlineClassRegisterUser> userManager, ApplicationDbContext context)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }

            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("onlineclassregister1@gmail.com", "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                ? $"Email to {toEmail} queued successfully!"
                : $"Failure Email to {toEmail}");
        }

        // "/hangfire" to check if job was scheduled 
        public async Task SendPeriodicEmail()
        {
            var client = new SendGridClient(Options.SendGridKey);

            var parents = await _userManager.GetUsersInRoleAsync("Parent");

            foreach (var parent in parents)
            {
                var parentEmail = new EmailAddress(parent.Email);
                var student = _context.Student.FirstOrDefault(s => s.surname == parent.LastName);
                var grades = _context.Grade.Where(g => g.studentId == student.id).ToList();

                string content = "";

                content = PrepareMessageContext(student, grades);
                
                SendGrades(client, parentEmail, content);
            }

        }

        private void SendGrades(SendGridClient client, EmailAddress email, string content)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("onlineclassregister1@gmail.com"));
            msg.SetSubject("Your child's  grades:");
            msg.AddTo(email);
            msg.AddContent(MimeType.Text, content);
            client.SendEmailAsync(msg);
        }

        private string PrepareMessageContext(Student student, List<Grade> grades)
        {
            string message = "";

            var uniqueSubjectIds = grades.Select(g => g.subjectId).Distinct();

            foreach (var id in uniqueSubjectIds)
            {
                var gradesForSubject = grades.Where(g => g.subjectId == id);
                
                string gradesAsMessage = "";
                
                foreach (var grade in gradesForSubject)
                {
                    gradesAsMessage += grade.value + ", ";
                }

                string subject = _context.Subject.FirstOrDefault(s => s.id == id).name;

                message += subject + ": " + gradesAsMessage + "\n";
            }

            return message;
        }
    }
}