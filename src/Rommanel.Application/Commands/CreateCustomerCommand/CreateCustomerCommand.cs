
using MediatR;
using Rommanel.Core.Entities;

namespace Rommanel.Application.Commands.CreateCustomerCommand
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string? Name { get; set; }
        public string? Document { get; set; }  // Documento como string
        public DateTime BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? StateRegistration { get; set; }
        public bool TaxExempt { get; set; }
    }
}
