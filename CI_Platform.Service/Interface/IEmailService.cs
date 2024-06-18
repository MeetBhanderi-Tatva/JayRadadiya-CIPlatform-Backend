using CI_Platform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Service.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(MailRequest mailrequest);
    }
}
