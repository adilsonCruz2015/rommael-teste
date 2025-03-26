

namespace Rommanel.Core.ValueObject
{
    public record PagedResponse<T>
    {
        public List<T> Data { get; init; }

        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalRecords { get; init; }
        public int TotalPages { get; init; }

        public PagedResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = pageSize > 0
            ? (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize)
            : 0;  
        }
    }
}
