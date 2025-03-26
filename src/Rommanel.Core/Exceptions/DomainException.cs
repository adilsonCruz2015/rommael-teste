

namespace Rommanel.Core.Exceptions
{
    public class DomainException : Exception
    {
        public List<string> Errors { get; }

        public DomainException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public DomainException(List<string> errors) : base("One or more domain validation errors occurred.")
        {
            Errors = errors;
        }
    }

}
