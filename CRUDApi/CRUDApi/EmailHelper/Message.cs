using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.EmailHelper
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }
    }
}
