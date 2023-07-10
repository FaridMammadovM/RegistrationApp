using AddressBook.Domain.Dtos.Employee;
using AddressBook.Domain.Models;
using AddressBook.Infrastructure.Repositories.OrderRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using System.Net.Mail;

namespace AddressBook.Api.Application.Commands.OrderCommand.Confirm
{
    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, long?>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<ConfirmOrderCommand> _validator;
        private readonly IMapper _mapper;

        public ConfirmOrderCommandHandler(IOrderRepository orderRepository, IValidator<ConfirmOrderCommand> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<long?> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            Order model = _mapper.Map<Order>(request);
            EmailDto? emailDto = await orderRepository.Confirm(model);

            if (emailDto.Status == "Təsdiqləndi")
            {
                string body = EmailTesdiq(emailDto.UserFullname, emailDto.UserusingFullname, emailDto.Car, emailDto.Time);
                Task.Run(() =>
                 SendEmail(emailDto.User, emailDto.Userusing, emailDto.Driver, "Maşın rezervasiyası təsdiqləndi",
                 body, request.MailFrom, request.Password));
            }
            else if (emailDto.Status == "İmtina edildi")
            {

                string body = EmailImtina(emailDto.UserFullname, emailDto.UserusingFullname, emailDto.Car, emailDto.Time, emailDto.Reject);
                Task.Run(() =>
                SendEmail(emailDto.User, emailDto.Userusing, emailDto.Driver, "Maşın rezervasiyası imtina edildi",
                body, request.MailFrom, request.Password));
            }

            long id = 1;
            return id;
        }


