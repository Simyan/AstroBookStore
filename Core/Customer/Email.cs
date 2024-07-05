using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Customer
{
    public class Email : IValueObject
    {
        public string emailId { get; private set; }

        
        bool IsValidEmail(string email)
        {
            //NOTE: Code from user CogWheel on SO: https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private Email() { }

        public Email(string emailId)
        {
            if (!IsValidEmail(emailId))
                new ArgumentException();

            this.emailId = emailId;
        }
    }
}
