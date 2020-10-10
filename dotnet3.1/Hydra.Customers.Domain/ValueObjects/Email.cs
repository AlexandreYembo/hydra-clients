using System.Text.RegularExpressions;
using Hydra.Core.DomainObjects;

namespace Hydra.Customers.Domain.ValueObjects
{
    public class Email
    {
        public const string MaxLength = "150";
        public string Value { get; private set; }

        protected Email(){}
        public Email(string email)
        {
            if(!IsValid(email)) throw new DomainException("Invalid Email");
            Value = email;
        }

        public bool IsValid(string email){
            var emailRegex = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return emailRegex.IsMatch(email);
        }
    }
}