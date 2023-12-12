using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// Specifications for sorting and paginating items. (REQUEST)
    /// </summary>
    public class SpecificationsDTO
    {
        // Sorting
        public string? SortField { get; set; }
        public SortOrder? SortOrder { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
