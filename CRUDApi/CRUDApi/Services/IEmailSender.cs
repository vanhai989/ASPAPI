using CRUDApi.EmailHelper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Services
{
    interface IEmailSender
    {
        Task SendEmail(Message message);
    }
}
