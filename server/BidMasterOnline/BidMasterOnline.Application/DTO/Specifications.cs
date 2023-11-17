using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// Specifications for sorting and paginating items.
    /// </summary>
    public class Specifications
    {
        // Sorting
        public string SortField { get; set; } = "Id";
        public SortOrder SortOrder { get; set; } = SortOrder.ASC;

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
