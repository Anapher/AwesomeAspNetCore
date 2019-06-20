using AutoMapper;
using AwesomeAspApp.Core.Domain.Entities;

namespace AwesomeAspApp.Infrastructure.Identity.Mapping
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<AppUser, User>().ConstructUsing(u => new User(u.Id, u.UserName, u.PasswordHash));
            CreateMap<User, AppUser>();
        }
    }
}
