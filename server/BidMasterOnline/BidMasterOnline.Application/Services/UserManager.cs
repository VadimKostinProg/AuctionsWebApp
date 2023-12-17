﻿using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.Enums;
using BidMasterOnline.Application.Exceptions;
using BidMasterOnline.Application.Helpers;
using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;

namespace BidMasterOnline.Application.Services
{
    public class UserManager : IUserManager
    {
        private readonly IAsyncRepository _repository;
        private readonly IImagesService _imagesService;
        private readonly IAuthService _authService;
        private readonly INotificationsService _notificationsService;

        public UserManager(IAsyncRepository repository, IImagesService imagesService, IAuthService authService,
            INotificationsService notificationsService)
        {
            _repository = repository;
            _imagesService = imagesService;
            _authService = authService;
            _notificationsService = notificationsService;
        }

        public async Task BlockUserAsync(BlockUserDTO request)
        {
            var user = await _repository.GetByIdAsync<User>(request.UserId);

            if (user is null)
                throw new KeyNotFoundException("User with such id does not exist.");

            if (user.UserStatus.Name == Enums.UserStatus.Blocked.ToString())
                throw new InvalidOperationException("User is already blocked.");

            if (user.UserStatus.Name == Enums.UserStatus.Deleted.ToString())
                throw new InvalidOperationException("User is deleted.");

            var userStatus = await _repository.FirstOrDefaultAsync<Domain.Entities.UserStatus>(x =>
                x.Name == Enums.UserStatus.Blocked.ToString());

            user.UserStatusId = userStatus!.Id;

            if (request.Days is not null)
            {
                if (request.Days.Value < 1)
                    throw new ArgumentException("Days number to block must be at least 1.");

                user.UnblockDateTime = DateTime.UtcNow.AddDays(request.Days.Value);
            }

            await _repository.UpdateAsync(user);

            _notificationsService.SendMessageOfBlockingAccountToUser(user, request.BlockingReason, request.Days);
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO request)
        {
            if (request is null)
                throw new ArgumentNullException("Change password form is null.");

            if (string.IsNullOrEmpty(request.CurrentPassword))
                throw new ArgumentNullException("Current password is blank.");

            if (string.IsNullOrEmpty(request.NewPassword))
                throw new ArgumentNullException("New password is blank.");

            PasswordComplexityValidator.Validate(request.NewPassword);

            var authenticatedUser = await _authService.GetAuthenticatedUserEntityAsync();

            if (authenticatedUser.UserStatus.Name == Enums.UserStatus.Deleted.ToString())
                throw new ForbiddenException("User is deleted.");

            var inputCurrentPasswordHashed = CryptographyHelper.Hash(request.CurrentPassword, authenticatedUser.PasswordSalt);

            if (!authenticatedUser.PasswordHashed.Equals(inputCurrentPasswordHashed))
                throw new ArgumentException("Current password is incorrect.");

            var passwordSalt = CryptographyHelper.GenerateSalt(size: 128);
            var newPasswordHashed = CryptographyHelper.Hash(request.NewPassword, passwordSalt);

            authenticatedUser.PasswordSalt = passwordSalt;
            authenticatedUser.PasswordHashed = newPasswordHashed;

            await _repository.UpdateAsync(authenticatedUser);
        }

