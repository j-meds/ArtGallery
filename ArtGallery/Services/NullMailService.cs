using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;


        public NullMailService(ILogger<NullMailService> logger) 
        {
            this._logger = logger;
        }

        public void SendMessage(string to, string subject, string body)
        {
            // log the message
            _logger.LogInformation($"\nTo: {to}\n Subject: {subject}\n Body: {body}\n");
        }
    }
}
