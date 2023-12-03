namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for bid specifications for filtering, sorting and paginating. (REQUEST)
    /// </summary>
    public class BidSpecificationsDTO : SpecificationsDTO
    {
        public bool OnlyWinning { get; set; } = false;
    }
}