        public async Task CreateUserAsync(CreateUserDTO request, UserRole role)
        {
            if (request is null)
                throw new ArgumentNullException("User form is null.");

            if (string.IsNullOrEmpty(request.Username))
                throw new ArgumentNullException("Username is blank.");

            if (string.IsNullOrEmpty(request.FullName))
                throw new ArgumentNullException("Full name is blank.");

            if (string.IsNullOrEmpty(request.Email))
                throw new ArgumentNullException("Email is blank.");

            if (string.IsNullOrEmpty(request.Password))
                throw new ArgumentNullException("Password is blank.");

            PasswordComplexityValidator.Validate(request.Password);

            if (await _repository.AnyAsync<User>(x => x.Username == request.Username))
                throw new ArgumentException("User with such username already exists.");

            if (await _repository.AnyAsync<User>(x => x.Email == request.Email))
                throw new ArgumentException("User with such email already exists.");

            var passwordSalt = CryptographyHelper.GenerateSalt(size: 128);
            var password = CryptographyHelper.Hash(request.Password, passwordSalt);

            var roleToAssign = await _repository.FirstOrDefaultAsync<Domain.Entities.Role>(x =>
                x.Name == role.ToString());
            var status = await _repository.FirstOrDefaultAsync<Domain.Entities.UserStatus>(x =>
                x.Name == Enums.UserStatus.Active.ToString());

            var user = new User
            {
                Username = request.Username,
                FullName = request.FullName,
                Email = request.Email,
                PasswordHashed = password,
                PasswordSalt = passwordSalt,
                RoleId = roleToAssign!.Id,
                UserStatusId = status!.Id,
            };

            if (request.Image is not null)
            {
                var uploadResult = await _imagesService.AddImageAsync(request.Image);

                user.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
                user.ImagePublicId = uploadResult.PublicId;
            }

            await _repository.AddAsync(user);

            _notificationsService.SendMessageOfEmailConfirmToUser(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _repository.GetByIdAsync<User>(userId);

            if (user is null)
                throw new KeyNotFoundException("User with such id does not exist.");

            if (user.UserStatus.Name == Enums.UserStatus.Deleted.ToString())
                throw new InvalidOperationException("User is already deleted.");

            var status = await _repository.FirstOrDefaultAsync<Domain.Entities.UserStatus>(x =>
                x.Name == Enums.UserStatus.Deleted.ToString());

            user.UserStatusId = status!.Id;

            await _repository.UpdateAsync(user);

            _notificationsService.SendMessageOfDeletingAccountToUser(user);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync<User>(id);

            if (user is null)
                throw new KeyNotFoundException("User with id does not exist.");

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Role = Enum.Parse<UserRole>(user.Role.Name),
                ImageUrl = user.ImageUrl,
                Status = Enum.Parse<Enums.UserStatus>(user.UserStatus.Name)
            };
        }

        public async Task<ListModel<UserDTO>> GetCustomersListAsync(UserSpecificationsDTO specifications)
        {
            if (specifications is null)
                throw new ArgumentNullException("Specifications are null.");

            var specification = this.GetSpecificationForCustomer(specifications);

            return await this.GetListModerAsync(specification);
        }

        private ISpecification<User> GetSpecificationForCustomer(UserSpecificationsDTO specifications)
        {
            var builder = new SpecificationBuilder<User>();

            builder.With(x => x.Role.Name == Enums.UserRole.Customer.ToString());

            if (!string.IsNullOrEmpty(specifications.SearchTerm))
                builder.With(x => x.FullName.Contains(specifications.SearchTerm) || x.Username.Contains(specifications.SearchTerm));

            if (!string.IsNullOrEmpty(specifications.SortField))
            {
                switch (specifications.SortField)
                {
                    case "Id":
                        builder.OrderBy(x => x.Id, specifications.SortDirection ?? SortDirection.ASC);
                        break;
                    case "FullName":
                        builder.OrderBy(x => x.FullName, specifications.SortDirection ?? SortDirection.ASC);
                        break;
                    case "DateOfBirth":
                        builder.OrderBy(x => x.DateOfBirth, specifications.SortDirection ?? SortDirection.ASC);
                        break;
                }
            }

            if (specifications.Status is not null)
                builder.With(x => x.UserStatus.Name == specifications.Status.ToString());

            builder.WithPagination(specifications.PageSize, specifications.PageNumber);

            return builder.Build();
        }

        public async Task<ListModel<UserDTO>> GetStaffListAsync(SpecificationsDTO specifications)
        {
            if (specifications is null)
                throw new ArgumentNullException("Specifications are null.");

            var specification = this.GetSpecificationForStuff(specifications);

            return await this.GetListModerAsync(specification);
        }