        public async Task SendEmail(string user, string userusing, string driver, string subject, string body,
            string mailFrom, string password)
        {

            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            mail.From = new MailAddress(mailFrom);

            if (user != null || userusing != null || driver != null)
            {
                mail.To.Add(user);
                if (userusing != null)
                {
                    mail.To.Add(userusing);
                }
                if (driver != null)
                {
                    mail.To.Add(driver);
                }

                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = body;
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(mailFrom, password);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mail);
            }

        }

        public string EmailImtina(string user, string userusing, string car, string time, string reject)
        {
            string imtina = $"<!DOCTYPE PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN” “https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n\r\n<head>\r\n " +
                $" <meta charset=\"UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width,initial-scale=1.0\">\r\n  <title></title>\r\n\r\n  <style>\r\n    @font-face {{\r\n      font-family: \"Poppins\";\r\n    " +
                $"  src: url('/fonts/Poppins-Regular.eot');\r\n      src: url('/fonts/Poppins-Regular.eot') format('embedded-opentype'),\r\n        url('/fonts/Poppins-Regular.woff2') format('woff2'),\r\n     " +
                $"   url('/fonts/Poppins-Regular.woff') format('woff'),\r\n        url('/fonts/Poppins-Regular.ttf') format('truetype');\r\n\r\n    }}\r\n  </style>\r\n  </style>\r\n\r\n\r\n</head>\r\n\r\n<body class=\"clean-body\"\r\n " +
                $" style=\"margin: auto;padding: 0;font-family: Poppins;width: 100%;display: flex;justify-content: center;\">\r\n  <div class=\"main\" style=\"width: 600px;background-color: #fff;padding: 40px 56px;\">\r\n   " +
                $" <table role=\"presentation\" class=\"table\" style=\"width:100%\">\r\n      <tr style=\" border: none !important;width: 100%;\">\r\n        <td align=\"center\" style=\"padding:0;width:100%;text-align: center;\">\r\n          <img\r\n            " +
                $"src=\"data:image/png;base64,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\"\r\n" +
                $"alt=\"its.gov.az logo\">\r\n  </td>\r\n  </tr>\r\n  <tr style=\"border: none !important;width: 100%;\">\r\n <td align=\"center\" style=\"padding:0\">\r\n   <h1\r\n  style=\"margin: 0 0 24px 0;font-weight: 600;font-size: 24px;line-height: 32px;width: 100%;text-align: center;\">\r\n  " +
                $"          TƏBİB Rezervasiya Sistemi\r\n          </h1>\r\n        </td>\r\n      </tr>\r\n    </table>\r\n\r\n    <table role=\"presentation\" class=\"table\" style=\"width: 100%;border-collapse: collapse;border: 0;border-spacing: 0;\">\r\n    " +
                $"  <tr style=\"width:100%;background: #FAF7F7;border: none!important; padding: 16px;\">\r\n        <td align=\"center\"\r\n          style=\"padding: 16px 16px 8px 16px;font-size: 14px;line-height: 24px;border: none !important;\">\r\n        " +
                $"  <strong>Sifarişçi : </strong>\r\n  " +
                $" <span style=\"margin-left: 8px;\">{user}</p>\r\n        </td>\r\n      </tr>\r\n      <tr style=\" background: #FAF7F7;border: none!important; padding: 16px;\">\r\n      " +
                $"  <td align=\"center\" style=\"padding: 0 16px 8px 16px;font-size: 14px;line-height: 24px;border: none !important;\">\r\n        " +
                $"  <strong>İstifadəçi : </strong>\r\n         " +
                $" <span style=\"margin-left: 8px;\">{userusing}</p>\r\n     " +
                $"   </td>\r\n      </tr>\r\n      <tr style=\" background: #FAF7F7;border: none!important; padding: 16px;\">\r\n        <td align=\"center\" style=\"padding: 0 16px 8px 16px;font-size: 14px;line-height: 24px;border: none !important;\">\r\n       " +
                $"   <strong>Gediş tarixi : </strong>\r\n        " +
                $"  <span style=\"margin-left: 8px;\">{time}</p>\r\n        </td>\r\n      </tr>\r\n      <tr style=\" background: #FAF7F7;border: none!important; padding: 16px;\">\r\n       " +
                $" <td align=\"center\" style=\"padding: 0 16px 8px 16px;font-size: 14px;line-height: 24px;border: none !important;\">\r\n         " +
                $" <strong>Avtomobil : </strong>\r\n          " +
                $"<span style=\"margin-left: 8px;\">{car}</p>\r\n        </td>\r\n   " +
                $"   </tr>\r\n\r\n    </table>\r\n\r\n    <table role=\"presentation\" class=\"table\"\r\n      style=\"width: 100%;border-collapse: collapse;border: 0;border-spacing: 0;margin-top: 16px;\">\r\n      <tr style=\"width:100%;background: #FAF7F7;border: none!important; padding: 16px;\">\r\n " +
                $"       <td align=\"center\"\r\n          style=\"padding: 16px 16px 16px 16px;font-size: 14px;line-height: 24px;border: none !important;\">\r\n        " +
                $"  <strong>İmtina səbəbi : </strong>\r\n        " +
                $"  <span style=\"margin-left: 8px;\">{reject}</p>\r\n        </td>\r\n " +
                $"     </tr>\r\n\r\n    </table>\r\n    <div style=\"margin-top: 24px;background: rgba(255, 0, 0, 0.24); border: 1px solid #FF0000; padding: 16px;\">\r\n      <p\r\n        style=\"margin: 0;text-align: center;text-align: center; color: #FF0000;;font-weight: 700;font-family: Poppins;\">\r\n      " +
                $"  Status: Imtina edildi\r\n      </p>\r\n    </div>\r\n  </div>\r\n</body>\r\n\r\n</html>";
            return imtina;
        }

        public string EmailTesdiq(string user, string userusing, string car, string time)
        {
            string tesdiq = $"<!DOCTYPE PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN” “https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html" +
                $" xmlns=\"http://www.w3.org/1999/xhtml\">\r\n\r\n<head>\r\n  <meta charset=\"UTF-8\">\r\n  " +
                $"<meta name=\"viewport\" content=\"width=device-width,initial-scale=1.0\">\r\n  " +
                $"<title></title>\r\n\r\n  <style>\r\n    @font-face {{\r\n      font-family: \"Poppins\";\r\n" +
                $"      src: url('/fonts/Poppins-Regular.eot');\r\n      src: url('/fonts/Poppins-Regular.eot')" +
                $" format('embedded-opentype'),\r\n        url('/fonts/Poppins-Regular.woff2') format('woff2'),\r\n" +
                $"        url('/fonts/Poppins-Regular.woff') format('woff'),\r\n        " +
                $"url('/fonts/Poppins-Regular.ttf') format('truetype');\r\n\r\n    }}\r\n  </style>\r\n" +
                $"  </style>\r\n\r\n\r\n</head>\r\n\r\n<body class=\"clean-body\" style=\"margin: auto;padding:" +
                $" 0;font-family: Poppins;width: 100%;display: flex;justify-content: center;\">\r\n " +
                $" <div class=\"main\" style=\"width: 600px;background-color: #fff;padding: 40px 56px;\">\r\n " +
                $"   <table role=\"presentation\" class=\"table\" style=\"width:100%\">\r\n     " +
                $" <tr style=\" border: none !important;width: 100%;\">\r\n       " +
                $" <td align=\"center\" style=\"padding:0;width:100%;text-align: center;\">\r\n <img\r\n " +
                $" src=\"data:image/png;base64,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\"\r\n" +
                $" alt=\"its.gov.az logo\">\r\n </td>\r\n </tr>\r\n  <tr style=\"border: none !important;width:" +
                $" 100%;\">\r\n        <td align=\"center\" style=\"padding:0\">\r\n          <h1\r\n            " +
                $"style=\"margin: 0 0 24px 0;font-weight: 600;font-size: 24px;line-height: 32px;width:" +
                $" 100%;text-align: center;\">\r\n            TƏBİB Rezervasiya Sistemi\r\n          </h1>\r\n  " +
                $"      </td>\r\n      </tr>\r\n    </table>\r\n\r\n   " +
                $" <table role=\"presentation\" class=\"table\" style=\"width: 100%;border-collapse:" +
                $" collapse;border: 0;border-spacing: 0;\">\r\n      <tr style=\"width:100%;background: " +
                $"#FAF7F7;border: none!important; padding: 16px;\">\r\n      " +
                $"  <td align=\"center\" style=\"padding: 16px 16px 8px 16px;font-size: 14px;line-height:" +
                $" 24px;border: none !important;\">\r\n          <strong>Sifarişçi : </strong>\r\n        " +
                $"  <span style=\"margin-left: 8px;\">{user}</p>\r\n      " +
                $"  </td>\r\n      </tr>\r\n      <tr style=\" background: #FAF7F7;border:" +
                $" none!important; padding: 16px;\">\r\n        <td align=\"center\" style=\"padding: " +
                $"0 16px 8px 16px;font-size: 14px;line-height: 24px;border: none !important;\">\r\n  " +
                $" <strong>İstifadəçi : </strong>\r\n         " +
                $" <span style=\"margin-left: 8px;\">{userusing}</p>\r\n " +
                $" </td>\r\n   </tr>\r\n  <tr style=\" background: #FAF7F7;border: none!important; padding:" +
                $" 16px;\">\r\n  <td align=\"center\" style=\"padding: 0 16px 8px 16px;font-size:" +
                $" 14px;line-height: 24px;border: none !important;\">\r\n         " +
                $" <strong>Gediş tarixi : </strong>\r\n        " +
                $" <span style=\"margin-left: 8px;\">{time}</p>\r\n " +
                $" </td>\r\n </tr>\r\n <tr style=\" background: #FAF7F7;border: none!important; padding: 16px;\">\r\n" +
                $" <td align=\"center\" style=\"padding: 0 16px 8px 16px;font-size: 14px;line-height: " +
                $"24px;border: none !important;\">\r\n <strong>Avtomobil : </strong>\r\n" +
                $"<span style=\"margin-left: 8px;\">{car}</p>\r\n </td>\r\n  </tr>\r\n\r\n  </table>\r\n\r\n " +
                $"<div style=\"margin-top: 24px;background: rgba(46, 153, 84, 0.24); border: 1px solid #2E9954;" +
                $"padding: 16px;\">\r\n  <p style=\"margin: 0;text-align: center;text-align: center; color: " +
                $"#2E9954;font-weight: 700;font-family: Poppins;\">\r\n      " +
                $"Status: Təsdiqləndi\r\n  </p>\r\n  </div>\r\n\r\n   \r\n  </div>\r\n</body>\r\n\r\n</html>";
            return tesdiq;
        }
    }
}
