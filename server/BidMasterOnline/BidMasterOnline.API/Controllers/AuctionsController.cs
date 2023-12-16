using BidMasterOnline.Application.Constants;
using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidMasterOnline.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionsService _auctionsService;
        private readonly IAuctionVerificationService _auctionVerificationService;
        private readonly IBidsService _bidsService;

        public AuctionsController(IAuctionsService auctionsService,
            IAuctionVerificationService auctionVerificationService,
            IBidsService bidsService)
        {
            _auctionsService = auctionsService;
            _auctionVerificationService = auctionVerificationService;
            _bidsService = bidsService;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ActionResult<ListModel<AuctionDTO>>> GetAuctionsList(
            [FromQuery] AuctionSpecificationsDTO specifications)
        {
            return Ok(await _auctionsService.GetAuctionsListAsync(specifications));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AuctionDTO>> GetAuctionById([FromRoute] Guid id)
        {
            return Ok(await _auctionsService.GetAuctionByIdAsync(id));
        }

        [HttpGet("{id}/details")]
        [AllowAnonymous]
        public async Task<ActionResult<AuctionDetailsDTO>> GetAuctionDetailsById([FromRoute] Guid id)
        {
            return Ok(await _auctionsService.GetAuctionDetailsByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<string>> PublishAuction([FromForm] PublishAuctionDTO auction)
        {
            await _auctionsService.PublishAuctionAsync(auction);

            return Ok("Auction has been successfully published for verification.");
        }

        [HttpPost("scores")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<string>> SetScoreForAuction([FromBody] SetAuctionScoreDTO request)
        {
            await _auctionsService.SetAuctionScoreAsync(request);

            return Ok("Your score for auction has been successfully set.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.TechnicalSupportSpecialist)]
        public async Task<ActionResult<string>> CancelAuction([FromRoute] Guid id)
        {
            await _auctionsService.CancelAuctionAsync(id);

            return Ok("Auction has been canceled successfully.");
        }

        [HttpPost("{id}/recover")]
        [Authorize(Roles = UserRoles.TechnicalSupportSpecialist)]
        public async Task<ActionResult<string>> RecoverAuction([FromRoute] Guid id)
        {
            await _auctionsService.RecoverAuctionAsync(id);

            return Ok("Auction has been recovered successfully.");
        }

        [HttpGet("{id}/bids")]
        [AllowAnonymous]
        public async Task<ActionResult<ListModel<BidDTO>>> GetBidsForAuction([FromRoute] Guid id, 
            [FromQuery] SpecificationsDTO specifications)
        {
            return Ok(await _bidsService.GetBidsListForAuctionAsync(id, specifications));
        }

        [HttpPost("{id}/bids")]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<ActionResult<ListModel<BidDTO>>> SetNewBid([FromBody] SetBidDTO bid)
        {
            await _bidsService.SetBidAsync(bid);

            return Ok("New bid has been set successfully.");
        }

        [HttpGet("not-approved/list")]
        [Authorize(Roles = UserRoles.TechnicalSupportSpecialist)]
        public async Task<ActionResult<ListModel<AuctionDTO>>> GetNotApprovedAuctionsList(
            [FromQuery] AuctionSpecificationsDTO specifications)
        {
            return Ok(await _auctionVerificationService.GetNotApprovedAuctionsListAsync(specifications));
        }

        [HttpGet("not-approved/{id}/details")]
        [Authorize(Roles = UserRoles.TechnicalSupportSpecialist)]
        public async Task<ActionResult<AuctionDetailsDTO>> GetNotApprovedAuctionDetails([FromRoute] Guid id)
        {
            return Ok(await _auctionVerificationService.GetNotApprovedAuctionDetailsByIdAsync(id));
        }

        [HttpPost("not-approved/{id}/approve")]
        [Authorize(Roles = UserRoles.TechnicalSupportSpecialist)]
        public async Task<ActionResult<string>> ApproveAuction([FromRoute] Guid id)
        {
            await _auctionVerificationService.ApproveAuctionAsync(id);

            return Ok("Auction has been approved successfully.");
        }

        [HttpPost("not-approved/reject")]
        [Authorize(Roles = UserRoles.TechnicalSupportSpecialist)]
        public async Task<ActionResult<string>> RejectAuction([FromBody] RejectAuctionDTO request)
        {
            await _auctionVerificationService.RejectAuctionAsync(request);

            return Ok("Auction has been rejected successfully.");
        }
    }
}