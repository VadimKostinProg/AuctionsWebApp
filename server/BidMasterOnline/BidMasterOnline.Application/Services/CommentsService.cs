using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.ServiceContracts;

namespace BidMasterOnline.Application.Services
{
    public class CommentsService : ICommentsService
    {
        public Task DeleteCommentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CommentDTO> GetCommentByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentDTO>> GetCommentsForAuctionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SetNewCommentAsync(SetCommentDTO comment)
        {
            throw new NotImplementedException();
        }
    }
}