        private ISpecification<User> GetSpecificationForStuff(SpecificationsDTO specifications)
        {
            var builder = new SpecificationBuilder<User>();

            builder.With(x => x.Role.Name == Enums.UserRole.Admin.ToString() || 
                              x.Role.Name == Enums.UserRole.TechnicalSupportSpecialist.ToString());

            if (!string.IsNullOrEmpty(specifications.SearchTerm))
                builder.With(x => x.FullName.Contains(specifications.SearchTerm) || x.Username.Contains(specifications.SearchTerm));

            if (!string.IsNullOrEmpty(specifications.SortField))
            {
                switch (specifications.SortField)
                {
                    case "Id":
                        builder.OrderBy(x => x.Id, specifications.SortDirection ?? SortDirection.ASC);
                        break;
                    case "FullName":
                        builder.OrderBy(x => x.FullName, specifications.SortDirection ?? SortDirection.ASC);
                        break;
                    case "DateOfBirth":
                        builder.OrderBy(x => x.DateOfBirth, specifications.SortDirection ?? SortDirection.ASC);
                        break;
                    case "Role":
                        builder.OrderBy(x => x.Role.Name, specifications.SortDirection ?? SortDirection.ASC);
                        break;
                }
            }

            builder.WithPagination(specifications.PageSize, specifications.PageNumber);

            return builder.Build();
        }

        private async Task<ListModel<UserDTO>> GetListModerAsync(ISpecification<User> specification)
        {
            var users = await _repository.GetAsync<User>(specification);

            long totalCount = specification.Predicate is null ?
                await _repository.CountAsync<User>() :
                await _repository.CountAsync<User>(specification.Predicate);

            var totalPages = (long)Math.Ceiling((double)totalCount / specification.Take);

            var list = new ListModel<UserDTO>
            {
                List = users
                    .Select(x => new UserDTO
                    {
                        Id = x.Id,
                        Username = x.Username,
                        FullName = x.FullName,
                        Email = x.Email,
                        Role = Enum.Parse<UserRole>(x.Role.Name),
                        ImageUrl = x.ImageUrl,
                        Status = Enum.Parse<Enums.UserStatus>(x.UserStatus.Name)
                    })
                    .ToList(),
                TotalPages = totalPages
            };

            return list;
        }

        public async Task UnblockUserAsync(Guid userId)
        {
            var user = await _repository.GetByIdAsync<User>(userId);

            if (user is null)
                throw new KeyNotFoundException("User with such id does not exist.");

            if (user.UserStatus.Name == Enums.UserStatus.Active.ToString())
                throw new InvalidOperationException("User is active.");

            if (user.UserStatus.Name == Enums.UserStatus.Deleted.ToString())
                throw new InvalidOperationException("User is deleted.");

            var status = await _repository.FirstOrDefaultAsync<Domain.Entities.UserStatus>(x =>
                x.Name == Enums.UserStatus.Active.ToString());

            user.UserStatusId = status!.Id;
            user.UnblockDateTime = null;

            await _repository.UpdateAsync(user);

            _notificationsService.SendMessageOfUnblockingAccountToUser(user);
        }

        public async Task DeleteUserAsync()
        {
            var user = await _authService.GetAuthenticatedUserEntityAsync();

            var status = await _repository.FirstOrDefaultAsync<Domain.Entities.UserStatus>(x =>
                x.Name == Enums.UserStatus.Deleted.ToString());

            user.UserStatusId = status!.Id;

            await _repository.UpdateAsync(user);
        }

        public async Task ConfirmEmailAsync(Guid userId)
        {
            var user = await _authService.GetAuthenticatedUserEntityAsync();

            if (user.Id != userId)
                throw new ForbiddenException("You cannot confirm the email of other user.");

            if (user.IsEmailConfirmed)
                throw new InvalidOperationException("User email has already been confimred.");

            user.IsEmailConfirmed = true;

            await _repository.UpdateAsync(user);
        }
    }
}