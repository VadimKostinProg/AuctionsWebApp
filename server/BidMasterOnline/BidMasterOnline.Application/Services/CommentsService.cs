using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.Exceptions;
using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;

namespace BidMasterOnline.Application.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IAsyncRepository _repository;
        private readonly IAuthService _authService;

        public CommentsService(IAsyncRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await _repository.FirstOrDefaultAsync<AuctionComment>(x =>
                x.Id == id && !x.IsDeleted);

            if (comment is null)
                throw new KeyNotFoundException("Comment with such id does not exists.");

            var user = await _authService.GetAuthenticatedUserEntityAsync();

            if (comment.UserId != user.Id)
                throw new ForbiddenException("Connot delete the comment of ohter user.");

            comment.IsDeleted = true;
            await _repository.UpdateAsync(comment);
        }

        public async Task<CommentDTO> GetCommentByIdAsync(Guid id)
        {
            var comment = await _repository.GetByIdAsync<AuctionComment>(id);

            if (comment is null)
                throw new KeyNotFoundException("Comment with such id does not exist.");

            return new CommentDTO
            {
                Id = comment.Id,
                UserId = comment.UserId,
                Username = comment.User.Username,
                AuctionId = comment.AuctionId,
                DateAndTime = comment.DateAndTime,
                CommentText = comment.CommentText,
                IsDeleted = comment.IsDeleted
            };
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsForAuctionAsync(Guid auctionId)
        {
            if (!await _repository.AnyAsync<Auction>(x => x.Id == auctionId && x.IsApproved))
                throw new KeyNotFoundException("Auction with such id does not exist.");

            var specification = new SpecificationBuilder<AuctionComment>()
                .With(x => x.AuctionId == auctionId)
                .With(x => !x.IsDeleted)
                .OrderBy(x => x.DateAndTime, Enums.SortDirection.DESC)
                .Build();

            var comments = await _repository.GetAsync<AuctionComment>(specification);

            return comments
                .Select(x => new CommentDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Username = x.User.Username,
                    AuctionId = x.AuctionId,
                    DateAndTime = x.DateAndTime,
                    CommentText = x.CommentText,
                    IsDeleted = x.IsDeleted
                })
                .ToList();
        }

        public async Task SetNewCommentAsync(SetCommentDTO request)
        {
            var user = await _authService.GetAuthenticatedUserEntityAsync();

            if (user.UserStatus.Name == Enums.UserStatus.Blocked.ToString())
                throw new ForbiddenException("Your account is blocked.");

            if (!await _repository.AnyAsync<Auction>(x => x.Id == request.AuctionId && x.IsApproved))
                throw new KeyNotFoundException("Auction with such id does not exist.");

            var comment = new AuctionComment
            {
                UserId = user.Id,
                AuctionId = request.AuctionId,
                DateAndTime = DateTime.Now,
                CommentText = request.CommentText,
            };

            await _repository.AddAsync(comment);
        }
    }
}
