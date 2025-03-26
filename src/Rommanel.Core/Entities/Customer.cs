using Rommanel.Core.Entities.Common;
using Rommanel.Core.Exceptions;
using Rommanel.Core.ValueObject;

namespace Rommanel.Core.Entities
{
    public class Customer : Entity
    {
        public string? Name { get; private set; }      
        public string? Document { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public Address? Address { get; private set; } 
        public bool TaxExempt { get; private set; }
        public string? StateRegistration { get; private set; }

        private Customer() { }

        public Customer(string name, string document, DateTime birthDate, string phone, string email, Address address, string? stateRegistration, bool taxExempt)
        {
            Name = name;
            Document = document;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Address = address;
            StateRegistration = stateRegistration;
            TaxExempt = taxExempt;
        }

        public void Update(string name, string document, DateTime birthDate, string phone, string email, Address address, string? stateRegistration, bool taxExempt)
        {
            Name = name;
            Document = document;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Address = address;
            StateRegistration = stateRegistration;
            TaxExempt = taxExempt;
        }
    }
}
