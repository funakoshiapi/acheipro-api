
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using AutoMapper;
using Shared.DataTransferObjects;
using Shared;

namespace Service
{
    public class SendEmail : ISendEmail
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;

        public SendEmail(IOptions<SmtpSettings> smtpSettings, IWebHostEnvironment env, IMapper mapper, IRepositoryManager repository)
        {
            _smtpSettings = smtpSettings.Value;
            _env = env;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task SendClientMessageAsync(ClientMessageDto clientMessage)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(clientMessage.RecipientCompanyEmail, clientMessage.RecipientCompanyEmail));
                message.Subject = $"AcheiPro - Tem Uma Nova Messagem de {clientMessage.FirstName} {clientMessage.LastName}";
                message.Body = new TextPart("html")
                {
                    Text = ClientEmailBuilder(clientMessage)
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_smtpSettings.Server,
                _smtpSettings.Port, true);
                    }
                    else
                    {
                        await client.ConnectAsync(_smtpSettings.Server);

                    }

                    await client.AuthenticateAsync(_smtpSettings.Username,
                                                   _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    await  SaveMessage(clientMessage);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        
    }


        public async Task SendRecoveryPasswordEmail(PasswordRecoveryDto passwordRecoveryDto, Guid recoveryId)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(passwordRecoveryDto.Email, passwordRecoveryDto.Email));
                message.Subject = $"AcheiPro - Pedido De Recuperação De Palava-Chave";

                 string emailBody = $"<h2>Pedido De Recuperação De Palava-Chave</h2>" +
                               $"<body>" +
                               $"<div>" +
                               $"<b>Mensagem:</b>" +
                               $"<p>Por favor click no link para renovar a sua palavra-chave: <a href={"https://acheipro.netlify.app/recuperar-chave/"+recoveryId}>Renove Palavra-Chave</a></p>" +
                               "</div>" +
                               $"</body>";

                message.Body = new TextPart("html")
                {
                    Text = emailBody
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_smtpSettings.Server,
                _smtpSettings.Port, true);
                    }
                    else
                    {
                        await client.ConnectAsync(_smtpSettings.Server);

                    }

                    await client.AuthenticateAsync(_smtpSettings.Username,
                                                   _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        
    }

        private async Task SaveMessage(ClientMessageDto clientMessage)
        {
            var entity = _mapper.Map<ClientMessage>(clientMessage);
            _repository.ClientMessage.CreateClientMessage(entity);
            await _repository.SaveAsync();
        }

        private string ClientEmailBuilder(ClientMessageDto clientMessage)
        {
            string emailBody = $"<h2>{clientMessage.Topic.ToUpper()}</h2>" +
                               $"<body>" +
                               $"<div>" +
                               $"<b>Mensagem:</b>" +
                               $"<p>{clientMessage.Message}</p>" +
                               $"</div>" +
                               $"<div>" +
                               $"<b>Nome do Cliente: </b>" +
                               $"<p>{clientMessage.FirstName} {clientMessage.LastName}</p>"+
                               $"<b>Numero de Telefone: </b>" +
                               $"<p>{clientMessage.PhoneNumber}</p>" +
                               $"<b>Email: </b>" +
                               $"<p>{clientMessage.Email}</p>" +
                               $"</div>" +
                               $"</body>";

            return emailBody;
        }
    }
}

