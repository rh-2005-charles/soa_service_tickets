using ic_tienda_business.Dtos.Responses;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace ic_tienda_data.Services.Image
{
    public class EmailOrderService
    {
        private readonly IConfiguration _configuration;
        public EmailOrderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendOrderConfirmationEmailAsync(OrderResponse orderResponse, string customerEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tu Nombre", _configuration["Email:UserName"]));
            message.To.Add(new MailboxAddress("", customerEmail)); // Aquí usas el email del cliente
            message.Subject = "Confirmación de Orden";

            message.Body = new TextPart("html")
            {
                Text = $"Tu orden ha sido aceptada. Detalles de la orden: {orderResponse.Id}, Monto Total: {orderResponse.TotalAmount}."
            };

#pragma warning disable CS8604

            // SmtpClient02
            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]),
                    MailKit.Security.SecureSocketOptions.SslOnConnect);
                //client.Connect(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_configuration["Email:UserName"], _configuration["Email:Password"]);

                await client.SendAsync(message);
                client.Disconnect(true);
            }
#pragma warning restore CS8604
        }
    }
}