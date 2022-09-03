using AutoMapper;
using Eskisehirspor.Application.Common.Identity;

namespace Eskisehirspor.Application.UseCases.Authentication.SignIn
{
    public class SignInCommandMapper : Profile
    {
        public SignInCommandMapper()
        {
            CreateMap<Domain.Entities.User, AuthenticatedUser>().ForMember(d => d.Roles, opt => opt.MapFrom(s => s.Roles.Split(',', StringSplitOptions.None)));
        }
    }
}
