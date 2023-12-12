namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for category specifications for filtering, sorting and paginating. (REQUEST)
    /// </summary>
    public class CategorySpecificationsDTO : SpecificationsDTO
    {
        public string? Name { get; set; }
    }
}
