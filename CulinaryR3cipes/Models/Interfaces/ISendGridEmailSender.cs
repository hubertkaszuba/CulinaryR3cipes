using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models.Interfaces
{
    public interface ISendGridEmailSender
    {
        Task SendMail(string email, string subject, string message);
    }
}
