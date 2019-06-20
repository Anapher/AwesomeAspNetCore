using AutoMapper;
using AwesomeAspApp.Core.Domain.Entities;
using AwesomeAspApp.Core.Dto.GatewayResponses.Repositories;
using AwesomeAspApp.Core.Interfaces.Gateways.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AwesomeAspApp.Infrastructure.Identity.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Create(string email, string userName, string password)
        {
            var appUser = new AppUser { Email = email, UserName = userName };
            var identityResult = await _userManager.CreateAsync(appUser, password);

            if (!identityResult.Succeeded)
            {
                return new CreateUserResponse(appUser.Id, false, IdentityErrorMapper.MapToError(identityResult.Errors));
            }

            return new CreateUserResponse(appUser.Id, true, null);
        }

        public async Task<User?> FindByName(string userName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);
            return appUser == null ? null : _mapper.Map<User>(appUser);
        }

        public Task<bool> CheckPassword(User user, string password)
        {
            return _userManager.CheckPasswordAsync(_mapper.Map<AppUser>(user), password);
        }

        public async Task Update(User entity)
        {
            var appUser = await _userManager.FindByIdAsync(entity.Id);
            _mapper.Map(entity, appUser);
            await _userManager.UpdateAsync(appUser);
        }

        public async Task Delete(User entity)
        {
            var appUser = await _userManager.FindByIdAsync(entity.Id);
            await _userManager.DeleteAsync(appUser);
        }

        public async Task<User?> FindById(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null) return null;

            return _mapper.Map<User>(appUser);
        }
    }
}
