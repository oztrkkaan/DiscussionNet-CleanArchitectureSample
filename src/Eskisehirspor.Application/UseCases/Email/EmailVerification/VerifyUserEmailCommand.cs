﻿using Eskisehirspor.Application.Common.Interfaces;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Email.EmailVerification
{
    public class VerifyUserEmailCommand : IRequest<VerifyUserEmailResponse>
    {
        public Guid VerificationGuid { get; set; }
    }

    public class VerifyUserEmailCommandHandler : IRequestHandler<VerifyUserEmailCommand, VerifyUserEmailResponse>
    {
        IForumDbContext _context;
        public async Task<VerifyUserEmailResponse> Handle(VerifyUserEmailCommand request, CancellationToken cancellationToken)
        {
            var emailVerification = _context.UserEmailVerifications.FirstOrDefault(m => m.Guid == request.VerificationGuid);
            ArgumentNullException.ThrowIfNull(emailVerification);
            
            if(emailVerification.IsValid)
            {
                throw new Exception("E-posta adresi zaten doğrulandı.");
            }
            if (emailVerification.IsExpired)
            {
                throw new Exception("E-posta doğrulamak için oluşturulan adresin süresi doldu.");
            }

            emailVerification.SetAsVerified();

            await _context.SaveChangesAsync(cancellationToken);

            return new VerifyUserEmailResponse
            {
                IsSuccess = true
            };
        }
    }

    public class VerifyUserEmailResponse
    {
        public bool IsSuccess { get; set; }
    }
}
