using Eskisehirspor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Thread = Eskisehirspor.Domain.Entities.Thread;

namespace Eskisehirspor.Application.Common.Interfaces
{
    public interface IForumDbContext
    {
        DbSet<User> Users { get; set; }
        //DbSet<UserEmailVerification> UserEmailVerifications { get; set; }
        //DbSet<Topic> Topics { get; set; }
        //DbSet<Tag> Tags { get; set; }
        //DbSet<Thread> Threads { get; set; }
        //DbSet<ThreadReaction> ThreadReactions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
