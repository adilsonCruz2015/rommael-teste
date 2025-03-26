

using Rommanel.Core.Exceptions;

namespace Rommanel.Core.ValueObject
{
    public class TaxId
    {
        public string Value { get; private set; }

        public TaxId(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || (!IsCpf(value) && !IsCnpj(value)))
                throw new DomainException("Invalid CPF or CNPJ.");

            Value = value;
        }

        public bool IsCpf() => Value.Length == 11;
        public bool IsCnpj() => Value.Length == 14;

        private static bool IsCpf(string cpf)
        {
            return true;
        }

        private static bool IsCnpj(string cnpj)
        {
            return true;
        }
    }

}
