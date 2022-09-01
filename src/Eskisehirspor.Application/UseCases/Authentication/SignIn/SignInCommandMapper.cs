using AutoMapper;
using Eskisehirspor.Domain.Entities;

namespace Eskisehirspor.Application.UseCases.Authentication.SignIn
{
    public class SignInCommandMapper : Profile
    {
        public SignInCommandMapper()
        {
            CreateMap<Domain.Entities.User, TokenUser>();
        }
    }
}
