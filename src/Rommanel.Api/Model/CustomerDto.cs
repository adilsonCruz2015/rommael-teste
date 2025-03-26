namespace Rommanel.Api.Model
{
    public class CustomerDto
    {
        public string? QueryField { get; set; } = null!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
